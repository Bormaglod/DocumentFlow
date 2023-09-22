//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Windows.Forms.PdfViewer;

using System.IO;
using System.Text.RegularExpressions;

namespace DocumentFlow.Dialogs;

[Dialog]
public partial class PreviewReportForm : Form
{
    [GeneratedRegex("^(\\d+)\\s*%?$")]
    private static partial Regex ZoomRegex();

    private readonly IServiceProvider services;
    private bool canZoom = true;
    private Guid? documentId;
    private string? pdf;

    public PreviewReportForm(IServiceProvider services)
    {
        InitializeComponent();

        this.services = services;
    }

    public void ShowReport(string pdf, string title) => ShowReport(null, pdf, title);

    public void ShowReport(Guid? documentId, string pdf, string title)
    {
        Text = title;

        this.pdf = pdf;
        this.documentId = documentId;

        buttonSend.Enabled = documentId != null;

        pdfDocumentView1.Load(pdf);
        ShowDialog();
    }

    private void UpdateNavigationButtons()
    {
        buttonFirst.Enabled = pdfDocumentView1.CanGoToFirstPage;
        buttonLast.Enabled = pdfDocumentView1.CanGoToLastPage;
        buttonPrev.Enabled = pdfDocumentView1.CanGoToPreviousPage;
        buttonNext.Enabled = pdfDocumentView1.CanGoToNextPage;

        labelCountPages.Text = pdfDocumentView1.PageCount.ToString();
        textCurrentPage.Text = pdfDocumentView1.CurrentPageIndex.ToString();
    }

    private void UpdateZoom()
    {
        Match m = ZoomRegex().Match(comboZoom.Text);
        if (m.Success)
        {
            if (int.TryParse(m.Groups[1].Value, out int zoom))
            {
                pdfDocumentView1.ZoomTo(zoom);
            }
        }
    }

    private void UpdateZoomStatus(int zoom)
    {
        canZoom = false;
        try
        {
            comboZoom.Text = $"{zoom} %";
        }
        finally
        {

            canZoom = true;
        }
    }

    private void ToolStripPageNum_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (int.TryParse(textCurrentPage.Text, out int current))
            {
                pdfDocumentView1.GoToPageAtIndex(current);
                UpdateNavigationButtons();
            }
        }
    }

    private void ComboZoom_TextUpdate(object sender, EventArgs e)
    {
        if (canZoom)
        {
            UpdateZoom();
        }
    }

    private void PdfDocumentView1_DocumentLoaded(object sender, EventArgs args)
    {
        UpdateNavigationButtons();
        UpdateZoomStatus(pdfDocumentView1.ZoomPercentage);
    }

    private void ButtonFirst_Click(object sender, EventArgs e)
    {
        pdfDocumentView1.GoToFirstPage();
        UpdateNavigationButtons();
    }

    private void ButtonPrev_Click(object sender, EventArgs e)
    {
        pdfDocumentView1.GoToPreviousPage();
        UpdateNavigationButtons();
    }

    private void ButtonNext_Click(object sender, EventArgs e)
    {
        pdfDocumentView1.GoToNextPage();
        UpdateNavigationButtons();
    }

    private void ButtonLast_Click(object sender, EventArgs e)
    {
        pdfDocumentView1.GoToLastPage();
        UpdateNavigationButtons();
    }

    private void TextCurrentPage_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (int.TryParse(textCurrentPage.Text, out int current))
            {
                pdfDocumentView1.GoToPageAtIndex(current);
                UpdateNavigationButtons();
            }
        }
    }

    private void ButtonZoomIn_Click(object sender, EventArgs e)
    {
        int zoom = pdfDocumentView1.ZoomPercentage;

        int[] zooms = { 50, 75, 100, 150, 200, 400 };
        for (int i = 0; i < zooms.Length; i++)
        {
            if (zoom < zooms[i])
            {
                zoom = zooms[i];
                break;
            }
        }

        pdfDocumentView1.ZoomTo(zoom);
    }

    private void ButtonZoomOut_Click(object sender, EventArgs e)
    {
        int zoom = pdfDocumentView1.ZoomPercentage;

        int[] zooms = { 50, 75, 100, 150, 200, 400 };
        for (int i = zooms.Length - 1; i >= 0; i--)
        {
            if (zoom > zooms[i])
            {
                zoom = zooms[i];
                break;
            }
        }

        pdfDocumentView1.ZoomTo(zoom);
    }

    private void PdfDocumentView1_ZoomChanged(object sender, int zoomFactor) => UpdateZoomStatus(zoomFactor);

    private void ButtonFillWindow_Click(object sender, EventArgs e) => pdfDocumentView1.ZoomMode = ZoomMode.FitWidth;

    private void ButtonFitPageToWindow_Click(object sender, EventArgs e) => pdfDocumentView1.ZoomMode = ZoomMode.FitPage;

    private void ButtonSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(pdf))
        {
            if (savePdfDialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(pdf, savePdfDialog.FileName, true);
            }
        }
    }

    private void ButtonPrint_Click(object sender, EventArgs e)
    {
        printDialog1.Document = pdfDocumentView1.PrintDocument;
        if (printDialog1.ShowDialog() == DialogResult.OK)
        {
            printDialog1.Document.Print();
        }
    }

    private void ButtonSend_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(pdf))
        {
            services
                .GetRequiredService<EmailSendDialog>()
                .ShowWindow(documentId, Text, pdf);
        }
    }
}
