//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.08.2019
//
// Версия 2023.1.15
//  - исправлен вызов Create, теперь он возвращает в случае успеха true,
//    а остальные параметры передаются через out
// Версия 2023.2.6
//  - удален метод CreateThumbnailImage, вместо него используется метод
//    класса DocumentRefs.CreateThumbnailImage()
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
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
            fileNameWithPath = refs.FileName ?? string.Empty;

            textFileName.Text = refs.FileName;
            textNote.Text = refs.Note;
            checkBoxThumbnail.Checked = refs.ThumbnailExist;

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
                OwnerId = owner,
                FileName = f.FileName,
                Note = f.textNote.Text,
                FileLength = fileInfo.Length
            };

            using FileStream stream = new(fileInfo.FullName, FileMode.Open, FileAccess.Read);
            document.FileContent = new byte[stream.Length];
            stream.Read(document.FileContent, 0, document.FileContent.Length);

            if (f.checkBoxThumbnail.Checked)
            {
                document.CreateThumbnailImage();
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
            refs.Note = f.textNote.Text;
            if (f.checkBoxThumbnail.Checked)
            {
                refs.CreateThumbnailImage();
            }
            else
            {
                refs.Thumbnail = null;
            }

            return true;
        }

        return false;
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
