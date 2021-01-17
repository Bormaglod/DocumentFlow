namespace DocumentFlow
{
    partial class ContentEditor
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
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn1 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn2 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.buttonStatus = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.panelItemActions = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.buttonSaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
            this.buttonCustomization = new System.Windows.Forms.ToolStripButton();
            this.buttonPrint = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparatorUserCommands = new System.Windows.Forms.ToolStripSeparator();
            this.tabSplitterContainer1 = new Syncfusion.Windows.Forms.Tools.TabSplitterContainer();
            this.tabSplitterMaster = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.tabSplitterDocs = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.gridDocuments = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.contextMenuStripEx1 = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuCreateDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuOpenFolderDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenFileDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDocuments = new System.Windows.Forms.ToolStrip();
            this.buttonCreate = new System.Windows.Forms.ToolStripButton();
            this.buttonEdit = new System.Windows.Forms.ToolStripButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonOpenFolder = new System.Windows.Forms.ToolStripButton();
            this.buttonOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonScan = new System.Windows.Forms.ToolStripSplitButton();
            this.buttonSingleScan = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonMultipleScan = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSplitterInfo = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxUpdater = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCreator = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxID = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.dateTimeCreate = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimeUpdate = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.label1 = new System.Windows.Forms.Label();
            this.timerDatabaseListen = new System.Windows.Forms.Timer(this.components);
            this.twain32 = new Saraff.Twain.Twain32(this.components);
            this.toolStripMain.SuspendLayout();
            this.tabSplitterContainer1.SuspendLayout();
            this.tabSplitterDocs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments)).BeginInit();
            this.contextMenuStripEx1.SuspendLayout();
            this.toolStripDocuments.SuspendLayout();
            this.tabSplitterInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxUpdater)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCreator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeCreate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeCreate.Calendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeUpdate.Calendar)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonStatus,
            this.toolStripSeparator4,
            this.panelItemActions,
            this.toolStripSeparator5,
            this.buttonSave,
            this.buttonSaveAndClose,
            this.toolStripSeparator6,
            this.buttonRefresh,
            this.buttonCustomization,
            this.buttonPrint,
            this.toolStripSeparatorUserCommands});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1118, 52);
            this.toolStripMain.TabIndex = 6;
            this.toolStripMain.Text = "toolStripMain";
            // 
            // buttonStatus
            // 
            this.buttonStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonStatus.Name = "buttonStatus";
            this.buttonStatus.Size = new System.Drawing.Size(70, 49);
            this.buttonStatus.Tag = "history";
            this.buttonStatus.Text = "Состояние";
            this.buttonStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonStatus.Click += new System.EventHandler(this.buttonStatus_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 52);
            // 
            // panelItemActions
            // 
            this.panelItemActions.CausesValidation = false;
            this.panelItemActions.ForeColor = System.Drawing.Color.MidnightBlue;
            this.panelItemActions.Name = "panelItemActions";
            this.panelItemActions.Size = new System.Drawing.Size(23, 52);
            this.panelItemActions.Text = "toolStripPanelItem1";
            this.panelItemActions.Transparent = true;
            this.panelItemActions.UseStandardLayout = true;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 52);
            // 
            // buttonSave
            // 
            this.buttonSave.Image = global::DocumentFlow.Properties.Resources.icons8_save_30;
            this.buttonSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 49);
            this.buttonSave.Tag = "save";
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
            this.buttonSaveAndClose.Tag = "save-as";
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 52);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonRefresh.Image = global::DocumentFlow.Properties.Resources.icons8_restart_30;
            this.buttonRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(65, 49);
            this.buttonRefresh.Tag = "refresh";
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonCustomization
            // 
            this.buttonCustomization.Image = global::DocumentFlow.Properties.Resources.icons8_source_30;
            this.buttonCustomization.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonCustomization.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCustomization.Name = "buttonCustomization";
            this.buttonCustomization.Size = new System.Drawing.Size(70, 49);
            this.buttonCustomization.Tag = "open-browser-code";
            this.buttonCustomization.Text = "Настройка";
            this.buttonCustomization.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonCustomization.Click += new System.EventHandler(this.buttonCustomization_Click);
            // 
            // buttonPrint
            // 
            this.buttonPrint.Image = global::DocumentFlow.Properties.Resources.icons8_print_30;
            this.buttonPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(62, 49);
            this.buttonPrint.Tag = "print";
            this.buttonPrint.Text = "Печать";
            this.buttonPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonPrint.ButtonClick += new System.EventHandler(this.buttonPrint_ButtonClick);
            // 
            // toolStripSeparatorUserCommands
            // 
            this.toolStripSeparatorUserCommands.Name = "toolStripSeparatorUserCommands";
            this.toolStripSeparatorUserCommands.Size = new System.Drawing.Size(6, 52);
            this.toolStripSeparatorUserCommands.Tag = "|user-defined";
            // 
            // tabSplitterContainer1
            // 
            this.tabSplitterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSplitterContainer1.Location = new System.Drawing.Point(0, 52);
            this.tabSplitterContainer1.Name = "tabSplitterContainer1";
            this.tabSplitterContainer1.PrimaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.tabSplitterMaster});
            this.tabSplitterContainer1.SecondaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.tabSplitterDocs,
            this.tabSplitterInfo});
            this.tabSplitterContainer1.Size = new System.Drawing.Size(1118, 424);
            this.tabSplitterContainer1.SplitterBackColor = System.Drawing.SystemColors.Control;
            this.tabSplitterContainer1.SplitterPosition = 277;
            this.tabSplitterContainer1.TabIndex = 7;
            this.tabSplitterContainer1.Text = "tabSplitterContainer1";
            // 
            // tabSplitterMaster
            // 
            this.tabSplitterMaster.AutoScroll = true;
            this.tabSplitterMaster.BackColor = System.Drawing.SystemColors.Window;
            this.tabSplitterMaster.Hide = false;
            this.tabSplitterMaster.Image = global::DocumentFlow.Properties.Resources.icons8_edit_prop_11;
            this.tabSplitterMaster.Location = new System.Drawing.Point(0, 0);
            this.tabSplitterMaster.Name = "tabSplitterMaster";
            this.tabSplitterMaster.Padding = new System.Windows.Forms.Padding(15);
            this.tabSplitterMaster.Size = new System.Drawing.Size(1118, 277);
            this.tabSplitterMaster.TabIndex = 1;
            this.tabSplitterMaster.Text = "Данные";
            this.tabSplitterMaster.VisibleChanged += new System.EventHandler(this.tabSplitterMaster_VisibleChanged);
            // 
            // tabSplitterDocs
            // 
            this.tabSplitterDocs.AutoScroll = true;
            this.tabSplitterDocs.Controls.Add(this.gridDocuments);
            this.tabSplitterDocs.Controls.Add(this.toolStripDocuments);
            this.tabSplitterDocs.Hide = false;
            this.tabSplitterDocs.Image = global::DocumentFlow.Properties.Resources.icons8_documents_11;
            this.tabSplitterDocs.Location = new System.Drawing.Point(0, 297);
            this.tabSplitterDocs.Name = "tabSplitterDocs";
            this.tabSplitterDocs.Size = new System.Drawing.Size(1118, 127);
            this.tabSplitterDocs.TabIndex = 2;
            this.tabSplitterDocs.Tag = "system";
            this.tabSplitterDocs.Text = "Документы";
            // 
            // gridDocuments
            // 
            this.gridDocuments.AccessibleName = "Table";
            this.gridDocuments.AllowEditing = false;
            this.gridDocuments.AllowGrouping = false;
            this.gridDocuments.AllowResizingColumns = true;
            this.gridDocuments.AllowTriStateSorting = true;
            this.gridDocuments.AutoGenerateColumns = false;
            this.gridDocuments.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.LastColumnFill;
            this.gridDocuments.BackColor = System.Drawing.SystemColors.Window;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowGrouping = false;
            gridTextColumn1.AllowResizing = true;
            gridTextColumn1.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
            gridTextColumn1.HeaderText = "Описание";
            gridTextColumn1.MappingName = "note";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowGrouping = false;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.HeaderText = "Имя файла";
            gridTextColumn2.MappingName = "file_name";
            gridTextColumn2.Width = 300D;
            this.gridDocuments.Columns.Add(gridTextColumn1);
            this.gridDocuments.Columns.Add(gridTextColumn2);
            this.gridDocuments.ContextMenuStrip = this.contextMenuStripEx1;
            this.gridDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDocuments.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridDocuments.Location = new System.Drawing.Point(0, 25);
            this.gridDocuments.Name = "gridDocuments";
            this.gridDocuments.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridDocuments.Size = new System.Drawing.Size(1118, 102);
            this.gridDocuments.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridDocuments.Style.GroupDropAreaStyle.BackColor = System.Drawing.SystemColors.Window;
            this.gridDocuments.Style.TableSummaryRowStyle.Font.Facename = "Segoe UI";
            this.gridDocuments.Style.TableSummaryRowStyle.Font.Size = 9F;
            this.gridDocuments.TabIndex = 3;
            this.gridDocuments.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.gridDocuments_CellDoubleClick);
            // 
            // contextMenuStripEx1
            // 
            this.contextMenuStripEx1.DropShadowEnabled = false;
            this.contextMenuStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCreateDocument,
            this.menuEditDocument,
            this.menuDeleteDocument,
            this.toolStripSeparator2,
            this.menuOpenFolderDocument,
            this.menuOpenFileDocument});
            this.contextMenuStripEx1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextMenuStripEx1.Name = "contextMenuStripEx1";
            this.contextMenuStripEx1.Size = new System.Drawing.Size(157, 120);
            this.contextMenuStripEx1.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextMenuStripEx1.ThemeName = "Metro";
            // 
            // menuCreateDocument
            // 
            this.menuCreateDocument.Image = global::DocumentFlow.Properties.Resources.icons8_file_add_16;
            this.menuCreateDocument.Name = "menuCreateDocument";
            this.menuCreateDocument.Size = new System.Drawing.Size(156, 22);
            this.menuCreateDocument.Text = "Создать";
            this.menuCreateDocument.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // menuEditDocument
            // 
            this.menuEditDocument.Image = global::DocumentFlow.Properties.Resources.icons8_file_edit_16;
            this.menuEditDocument.Name = "menuEditDocument";
            this.menuEditDocument.Size = new System.Drawing.Size(156, 22);
            this.menuEditDocument.Text = "Изменить";
            this.menuEditDocument.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // menuDeleteDocument
            // 
            this.menuDeleteDocument.Image = global::DocumentFlow.Properties.Resources.icons8_file_delete_16;
            this.menuDeleteDocument.Name = "menuDeleteDocument";
            this.menuDeleteDocument.Size = new System.Drawing.Size(156, 22);
            this.menuDeleteDocument.Text = "Удалить";
            this.menuDeleteDocument.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(153, 6);
            // 
            // menuOpenFolderDocument
            // 
            this.menuOpenFolderDocument.Image = global::DocumentFlow.Properties.Resources.icons8_open_folder_16;
            this.menuOpenFolderDocument.Name = "menuOpenFolderDocument";
            this.menuOpenFolderDocument.Size = new System.Drawing.Size(156, 22);
            this.menuOpenFolderDocument.Text = "Открыть папку";
            this.menuOpenFolderDocument.Click += new System.EventHandler(this.buttonOpenFolder_Click);
            // 
            // menuOpenFileDocument
            // 
            this.menuOpenFileDocument.Image = global::DocumentFlow.Properties.Resources.icons8_open_document_16;
            this.menuOpenFileDocument.Name = "menuOpenFileDocument";
            this.menuOpenFileDocument.Size = new System.Drawing.Size(156, 22);
            this.menuOpenFileDocument.Text = "Открыть файл";
            this.menuOpenFileDocument.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // toolStripDocuments
            // 
            this.toolStripDocuments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonCreate,
            this.buttonEdit,
            this.buttonDelete,
            this.toolStripSeparator1,
            this.buttonOpenFolder,
            this.buttonOpenFile,
            this.toolStripSeparator3,
            this.buttonScan});
            this.toolStripDocuments.Location = new System.Drawing.Point(0, 0);
            this.toolStripDocuments.Name = "toolStripDocuments";
            this.toolStripDocuments.Size = new System.Drawing.Size(1118, 25);
            this.toolStripDocuments.TabIndex = 1;
            this.toolStripDocuments.Text = "toolStripDocuments";
            // 
            // buttonCreate
            // 
            this.buttonCreate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonCreate.Image = global::DocumentFlow.Properties.Resources.icons8_file_add_16;
            this.buttonCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(23, 22);
            this.buttonCreate.Text = "Создать...";
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEdit.Image = global::DocumentFlow.Properties.Resources.icons8_file_edit_16;
            this.buttonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(23, 22);
            this.buttonEdit.Text = "Изменить...";
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonDelete.Image = global::DocumentFlow.Properties.Resources.icons8_file_delete_16;
            this.buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(23, 22);
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonOpenFolder
            // 
            this.buttonOpenFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonOpenFolder.Image = global::DocumentFlow.Properties.Resources.icons8_open_folder_16;
            this.buttonOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpenFolder.Name = "buttonOpenFolder";
            this.buttonOpenFolder.Size = new System.Drawing.Size(23, 22);
            this.buttonOpenFolder.Text = "Открыть папку";
            this.buttonOpenFolder.Click += new System.EventHandler(this.buttonOpenFolder_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonOpenFile.Image = global::DocumentFlow.Properties.Resources.icons8_open_document_16;
            this.buttonOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(23, 22);
            this.buttonOpenFile.Text = "Открыть файл";
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonScan
            // 
            this.buttonScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonScan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonSingleScan,
            this.buttonMultipleScan});
            this.buttonScan.Image = global::DocumentFlow.Properties.Resources.icons8_rescan_document_16;
            this.buttonScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonScan.Name = "buttonScan";
            this.buttonScan.Size = new System.Drawing.Size(32, 22);
            this.buttonScan.Text = "toolStripSplitButton1";
            this.buttonScan.ButtonClick += new System.EventHandler(this.buttonScan_Click);
            // 
            // buttonSingleScan
            // 
            this.buttonSingleScan.Name = "buttonSingleScan";
            this.buttonSingleScan.Size = new System.Drawing.Size(317, 22);
            this.buttonSingleScan.Text = "Сканировать один лист/документ";
            this.buttonSingleScan.Click += new System.EventHandler(this.buttonScan_Click);
            // 
            // buttonMultipleScan
            // 
            this.buttonMultipleScan.Name = "buttonMultipleScan";
            this.buttonMultipleScan.Size = new System.Drawing.Size(317, 22);
            this.buttonMultipleScan.Text = "Сканировать несколько листов/документов";
            this.buttonMultipleScan.Click += new System.EventHandler(this.buttonMultipleScan_Click);
            // 
            // tabSplitterInfo
            // 
            this.tabSplitterInfo.AutoScroll = true;
            this.tabSplitterInfo.BackColor = System.Drawing.SystemColors.Window;
            this.tabSplitterInfo.Controls.Add(this.label4);
            this.tabSplitterInfo.Controls.Add(this.textBoxUpdater);
            this.tabSplitterInfo.Controls.Add(this.label3);
            this.tabSplitterInfo.Controls.Add(this.textBoxCreator);
            this.tabSplitterInfo.Controls.Add(this.label5);
            this.tabSplitterInfo.Controls.Add(this.textBoxID);
            this.tabSplitterInfo.Controls.Add(this.dateTimeCreate);
            this.tabSplitterInfo.Controls.Add(this.label2);
            this.tabSplitterInfo.Controls.Add(this.dateTimeUpdate);
            this.tabSplitterInfo.Controls.Add(this.label1);
            this.tabSplitterInfo.Hide = false;
            this.tabSplitterInfo.Image = global::DocumentFlow.Properties.Resources.icons8_info_11;
            this.tabSplitterInfo.Location = new System.Drawing.Point(0, 297);
            this.tabSplitterInfo.Name = "tabSplitterInfo";
            this.tabSplitterInfo.Size = new System.Drawing.Size(1118, 127);
            this.tabSplitterInfo.TabIndex = 3;
            this.tabSplitterInfo.Tag = "system";
            this.tabSplitterInfo.Text = "Информация";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(306, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Редактор";
            // 
            // textBoxUpdater
            // 
            this.textBoxUpdater.BeforeTouchSize = new System.Drawing.Size(484, 25);
            this.textBoxUpdater.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.textBoxUpdater.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUpdater.Enabled = false;
            this.textBoxUpdater.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxUpdater.Location = new System.Drawing.Point(375, 78);
            this.textBoxUpdater.Name = "textBoxUpdater";
            this.textBoxUpdater.Size = new System.Drawing.Size(214, 25);
            this.textBoxUpdater.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.textBoxUpdater.TabIndex = 18;
            this.textBoxUpdater.ThemeName = "Metro";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(306, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Автор";
            // 
            // textBoxCreator
            // 
            this.textBoxCreator.BeforeTouchSize = new System.Drawing.Size(484, 25);
            this.textBoxCreator.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.textBoxCreator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCreator.Enabled = false;
            this.textBoxCreator.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCreator.Location = new System.Drawing.Point(375, 47);
            this.textBoxCreator.Name = "textBoxCreator";
            this.textBoxCreator.Size = new System.Drawing.Size(214, 25);
            this.textBoxCreator.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.textBoxCreator.TabIndex = 17;
            this.textBoxCreator.ThemeName = "Metro";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Идентификатор";
            // 
            // textBoxID
            // 
            this.textBoxID.BeforeTouchSize = new System.Drawing.Size(484, 25);
            this.textBoxID.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.textBoxID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxID.Location = new System.Drawing.Point(124, 16);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.ReadOnly = true;
            this.textBoxID.Size = new System.Drawing.Size(465, 25);
            this.textBoxID.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.textBoxID.TabIndex = 19;
            this.textBoxID.ThemeName = "Metro";
            // 
            // dateTimeCreate
            // 
            this.dateTimeCreate.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.dateTimeCreate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimeCreate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dateTimeCreate.Calendar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimeCreate.Calendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimeCreate.Calendar.BottomHeight = 25;
            this.dateTimeCreate.Calendar.Culture = new System.Globalization.CultureInfo("ru-RU");
            this.dateTimeCreate.Calendar.DayNamesFont = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeCreate.Calendar.DayNamesHeight = 20;
            this.dateTimeCreate.Calendar.DaysFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimeCreate.Calendar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimeCreate.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None;
            this.dateTimeCreate.Calendar.HeaderEndColor = System.Drawing.Color.White;
            this.dateTimeCreate.Calendar.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeCreate.Calendar.HeaderHeight = 30;
            this.dateTimeCreate.Calendar.HeaderStartColor = System.Drawing.Color.White;
            this.dateTimeCreate.Calendar.HighlightColor = System.Drawing.Color.Black;
            this.dateTimeCreate.Calendar.Iso8601CalenderFormat = false;
            this.dateTimeCreate.Calendar.Location = new System.Drawing.Point(0, 0);
            this.dateTimeCreate.Calendar.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeCreate.Calendar.Name = "monthCalendar";
            this.dateTimeCreate.Calendar.ScrollButtonSize = new System.Drawing.Size(28, 31);
            this.dateTimeCreate.Calendar.Size = new System.Drawing.Size(175, 175);
            this.dateTimeCreate.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.dateTimeCreate.Calendar.TabIndex = 0;
            this.dateTimeCreate.Calendar.WeekFont = new System.Drawing.Font("Verdana", 8F);
            // 
            // 
            // 
            this.dateTimeCreate.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimeCreate.Calendar.NoneButton.AutoSize = true;
            this.dateTimeCreate.Calendar.NoneButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeCreate.Calendar.NoneButton.ForeColor = System.Drawing.Color.White;
            this.dateTimeCreate.Calendar.NoneButton.Location = new System.Drawing.Point(-185935, 0);
            this.dateTimeCreate.Calendar.NoneButton.Size = new System.Drawing.Size(186110, 25);
            this.dateTimeCreate.Calendar.NoneButton.ThemeName = "Metro";
            this.dateTimeCreate.Calendar.NoneButton.UseVisualStyle = true;
            // 
            // 
            // 
            this.dateTimeCreate.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimeCreate.Calendar.TodayButton.AutoSize = true;
            this.dateTimeCreate.Calendar.TodayButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeCreate.Calendar.TodayButton.ForeColor = System.Drawing.Color.White;
            this.dateTimeCreate.Calendar.TodayButton.Location = new System.Drawing.Point(0, 0);
            this.dateTimeCreate.Calendar.TodayButton.Size = new System.Drawing.Size(-185935, 25);
            this.dateTimeCreate.Calendar.TodayButton.ThemeName = "Metro";
            this.dateTimeCreate.Calendar.TodayButton.UseVisualStyle = true;
            this.dateTimeCreate.CalendarSize = new System.Drawing.Size(189, 176);
            this.dateTimeCreate.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimeCreate.DropDownImage = null;
            this.dateTimeCreate.DropDownNormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeCreate.DropDownPressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeCreate.DropDownSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(191)))), ((int)(((byte)(237)))));
            this.dateTimeCreate.Enabled = false;
            this.dateTimeCreate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimeCreate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeCreate.Location = new System.Drawing.Point(124, 47);
            this.dateTimeCreate.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeCreate.MinValue = new System.DateTime(((long)(0)));
            this.dateTimeCreate.Name = "dateTimeCreate";
            this.dateTimeCreate.ShowCheckBox = false;
            this.dateTimeCreate.ShowDropButton = false;
            this.dateTimeCreate.Size = new System.Drawing.Size(152, 25);
            this.dateTimeCreate.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.dateTimeCreate.TabIndex = 16;
            this.dateTimeCreate.Value = new System.DateTime(2019, 4, 3, 22, 32, 0, 861);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(9, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Дата изменения";
            // 
            // dateTimeUpdate
            // 
            this.dateTimeUpdate.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.dateTimeUpdate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimeUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dateTimeUpdate.Calendar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimeUpdate.Calendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimeUpdate.Calendar.BottomHeight = 25;
            this.dateTimeUpdate.Calendar.Culture = new System.Globalization.CultureInfo("ru-RU");
            this.dateTimeUpdate.Calendar.DayNamesFont = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeUpdate.Calendar.DayNamesHeight = 20;
            this.dateTimeUpdate.Calendar.DaysFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimeUpdate.Calendar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimeUpdate.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None;
            this.dateTimeUpdate.Calendar.HeaderEndColor = System.Drawing.Color.White;
            this.dateTimeUpdate.Calendar.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeUpdate.Calendar.HeaderHeight = 30;
            this.dateTimeUpdate.Calendar.HeaderStartColor = System.Drawing.Color.White;
            this.dateTimeUpdate.Calendar.HighlightColor = System.Drawing.Color.Black;
            this.dateTimeUpdate.Calendar.Iso8601CalenderFormat = false;
            this.dateTimeUpdate.Calendar.Location = new System.Drawing.Point(0, 0);
            this.dateTimeUpdate.Calendar.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeUpdate.Calendar.Name = "monthCalendar";
            this.dateTimeUpdate.Calendar.ScrollButtonSize = new System.Drawing.Size(28, 31);
            this.dateTimeUpdate.Calendar.Size = new System.Drawing.Size(175, 175);
            this.dateTimeUpdate.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.dateTimeUpdate.Calendar.TabIndex = 0;
            this.dateTimeUpdate.Calendar.WeekFont = new System.Drawing.Font("Verdana", 8F);
            // 
            // 
            // 
            this.dateTimeUpdate.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimeUpdate.Calendar.NoneButton.AutoSize = true;
            this.dateTimeUpdate.Calendar.NoneButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeUpdate.Calendar.NoneButton.ForeColor = System.Drawing.Color.White;
            this.dateTimeUpdate.Calendar.NoneButton.Location = new System.Drawing.Point(-185935, 0);
            this.dateTimeUpdate.Calendar.NoneButton.Size = new System.Drawing.Size(186110, 25);
            this.dateTimeUpdate.Calendar.NoneButton.ThemeName = "Metro";
            this.dateTimeUpdate.Calendar.NoneButton.UseVisualStyle = true;
            // 
            // 
            // 
            this.dateTimeUpdate.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimeUpdate.Calendar.TodayButton.AutoSize = true;
            this.dateTimeUpdate.Calendar.TodayButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeUpdate.Calendar.TodayButton.ForeColor = System.Drawing.Color.White;
            this.dateTimeUpdate.Calendar.TodayButton.Location = new System.Drawing.Point(0, 0);
            this.dateTimeUpdate.Calendar.TodayButton.Size = new System.Drawing.Size(-185935, 25);
            this.dateTimeUpdate.Calendar.TodayButton.ThemeName = "Metro";
            this.dateTimeUpdate.Calendar.TodayButton.UseVisualStyle = true;
            this.dateTimeUpdate.CalendarSize = new System.Drawing.Size(189, 176);
            this.dateTimeUpdate.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimeUpdate.DropDownImage = null;
            this.dateTimeUpdate.DropDownNormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeUpdate.DropDownPressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeUpdate.DropDownSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(191)))), ((int)(((byte)(237)))));
            this.dateTimeUpdate.Enabled = false;
            this.dateTimeUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimeUpdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeUpdate.Location = new System.Drawing.Point(124, 78);
            this.dateTimeUpdate.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeUpdate.MinValue = new System.DateTime(((long)(0)));
            this.dateTimeUpdate.Name = "dateTimeUpdate";
            this.dateTimeUpdate.ShowCheckBox = false;
            this.dateTimeUpdate.ShowDropButton = false;
            this.dateTimeUpdate.Size = new System.Drawing.Size(152, 25);
            this.dateTimeUpdate.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.dateTimeUpdate.TabIndex = 15;
            this.dateTimeUpdate.Value = new System.DateTime(2019, 4, 3, 22, 32, 0, 861);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Дата создания";
            // 
            // timerDatabaseListen
            // 
            this.timerDatabaseListen.Tick += new System.EventHandler(this.timerDatabaseListen_Tick);
            // 
            // twain32
            // 
            this.twain32.AppProductName = "Saraff.Twain.NET";
            this.twain32.Country = Saraff.Twain.TwCountry.USSR;
            this.twain32.Parent = null;
            this.twain32.AcquireCompleted += new System.EventHandler(this.twain32_AcquireCompleted);
            // 
            // ContentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 476);
            this.Controls.Add(this.tabSplitterContainer1);
            this.Controls.Add(this.toolStripMain);
            this.Name = "ContentEditor";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.tabSplitterContainer1.ResumeLayout(false);
            this.tabSplitterDocs.ResumeLayout(false);
            this.tabSplitterDocs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments)).EndInit();
            this.contextMenuStripEx1.ResumeLayout(false);
            this.toolStripDocuments.ResumeLayout(false);
            this.toolStripDocuments.PerformLayout();
            this.tabSplitterInfo.ResumeLayout(false);
            this.tabSplitterInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxUpdater)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCreator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeCreate.Calendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeCreate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeUpdate.Calendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeUpdate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton buttonStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private Syncfusion.Windows.Forms.Tools.ToolStripPanelItem panelItemActions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton buttonSave;
        private System.Windows.Forms.ToolStripButton buttonSaveAndClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton buttonRefresh;
        private System.Windows.Forms.ToolStripSplitButton buttonPrint;
        private Syncfusion.Windows.Forms.Tools.TabSplitterContainer tabSplitterContainer1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage tabSplitterMaster;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage tabSplitterDocs;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage tabSplitterInfo;
        private System.Windows.Forms.Label label4;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxUpdater;
        private System.Windows.Forms.Label label3;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxCreator;
        private System.Windows.Forms.Label label5;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxID;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dateTimeCreate;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dateTimeUpdate;
        private System.Windows.Forms.Label label1;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridDocuments;
        private System.Windows.Forms.ToolStrip toolStripDocuments;
        private System.Windows.Forms.ToolStripButton buttonCreate;
        private System.Windows.Forms.ToolStripButton buttonEdit;
        private System.Windows.Forms.ToolStripButton buttonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonOpenFolder;
        private System.Windows.Forms.ToolStripButton buttonOpenFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextMenuStripEx1;
        private System.Windows.Forms.ToolStripMenuItem menuCreateDocument;
        private System.Windows.Forms.ToolStripMenuItem menuEditDocument;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteDocument;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuOpenFolderDocument;
        private System.Windows.Forms.ToolStripMenuItem menuOpenFileDocument;
        private System.Windows.Forms.Timer timerDatabaseListen;
        private System.Windows.Forms.ToolStripButton buttonCustomization;
        private Saraff.Twain.Twain32 twain32;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorUserCommands;
        private System.Windows.Forms.ToolStripSplitButton buttonScan;
        private System.Windows.Forms.ToolStripMenuItem buttonSingleScan;
        private System.Windows.Forms.ToolStripMenuItem buttonMultipleScan;
    }
}
