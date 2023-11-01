namespace DocumentFlow.Controls.PageContents
{
    partial class EmailPage
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
            Syncfusion.WinForms.DataGrid.SortColumnDescription sortColumnDescription1 = new Syncfusion.WinForms.DataGrid.SortColumnDescription();
            toolStrip1 = new ToolStrip();
            buttonContractor = new ToolStripButton();
            buttonDocument = new ToolStripButton();
            gridContent = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            contextMenuStripEx1 = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            menuContractor = new ToolStripMenuItem();
            menuDocument = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            listCompany = new Syncfusion.WinForms.ListView.SfListView();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridContent).BeginInit();
            contextMenuStripEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { buttonContractor, buttonDocument });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1060, 52);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // buttonContractor
            // 
            buttonContractor.Image = DocumentFlow.Properties.Resources.icons8_contractor_30;
            buttonContractor.ImageScaling = ToolStripItemImageScaling.None;
            buttonContractor.ImageTransparentColor = Color.Magenta;
            buttonContractor.Name = "buttonContractor";
            buttonContractor.Size = new Size(73, 49);
            buttonContractor.Text = "Контрагент";
            buttonContractor.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonContractor.Click += ButtonContractor_Click;
            // 
            // buttonDocument
            // 
            buttonDocument.Image = DocumentFlow.Properties.Resources.icons8_e_mail_document_30;
            buttonDocument.ImageScaling = ToolStripItemImageScaling.None;
            buttonDocument.ImageTransparentColor = Color.Magenta;
            buttonDocument.Name = "buttonDocument";
            buttonDocument.Size = new Size(65, 49);
            buttonDocument.Text = "Документ";
            buttonDocument.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonDocument.Click += ButtonDocument_Click;
            // 
            // gridContent
            // 
            gridContent.AccessibleName = "Table";
            gridContent.AllowEditing = false;
            gridContent.AllowGrouping = false;
            gridContent.AllowResizingColumns = true;
            gridContent.BackColor = SystemColors.Window;
            gridContent.ContextMenuStrip = contextMenuStripEx1;
            gridContent.Dock = DockStyle.Fill;
            gridContent.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            gridContent.Location = new Point(0, 0);
            gridContent.Name = "gridContent";
            gridContent.ShowRowHeader = true;
            gridContent.ShowSortNumbers = true;
            gridContent.Size = new Size(804, 467);
            sortColumnDescription1.ColumnName = "DateTimeSending";
            gridContent.SortColumnDescriptions.Add(sortColumnDescription1);
            gridContent.Style.BorderColor = Color.FromArgb(204, 204, 204);
            gridContent.Style.CheckBoxStyle.CheckedBackColor = Color.FromArgb(0, 120, 215);
            gridContent.Style.CheckBoxStyle.CheckedBorderColor = Color.FromArgb(0, 120, 215);
            gridContent.Style.CheckBoxStyle.IndeterminateBorderColor = Color.FromArgb(0, 120, 215);
            gridContent.Style.HyperlinkStyle.DefaultLinkColor = Color.FromArgb(0, 120, 215);
            gridContent.TabIndex = 1;
            gridContent.Text = "sfDataGrid1";
            gridContent.AutoGeneratingColumn += GridContent_AutoGeneratingColumn;
            // 
            // contextMenuStripEx1
            // 
            contextMenuStripEx1.DropShadowEnabled = false;
            contextMenuStripEx1.Items.AddRange(new ToolStripItem[] { menuContractor, menuDocument });
            contextMenuStripEx1.MetroColor = Color.FromArgb(204, 236, 249);
            contextMenuStripEx1.Name = "contextMenuStripEx1";
            contextMenuStripEx1.Size = new Size(137, 48);
            contextMenuStripEx1.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Office2016Colorful;
            contextMenuStripEx1.ThemeName = "Office2016Colorful";
            // 
            // menuContractor
            // 
            menuContractor.Image = DocumentFlow.Properties.Resources.icons8_contractor_16;
            menuContractor.Name = "menuContractor";
            menuContractor.Size = new Size(136, 22);
            menuContractor.Text = "Контрагент";
            menuContractor.Click += ButtonContractor_Click;
            // 
            // menuDocument
            // 
            menuDocument.Image = DocumentFlow.Properties.Resources.icons8_e_mail_document_16;
            menuDocument.Name = "menuDocument";
            menuDocument.Size = new Size(136, 22);
            menuDocument.Text = "Документ";
            menuDocument.Click += ButtonDocument_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 52);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(listCompany);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(gridContent);
            splitContainer1.Size = new Size(1060, 467);
            splitContainer1.SplitterDistance = 252;
            splitContainer1.TabIndex = 3;
            // 
            // listCompany
            // 
            listCompany.AccessibleName = "ScrollControl";
            listCompany.BackColor = SystemColors.Window;
            listCompany.Dock = DockStyle.Fill;
            listCompany.Location = new Point(0, 0);
            listCompany.Name = "listCompany";
            listCompany.Size = new Size(252, 467);
            listCompany.Style.BorderColor = Color.FromArgb(204, 204, 204);
            listCompany.TabIndex = 0;
            listCompany.Text = "sfListView1";
            listCompany.GroupCollapsing += ListCompany_GroupCollapsing;
            listCompany.SelectionChanging += ListCompany_SelectionChanging;
            // 
            // EmailPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(toolStrip1);
            Name = "EmailPage";
            Size = new Size(1060, 519);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridContent).EndInit();
            contextMenuStripEx1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridContent;
        private SplitContainer splitContainer1;
        private Syncfusion.WinForms.ListView.SfListView listCompany;
        private ToolStripButton buttonContractor;
        private ToolStripButton buttonDocument;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextMenuStripEx1;
        private ToolStripMenuItem menuContractor;
        private ToolStripMenuItem menuDocument;
    }
}
