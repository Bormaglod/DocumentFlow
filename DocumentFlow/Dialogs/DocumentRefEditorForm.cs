//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.08.2019
//
// Версия 2023.1.15
//  - исправлен вызов Create, теперь он возвращает в случае успеха true,
//    а остальные параметры передаются через out
//
//-----------------------------------------------------------------------

using DocumentFlow.Core;
using DocumentFlow.Data.Core;

using System.Drawing.Imaging;
using System.IO;

namespace DocumentFlow.Dialogs;

public partial class DocumentRefEditorForm : Form
{
    private string fileNameWithPath = string.Empty;

    protected DocumentRefEditorForm(DocumentRefs? refs = null)
    {
        InitializeComponent();

        if (refs != null)
        {
            fileNameWithPath = refs.file_name ?? string.Empty;

            textFileName.Text = refs.file_name;
            textNote.Text = refs.note;
            checkBoxThumbnail.Checked = refs.thumbnail_exist;

            textFileName.Enabled = false;
            buttonSelectFile.Enabled = false;
        }
    }

    public string FileName => Path.GetFileName(FileNameWithPath);

    public string FileNameWithPath => fileNameWithPath;

    public static bool Create(Guid owner, out DocumentRefs? document, out string fileName)
    {
        DocumentRefEditorForm f = new();
        if (f.ShowDialog() == DialogResult.OK)
        {
            var fileInfo = new FileInfo(f.FileNameWithPath);
            document = new DocumentRefs
            {
                owner_id = owner,
                file_name = f.FileName,
                note = f.textNote.Text,
                file_length = fileInfo.Length
            };

            using FileStream stream = new(fileInfo.FullName, FileMode.Open, FileAccess.Read);
            document.file_content = new byte[stream.Length];
            stream.Read(document.file_content, 0, document.file_content.Length);

            if (f.checkBoxThumbnail.Checked)
            {
                document.thumbnail = CreateThumbnailImage(document);
            }

            fileName = f.FileNameWithPath;
            return true;
        }

        document = null;
        fileName = string.Empty;
        return false;
    }

    public static bool Edit(DocumentRefs refs)
    {
        DocumentRefEditorForm f = new(refs);
        if (f.ShowDialog() == DialogResult.OK)
        {
            refs.note = f.textNote.Text;
            refs.thumbnail = f.checkBoxThumbnail.Checked ? CreateThumbnailImage(refs) : null;

            return true;
        }

        return false;
    }

    private static string? CreateThumbnailImage(DocumentRefs refs)
    {
        string[] images = { ".jpg", ".jpeg", ".png", ".bmp" };
        if (images.Contains(Path.GetExtension(refs.file_name)) && refs.file_content != null)
        {
            using MemoryStream stream = new(refs.file_content);
            Image image = Image.FromStream(stream);
            Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
            return ImageHelper.ImageToBase64(thumb, ImageFormat.Bmp);
        }

        return null;
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(FileNameWithPath))
        {
            MessageBox.Show("Необходимо выбрать файл. Без файла - никак.....", "Файл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DialogResult = DialogResult.None;
        }
    }

    private void ButtonSelectFile_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            fileNameWithPath = openFileDialog1.FileName;
            textFileName.Text = FileName;
        }
    }
}
