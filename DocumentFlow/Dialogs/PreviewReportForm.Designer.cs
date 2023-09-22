namespace DocumentFlow.Dialogs
{
    partial class PreviewReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings messageBoxSettings1 = new Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings();
            Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings pdfViewerPrinterSettings1 = new Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewReportForm));
            Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings textSearchSettings1 = new Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings();
            toolStrip1 = new ToolStrip();
            buttonSave = new ToolStripButton();
            buttonPrint = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            buttonFirst = new ToolStripButton();
            buttonPrev = new ToolStripButton();
            textCurrentPage = new ToolStripTextBox();
            toolStripLabel1 = new ToolStripLabel();
            labelCountPages = new ToolStripLabel();
            buttonNext = new ToolStripButton();
            buttonLast = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            comboZoom = new Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx();
            buttonZoomIn = new ToolStripButton();
            buttonZoomOut = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            buttonFillWindow = new ToolStripButton();
            buttonFitPageToWindow = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            buttonSend = new ToolStripButton();
            panel1 = new Panel();
            pdfDocumentView1 = new Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView();
            savePdfDialog = new SaveFileDialog();
            printDialog1 = new PrintDialog();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { buttonSave, buttonPrint, toolStripSeparator2, buttonFirst, buttonPrev, textCurrentPage, toolStripLabel1, labelCountPages, buttonNext, buttonLast, toolStripSeparator1, comboZoom, buttonZoomIn, buttonZoomOut, toolStripSeparator3, buttonFillWindow, buttonFitPageToWindow, toolStripSeparator4, buttonSend });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(860, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // buttonSave
            // 
            buttonSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonSave.Image = Properties.Resources.icons8_save_16;
            buttonSave.ImageTransparentColor = Color.Magenta;
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(23, 22);
            buttonSave.Text = "Сохранить";
            buttonSave.Click += ButtonSave_Click;
            // 
            // buttonPrint
            // 
            buttonPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonPrint.Image = Properties.Resources.icons8_print_16;
            buttonPrint.ImageTransparentColor = Color.Magenta;
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new Size(23, 22);
            buttonPrint.Text = "Печать";
            buttonPrint.Click += ButtonPrint_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // buttonFirst
            // 
            buttonFirst.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonFirst.Image = Properties.Resources.icons8_start_16;
            buttonFirst.ImageTransparentColor = Color.Magenta;
            buttonFirst.Name = "buttonFirst";
            buttonFirst.Size = new Size(23, 22);
            buttonFirst.Text = "Первая страница";
            buttonFirst.Click += ButtonFirst_Click;
            // 
            // buttonPrev
            // 
            buttonPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonPrev.Image = Properties.Resources.icons8_left_16;
            buttonPrev.ImageTransparentColor = Color.Magenta;
            buttonPrev.Name = "buttonPrev";
            buttonPrev.Size = new Size(23, 22);
            buttonPrev.Text = "Предыдущая страница";
            buttonPrev.Click += ButtonPrev_Click;
            // 
            // textCurrentPage
            // 
            textCurrentPage.Name = "textCurrentPage";
            textCurrentPage.Size = new Size(50, 25);
            textCurrentPage.KeyDown += TextCurrentPage_KeyDown;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(12, 22);
            toolStripLabel1.Text = "/";
            // 
            // labelCountPages
            // 
            labelCountPages.Name = "labelCountPages";
            labelCountPages.Size = new Size(13, 22);
            labelCountPages.Text = "0";
            // 
            // buttonNext
            // 
            buttonNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonNext.Image = Properties.Resources.icons8_right_16;
            buttonNext.ImageTransparentColor = Color.Magenta;
            buttonNext.Name = "buttonNext";
            buttonNext.Size = new Size(23, 22);
            buttonNext.Text = "Следующая страница";
            buttonNext.Click += ButtonNext_Click;
            // 
            // buttonLast
            // 
            buttonLast.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonLast.Image = Properties.Resources.icons8_end_16;
            buttonLast.ImageTransparentColor = Color.Magenta;
            buttonLast.Name = "buttonLast";
            buttonLast.Size = new Size(23, 22);
            buttonLast.Text = "Последняя страница";
            buttonLast.Click += ButtonLast_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // comboZoom
            // 
            comboZoom.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            comboZoom.Items.AddRange(new object[] { "50 %", "75 %", "100 %", "150 %", "200 %", "400 %" });
            comboZoom.MaxLength = 32767;
            comboZoom.Name = "comboZoom";
            comboZoom.Size = new Size(60, 25);
            comboZoom.Text = "100 %";
            comboZoom.TextUpdate += ComboZoom_TextUpdate;
            // 
            // buttonZoomIn
            // 
            buttonZoomIn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonZoomIn.Image = Properties.Resources.icons8_zoom_in_16;
            buttonZoomIn.ImageTransparentColor = Color.Magenta;
            buttonZoomIn.Name = "buttonZoomIn";
            buttonZoomIn.Size = new Size(23, 22);
            buttonZoomIn.Text = "Увеличить";
            buttonZoomIn.Click += ButtonZoomIn_Click;
            // 
            // buttonZoomOut
            // 
            buttonZoomOut.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonZoomOut.Image = Properties.Resources.icons8_zoom_out_16;
            buttonZoomOut.ImageTransparentColor = Color.Magenta;
            buttonZoomOut.Name = "buttonZoomOut";
            buttonZoomOut.Size = new Size(23, 22);
            buttonZoomOut.Text = "Уменьшить";
            buttonZoomOut.Click += ButtonZoomOut_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // buttonFillWindow
            // 
            buttonFillWindow.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonFillWindow.Image = Properties.Resources.icons8_fill_window_16;
            buttonFillWindow.ImageTransparentColor = Color.Magenta;
            buttonFillWindow.Name = "buttonFillWindow";
            buttonFillWindow.Size = new Size(23, 22);
            buttonFillWindow.Text = "По ширине окна";
            buttonFillWindow.Click += ButtonFillWindow_Click;
            // 
            // buttonFitPageToWindow
            // 
            buttonFitPageToWindow.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonFitPageToWindow.Image = Properties.Resources.icons8_fit_page_16;
            buttonFitPageToWindow.ImageTransparentColor = Color.Magenta;
            buttonFitPageToWindow.Name = "buttonFitPageToWindow";
            buttonFitPageToWindow.Size = new Size(23, 22);
            buttonFitPageToWindow.Text = "Страница целиком";
            buttonFitPageToWindow.Click += ButtonFitPageToWindow_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // buttonSend
            // 
            buttonSend.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonSend.Image = Properties.Resources.icons8_send_16;
            buttonSend.ImageTransparentColor = Color.Magenta;
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(23, 22);
            buttonSend.Text = "Отправить по эл. почте";
            buttonSend.Click += ButtonSend_Click;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(pdfDocumentView1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 25);
            panel1.Name = "panel1";
            panel1.Size = new Size(860, 547);
            panel1.TabIndex = 2;
            // 
            // pdfDocumentView1
            // 
            pdfDocumentView1.AutoScroll = true;
            pdfDocumentView1.BackColor = Color.FromArgb(237, 237, 237);
            pdfDocumentView1.CursorMode = Syncfusion.Windows.Forms.PdfViewer.PdfViewerCursorMode.SelectTool;
            pdfDocumentView1.Dock = DockStyle.Fill;
            pdfDocumentView1.EnableContextMenu = true;
            pdfDocumentView1.HorizontalScrollOffset = 0;
            pdfDocumentView1.IsTextSearchEnabled = true;
            pdfDocumentView1.IsTextSelectionEnabled = true;
            pdfDocumentView1.Location = new Point(0, 0);
            messageBoxSettings1.EnableNotification = true;
            pdfDocumentView1.MessageBoxSettings = messageBoxSettings1;
            pdfDocumentView1.MinimumZoomPercentage = 50;
            pdfDocumentView1.Name = "pdfDocumentView1";
            pdfDocumentView1.PageBorderThickness = 1;
            pdfViewerPrinterSettings1.Copies = 1;
            pdfViewerPrinterSettings1.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto;
            pdfViewerPrinterSettings1.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize;
            pdfViewerPrinterSettings1.ShowPrintStatusDialog = true;
            pdfDocumentView1.PrinterSettings = pdfViewerPrinterSettings1;
            pdfDocumentView1.ReferencePath = null;
            pdfDocumentView1.ScrollDisplacementValue = 0;
            pdfDocumentView1.ShowHorizontalScrollBar = true;
            pdfDocumentView1.ShowVerticalScrollBar = true;
            pdfDocumentView1.Size = new Size(860, 547);
            pdfDocumentView1.SpaceBetweenPages = 8;
            pdfDocumentView1.TabIndex = 1;
            textSearchSettings1.CurrentInstanceColor = Color.FromArgb(127, 255, 171, 64);
            textSearchSettings1.HighlightAllInstance = true;
            textSearchSettings1.OtherInstanceColor = Color.FromArgb(127, 254, 255, 0);
            pdfDocumentView1.TextSearchSettings = textSearchSettings1;
            pdfDocumentView1.ThemeName = "Default";
            pdfDocumentView1.VerticalScrollOffset = 0;
            pdfDocumentView1.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Default;
            pdfDocumentView1.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.Default;
            pdfDocumentView1.DocumentLoaded += PdfDocumentView1_DocumentLoaded;
            pdfDocumentView1.ZoomChanged += PdfDocumentView1_ZoomChanged;
            // 
            // savePdfDialog
            // 
            savePdfDialog.DefaultExt = "pdf";
            savePdfDialog.Filter = "PDF файлы|*.pdf";
            // 
            // printDialog1
            // 
            printDialog1.AllowCurrentPage = true;
            printDialog1.AllowSomePages = true;
            printDialog1.UseEXDialog = true;
            // 
            // PreviewReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 572);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Name = "PreviewReportForm";
            Text = "PreviewReportForm";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStrip toolStrip1;
        private ToolStripButton buttonFirst;
        private ToolStripButton buttonPrev;
        private ToolStripTextBox textCurrentPage;
        private ToolStripButton buttonNext;
        private ToolStripButton buttonLast;
        private Panel panel1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton buttonZoomIn;
        private ToolStripButton buttonZoomOut;
        private ToolStripButton buttonSave;
        private ToolStripButton buttonPrint;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripLabel toolStripLabel1;
        private ToolStripLabel labelCountPages;
        private Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx comboZoom;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton buttonSend;
        private Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView pdfDocumentView1;
        private ToolStripButton buttonFillWindow;
        private ToolStripButton buttonFitPageToWindow;
        private ToolStripSeparator toolStripSeparator4;
        private SaveFileDialog savePdfDialog;
        private PrintDialog printDialog1;
    }
}