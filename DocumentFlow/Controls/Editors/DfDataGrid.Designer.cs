
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
            toolStrip1 = new ToolStrip();
            buttonCreate = new ToolStripButton();
            buttonEdit = new ToolStripButton();
            buttonDelete = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            buttonCopy = new ToolStripButton();
            toolStripSeparatorCustom1 = new ToolStripSeparator();
            buttonRefresh = new ToolStripButton();
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
            labelHeader = new Label();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridMain).BeginInit();
            contextMenuStripEx1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { buttonCreate, buttonEdit, buttonDelete, toolStripSeparator1, buttonCopy, toolStripSeparatorCustom1, buttonRefresh });
            toolStrip1.Location = new Point(0, 32);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // buttonCreate
            // 
            buttonCreate.Image = Properties.Resources.icons8_file_add_16;
            buttonCreate.ImageTransparentColor = Color.Magenta;
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(70, 22);
            buttonCreate.Text = "Создать";
            buttonCreate.Click += ButtonCreate_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Image = Properties.Resources.icons8_file_edit_16;
            buttonEdit.ImageTransparentColor = Color.Magenta;
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(81, 22);
            buttonEdit.Text = "Изменить";
            buttonEdit.Click += ButtonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Image = Properties.Resources.icons8_file_delete_16;
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
            buttonCopy.Image = Properties.Resources.icons8_copy_edit_16;
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
            // buttonRefresh
            // 
            buttonRefresh.Alignment = ToolStripItemAlignment.Right;
            buttonRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonRefresh.Image = Properties.Resources.icons8_refresh_16;
            buttonRefresh.ImageTransparentColor = Color.Magenta;
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(23, 22);
            buttonRefresh.Text = "Обновить";
            buttonRefresh.Click += ButtonRefresh_Click;
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
            gridMain.Location = new Point(0, 57);
            gridMain.Name = "gridMain";
            gridMain.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            gridMain.Size = new Size(800, 135);
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
            contextMenuStripEx1.Size = new Size(182, 132);
            contextMenuStripEx1.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            contextMenuStripEx1.ThemeName = "Metro";
            // 
            // menuCopyToClipboard
            // 
            menuCopyToClipboard.Image = Properties.Resources.icons8_copy_16;
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
            menuCreate.Image = Properties.Resources.icons8_file_add_16;
            menuCreate.Name = "menuCreate";
            menuCreate.Size = new Size(181, 22);
            menuCreate.Text = "Создать";
            menuCreate.Click += ButtonCreate_Click;
            // 
            // menuEdit
            // 
            menuEdit.Image = Properties.Resources.icons8_file_edit_16;
            menuEdit.Name = "menuEdit";
            menuEdit.Size = new Size(181, 22);
            menuEdit.Text = "Изменить";
            menuEdit.Click += ButtonEdit_Click;
            // 
            // menuDelete
            // 
            menuDelete.Image = Properties.Resources.icons8_file_delete_16;
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
            menuCopy.Image = Properties.Resources.icons8_copy_edit_16;
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
            // labelHeader
            // 
            labelHeader.Dock = DockStyle.Top;
            labelHeader.Location = new Point(0, 0);
            labelHeader.Name = "labelHeader";
            labelHeader.Size = new Size(800, 32);
            labelHeader.TabIndex = 2;
            labelHeader.Text = "Заголовок";
            labelHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // DfDataGrid
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridMain);
            Controls.Add(toolStrip1);
            Controls.Add(labelHeader);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "DfDataGrid";
            Padding = new Padding(0, 0, 0, 8);
            Size = new Size(800, 200);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridMain).EndInit();
            contextMenuStripEx1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private ToolStripButton buttonRefresh;
        private Label labelHeader;
    }
}
