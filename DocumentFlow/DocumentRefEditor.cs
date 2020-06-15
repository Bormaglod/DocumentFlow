//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.08.2019
// Time: 12:48
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using IO = System.IO;
    using System.Net;
    using System.Windows.Forms;
    using Crc32C;
    using FluentFTP;
    using Syncfusion.Windows.Forms;
    using DocumentFlow.Core;
    using DocumentFlow.Data.Entities;
    using DocumentFlow.Properties;

    public partial class DocumentRefEditor : MetroForm
    {
        private const string FtpUser = "sergio";
        private const string FtpPassword = "271117";

        private string selectedFileName;
        private string localPath;
        private string ftpPath;

        public enum EditingResult { Ok, Cancel, RemovalRequired }

        public DocumentRefEditor(string destLocalPath, string destFtpPath)
        {
            InitializeComponent();

            localPath = destLocalPath;
            ftpPath = destFtpPath;
        }

        public DocumentRefs Create(Guid owner, string sourceFileName)
        {
            selectedFileName = sourceFileName;
            selectFile.SelectedItem = sourceFileName;
            selectFile.Enabled = false;
            return Create(owner);
        }

        public DocumentRefs Create(Guid owner)
        {
            selectFile.SelectedItem = null;
            textNote.Text = string.Empty;

            if (ShowDialog() != DialogResult.OK)
                return null;

            string name = IO.Path.GetFileName(selectedFileName);

            FtpClient ftp = new FtpClient(Settings.Default.FtpHost);
            ftp.Credentials = new NetworkCredential(FtpUser, FtpPassword);
            ftp.Connect();

            try
            {
                DocumentRefs docRef = new DocumentRefs()
                {
                    OwnerId = owner,
                    FileName = name,
                    Note = textNote.Text,
                    Crc = GetCrcFile(selectedFileName)
                };

                if (ftp.UploadFile(selectedFileName, IO.Path.Combine(ftpPath, name), FtpRemoteExists.Overwrite, true) != FtpStatus.Success)
                    throw new FtpUploadException($"Файл {name} не получилось загрузить на сервер.");

                if (!IO.Directory.Exists(localPath))
                {
                    IO.Directory.CreateDirectory(localPath);
                }

                string localFile = IO.Path.Combine(localPath, name);
                if (selectedFileName != localFile)
                    IO.File.Copy(selectedFileName, localFile, true);

                return docRef;
            }
            finally
            {
                ftp.Disconnect();
            }
        }

        public EditingResult Edit(DocumentRefs refs)
        {
            FtpClient ftp = new FtpClient(Settings.Default.FtpHost);
            ftp.Credentials = new NetworkCredential(FtpUser, FtpPassword);
            ftp.Connect();

            try
            {
                string localName = IO.Path.Combine(localPath, refs.FileName);
                string ftpName = IO.Path.Combine(ftpPath, refs.FileName);

                selectFile.SelectedItem = localName;
                textNote.Text = refs.Note;

                // Если отсутствует локальный файл, то попробуем его загрузить с сервера (или удалим запись о файле)
                if (!IO.File.Exists(localName))
                {
                    if (MessageBox.Show($"Файл {refs.FileName} отсутствует. Восстановить файл?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (ftp.DownloadFile(localName, ftpName) == FtpStatus.Failed)
                        {
                            if (MessageBox.Show($"Возникла проблема при загрузке файла {refs.FileName}. Удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Delete(ftp, refs);
                                return EditingResult.RemovalRequired;
                            }
                            else
                                return EditingResult.Cancel;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show($"Удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Delete(ftp, refs);
                            return EditingResult.RemovalRequired;
                        }
                        else
                            return EditingResult.Cancel;
                    }
                }

                uint crc = GetCrcFile(localName);

                // Если файл отсутствует на сервере, то загрузим его туда
                if (!ftp.FileExists(ftpName))
                {
                    ftp.UploadFile(localName, ftpName, FtpRemoteExists.Overwrite, true);
                    refs.Crc = crc;
                }

                // Если локальный файл файл присутствует, то проверим его CRC и если это число не совпадает, то опять пробуем скачать его с сервера.
                // Если не удалось скачать, то редактировать не дадим (надо разобраться в чём проблема)
                // Если пользователь отказывается загружать файл, то это его проблемы
                if (refs.Crc != crc)
                {
                    if (MessageBox.Show($"Файл {refs.FileName} отличается от файла на сервере. Восстановить его?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (ftp.DownloadFile(localName, ftpName) == FtpStatus.Failed)
                        {
                            throw new FtpDownloadException($"Возникла проблема при загрузке файла {refs.FileName}");
                        }

                        refs.Crc = crc;
                    }
                }

                if (ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(selectedFileName) && selectedFileName != localName)
                    {
                        IO.File.Delete(localName);
                        ftp.DeleteFile(ftpName);

                        refs.FileName = IO.Path.GetFileName(selectedFileName);
                        refs.Crc = GetCrcFile(selectedFileName);

                        localName = IO.Path.Combine(localPath, refs.FileName);
                        ftpName = IO.Path.Combine(ftpPath, refs.FileName);

                        ftp.UploadFile(selectedFileName, ftpName, FtpRemoteExists.Overwrite, true);
                        IO.File.Copy(selectedFileName, localName, true);
                    }

                    refs.Note = textNote.Text;

                    return EditingResult.Ok;
                }
            }
            finally
            {
                ftp.Disconnect();
            }

            return EditingResult.Cancel;
        }

        public void Delete(DocumentRefs refs)
        {
            FtpClient ftp = new FtpClient(Settings.Default.FtpHost);
            ftp.Credentials = new NetworkCredential(FtpUser, FtpPassword);
            ftp.Connect();

            Delete(ftp, refs);
        }

        public EditingResult GetLocalFileName(DocumentRefs refs, out string localName)
        {
            FtpClient ftp = new FtpClient(Settings.Default.FtpHost);
            ftp.Credentials = new NetworkCredential(FtpUser, FtpPassword);
            ftp.Connect();

            try
            {
                localName = IO.Path.Combine(localPath, refs.FileName);
                string ftpName = IO.Path.Combine(ftpPath, refs.FileName);

                if (!IO.File.Exists(localName))
                {
                    IO.DirectoryInfo di = IO.Directory.GetParent(localPath);
                    string p_file = IO.Path.Combine(di.FullName, refs.FileName);

                    // Если отсутствует локальный файл, то попробуем его загрузить с сервера (или удалим запись о файле)
                    if (!IO.File.Exists(p_file))
                    {
                        if (ftp.DownloadFile(localName, ftpName) == FtpStatus.Failed)
                        {
                            if (MessageBox.Show($"Файл {refs.FileName} отсутствует. Удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Delete(ftp, refs);
                                return EditingResult.RemovalRequired;
                            }
                            else
                                return EditingResult.Cancel;
                        }

                        return EditingResult.Ok;
                    }
                    else
                    {
                        if (!IO.Directory.Exists(localPath))
                        {
                            IO.Directory.CreateDirectory(localPath);
                        }

                        IO.File.Move(p_file, localName);
                    }
                }

                uint crc = GetCrcFile(localName);
                
                // Если файл отсутствует на сервере, то загрузим его туда
                if (!ftp.FileExists(ftpName))
                {
                    ftp.UploadFile(localName, ftpName, FtpRemoteExists.Overwrite, true);
                    refs.Crc = crc;

                    return EditingResult.Ok;
                }

                if (refs.Crc != crc)
                {
                    if (MessageBox.Show($"Файл {refs.FileName} отличается от файла на сервере. Восстановить его?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (ftp.DownloadFile(localName, ftpName) == FtpStatus.Failed)
                        {
                            throw new FtpDownloadException($"Возникла проблема при загрузке файла {refs.FileName}");
                        }

                        refs.Crc = crc;
                    }
                }

                return EditingResult.Ok;
            }
            finally
            {
                ftp.Disconnect();
            }
        }

        private void Delete(FtpClient ftp, DocumentRefs refs)
        {
            string fileName = IO.Path.Combine(localPath, refs.FileName);
            if (IO.File.Exists(fileName))
                IO.File.Delete(fileName);

            fileName = IO.Path.Combine(ftpPath, refs.FileName);
            if (ftp.FileExists(fileName))
                ftp.DeleteFile(fileName);
        }

        private uint GetCrcFile(string fileName)
        {
            uint crc = 0;
            using (IO.FileStream stream = IO.File.Open(fileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read))
            {
                int length = stream.Length > int.MaxValue ? int.MaxValue : Convert.ToInt32(stream.Length);

                byte[] buffer = new byte[length];

                stream.Read(buffer, 0, length);
                crc = Crc32CAlgorithm.Compute(buffer);
            }

            return crc;
        }

        private void SelectFile_ValueChanged(object sender, Controls.Forms.SelectBoxValueChanged e)
        {
            selectedFileName = e.SelectedItem?.ToString();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (selectFile.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать файл. Без файла - никак.....", "Файл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
            }
        }
    }
}
