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
// Версия 2023.6.16
//  - удалены свойства FileName и FileNameWithPath
//  - из метода Create удалён параметр fileName
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

    public static bool Create(Guid owner, out DocumentRefs? document)
    {
        DocumentRefEditorForm f = new();
        if (f.ShowDialog() == DialogResult.OK)
        {
            var fileInfo = new FileInfo(f.fileNameWithPath);
            document = new DocumentRefs
            {
                OwnerId = owner,
                FileName = Path.GetFileName(f.fileNameWithPath),
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

            return true;
        }

        document = null;
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
        if (string.IsNullOrEmpty(fileNameWithPath))
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
            textFileName.Text = Path.GetFileName(fileNameWithPath);
        }
    }
}
