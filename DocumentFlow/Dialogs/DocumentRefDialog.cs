//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.08.2019
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Tools;

using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace DocumentFlow.Dialogs;

public partial class DocumentRefDialog : Form, IDocumentRefDialog
{
    private string fileNameWithPath = string.Empty;

    public DocumentRefDialog()
    {
        InitializeComponent();
   }

    bool IDocumentRefDialog.CreateThumbnailImage => checkBoxThumbnail.Checked;

    string IDocumentRefDialog.FileNameWithPath => fileNameWithPath;

    bool IDocumentRefDialog.Create(Guid owner, [MaybeNullWhen(false)] out DocumentRefs document)
    {
        textFileName.Text = string.Empty;
        textNote.Text = string.Empty;
        checkBoxThumbnail.Checked = false;

        textFileName.Enabled = true;
        buttonSelectFile.Enabled = true;

        if (ShowDialog() == DialogResult.OK)
        {
            var fileInfo = new FileInfo(fileNameWithPath);
            document = new DocumentRefs
            {
                OwnerId = owner,
                FileName = Path.GetFileName(fileNameWithPath),
                Note = textNote.Text,
                FileLength = fileInfo.Length
            };

            return true;
        }

        document = default;
        return false;
    }

    bool IDocumentRefDialog.Edit(DocumentRefs refs)
    {
        fileNameWithPath = refs.FileName ?? string.Empty;

        textFileName.Text = refs.FileName;
        textNote.Text = refs.Note;
        checkBoxThumbnail.Checked = refs.ThumbnailExist;

        textFileName.Enabled = false;
        buttonSelectFile.Enabled = false;
        checkBoxThumbnail.Enabled = false;

        if (ShowDialog() == DialogResult.OK)
        {
            refs.Note = textNote.Text;
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
