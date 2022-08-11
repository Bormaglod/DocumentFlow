
namespace DocumentFlow.Controls.Editors
{
    partial class DfDataGrid<T>
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
            this.buttonCreate = new System.Windows.Forms.ToolStripButton();
            this.buttonEdit = new System.Windows.Forms.ToolStripButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonCopy = new System.Windows.Forms.ToolStripButton();
            this.gridMain = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.contextMenuStripEx1 = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuCopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorCustom1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparatorCustom2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            this.contextMenuStripEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonCreate,
            this.buttonEdit,
            this.buttonDelete,
            this.toolStripSeparator1,
            this.buttonCopy,
            this.toolStripSeparatorCustom1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Image = global::DocumentFlow.Properties.Resources.icons8_file_add_16;
            this.buttonCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(70, 22);
            this.buttonCreate.Text = "Создать";
            this.buttonCreate.Click += new System.EventHandler(this.ButtonCreate_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Image = global::DocumentFlow.Properties.Resources.icons8_file_edit_16;
            this.buttonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(81, 22);
            this.buttonEdit.Text = "Изменить";
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Image = global::DocumentFlow.Properties.Resources.icons8_file_delete_16;
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
            // buttonCopy
            // 
            this.buttonCopy.Image = global::DocumentFlow.Properties.Resources.icons8_copy_edit_16;
            this.buttonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(61, 22);
            this.buttonCopy.Text = "Копия";
            this.buttonCopy.ToolTipText = "Создать копированием";
            this.buttonCopy.Click += new System.EventHandler(this.ButtonCopy_Click);
            // 
            // gridMain
            // 
            this.gridMain.AccessibleName = "Table";
            this.gridMain.AllowEditing = false;
            this.gridMain.AllowGrouping = false;
            this.gridMain.AllowResizingColumns = true;
            this.gridMain.AllowTriStateSorting = true;
            this.gridMain.ContextMenuStrip = this.contextMenuStripEx1;
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.LiveDataUpdateMode = Syncfusion.Data.LiveDataUpdateMode.AllowSummaryUpdate;
            this.gridMain.Location = new System.Drawing.Point(0, 25);
            this.gridMain.Name = "gridMain";
            this.gridMain.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridMain.Size = new System.Drawing.Size(800, 175);
            this.gridMain.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridMain.Style.CheckBoxStyle.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridMain.Style.CheckBoxStyle.CheckedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridMain.Style.CheckBoxStyle.IndeterminateBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridMain.Style.HyperlinkStyle.DefaultLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridMain.Style.ProgressBarStyle.ForegroundStyle = Syncfusion.WinForms.DataGrid.Enums.GridProgressBarStyle.Tube;
            this.gridMain.Style.ProgressBarStyle.ProgressTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.gridMain.Style.ProgressBarStyle.TubeForegroundEndColor = System.Drawing.Color.White;
            this.gridMain.Style.ProgressBarStyle.TubeForegroundStartColor = System.Drawing.Color.SkyBlue;
            this.gridMain.TabIndex = 1;
            this.gridMain.Text = "sfDataGrid1";
            this.gridMain.AutoGeneratingColumn += new Syncfusion.WinForms.DataGrid.Events.AutoGeneratingColumnEventHandler(this.GridMain_AutoGeneratingColumn);
            this.gridMain.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.GridMain_CellDoubleClick);
            // 
            // contextMenuStripEx1
            // 
            this.contextMenuStripEx1.DropShadowEnabled = false;
            this.contextMenuStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCopyToClipboard,
            this.toolStripSeparator2,
            this.menuCreate,
            this.menuEdit,
            this.menuDelete,
            this.toolStripSeparator3,
            this.menuCopy,
            this.toolStripSeparatorCustom2});
            this.contextMenuStripEx1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextMenuStripEx1.Name = "contextMenuStripEx1";
            this.contextMenuStripEx1.Size = new System.Drawing.Size(182, 132);
            this.contextMenuStripEx1.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextMenuStripEx1.ThemeName = "Metro";
            // 
            // menuCopyToClipboard
            // 
            this.menuCopyToClipboard.Image = global::DocumentFlow.Properties.Resources.icons8_copy_16;
            this.menuCopyToClipboard.Name = "menuCopyToClipboard";
            this.menuCopyToClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuCopyToClipboard.Size = new System.Drawing.Size(181, 22);
            this.menuCopyToClipboard.Text = "Копировать";
            this.menuCopyToClipboard.Click += new System.EventHandler(this.MenuCopyToClipboard_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(178, 6);
            // 
            // menuCreate
            // 
            this.menuCreate.Image = global::DocumentFlow.Properties.Resources.icons8_file_add_16;
            this.menuCreate.Name = "menuCreate";
            this.menuCreate.Size = new System.Drawing.Size(181, 22);
            this.menuCreate.Text = "Создать";
            this.menuCreate.Click += new System.EventHandler(this.ButtonCreate_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.Image = global::DocumentFlow.Properties.Resources.icons8_file_edit_16;
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(181, 22);
            this.menuEdit.Text = "Изменить";
            this.menuEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // menuDelete
            // 
            this.menuDelete.Image = global::DocumentFlow.Properties.Resources.icons8_file_delete_16;
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(181, 22);
            this.menuDelete.Text = "Удалить";
            this.menuDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(178, 6);
            // 
            // menuCopy
            // 
            this.menuCopy.Image = global::DocumentFlow.Properties.Resources.icons8_copy_edit_16;
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.Size = new System.Drawing.Size(181, 22);
            this.menuCopy.Text = "Создать копию";
            this.menuCopy.Click += new System.EventHandler(this.ButtonCopy_Click);
            // 
            // toolStripSeparatorCustom1
            // 
            this.toolStripSeparatorCustom1.Name = "toolStripSeparatorCustom1";
            this.toolStripSeparatorCustom1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparatorCustom1.Visible = false;
            // 
            // toolStripSeparatorCustom2
            // 
            this.toolStripSeparatorCustom2.Name = "toolStripSeparatorCustom2";
            this.toolStripSeparatorCustom2.Size = new System.Drawing.Size(178, 6);
            this.toolStripSeparatorCustom2.Visible = false;
            // 
            // DfDataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridMain);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "DfDataGrid";
            this.Size = new System.Drawing.Size(800, 200);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            this.contextMenuStripEx1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonCreate;
        private System.Windows.Forms.ToolStripButton buttonEdit;
        private System.Windows.Forms.ToolStripButton buttonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonCopy;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridMain;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextMenuStripEx1;
        private System.Windows.Forms.ToolStripMenuItem menuCopyToClipboard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuCreate;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuCopy;
        private ToolStripSeparator toolStripSeparatorCustom1;
        private ToolStripSeparator toolStripSeparatorCustom2;
    }
}
