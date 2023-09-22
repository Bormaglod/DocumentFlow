//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.08.2019
//-----------------------------------------------------------------------

using DocumentFlow.Tools;
using DocumentFlow.Data;
using DocumentFlow.Settings;

using Microsoft.Extensions.Options;

using System.IO;

namespace DocumentFlow.Dialogs;

[Dialog]
public partial class DocumentRefDialog : Form
{
    private string fileNameWithPath = string.Empty;
    private readonly ThumbnailRowSettings settings;

    public DocumentRefDialog(IOptions<LocalSettings> options)
    {
        InitializeComponent();

        settings = options.Value.PreviewRows.ThumbnailRow;
    }

    public DocumentRefs? Create(Guid owner)
    {
        textFileName.Text = string.Empty;
        textNote.Text = string.Empty;
        checkBoxThumbnail.Checked = false;

        textFileName.Enabled = true;
        buttonSelectFile.Enabled = true;

        if (ShowDialog() == DialogResult.OK)
        {
            var fileInfo = new FileInfo(fileNameWithPath);
            var document = new DocumentRefs
            {
                OwnerId = owner,
                FileName = Path.GetFileName(fileNameWithPath),
                Note = textNote.Text,
                FileLength = fileInfo.Length
            };

            using FileStream stream = new(fileInfo.FullName, FileMode.Open, FileAccess.Read);
            document.FileContent = new byte[stream.Length];
            stream.Read(document.FileContent, 0, document.FileContent.Length);

            if (checkBoxThumbnail.Checked)
            {
                document.CreateThumbnailImage(settings.ImageSize);
            }

            return document;
        }

        return null;
    }

    public bool Edit(DocumentRefs refs)
    {
        fileNameWithPath = refs.FileName ?? string.Empty;

        textFileName.Text = refs.FileName;
        textNote.Text = refs.Note;
        checkBoxThumbnail.Checked = refs.ThumbnailExist;

        textFileName.Enabled = false;
        buttonSelectFile.Enabled = false;

        if (ShowDialog() == DialogResult.OK)
        {
            refs.Note = textNote.Text;
            if (checkBoxThumbnail.Checked)
            {
                refs.CreateThumbnailImage(settings.ImageSize);
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
