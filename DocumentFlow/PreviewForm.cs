//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.02.2021
// Time: 19:17
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.Forms.PdfViewer;

namespace DocumentFlow
{
    public partial class PreviewForm : Form
    {
        private bool canZoom;
        private readonly PdfLoadedDocument document;
        private string documentName;
        private Guid documentId;

        private PreviewForm(PdfLoadedDocument doc)
        {
            InitializeComponent();
            canZoom = true;
            document = doc;
        }

        public static void PreviewPdf(Guid documentId, PdfLoadedDocument doc, string title, string docName)
        {
            PreviewForm window = new(doc)
            {
                Text = title,
                documentName = docName
            };

            window.pdfDocumentView1.Load(doc);
            window.documentId = documentId;
            window.ShowDialog();
        }

        private void UpdateNavigationButtons()
        {
            buttonFirstPage.Enabled = pdfDocumentView1.CanGoToFirstPage;
            buttonLastPage.Enabled = pdfDocumentView1.CanGoToLastPage;
            buttonPrevPage.Enabled = pdfDocumentView1.CanGoToPreviousPage;
            buttonNextPage.Enabled = pdfDocumentView1.CanGoToNextPage;

            labelCountPages.Text = pdfDocumentView1.PageCount.ToString();
            textCurrentPage.Text = pdfDocumentView1.CurrentPageIndex.ToString();
        }

        private void UpdateZoom()
        {
            Match m = Regex.Match(comboZoom.Text, @"(\d+)\s*%");
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


        private void pdfDocumentView1_DocumentLoaded(object sender, EventArgs args)
        {
            UpdateNavigationButtons();
            UpdateZoomStatus(pdfDocumentView1.ZoomPercentage);
        }

        private void buttonFillWindow_Click(object sender, EventArgs e)
        {
            pdfDocumentView1.ZoomMode = ZoomMode.FitWidth;
        }

        private void buttonFitPageToWindow_Click(object sender, EventArgs e)
        {
            pdfDocumentView1.ZoomMode = ZoomMode.FitPage;
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            pdfDocumentView1.GoToFirstPage();
            UpdateNavigationButtons();
        }

        private void buttonPrevPage_Click(object sender, EventArgs e)
        {
            pdfDocumentView1.GoToPreviousPage();
            UpdateNavigationButtons();
        }

        private void buttonNextPage_Click(object sender, EventArgs e)
        {
            pdfDocumentView1.GoToNextPage();
            UpdateNavigationButtons();
        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {
            pdfDocumentView1.GoToLastPage();
            UpdateNavigationButtons();
        }

        private void textCurrentPage_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textCurrentPage.Text, out int current))
            {
                pdfDocumentView1.GoToPageAtIndex(current);
                UpdateNavigationButtons();
            }
        }

        private void comboZoom_TextUpdate(object sender, EventArgs e)
        {
            if (canZoom)
            {
                UpdateZoom();
            }
        }

        private void buttonZoomIn_Click(object sender, EventArgs e)
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

        private void buttonZoomOut_Click(object sender, EventArgs e)
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

        private void pdfDocumentView1_ZoomChanged(object sender, int zoomFactor)
        {
            UpdateZoomStatus(zoomFactor);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (savePdfDialog.ShowDialog() == DialogResult.OK)
            {
                document.Save(savePdfDialog.FileName);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            printDialog1.Document = pdfDocumentView1.PrintDocument;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDialog1.Document.Print();
            }
        }

        private void buttonHand_Click(object sender, EventArgs e)
        {
            if (buttonSelect.Checked)
            {
                buttonSelect.Checked = false;
                buttonHand.Checked = true;
                pdfDocumentView1.CursorMode = PdfViewerCursorMode.HandTool;
            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (buttonHand.Checked)
            {
                buttonHand.Checked = false;
                buttonSelect.Checked = true;
                pdfDocumentView1.CursorMode = PdfViewerCursorMode.SelectTool;
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string file = Path.Combine(Path.GetTempPath(), documentName + ".pdf");
            document.Save(file);

            SelectEmailForm.ShowWindow(documentId, documentName, file);
        }
    }
}
