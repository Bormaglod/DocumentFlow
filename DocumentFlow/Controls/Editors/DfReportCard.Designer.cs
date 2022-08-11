namespace DocumentFlow.Controls.Editors
{
    partial class DfReportCard
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.buttonEdit = new System.Windows.Forms.ToolStripButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonPopulate = new System.Windows.Forms.ToolStripButton();
            this.gridContent = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.contextMenuStripEx1 = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuPopulate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridContent)).BeginInit();
            this.contextMenuStripEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAdd,
            this.buttonEdit,
            this.buttonDelete,
            this.toolStripSeparator1,
            this.buttonPopulate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(667, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Image = global::DocumentFlow.Properties.Resources.icons8_add_user_16;
            this.buttonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(79, 22);
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Image = global::DocumentFlow.Properties.Resources.icons8_edit_user_16;
            this.buttonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(81, 22);
            this.buttonEdit.Text = "Изменить";
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Image = global::DocumentFlow.Properties.Resources.icons8_delete_user_16;
            this.buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(71, 22);
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonPopulate
            // 
            this.buttonPopulate.Image = global::DocumentFlow.Properties.Resources.icons8_incoming_data_16;
            this.buttonPopulate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPopulate.Name = "buttonPopulate";
            this.buttonPopulate.Size = new System.Drawing.Size(86, 22);
            this.buttonPopulate.Text = "Заполнить";
            this.buttonPopulate.Click += new System.EventHandler(this.ButtonPopulate_Click);
            // 
            // gridContent
            // 
            this.gridContent.AccessibleName = "Table";
            this.gridContent.AllowGrouping = false;
            this.gridContent.AllowResizingColumns = true;
            this.gridContent.AutoGenerateColumns = false;
            this.gridContent.ContextMenuStrip = this.contextMenuStripEx1;
            this.gridContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridContent.FrozenColumnCount = 1;
            this.gridContent.Location = new System.Drawing.Point(0, 25);
            this.gridContent.Name = "gridContent";
            this.gridContent.Size = new System.Drawing.Size(667, 350);
            this.gridContent.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridContent.Style.CheckBoxStyle.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridContent.Style.CheckBoxStyle.CheckedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridContent.Style.CheckBoxStyle.IndeterminateBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridContent.Style.HyperlinkStyle.DefaultLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridContent.TabIndex = 1;
            this.gridContent.Text = "sfDataGrid1";
            // 
            // contextMenuStripEx1
            // 
            this.contextMenuStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAdd,
            this.menuEdit,
            this.menuDelete,
            this.toolStripSeparator2,
            this.menuPopulate});
            this.contextMenuStripEx1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextMenuStripEx1.Name = "contextMenuStripEx1";
            this.contextMenuStripEx1.Size = new System.Drawing.Size(134, 98);
            this.contextMenuStripEx1.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Default;
            this.contextMenuStripEx1.ThemeName = "Default";
            // 
            // menuAdd
            // 
            this.menuAdd.Image = global::DocumentFlow.Properties.Resources.icons8_add_user_16;
            this.menuAdd.Name = "menuAdd";
            this.menuAdd.Size = new System.Drawing.Size(133, 22);
            this.menuAdd.Text = "Добавить";
            this.menuAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.Image = global::DocumentFlow.Properties.Resources.icons8_edit_user_16;
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(133, 22);
            this.menuEdit.Text = "Изменить";
            this.menuEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // menuDelete
            // 
            this.menuDelete.Image = global::DocumentFlow.Properties.Resources.icons8_delete_user_16;
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(133, 22);
            this.menuDelete.Text = "Удалить";
            this.menuDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
            // 
            // menuPopulate
            // 
            this.menuPopulate.Image = global::DocumentFlow.Properties.Resources.icons8_incoming_data_16;
            this.menuPopulate.Name = "menuPopulate";
            this.menuPopulate.Size = new System.Drawing.Size(133, 22);
            this.menuPopulate.Text = "Заполнить";
            this.menuPopulate.Click += new System.EventHandler(this.ButtonPopulate_Click);
            // 
            // DfReportCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridContent);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DfReportCard";
            this.Size = new System.Drawing.Size(667, 375);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridContent)).EndInit();
            this.contextMenuStripEx1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton buttonAdd;
        private ToolStripButton buttonEdit;
        private ToolStripButton buttonDelete;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton buttonPopulate;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridContent;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextMenuStripEx1;
        private ToolStripMenuItem menuAdd;
        private ToolStripMenuItem menuEdit;
        private ToolStripMenuItem menuDelete;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuPopulate;
    }
}
