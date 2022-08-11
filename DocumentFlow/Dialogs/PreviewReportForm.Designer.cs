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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.buttonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonFirst = new System.Windows.Forms.ToolStripButton();
            this.buttonPrev = new System.Windows.Forms.ToolStripButton();
            this.textCurrentPage = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.labelCountPages = new System.Windows.Forms.ToolStripLabel();
            this.buttonNext = new System.Windows.Forms.ToolStripButton();
            this.buttonLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.comboZoom = new Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx();
            this.buttonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.buttonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonFillWindow = new System.Windows.Forms.ToolStripButton();
            this.buttonFitPageToWindow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonSend = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pdfDocumentView1 = new Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView();
            this.savePdfDialog = new System.Windows.Forms.SaveFileDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonSave,
            this.buttonPrint,
            this.toolStripSeparator2,
            this.buttonFirst,
            this.buttonPrev,
            this.textCurrentPage,
            this.toolStripLabel1,
            this.labelCountPages,
            this.buttonNext,
            this.buttonLast,
            this.toolStripSeparator1,
            this.comboZoom,
            this.buttonZoomIn,
            this.buttonZoomOut,
            this.toolStripSeparator3,
            this.buttonFillWindow,
            this.buttonFitPageToWindow,
            this.toolStripSeparator4,
            this.buttonSend});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(860, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonSave
            // 
            this.buttonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSave.Image = global::DocumentFlow.Properties.Resources.icons8_save_16;
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(23, 22);
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonPrint
            // 
            this.buttonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonPrint.Image = global::DocumentFlow.Properties.Resources.icons8_print_16;
            this.buttonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(23, 22);
            this.buttonPrint.Text = "Печать";
            this.buttonPrint.Click += new System.EventHandler(this.ButtonPrint_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonFirst
            // 
            this.buttonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonFirst.Image = global::DocumentFlow.Properties.Resources.icons8_start_16;
            this.buttonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonFirst.Name = "buttonFirst";
            this.buttonFirst.Size = new System.Drawing.Size(23, 22);
            this.buttonFirst.Text = "Первая страница";
            this.buttonFirst.Click += new System.EventHandler(this.ButtonFirst_Click);
            // 
            // buttonPrev
            // 
            this.buttonPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonPrev.Image = global::DocumentFlow.Properties.Resources.icons8_left_16;
            this.buttonPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(23, 22);
            this.buttonPrev.Text = "Предыдущая страница";
            this.buttonPrev.Click += new System.EventHandler(this.ButtonPrev_Click);
            // 
            // textCurrentPage
            // 
            this.textCurrentPage.Name = "textCurrentPage";
            this.textCurrentPage.Size = new System.Drawing.Size(50, 25);
            this.textCurrentPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextCurrentPage_KeyDown);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(12, 22);
            this.toolStripLabel1.Text = "/";
            // 
            // labelCountPages
            // 
            this.labelCountPages.Name = "labelCountPages";
            this.labelCountPages.Size = new System.Drawing.Size(13, 22);
            this.labelCountPages.Text = "0";
            // 
            // buttonNext
            // 
            this.buttonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonNext.Image = global::DocumentFlow.Properties.Resources.icons8_right_16;
            this.buttonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(23, 22);
            this.buttonNext.Text = "Следующая страница";
            this.buttonNext.Click += new System.EventHandler(this.ButtonNext_Click);
            // 
            // buttonLast
            // 
            this.buttonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonLast.Image = global::DocumentFlow.Properties.Resources.icons8_end_16;
            this.buttonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonLast.Name = "buttonLast";
            this.buttonLast.Size = new System.Drawing.Size(23, 22);
            this.buttonLast.Text = "Последняя страница";
            this.buttonLast.Click += new System.EventHandler(this.ButtonLast_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // comboZoom
            // 
            this.comboZoom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboZoom.Items.AddRange(new object[] {
            "50 %",
            "75 %",
            "100 %",
            "150 %",
            "200 %",
            "400 %"});
            this.comboZoom.MaxLength = 32767;
            this.comboZoom.Name = "comboZoom";
            this.comboZoom.Size = new System.Drawing.Size(60, 25);
            this.comboZoom.Text = "100 %";
            this.comboZoom.TextUpdate += new System.EventHandler(this.ComboZoom_TextUpdate);
            // 
            // buttonZoomIn
            // 
            this.buttonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonZoomIn.Image = global::DocumentFlow.Properties.Resources.icons8_zoom_in_16;
            this.buttonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonZoomIn.Name = "buttonZoomIn";
            this.buttonZoomIn.Size = new System.Drawing.Size(23, 22);
            this.buttonZoomIn.Text = "Увеличить";
            this.buttonZoomIn.Click += new System.EventHandler(this.ButtonZoomIn_Click);
            // 
            // buttonZoomOut
            // 
            this.buttonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonZoomOut.Image = global::DocumentFlow.Properties.Resources.icons8_zoom_out_16;
            this.buttonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonZoomOut.Name = "buttonZoomOut";
            this.buttonZoomOut.Size = new System.Drawing.Size(23, 22);
            this.buttonZoomOut.Text = "Уменьшить";
            this.buttonZoomOut.Click += new System.EventHandler(this.ButtonZoomOut_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonFillWindow
            // 
            this.buttonFillWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonFillWindow.Image = global::DocumentFlow.Properties.Resources.icons8_fill_window_16;
            this.buttonFillWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonFillWindow.Name = "buttonFillWindow";
            this.buttonFillWindow.Size = new System.Drawing.Size(23, 22);
            this.buttonFillWindow.Text = "По ширине окна";
            this.buttonFillWindow.Click += new System.EventHandler(this.ButtonFillWindow_Click);
            // 
            // buttonFitPageToWindow
            // 
            this.buttonFitPageToWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonFitPageToWindow.Image = global::DocumentFlow.Properties.Resources.icons8_fit_page_16;
            this.buttonFitPageToWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonFitPageToWindow.Name = "buttonFitPageToWindow";
            this.buttonFitPageToWindow.Size = new System.Drawing.Size(23, 22);
            this.buttonFitPageToWindow.Text = "Страница целиком";
            this.buttonFitPageToWindow.Click += new System.EventHandler(this.ButtonFitPageToWindow_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonSend
            // 
            this.buttonSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSend.Image = global::DocumentFlow.Properties.Resources.icons8_send_16;
            this.buttonSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(23, 22);
            this.buttonSend.Text = "Отправить по эл. почте";
            this.buttonSend.Click += new System.EventHandler(this.ButtonSend_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pdfDocumentView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 547);
            this.panel1.TabIndex = 2;
            // 
            // pdfDocumentView1
            // 
            this.pdfDocumentView1.AutoScroll = true;
            this.pdfDocumentView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.pdfDocumentView1.CursorMode = Syncfusion.Windows.Forms.PdfViewer.PdfViewerCursorMode.SelectTool;
            this.pdfDocumentView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfDocumentView1.EnableContextMenu = true;
            this.pdfDocumentView1.HorizontalScrollOffset = 0;
            this.pdfDocumentView1.IsTextSearchEnabled = true;
            this.pdfDocumentView1.IsTextSelectionEnabled = true;
            this.pdfDocumentView1.Location = new System.Drawing.Point(0, 0);
            messageBoxSettings1.EnableNotification = true;
            this.pdfDocumentView1.MessageBoxSettings = messageBoxSettings1;
            this.pdfDocumentView1.MinimumZoomPercentage = 50;
            this.pdfDocumentView1.Name = "pdfDocumentView1";
            this.pdfDocumentView1.PageBorderThickness = 1;
            pdfViewerPrinterSettings1.Copies = 1;
            pdfViewerPrinterSettings1.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto;
            pdfViewerPrinterSettings1.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize;
            pdfViewerPrinterSettings1.PrintLocation = ((System.Drawing.PointF)(resources.GetObject("pdfViewerPrinterSettings1.PrintLocation")));
            pdfViewerPrinterSettings1.ShowPrintStatusDialog = true;
            this.pdfDocumentView1.PrinterSettings = pdfViewerPrinterSettings1;
            this.pdfDocumentView1.ReferencePath = null;
            this.pdfDocumentView1.ScrollDisplacementValue = 0;
            this.pdfDocumentView1.ShowHorizontalScrollBar = true;
            this.pdfDocumentView1.ShowVerticalScrollBar = true;
            this.pdfDocumentView1.Size = new System.Drawing.Size(860, 547);
            this.pdfDocumentView1.SpaceBetweenPages = 8;
            this.pdfDocumentView1.TabIndex = 1;
            textSearchSettings1.CurrentInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(64)))));
            textSearchSettings1.HighlightAllInstance = true;
            textSearchSettings1.OtherInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.pdfDocumentView1.TextSearchSettings = textSearchSettings1;
            this.pdfDocumentView1.ThemeName = "Default";
            this.pdfDocumentView1.VerticalScrollOffset = 0;
            this.pdfDocumentView1.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Default;
            this.pdfDocumentView1.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.Default;
            this.pdfDocumentView1.DocumentLoaded += new Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView.DocumentLoadedEventHandler(this.PdfDocumentView1_DocumentLoaded);
            this.pdfDocumentView1.ZoomChanged += new Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView.ZoomChangedEventHandler(this.PdfDocumentView1_ZoomChanged);
            // 
            // savePdfDialog
            // 
            this.savePdfDialog.DefaultExt = "pdf";
            this.savePdfDialog.Filter = "PDF файлы|*.pdf";
            // 
            // printDialog1
            // 
            this.printDialog1.AllowCurrentPage = true;
            this.printDialog1.AllowSomePages = true;
            this.printDialog1.UseEXDialog = true;
            // 
            // PreviewReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 572);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PreviewReportForm";
            this.Text = "PreviewReportForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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