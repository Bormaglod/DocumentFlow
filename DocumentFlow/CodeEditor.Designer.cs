namespace DocumentFlow
{
    partial class CodeEditor
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            Syncfusion.Windows.Forms.Edit.Implementation.Config.Config config1 = new Syncfusion.Windows.Forms.Edit.Implementation.Config.Config();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tabSplitterContainer1 = new Syncfusion.Windows.Forms.Tools.TabSplitterContainer();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tabSplitterPage1 = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.editControl = new Syncfusion.Windows.Forms.Edit.EditControl();
            this.tabSplitterPage2 = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.gridErrors = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.buttonSaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.buttonCompile = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.tabSplitterContainer1.SuspendLayout();
            this.tabSplitterPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editControl)).BeginInit();
            this.tabSplitterPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonSave,
            this.buttonSaveAndClose,
            this.toolStripSeparator1,
            this.buttonCompile});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1136, 52);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tabSplitterContainer1
            // 
            this.tabSplitterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSplitterContainer1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabSplitterContainer1.Location = new System.Drawing.Point(0, 52);
            this.tabSplitterContainer1.Name = "tabSplitterContainer1";
            this.tabSplitterContainer1.PrimaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.tabSplitterPage1});
            this.tabSplitterContainer1.SecondaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.tabSplitterPage2});
            this.tabSplitterContainer1.Size = new System.Drawing.Size(1136, 442);
            this.tabSplitterContainer1.SplitterBackColor = System.Drawing.SystemColors.Control;
            this.tabSplitterContainer1.SplitterPosition = 242;
            this.tabSplitterContainer1.TabIndex = 2;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // tabSplitterPage1
            // 
            this.tabSplitterPage1.AutoScroll = true;
            this.tabSplitterPage1.Controls.Add(this.editControl);
            this.tabSplitterPage1.Hide = false;
            this.tabSplitterPage1.Location = new System.Drawing.Point(0, 0);
            this.tabSplitterPage1.Name = "tabSplitterPage1";
            this.tabSplitterPage1.Size = new System.Drawing.Size(1136, 242);
            this.tabSplitterPage1.TabIndex = 1;
            this.tabSplitterPage1.Text = "Код";
            // 
            // editControl
            // 
            this.editControl.AllowZoom = false;
            this.editControl.ChangedLinesMarkingLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(98)))));
            this.editControl.CodeSnipptSize = new System.Drawing.Size(100, 100);
            this.editControl.Configurator = config1;
            this.editControl.ContextChoiceBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.editControl.ContextChoiceBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(166)))), ((int)(((byte)(50)))));
            this.editControl.ContextChoiceForeColor = System.Drawing.SystemColors.InfoText;
            this.editControl.ContextPromptBackgroundBrush = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))));
            this.editControl.ContextTooltipBackgroundBrush = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(236))))));
            this.editControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editControl.GraphicsCompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.editControl.GraphicsInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.editControl.GraphicsSmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.editControl.GraphicsTextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.editControl.IndicatorMarginBackColor = System.Drawing.Color.Empty;
            this.editControl.LineNumbersColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.editControl.LineNumbersFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editControl.Location = new System.Drawing.Point(0, 0);
            this.editControl.Name = "editControl";
            this.editControl.RenderRightToLeft = false;
            this.editControl.ScrollPosition = new System.Drawing.Point(0, 0);
            this.editControl.ScrollVisualStyle = Syncfusion.Windows.Forms.ScrollBarCustomDrawStyles.Office2016;
            this.editControl.SelectionTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(214)))), ((int)(((byte)(255)))));
            this.editControl.ShowHorizontalSplitters = false;
            this.editControl.ShowVerticalSplitters = false;
            this.editControl.Size = new System.Drawing.Size(1136, 242);
            this.editControl.StatusBarSettings.CoordsPanel.Width = 150;
            this.editControl.StatusBarSettings.EncodingPanel.Width = 100;
            this.editControl.StatusBarSettings.FileNamePanel.Visible = false;
            this.editControl.StatusBarSettings.FileNamePanel.Width = 100;
            this.editControl.StatusBarSettings.GripVisibility = Syncfusion.Windows.Forms.Edit.Enums.SizingGripVisibility.Hidden;
            this.editControl.StatusBarSettings.InsertPanel.Width = 33;
            this.editControl.StatusBarSettings.Offcie2007ColorScheme = Syncfusion.Windows.Forms.Office2007Theme.Blue;
            this.editControl.StatusBarSettings.Offcie2010ColorScheme = Syncfusion.Windows.Forms.Office2010Theme.Blue;
            this.editControl.StatusBarSettings.StatusPanel.Width = 70;
            this.editControl.StatusBarSettings.TextPanel.Width = 766;
            this.editControl.StatusBarSettings.Visible = true;
            this.editControl.StatusBarSettings.VisualStyle = Syncfusion.Windows.Forms.Tools.Controls.StatusBar.VisualStyle.Office2016Colorful;
            this.editControl.Style = Syncfusion.Windows.Forms.Edit.EditControlStyle.Office2016Colorful;
            this.editControl.TabIndex = 1;
            this.editControl.TabSize = 4;
            this.editControl.Text = "";
            this.editControl.ThemeName = "Office2016Colorful";
            this.editControl.UseXPStyle = false;
            this.editControl.UseXPStyleBorder = true;
            this.editControl.VisualColumn = 1;
            this.editControl.VScrollMode = Syncfusion.Windows.Forms.Edit.ScrollMode.Immediate;
            this.editControl.ZoomFactor = 1F;
            this.editControl.Closing += new Syncfusion.Windows.Forms.Edit.StreamCloseEventHandler(this.editControl_Closing);
            // 
            // tabSplitterPage2
            // 
            this.tabSplitterPage2.AutoScroll = true;
            this.tabSplitterPage2.Controls.Add(this.gridErrors);
            this.tabSplitterPage2.Hide = false;
            this.tabSplitterPage2.Location = new System.Drawing.Point(0, 263);
            this.tabSplitterPage2.Name = "tabSplitterPage2";
            this.tabSplitterPage2.Size = new System.Drawing.Size(1136, 179);
            this.tabSplitterPage2.TabIndex = 2;
            this.tabSplitterPage2.Text = "Список ошибок";
            // 
            // gridErrors
            // 
            this.gridErrors.AccessibleName = "Table";
            this.gridErrors.AllowEditing = false;
            this.gridErrors.AllowGrouping = false;
            this.gridErrors.AllowResizingColumns = true;
            this.gridErrors.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCellsWithLastColumnFill;
            this.gridErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridErrors.Location = new System.Drawing.Point(0, 0);
            this.gridErrors.Name = "gridErrors";
            this.gridErrors.ShowRowHeader = true;
            this.gridErrors.Size = new System.Drawing.Size(1136, 179);
            this.gridErrors.TabIndex = 1;
            this.gridErrors.Text = "sfDataGrid1";
            // 
            // buttonSave
            // 
            this.buttonSave.Image = global::DocumentFlow.Properties.Resources.icons8_save_30;
            this.buttonSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 49);
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Image = global::DocumentFlow.Properties.Resources.icons8_save_close_30;
            this.buttonSaveAndClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonSaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(127, 49);
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonCompile
            // 
            this.buttonCompile.Image = global::DocumentFlow.Properties.Resources.icons8_assembly_line_30;
            this.buttonCompile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonCompile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCompile.Name = "buttonCompile";
            this.buttonCompile.Size = new System.Drawing.Size(81, 49);
            this.buttonCompile.Text = "Компиляция";
            this.buttonCompile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonCompile.Click += new System.EventHandler(this.buttonCompile_Click);
            // 
            // CodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 494);
            this.Controls.Add(this.tabSplitterContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CodeEditor";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabSplitterContainer1.ResumeLayout(false);
            this.tabSplitterPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editControl)).EndInit();
            this.tabSplitterPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonSave;
        private System.Windows.Forms.ToolStripButton buttonSaveAndClose;
        private Syncfusion.Windows.Forms.Edit.EditControl editControl;
        private Syncfusion.Windows.Forms.Tools.TabSplitterContainer tabSplitterContainer1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage tabSplitterPage1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage tabSplitterPage2;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridErrors;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonCompile;
    }
}
