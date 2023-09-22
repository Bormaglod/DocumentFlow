
namespace DocumentFlow.Controls.Editors
{
    partial class DfDataGrid
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
            toolStrip1 = new ToolStrip();
            buttonCreate = new ToolStripButton();
            buttonEdit = new ToolStripButton();
            buttonDelete = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            buttonCopy = new ToolStripButton();
            toolStripSeparatorCustom1 = new ToolStripSeparator();
            gridMain = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            contextMenuStripEx1 = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            menuCopyToClipboard = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            menuCreate = new ToolStripMenuItem();
            menuEdit = new ToolStripMenuItem();
            menuDelete = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            menuCopy = new ToolStripMenuItem();
            toolStripSeparatorCustom2 = new ToolStripSeparator();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridMain).BeginInit();
            contextMenuStripEx1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { buttonCreate, buttonEdit, buttonDelete, toolStripSeparator1, buttonCopy, toolStripSeparatorCustom1 });
            toolStrip1.Location = new Point(0, 25);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // buttonCreate
            // 
            buttonCreate.Image = DocumentFlow.Properties.Resources.icons8_file_add_16;
            buttonCreate.ImageTransparentColor = Color.Magenta;
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(70, 22);
            buttonCreate.Text = "Создать";
            buttonCreate.Click += ButtonCreate_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Image = DocumentFlow.Properties.Resources.icons8_file_edit_16;
            buttonEdit.ImageTransparentColor = Color.Magenta;
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(81, 22);
            buttonEdit.Text = "Изменить";
            buttonEdit.Click += ButtonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Image = DocumentFlow.Properties.Resources.icons8_file_delete_16;
            buttonDelete.ImageTransparentColor = Color.Magenta;
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(71, 22);
            buttonDelete.Text = "Удалить";
            buttonDelete.Click += ButtonDelete_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // buttonCopy
            // 
            buttonCopy.Image = DocumentFlow.Properties.Resources.icons8_copy_edit_16;
            buttonCopy.ImageTransparentColor = Color.Magenta;
            buttonCopy.Name = "buttonCopy";
            buttonCopy.Size = new Size(61, 22);
            buttonCopy.Text = "Копия";
            buttonCopy.ToolTipText = "Создать копированием";
            buttonCopy.Click += ButtonCopy_Click;
            // 
            // toolStripSeparatorCustom1
            // 
            toolStripSeparatorCustom1.Name = "toolStripSeparatorCustom1";
            toolStripSeparatorCustom1.Size = new Size(6, 25);
            toolStripSeparatorCustom1.Visible = false;
            // 
            // gridMain
            // 
            gridMain.AccessibleName = "Table";
            gridMain.AllowEditing = false;
            gridMain.AllowGrouping = false;
            gridMain.AllowResizingColumns = true;
            gridMain.AllowTriStateSorting = true;
            gridMain.ContextMenuStrip = contextMenuStripEx1;
            gridMain.Dock = DockStyle.Fill;
            gridMain.LiveDataUpdateMode = Syncfusion.Data.LiveDataUpdateMode.AllowSummaryUpdate;
            gridMain.Location = new Point(0, 50);
            gridMain.Name = "gridMain";
            gridMain.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            gridMain.Size = new Size(800, 143);
            gridMain.Style.BorderColor = Color.FromArgb(204, 204, 204);
            gridMain.Style.CheckBoxStyle.CheckedBackColor = Color.FromArgb(0, 120, 215);
            gridMain.Style.CheckBoxStyle.CheckedBorderColor = Color.FromArgb(0, 120, 215);
            gridMain.Style.CheckBoxStyle.IndeterminateBorderColor = Color.FromArgb(0, 120, 215);
            gridMain.Style.HyperlinkStyle.DefaultLinkColor = Color.FromArgb(0, 120, 215);
            gridMain.Style.ProgressBarStyle.ForegroundStyle = Syncfusion.WinForms.DataGrid.Enums.GridProgressBarStyle.Tube;
            gridMain.Style.ProgressBarStyle.ProgressTextColor = Color.FromArgb(68, 68, 68);
            gridMain.Style.ProgressBarStyle.TubeForegroundEndColor = Color.White;
            gridMain.Style.ProgressBarStyle.TubeForegroundStartColor = Color.SkyBlue;
            gridMain.TabIndex = 1;
            gridMain.Text = "sfDataGrid1";
            gridMain.AutoGeneratingColumn += GridMain_AutoGeneratingColumn;
            gridMain.CellDoubleClick += GridMain_CellDoubleClick;
            // 
            // contextMenuStripEx1
            // 
            contextMenuStripEx1.DropShadowEnabled = false;
            contextMenuStripEx1.Items.AddRange(new ToolStripItem[] { menuCopyToClipboard, toolStripSeparator2, menuCreate, menuEdit, menuDelete, toolStripSeparator3, menuCopy, toolStripSeparatorCustom2 });
            contextMenuStripEx1.MetroColor = Color.FromArgb(204, 236, 249);
            contextMenuStripEx1.Name = "contextMenuStripEx1";
            contextMenuStripEx1.Size = new Size(182, 154);
            contextMenuStripEx1.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            contextMenuStripEx1.ThemeName = "Metro";
            // 
            // menuCopyToClipboard
            // 
            menuCopyToClipboard.Image = DocumentFlow.Properties.Resources.icons8_copy_16;
            menuCopyToClipboard.Name = "menuCopyToClipboard";
            menuCopyToClipboard.ShortcutKeys = Keys.Control | Keys.C;
            menuCopyToClipboard.Size = new Size(181, 22);
            menuCopyToClipboard.Text = "Копировать";
            menuCopyToClipboard.Click += MenuCopyToClipboard_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(178, 6);
            // 
            // menuCreate
            // 
            menuCreate.Image = DocumentFlow.Properties.Resources.icons8_file_add_16;
            menuCreate.Name = "menuCreate";
            menuCreate.Size = new Size(181, 22);
            menuCreate.Text = "Создать";
            menuCreate.Click += ButtonCreate_Click;
            // 
            // menuEdit
            // 
            menuEdit.Image = DocumentFlow.Properties.Resources.icons8_file_edit_16;
            menuEdit.Name = "menuEdit";
            menuEdit.Size = new Size(181, 22);
            menuEdit.Text = "Изменить";
            menuEdit.Click += ButtonEdit_Click;
            // 
            // menuDelete
            // 
            menuDelete.Image = DocumentFlow.Properties.Resources.icons8_file_delete_16;
            menuDelete.Name = "menuDelete";
            menuDelete.Size = new Size(181, 22);
            menuDelete.Text = "Удалить";
            menuDelete.Click += ButtonDelete_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(178, 6);
            // 
            // menuCopy
            // 
            menuCopy.Image = DocumentFlow.Properties.Resources.icons8_copy_edit_16;
            menuCopy.Name = "menuCopy";
            menuCopy.Size = new Size(181, 22);
            menuCopy.Text = "Создать копию";
            menuCopy.Click += ButtonCopy_Click;
            // 
            // toolStripSeparatorCustom2
            // 
            toolStripSeparatorCustom2.Name = "toolStripSeparatorCustom2";
            toolStripSeparatorCustom2.Size = new Size(178, 6);
            toolStripSeparatorCustom2.Visible = false;
            // 
            // DfDataGrid
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridMain);
            Controls.Add(toolStrip1);
            HeaderDock = DockStyle.Top;
            Name = "DfDataGrid";
            Size = new Size(800, 200);
            Controls.SetChildIndex(toolStrip1, 0);
            Controls.SetChildIndex(gridMain, 0);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridMain).EndInit();
            contextMenuStripEx1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton buttonCreate;
        private ToolStripButton buttonEdit;
        private ToolStripButton buttonDelete;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton buttonCopy;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridMain;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextMenuStripEx1;
        private ToolStripMenuItem menuCopyToClipboard;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuCreate;
        private ToolStripMenuItem menuEdit;
        private ToolStripMenuItem menuDelete;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem menuCopy;
        private ToolStripSeparator toolStripSeparatorCustom1;
        private ToolStripSeparator toolStripSeparatorCustom2;
    }
}
