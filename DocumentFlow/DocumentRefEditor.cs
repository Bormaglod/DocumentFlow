//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.08.2019
// Time: 12:48
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Crc32C;
using Dapper;
using FluentFTP;
using DocumentFlow.Authorization;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;
using DocumentFlow.Properties;

namespace DocumentFlow
{
    public partial class DocumentRefEditor : Form
    {
        private string ftpPath;

        public DocumentRefEditor(string ftpPath)
        {
            InitializeComponent();

            this.ftpPath = ftpPath;
        }

        public DocumentRefs Create(Guid owner, string sourceFileName = null)
        {
            if (string.IsNullOrEmpty(sourceFileName))
            {
                selectFile.ClearCurrent();
            }
            else
            {
                selectFile.FileName = sourceFileName;
                selectFile.Enabled = false;
            }

            textNote.Text = string.Empty;

            if (ShowDialog() != DialogResult.OK)
                return null;

            string name = Path.GetFileName(selectFile.FileName);

            if (CanceledFtpConnection())
            {
                return null;
            }

            FtpClient ftp = new FtpClient(Settings.Default.FtpHost)
            {
                Credentials = new NetworkCredential(Settings.Default.FtpUser, Settings.Default.FtpPassword)
            };

            ftp.Connect();

            try
            {
                FileInfo fileInfo = new FileInfo(selectFile.FileName);
                DocumentRefs docRef = new DocumentRefs()
                {
                    owner_id = owner,
                    file_name = name,
                    note = textNote.Text,
                    crc = GetCrcFile(selectFile.FileName),
                    length = fileInfo.Length
                };

                if (ftp.UploadFile(selectFile.FileName, Path.Combine(ftpPath, name), FtpRemoteExists.Overwrite, true) != FtpStatus.Success)
                    throw new FtpUploadException($"Файл {name} не получилось загрузить на сервер.");

                return docRef;
            }
            finally
            {
                ftp.Disconnect();
            }
        }

        public bool Edit(DocumentRefs refs)
        {
            if (CanceledFtpConnection())
            {
                return false;
            }

            FtpClient ftp = new FtpClient(Settings.Default.FtpHost)
            {
                Credentials = new NetworkCredential(Settings.Default.FtpUser, Settings.Default.FtpPassword)
            };

            ftp.Connect();

            string ftpName = Path.Combine(ftpPath, refs.file_name);
            if (!ftp.FileExists(ftpName))
            {
                if (MessageBox.Show($"Файл {refs.file_name} отсутствует. Удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Delete(ftp, refs);
                    return false;
                }
            }

            try
            {
                selectFile.FileName = ftpName;
                textNote.Text = refs.note;

                if (ShowDialog() == DialogResult.OK)
                {
                    if (selectFile.FileName != ftpName)
                    {
                        ftp.DeleteFile(ftpName);

                        refs.file_name = Path.GetFileName(selectFile.FileName);
                        refs.crc = GetCrcFile(selectFile.FileName);
                        refs.length = (new FileInfo(selectFile.FileName)).Length;

                        ftpName = Path.Combine(ftpPath, refs.file_name);

                        ftp.UploadFile(selectFile.FileName, ftpName, FtpRemoteExists.Overwrite, true);
                    }

                    if (refs.length == 0)
                    {
                        refs.length = ftp.GetFileSize(selectFile.FileName);
                    }

                    refs.note = textNote.Text;

                    return true;
                }
            }
            finally
            {
                ftp.Disconnect();
            }

            return false;
        }

        public void Delete(DocumentRefs refs)
        {
            if (CanceledFtpConnection())
            {
                return;
            }

            FtpClient ftp = new FtpClient(Settings.Default.FtpHost)
            {
                Credentials = new NetworkCredential(Settings.Default.FtpUser, Settings.Default.FtpPassword)
            };

            ftp.Connect();

            Delete(ftp, refs);
        }

        public bool GetLocalFileName(DocumentRefs refs, out string localName)
        {
            localName = Path.Combine(Path.GetTempPath(), refs.file_name);
            if (CanceledFtpConnection())
            {
                return false;
            }

            FtpClient ftp = new FtpClient(Settings.Default.FtpHost)
            {
                Credentials = new NetworkCredential(Settings.Default.FtpUser, Settings.Default.FtpPassword)
            };

            ftp.Connect();

            try
            {
                string ftpName = Path.Combine(ftpPath, refs.file_name);

                if (File.Exists(localName))
                {
                    uint crc = GetCrcFile(localName);
                    if (crc == refs.crc)
                    {
                        return true;
                    }

                    File.Delete(localName);
                }

                if (ftp.DownloadFile(localName, ftpName) == FtpStatus.Failed)
                {
                    if (MessageBox.Show($"Файл {refs.file_name} отсутствует. Удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Delete(ftp, refs);
                    }

                    return false;
                }

                return true;
            }
            finally
            {
                ftp.Disconnect();
            }
        }

        private bool CanceledFtpConnection()
        {
            if (string.IsNullOrEmpty(Settings.Default.FtpUser) || string.IsNullOrEmpty(Settings.Default.FtpPassword))
            {
                FtpLoginForm ftpLogin = new FtpLoginForm(Settings.Default.FtpUser, Settings.Default.FtpPassword);
                if (ftpLogin.ShowDialog() == DialogResult.Cancel)
                {
                    return true;
                }

                Settings.Default.FtpUser = ftpLogin.UserName;
                Settings.Default.FtpPassword = ftpLogin.Password;
            }

            return false;
        }

        private void Delete(FtpClient ftp, DocumentRefs refs)
        {
            string fileName = Path.Combine(ftpPath, refs.file_name);
            if (ftp.FileExists(fileName))
                ftp.DeleteFile(fileName);

            using (var conn = Db.OpenConnection())
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Query("delete from document_refs where id = :id", new { refs.Id }, transaction);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        ExceptionHelper.MesssageBox(e);
                    }
                }
            }
        }

        private uint GetCrcFile(string fileName)
        {
            uint crc = 0;
            using (FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                int length = stream.Length > int.MaxValue ? int.MaxValue : Convert.ToInt32(stream.Length);

                byte[] buffer = new byte[length];

                stream.Read(buffer, 0, length);
                crc = Crc32CAlgorithm.Compute(buffer);
            }

            return crc;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectFile.FileName))
            {
                MessageBox.Show("Необходимо выбрать файл. Без файла - никак.....", "Файл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
            }
        }
    }
}
