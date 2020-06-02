namespace DocumentFlow
{
    partial class ContentViewer
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
            this.components = new System.ComponentModel.Container();
            this.contextGridMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuVisibleColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.panelDirectory = new System.Windows.Forms.Panel();
            this.breadcrumb1 = new DocumentFlow.Controls.Breadcrumb();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxSearchDirectory = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panelDocument = new System.Windows.Forms.Panel();
            this.textBoxSearchDocument = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.buttonSelectRange = new DocumentFlow.Controls.ToolButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dateTimePickerTo = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolButton1 = new DocumentFlow.Controls.ToolButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboOrg = new Syncfusion.WinForms.ListView.SfComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridContent = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.panelCommandBar = new System.Windows.Forms.Panel();
            this.buttonHistory = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonCreate = new System.Windows.Forms.ToolStripButton();
            this.buttonEdit = new System.Windows.Forms.ToolStripButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonCopy = new System.Windows.Forms.ToolStripButton();
            this.buttonAddFolder = new System.Windows.Forms.ToolStripButton();
            this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.timerDatabaseListen = new System.Windows.Forms.Timer(this.components);
            this.contextGridMenu.SuspendLayout();
            this.panelDirectory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxSearchDirectory)).BeginInit();
            this.panelDocument.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxSearchDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerTo.Calendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerFrom.Calendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboOrg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridContent)).BeginInit();
            this.panelCommandBar.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextGridMenu
            // 
            this.contextGridMenu.DropShadowEnabled = false;
            this.contextGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCopyClipboard,
            this.toolStripSeparator6,
            this.menuCreate,
            this.menuEdit,
            this.menuDelete,
            this.toolStripSeparator4,
            this.menuCopy,
            this.menuAddFolder,
            this.toolStripSeparator1,
            this.menuVisibleColumns});
            this.contextGridMenu.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextGridMenu.Name = "contextGridMenu";
            this.contextGridMenu.Size = new System.Drawing.Size(182, 176);
            this.contextGridMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextGridMenu.ThemeName = "Metro";
            // 
            // menuCopyClipboard
            // 
            this.menuCopyClipboard.Image = global::DocumentFlow.Properties.Resources.icons8_copy_16;
            this.menuCopyClipboard.Name = "menuCopyClipboard";
            this.menuCopyClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuCopyClipboard.Size = new System.Drawing.Size(181, 22);
            this.menuCopyClipboard.Tag = "copy-text|edit-text";
            this.menuCopyClipboard.Text = "Копировать";
            this.menuCopyClipboard.Click += new System.EventHandler(this.menuCopyClipboard_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(178, 6);
            this.toolStripSeparator6.Tag = "|edit";
            // 
            // menuCreate
            // 
            this.menuCreate.Image = global::DocumentFlow.Properties.Resources.icons8_file_add_16;
            this.menuCreate.Name = "menuCreate";
            this.menuCreate.Size = new System.Drawing.Size(181, 22);
            this.menuCreate.Tag = "add-record|edit";
            this.menuCreate.Text = "Создать";
            this.menuCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.Image = global::DocumentFlow.Properties.Resources.icons8_file_edit_16;
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(181, 22);
            this.menuEdit.Tag = "edit-record|edit";
            this.menuEdit.Text = "Изменить";
            this.menuEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // menuDelete
            // 
            this.menuDelete.Image = global::DocumentFlow.Properties.Resources.icons8_file_delete_16;
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(181, 22);
            this.menuDelete.Tag = "delete-record|edit";
            this.menuDelete.Text = "Удалить";
            this.menuDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(178, 6);
            this.toolStripSeparator4.Tag = "|additional";
            // 
            // menuCopy
            // 
            this.menuCopy.Image = global::DocumentFlow.Properties.Resources.icons8_copy_edit_16;
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.Size = new System.Drawing.Size(181, 22);
            this.menuCopy.Tag = "copy-record|additional";
            this.menuCopy.Text = "Создать копию";
            this.menuCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // menuAddFolder
            // 
            this.menuAddFolder.Image = global::DocumentFlow.Properties.Resources.icons8_folder_add_16;
            this.menuAddFolder.Name = "menuAddFolder";
            this.menuAddFolder.Size = new System.Drawing.Size(181, 22);
            this.menuAddFolder.Tag = "create-group|additional";
            this.menuAddFolder.Text = "Группа";
            this.menuAddFolder.Click += new System.EventHandler(this.buttonAddFolder_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // menuVisibleColumns
            // 
            this.menuVisibleColumns.Image = global::DocumentFlow.Properties.Resources.icons8_columns_16;
            this.menuVisibleColumns.Name = "menuVisibleColumns";
            this.menuVisibleColumns.Size = new System.Drawing.Size(181, 22);
            this.menuVisibleColumns.Text = "Видимые столбцы";
            // 
            // panelDirectory
            // 
            this.panelDirectory.BackColor = System.Drawing.SystemColors.Window;
            this.panelDirectory.Controls.Add(this.breadcrumb1);
            this.panelDirectory.Controls.Add(this.panel3);
            this.panelDirectory.Controls.Add(this.textBoxSearchDirectory);
            this.panelDirectory.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDirectory.Location = new System.Drawing.Point(0, 0);
            this.panelDirectory.Name = "panelDirectory";
            this.panelDirectory.Padding = new System.Windows.Forms.Padding(3);
            this.panelDirectory.Size = new System.Drawing.Size(1103, 30);
            this.panelDirectory.TabIndex = 9;
            // 
            // breadcrumb1
            // 
            this.breadcrumb1.BackColor = System.Drawing.SystemColors.Window;
            this.breadcrumb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.breadcrumb1.Location = new System.Drawing.Point(3, 3);
            this.breadcrumb1.Name = "breadcrumb1";
            this.breadcrumb1.ShowButtonRefresh = false;
            this.breadcrumb1.Size = new System.Drawing.Size(912, 24);
            this.breadcrumb1.TabIndex = 0;
            this.breadcrumb1.CrumbClick += new System.EventHandler<DocumentFlow.Controls.CrumbClickEventArgs>(this.breadcrumb1_CrumbClick);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(915, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(6, 24);
            this.panel3.TabIndex = 17;
            // 
            // textBoxSearchDirectory
            // 
            this.textBoxSearchDirectory.BeforeTouchSize = new System.Drawing.Size(179, 24);
            this.textBoxSearchDirectory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.textBoxSearchDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSearchDirectory.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBoxSearchDirectory.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBoxSearchDirectory.FarImage = global::DocumentFlow.Properties.Resources.icons8_find_filled_16;
            this.textBoxSearchDirectory.Location = new System.Drawing.Point(921, 3);
            this.textBoxSearchDirectory.Multiline = true;
            this.textBoxSearchDirectory.Name = "textBoxSearchDirectory";
            this.textBoxSearchDirectory.Size = new System.Drawing.Size(179, 24);
            this.textBoxSearchDirectory.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.textBoxSearchDirectory.TabIndex = 16;
            this.textBoxSearchDirectory.ThemeName = "Metro";
            this.textBoxSearchDirectory.TextChanged += new System.EventHandler(this.SearchData);
            // 
            // panelDocument
            // 
            this.panelDocument.BackColor = System.Drawing.SystemColors.Window;
            this.panelDocument.Controls.Add(this.textBoxSearchDocument);
            this.panelDocument.Controls.Add(this.buttonSelectRange);
            this.panelDocument.Controls.Add(this.panel4);
            this.panelDocument.Controls.Add(this.dateTimePickerTo);
            this.panelDocument.Controls.Add(this.label3);
            this.panelDocument.Controls.Add(this.dateTimePickerFrom);
            this.panelDocument.Controls.Add(this.label2);
            this.panelDocument.Controls.Add(this.panel2);
            this.panelDocument.Controls.Add(this.toolButton1);
            this.panelDocument.Controls.Add(this.panel1);
            this.panelDocument.Controls.Add(this.comboOrg);
            this.panelDocument.Controls.Add(this.label1);
            this.panelDocument.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDocument.Location = new System.Drawing.Point(0, 30);
            this.panelDocument.Name = "panelDocument";
            this.panelDocument.Padding = new System.Windows.Forms.Padding(3);
            this.panelDocument.Size = new System.Drawing.Size(1103, 30);
            this.panelDocument.TabIndex = 10;
            // 
            // textBoxSearchDocument
            // 
            this.textBoxSearchDocument.BeforeTouchSize = new System.Drawing.Size(179, 24);
            this.textBoxSearchDocument.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.textBoxSearchDocument.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSearchDocument.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBoxSearchDocument.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBoxSearchDocument.FarImage = global::DocumentFlow.Properties.Resources.icons8_find_filled_16;
            this.textBoxSearchDocument.Location = new System.Drawing.Point(921, 3);
            this.textBoxSearchDocument.Multiline = true;
            this.textBoxSearchDocument.Name = "textBoxSearchDocument";
            this.textBoxSearchDocument.Size = new System.Drawing.Size(179, 24);
            this.textBoxSearchDocument.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.textBoxSearchDocument.TabIndex = 21;
            this.textBoxSearchDocument.ThemeName = "Metro";
            // 
            // buttonSelectRange
            // 
            this.buttonSelectRange.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.buttonSelectRange.BackColor = System.Drawing.Color.White;
            this.buttonSelectRange.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.buttonSelectRange.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.buttonSelectRange.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.buttonSelectRange.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.buttonSelectRange.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonSelectRange.Kind = DocumentFlow.Controls.ToolButtonKind.Select;
            this.buttonSelectRange.Location = new System.Drawing.Point(702, 3);
            this.buttonSelectRange.Name = "buttonSelectRange";
            this.buttonSelectRange.Size = new System.Drawing.Size(24, 24);
            this.buttonSelectRange.TabIndex = 20;
            this.buttonSelectRange.Text = "toolButton2";
            this.buttonSelectRange.Click += new System.EventHandler(this.buttonSelectRange_Click);
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(696, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(6, 24);
            this.panel4.TabIndex = 19;
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.dateTimePickerTo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimePickerTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dateTimePickerTo.Calendar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimePickerTo.Calendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimePickerTo.Calendar.Culture = new System.Globalization.CultureInfo("ru-RU");
            this.dateTimePickerTo.Calendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dateTimePickerTo.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None;
            this.dateTimePickerTo.Calendar.HeaderEndColor = System.Drawing.Color.White;
            this.dateTimePickerTo.Calendar.HeaderStartColor = System.Drawing.Color.White;
            this.dateTimePickerTo.Calendar.HighlightColor = System.Drawing.Color.Black;
            this.dateTimePickerTo.Calendar.Iso8601CalenderFormat = false;
            this.dateTimePickerTo.Calendar.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerTo.Calendar.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerTo.Calendar.Name = "monthCalendar";
            this.dateTimePickerTo.Calendar.ScrollButtonSize = new System.Drawing.Size(24, 24);
            this.dateTimePickerTo.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.dateTimePickerTo.Calendar.TabIndex = 0;
            this.dateTimePickerTo.Calendar.WeekFont = new System.Drawing.Font("Verdana", 8F);
            // 
            // 
            // 
            this.dateTimePickerTo.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimePickerTo.Calendar.NoneButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerTo.Calendar.NoneButton.ForeColor = System.Drawing.Color.White;
            this.dateTimePickerTo.Calendar.NoneButton.Location = new System.Drawing.Point(78, 0);
            this.dateTimePickerTo.Calendar.NoneButton.Size = new System.Drawing.Size(72, 20);
            this.dateTimePickerTo.Calendar.NoneButton.ThemeName = "Metro";
            this.dateTimePickerTo.Calendar.NoneButton.UseVisualStyle = true;
            // 
            // 
            // 
            this.dateTimePickerTo.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimePickerTo.Calendar.TodayButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerTo.Calendar.TodayButton.ForeColor = System.Drawing.Color.White;
            this.dateTimePickerTo.Calendar.TodayButton.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerTo.Calendar.TodayButton.Size = new System.Drawing.Size(78, 20);
            this.dateTimePickerTo.Calendar.TodayButton.ThemeName = "Metro";
            this.dateTimePickerTo.Calendar.TodayButton.UseVisualStyle = true;
            this.dateTimePickerTo.CalendarSize = new System.Drawing.Size(189, 176);
            this.dateTimePickerTo.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerTo.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateTimePickerTo.DropDownImage = null;
            this.dateTimePickerTo.DropDownNormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerTo.DropDownPressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerTo.DropDownSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(191)))), ((int)(((byte)(237)))));
            this.dateTimePickerTo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTo.Location = new System.Drawing.Point(576, 3);
            this.dateTimePickerTo.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerTo.MinValue = new System.DateTime(((long)(0)));
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(120, 24);
            this.dateTimePickerTo.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.dateTimePickerTo.TabIndex = 18;
            this.dateTimePickerTo.Value = new System.DateTime(2019, 6, 9, 16, 1, 37, 456);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(541, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 24);
            this.label3.TabIndex = 17;
            this.label3.Text = "по:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.dateTimePickerFrom.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimePickerFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dateTimePickerFrom.Calendar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimePickerFrom.Calendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimePickerFrom.Calendar.Culture = new System.Globalization.CultureInfo("ru-RU");
            this.dateTimePickerFrom.Calendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dateTimePickerFrom.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None;
            this.dateTimePickerFrom.Calendar.HeaderEndColor = System.Drawing.Color.White;
            this.dateTimePickerFrom.Calendar.HeaderStartColor = System.Drawing.Color.White;
            this.dateTimePickerFrom.Calendar.HighlightColor = System.Drawing.Color.Black;
            this.dateTimePickerFrom.Calendar.Iso8601CalenderFormat = false;
            this.dateTimePickerFrom.Calendar.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerFrom.Calendar.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerFrom.Calendar.Name = "monthCalendar";
            this.dateTimePickerFrom.Calendar.ScrollButtonSize = new System.Drawing.Size(24, 24);
            this.dateTimePickerFrom.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.dateTimePickerFrom.Calendar.TabIndex = 0;
            this.dateTimePickerFrom.Calendar.WeekFont = new System.Drawing.Font("Verdana", 8F);
            // 
            // 
            // 
            this.dateTimePickerFrom.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimePickerFrom.Calendar.NoneButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerFrom.Calendar.NoneButton.ForeColor = System.Drawing.Color.White;
            this.dateTimePickerFrom.Calendar.NoneButton.Location = new System.Drawing.Point(78, 0);
            this.dateTimePickerFrom.Calendar.NoneButton.Size = new System.Drawing.Size(72, 20);
            this.dateTimePickerFrom.Calendar.NoneButton.ThemeName = "Metro";
            this.dateTimePickerFrom.Calendar.NoneButton.UseVisualStyle = true;
            // 
            // 
            // 
            this.dateTimePickerFrom.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimePickerFrom.Calendar.TodayButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerFrom.Calendar.TodayButton.ForeColor = System.Drawing.Color.White;
            this.dateTimePickerFrom.Calendar.TodayButton.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerFrom.Calendar.TodayButton.Size = new System.Drawing.Size(78, 20);
            this.dateTimePickerFrom.Calendar.TodayButton.ThemeName = "Metro";
            this.dateTimePickerFrom.Calendar.TodayButton.UseVisualStyle = true;
            this.dateTimePickerFrom.CalendarSize = new System.Drawing.Size(189, 176);
            this.dateTimePickerFrom.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFrom.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateTimePickerFrom.DropDownImage = null;
            this.dateTimePickerFrom.DropDownNormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerFrom.DropDownPressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerFrom.DropDownSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(191)))), ((int)(((byte)(237)))));
            this.dateTimePickerFrom.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(421, 3);
            this.dateTimePickerFrom.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerFrom.MinValue = new System.DateTime(((long)(0)));
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(120, 24);
            this.dateTimePickerFrom.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.dateTimePickerFrom.TabIndex = 16;
            this.dateTimePickerFrom.Value = new System.DateTime(2019, 6, 9, 16, 1, 37, 456);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(355, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 24);
            this.label2.TabIndex = 15;
            this.label2.Text = "Период с:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(349, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(6, 24);
            this.panel2.TabIndex = 14;
            // 
            // toolButton1
            // 
            this.toolButton1.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.toolButton1.BackColor = System.Drawing.Color.White;
            this.toolButton1.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.toolButton1.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.toolButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.toolButton1.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.toolButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolButton1.Kind = DocumentFlow.Controls.ToolButtonKind.Delete;
            this.toolButton1.Location = new System.Drawing.Point(325, 3);
            this.toolButton1.Name = "toolButton1";
            this.toolButton1.Size = new System.Drawing.Size(24, 24);
            this.toolButton1.TabIndex = 13;
            this.toolButton1.Text = "toolButton1";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(319, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(6, 24);
            this.panel1.TabIndex = 12;
            // 
            // comboOrg
            // 
            this.comboOrg.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboOrg.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboOrg.Location = new System.Drawing.Point(92, 3);
            this.comboOrg.Name = "comboOrg";
            this.comboOrg.Size = new System.Drawing.Size(227, 24);
            this.comboOrg.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboOrg.TabIndex = 10;
            this.comboOrg.ThemeName = "Metro";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Организация";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridContent
            // 
            this.gridContent.AccessibleName = "Table";
            this.gridContent.AllowEditing = false;
            this.gridContent.AllowFiltering = true;
            this.gridContent.AllowResizingColumns = true;
            this.gridContent.AllowTriStateSorting = true;
            this.gridContent.AutoGenerateColumns = false;
            this.gridContent.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.LastColumnFill;
            this.gridContent.BackColor = System.Drawing.SystemColors.Window;
            this.gridContent.ContextMenuStrip = this.contextGridMenu;
            this.gridContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridContent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridContent.Location = new System.Drawing.Point(0, 112);
            this.gridContent.Name = "gridContent";
            this.gridContent.ShowRowHeader = true;
            this.gridContent.Size = new System.Drawing.Size(1103, 438);
            this.gridContent.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridContent.Style.GroupDropAreaStyle.BackColor = System.Drawing.SystemColors.Window;
            this.gridContent.Style.TableSummaryRowStyle.Font.Facename = "Segoe UI";
            this.gridContent.Style.TableSummaryRowStyle.Font.Size = 9F;
            this.gridContent.TabIndex = 12;
            this.gridContent.QueryCellStyle += new Syncfusion.WinForms.DataGrid.Events.QueryCellStyleEventHandler(this.gridContent_QueryCellStyle);
            this.gridContent.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.gridContent_CellDoubleClick);
            // 
            // panelCommandBar
            // 
            this.panelCommandBar.Controls.Add(this.panelDocument);
            this.panelCommandBar.Controls.Add(this.panelDirectory);
            this.panelCommandBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCommandBar.Location = new System.Drawing.Point(0, 52);
            this.panelCommandBar.Name = "panelCommandBar";
            this.panelCommandBar.Size = new System.Drawing.Size(1103, 60);
            this.panelCommandBar.TabIndex = 13;
            // 
            // buttonHistory
            // 
            this.buttonHistory.Image = global::DocumentFlow.Properties.Resources.icons8_elapsed_time_30;
            this.buttonHistory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonHistory.Name = "buttonHistory";
            this.buttonHistory.Size = new System.Drawing.Size(58, 49);
            this.buttonHistory.Tag = "history|history";
            this.buttonHistory.Text = "История";
            this.buttonHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonHistory.Click += new System.EventHandler(this.buttonHistory_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 52);
            this.toolStripSeparator2.Tag = "|edit";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Image = global::DocumentFlow.Properties.Resources.icons8_file_add_30;
            this.buttonCreate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(54, 49);
            this.buttonCreate.Tag = "add-record|edit";
            this.buttonCreate.Text = "Создать";
            this.buttonCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Image = global::DocumentFlow.Properties.Resources.icons8_file_edit_30;
            this.buttonEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(65, 49);
            this.buttonEdit.Tag = "edit-record|edit";
            this.buttonEdit.Text = "Изменить";
            this.buttonEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Image = global::DocumentFlow.Properties.Resources.icons8_file_delete_30;
            this.buttonDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(55, 49);
            this.buttonDelete.Tag = "delete-record|edit";
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 52);
            this.toolStripSeparator3.Tag = "|additional";
            // 
            // buttonCopy
            // 
            this.buttonCopy.Image = global::DocumentFlow.Properties.Resources.icons8_copy_edit_30;
            this.buttonCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(45, 49);
            this.buttonCopy.Tag = "copy-record|additional";
            this.buttonCopy.Text = "Копия";
            this.buttonCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonCopy.ToolTipText = "Создать копированием";
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonAddFolder
            // 
            this.buttonAddFolder.Image = global::DocumentFlow.Properties.Resources.icons8_folder_add_30;
            this.buttonAddFolder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAddFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAddFolder.Name = "buttonAddFolder";
            this.buttonAddFolder.Size = new System.Drawing.Size(50, 49);
            this.buttonAddFolder.Tag = "create-group|additional";
            this.buttonAddFolder.Text = "Группа";
            this.buttonAddFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonAddFolder.Click += new System.EventHandler(this.buttonAddFolder_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonRefresh.Image = global::DocumentFlow.Properties.Resources.icons8_restart_30;
            this.buttonRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(65, 49);
            this.buttonRefresh.Tag = "refresh|refresh";
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRefresh.Click += new System.EventHandler(this.RefreshEntities);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonHistory,
            this.toolStripSeparator2,
            this.buttonCreate,
            this.buttonEdit,
            this.buttonDelete,
            this.toolStripSeparator3,
            this.buttonCopy,
            this.buttonAddFolder,
            this.buttonRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(1103, 52);
            this.toolStrip1.TabIndex = 7;
            // 
            // timerDatabaseListen
            // 
            this.timerDatabaseListen.Tick += new System.EventHandler(this.timerDatabaseListen_Tick);
            // 
            // ContentViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridContent);
            this.Controls.Add(this.panelCommandBar);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ContentViewer";
            this.Size = new System.Drawing.Size(1103, 550);
            this.contextGridMenu.ResumeLayout(false);
            this.panelDirectory.ResumeLayout(false);
            this.panelDirectory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxSearchDirectory)).EndInit();
            this.panelDocument.ResumeLayout(false);
            this.panelDocument.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxSearchDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerTo.Calendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerFrom.Calendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboOrg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridContent)).EndInit();
            this.panelCommandBar.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextGridMenu;
        private System.Windows.Forms.ToolStripMenuItem menuCopyClipboard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem menuCreate;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuCopy;
        private System.Windows.Forms.ToolStripMenuItem menuAddFolder;
        private System.Windows.Forms.Panel panelDirectory;
        private Controls.Breadcrumb breadcrumb1;
        private System.Windows.Forms.Panel panel3;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxSearchDirectory;
        private System.Windows.Forms.Panel panelDocument;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuVisibleColumns;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridContent;
        private System.Windows.Forms.Panel panelCommandBar;
        private System.Windows.Forms.ToolStripButton buttonHistory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton buttonCreate;
        private System.Windows.Forms.ToolStripButton buttonEdit;
        private System.Windows.Forms.ToolStripButton buttonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton buttonCopy;
        private System.Windows.Forms.ToolStripButton buttonAddFolder;
        private System.Windows.Forms.ToolStripButton buttonRefresh;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Timer timerDatabaseListen;
        private System.Windows.Forms.Label label1;
        private Controls.ToolButton buttonSelectRange;
        private System.Windows.Forms.Panel panel4;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dateTimePickerTo;
        private System.Windows.Forms.Label label3;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dateTimePickerFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private Controls.ToolButton toolButton1;
        private System.Windows.Forms.Panel panel1;
        private Syncfusion.WinForms.ListView.SfComboBox comboOrg;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxSearchDocument;
    }
}
