namespace DocumentFlow.Controls.Editor
{
    partial class L_DataGrid
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
            this.gridMain = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.contextMenuStripEx1 = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuItemCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bttonCreate = new System.Windows.Forms.ToolStripButton();
            this.buttonEdit = new System.Windows.Forms.ToolStripButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            this.contextMenuStripEx1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridMain
            // 
            this.gridMain.AccessibleName = "Table";
            this.gridMain.AllowEditing = false;
            this.gridMain.AllowGrouping = false;
            this.gridMain.AllowResizingColumns = true;
            this.gridMain.AllowTriStateSorting = true;
            this.gridMain.AutoGenerateColumns = false;
            this.gridMain.ContextMenuStrip = this.contextMenuStripEx1;
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridMain.Location = new System.Drawing.Point(0, 25);
            this.gridMain.Name = "gridMain";
            this.gridMain.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridMain.Size = new System.Drawing.Size(725, 264);
            this.gridMain.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridMain.Style.ProgressBarStyle.BackgroundStyle = Syncfusion.WinForms.DataGrid.Enums.GridProgressBarStyle.None;
            this.gridMain.Style.ProgressBarStyle.ForegroundStyle = Syncfusion.WinForms.DataGrid.Enums.GridProgressBarStyle.Tube;
            this.gridMain.Style.ProgressBarStyle.ProgressTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.gridMain.Style.ProgressBarStyle.TubeForegroundEndColor = System.Drawing.Color.White;
            this.gridMain.Style.ProgressBarStyle.TubeForegroundStartColor = System.Drawing.Color.SkyBlue;
            this.gridMain.TabIndex = 1;
            this.gridMain.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.ExecuteDoubleClickCommand);
            // 
            // contextMenuStripEx1
            // 
            this.contextMenuStripEx1.DropShadowEnabled = false;
            this.contextMenuStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCreate,
            this.menuItemEdit,
            this.menuItemDelete});
            this.contextMenuStripEx1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextMenuStripEx1.Name = "contextMenuStripEx1";
            this.contextMenuStripEx1.Size = new System.Drawing.Size(129, 70);
            this.contextMenuStripEx1.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextMenuStripEx1.ThemeName = "Metro";
            // 
            // menuItemCreate
            // 
            this.menuItemCreate.Image = global::DocumentFlow.Controls.Properties.Resources.icons8_file_add_16;
            this.menuItemCreate.Name = "menuItemCreate";
            this.menuItemCreate.Size = new System.Drawing.Size(128, 22);
            this.menuItemCreate.Text = "Создать";
            this.menuItemCreate.Click += new System.EventHandler(this.BttonCreate_Click);
            // 
            // menuItemEdit
            // 
            this.menuItemEdit.Image = global::DocumentFlow.Controls.Properties.Resources.icons8_file_edit_16;
            this.menuItemEdit.Name = "menuItemEdit";
            this.menuItemEdit.Size = new System.Drawing.Size(128, 22);
            this.menuItemEdit.Text = "Изменить";
            this.menuItemEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // menuItemDelete
            // 
            this.menuItemDelete.Image = global::DocumentFlow.Controls.Properties.Resources.icons8_file_delete_16;
            this.menuItemDelete.Name = "menuItemDelete";
            this.menuItemDelete.Size = new System.Drawing.Size(128, 22);
            this.menuItemDelete.Text = "Удалить";
            this.menuItemDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bttonCreate,
            this.buttonEdit,
            this.buttonDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(725, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // bttonCreate
            // 
            this.bttonCreate.Image = global::DocumentFlow.Controls.Properties.Resources.icons8_file_add_16;
            this.bttonCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttonCreate.Name = "bttonCreate";
            this.bttonCreate.Size = new System.Drawing.Size(70, 22);
            this.bttonCreate.Text = "Создать";
            this.bttonCreate.Click += new System.EventHandler(this.BttonCreate_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Image = global::DocumentFlow.Controls.Properties.Resources.icons8_file_edit_16;
            this.buttonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(81, 22);
            this.buttonEdit.Text = "Изменить";
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Image = global::DocumentFlow.Controls.Properties.Resources.icons8_file_delete_16;
            this.buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(71, 22);
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // L_DataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "L_DataGrid";
            this.Size = new System.Drawing.Size(725, 289);
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            this.contextMenuStripEx1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextMenuStripEx1;
        private System.Windows.Forms.ToolStripButton bttonCreate;
        private System.Windows.Forms.ToolStripButton buttonEdit;
        private System.Windows.Forms.ToolStripButton buttonDelete;
        private System.Windows.Forms.ToolStripMenuItem menuItemCreate;
        private System.Windows.Forms.ToolStripMenuItem menuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem menuItemDelete;
    }
}
