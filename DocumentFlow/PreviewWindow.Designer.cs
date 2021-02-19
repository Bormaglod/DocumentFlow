
namespace DocumentFlow
{
    partial class PreviewWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewWindow));
            Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings textSearchSettings1 = new Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings();
            this.pdfDocumentView1 = new Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView();
            this.savePdfDialog = new System.Windows.Forms.SaveFileDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.toolStripEx1 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.buttonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonHand = new System.Windows.Forms.ToolStripButton();
            this.buttonSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonFirstPage = new System.Windows.Forms.ToolStripButton();
            this.buttonPrevPage = new System.Windows.Forms.ToolStripButton();
            this.textCurrentPage = new System.Windows.Forms.ToolStripTextBox();
            this.labelPageSeparator = new System.Windows.Forms.ToolStripLabel();
            this.labelCountPages = new System.Windows.Forms.ToolStripLabel();
            this.buttonNextPage = new System.Windows.Forms.ToolStripButton();
            this.buttonLastPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.comboZoom = new Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx();
            this.buttonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.buttonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonFillWindow = new System.Windows.Forms.ToolStripButton();
            this.buttonFitPageToWindow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonSend = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pdfDocumentView1
            // 
            this.pdfDocumentView1.AutoScroll = true;
            this.pdfDocumentView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.pdfDocumentView1.CursorMode = Syncfusion.Windows.Forms.PdfViewer.PdfViewerCursorMode.HandTool;
            this.pdfDocumentView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfDocumentView1.EnableContextMenu = true;
            this.pdfDocumentView1.HorizontalScrollOffset = 0;
            this.pdfDocumentView1.IsTextSearchEnabled = true;
            this.pdfDocumentView1.IsTextSelectionEnabled = true;
            this.pdfDocumentView1.Location = new System.Drawing.Point(0, 25);
            messageBoxSettings1.EnableNotification = true;
            this.pdfDocumentView1.MessageBoxSettings = messageBoxSettings1;
            this.pdfDocumentView1.MinimumZoomPercentage = 50;
            this.pdfDocumentView1.Name = "pdfDocumentView1";
            this.pdfDocumentView1.PageBorderThickness = 1;
            pdfViewerPrinterSettings1.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto;
            pdfViewerPrinterSettings1.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize;
            pdfViewerPrinterSettings1.PrintLocation = ((System.Drawing.PointF)(resources.GetObject("pdfViewerPrinterSettings1.PrintLocation")));
            pdfViewerPrinterSettings1.ShowPrintStatusDialog = true;
            this.pdfDocumentView1.PrinterSettings = pdfViewerPrinterSettings1;
            this.pdfDocumentView1.ReferencePath = null;
            this.pdfDocumentView1.ScrollDisplacementValue = 0;
            this.pdfDocumentView1.ShowHorizontalScrollBar = true;
            this.pdfDocumentView1.ShowVerticalScrollBar = true;
            this.pdfDocumentView1.Size = new System.Drawing.Size(800, 425);
            this.pdfDocumentView1.SpaceBetweenPages = 8;
            this.pdfDocumentView1.TabIndex = 0;
            textSearchSettings1.CurrentInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(64)))));
            textSearchSettings1.HighlightAllInstance = true;
            textSearchSettings1.OtherInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.pdfDocumentView1.TextSearchSettings = textSearchSettings1;
            this.pdfDocumentView1.ThemeName = "Office2016Colorful";
            this.pdfDocumentView1.VerticalScrollOffset = 0;
            this.pdfDocumentView1.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Office2016Colorful;
            this.pdfDocumentView1.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.Default;
            this.pdfDocumentView1.DocumentLoaded += new Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView.DocumentLoadedEventHandler(this.pdfDocumentView1_DocumentLoaded);
            this.pdfDocumentView1.ZoomChanged += new Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView.ZoomChangedEventHandler(this.pdfDocumentView1_ZoomChanged);
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
            // toolStripEx1
            // 
            this.toolStripEx1.CaptionStyle = Syncfusion.Windows.Forms.Tools.CaptionStyle.Top;
            this.toolStripEx1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx1.Image = global::DocumentFlow.Properties.Resources.icons8_hand_16;
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonSave,
            this.buttonPrint,
            this.toolStripSeparator4,
            this.buttonHand,
            this.buttonSelect,
            this.toolStripSeparator1,
            this.buttonFirstPage,
            this.buttonPrevPage,
            this.textCurrentPage,
            this.labelPageSeparator,
            this.labelCountPages,
            this.buttonNextPage,
            this.buttonLastPage,
            this.toolStripSeparator2,
            this.comboZoom,
            this.buttonZoomIn,
            this.buttonZoomOut,
            this.toolStripSeparator3,
            this.buttonFillWindow,
            this.buttonFitPageToWindow,
            this.toolStripSeparator5,
            this.buttonSend});
            this.toolStripEx1.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.Office12Mode = false;
            this.toolStripEx1.ShowCaption = false;
            this.toolStripEx1.ShowLauncher = true;
            this.toolStripEx1.Size = new System.Drawing.Size(800, 25);
            this.toolStripEx1.TabIndex = 2;
            this.toolStripEx1.Text = "toolStripEx1";
            this.toolStripEx1.ThemeName = "Office2016Colorful";
            this.toolStripEx1.VisualStyle = Syncfusion.Windows.Forms.Tools.ToolStripExStyle.Office2016Colorful;
            // 
            // buttonSave
            // 
            this.buttonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSave.Image = global::DocumentFlow.Properties.Resources.icons8_save_16;
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(23, 22);
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonPrint
            // 
            this.buttonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonPrint.Image = global::DocumentFlow.Properties.Resources.icons8_print_16;
            this.buttonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(23, 22);
            this.buttonPrint.Text = "Печать";
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonHand
            // 
            this.buttonHand.Checked = true;
            this.buttonHand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buttonHand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonHand.Image = global::DocumentFlow.Properties.Resources.icons8_hand_16;
            this.buttonHand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonHand.Name = "buttonHand";
            this.buttonHand.Size = new System.Drawing.Size(23, 22);
            this.buttonHand.Text = "Hand";
            this.buttonHand.Click += new System.EventHandler(this.buttonHand_Click);
            // 
            // buttonSelect
            // 
            this.buttonSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSelect.Image = global::DocumentFlow.Properties.Resources.icons8_select_text_16;
            this.buttonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(23, 22);
            this.buttonSelect.Text = "Select";
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonFirstPage
            // 
            this.buttonFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonFirstPage.Image = global::DocumentFlow.Properties.Resources.icons8_start_16;
            this.buttonFirstPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonFirstPage.Name = "buttonFirstPage";
            this.buttonFirstPage.Size = new System.Drawing.Size(23, 22);
            this.buttonFirstPage.Text = "Первая страница";
            this.buttonFirstPage.Click += new System.EventHandler(this.buttonFirstPage_Click);
            // 
            // buttonPrevPage
            // 
            this.buttonPrevPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonPrevPage.Image = global::DocumentFlow.Properties.Resources.icons8_left_16;
            this.buttonPrevPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPrevPage.Name = "buttonPrevPage";
            this.buttonPrevPage.Size = new System.Drawing.Size(23, 22);
            this.buttonPrevPage.Text = "Предыдущая страница";
            this.buttonPrevPage.Click += new System.EventHandler(this.buttonPrevPage_Click);
            // 
            // textCurrentPage
            // 
            this.textCurrentPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textCurrentPage.Name = "textCurrentPage";
            this.textCurrentPage.Size = new System.Drawing.Size(40, 25);
            this.textCurrentPage.TextChanged += new System.EventHandler(this.textCurrentPage_TextChanged);
            // 
            // labelPageSeparator
            // 
            this.labelPageSeparator.Name = "labelPageSeparator";
            this.labelPageSeparator.Size = new System.Drawing.Size(12, 22);
            this.labelPageSeparator.Text = "/";
            // 
            // labelCountPages
            // 
            this.labelCountPages.Name = "labelCountPages";
            this.labelCountPages.Size = new System.Drawing.Size(13, 22);
            this.labelCountPages.Text = "0";
            // 
            // buttonNextPage
            // 
            this.buttonNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonNextPage.Image = global::DocumentFlow.Properties.Resources.icons8_right_16;
            this.buttonNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNextPage.Name = "buttonNextPage";
            this.buttonNextPage.Size = new System.Drawing.Size(23, 22);
            this.buttonNextPage.Text = "Следующая страница";
            this.buttonNextPage.Click += new System.EventHandler(this.buttonNextPage_Click);
            // 
            // buttonLastPage
            // 
            this.buttonLastPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonLastPage.Image = global::DocumentFlow.Properties.Resources.icons8_end_16;
            this.buttonLastPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonLastPage.Name = "buttonLastPage";
            this.buttonLastPage.Size = new System.Drawing.Size(23, 22);
            this.buttonLastPage.Text = "Последняя страница";
            this.buttonLastPage.Click += new System.EventHandler(this.buttonLastPage_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // comboZoom
            // 
            this.comboZoom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboZoom.ForeColor = System.Drawing.SystemColors.WindowText;
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
            this.comboZoom.TextUpdate += new System.EventHandler(this.comboZoom_TextUpdate);
            // 
            // buttonZoomIn
            // 
            this.buttonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonZoomIn.Image = global::DocumentFlow.Properties.Resources.icons8_zoom_in_16;
            this.buttonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonZoomIn.Name = "buttonZoomIn";
            this.buttonZoomIn.Size = new System.Drawing.Size(23, 22);
            this.buttonZoomIn.Text = "Увеличить";
            this.buttonZoomIn.Click += new System.EventHandler(this.buttonZoomIn_Click);
            // 
            // buttonZoomOut
            // 
            this.buttonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonZoomOut.Image = global::DocumentFlow.Properties.Resources.icons8_zoom_out_16;
            this.buttonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonZoomOut.Name = "buttonZoomOut";
            this.buttonZoomOut.Size = new System.Drawing.Size(23, 22);
            this.buttonZoomOut.Text = "Уменьшить";
            this.buttonZoomOut.Click += new System.EventHandler(this.buttonZoomOut_Click);
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
            this.buttonFillWindow.Click += new System.EventHandler(this.buttonFillWindow_Click);
            // 
            // buttonFitPageToWindow
            // 
            this.buttonFitPageToWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonFitPageToWindow.Image = global::DocumentFlow.Properties.Resources.icons8_fit_page_16;
            this.buttonFitPageToWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonFitPageToWindow.Name = "buttonFitPageToWindow";
            this.buttonFitPageToWindow.Size = new System.Drawing.Size(23, 22);
            this.buttonFitPageToWindow.Text = "Страница целиком";
            this.buttonFitPageToWindow.Click += new System.EventHandler(this.buttonFitPageToWindow_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonSend
            // 
            this.buttonSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSend.Image = global::DocumentFlow.Properties.Resources.icons8_send_16;
            this.buttonSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(23, 22);
            this.buttonSend.Text = "Отправить по эл. почте";
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // PreviewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pdfDocumentView1);
            this.Controls.Add(this.toolStripEx1);
            this.Name = "PreviewWindow";
            this.Text = "PreviewWindow";
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.PdfViewer.PdfDocumentView pdfDocumentView1;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripButton buttonSave;
        private System.Windows.Forms.ToolStripButton buttonPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonFirstPage;
        private System.Windows.Forms.ToolStripButton buttonPrevPage;
        private System.Windows.Forms.ToolStripTextBox textCurrentPage;
        private System.Windows.Forms.ToolStripLabel labelPageSeparator;
        private System.Windows.Forms.ToolStripLabel labelCountPages;
        private System.Windows.Forms.ToolStripButton buttonNextPage;
        private System.Windows.Forms.ToolStripButton buttonLastPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx comboZoom;
        private System.Windows.Forms.ToolStripButton buttonZoomIn;
        private System.Windows.Forms.ToolStripButton buttonZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton buttonFillWindow;
        private System.Windows.Forms.ToolStripButton buttonFitPageToWindow;
        private System.Windows.Forms.SaveFileDialog savePdfDialog;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton buttonHand;
        private System.Windows.Forms.ToolStripButton buttonSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton buttonSend;
    }
}