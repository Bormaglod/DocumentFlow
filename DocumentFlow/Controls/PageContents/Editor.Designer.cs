
namespace DocumentFlow.Controls.PageContents
{
    partial class Editor<T>
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
            this.editorToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.buttonSaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.buttonAccept = new System.Windows.Forms.ToolStripButton();
            this.buttonAcceptAndClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
            this.buttonPrint = new System.Windows.Forms.ToolStripDropDownButton();
            this.container = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDoc = new System.Windows.Forms.TabPage();
            this.gridDocuments = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.contextDocumentMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuSaveDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCreateDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuOpenFileDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonSaveDoc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonAddDoc = new System.Windows.Forms.ToolStripButton();
            this.buttonEditDoc = new System.Windows.Forms.ToolStripButton();
            this.buttonDeleteDoc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonOpenDoc = new System.Windows.Forms.ToolStripButton();
            this.buttonDocumentRefresh = new System.Windows.Forms.ToolStripButton();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.dateTimeUpdate = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.dateTimeCreate = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.textAuthor = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.textEditor = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.textId = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.editorToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.container)).BeginInit();
            this.container.Panel2.SuspendLayout();
            this.container.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageDoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments)).BeginInit();
            this.contextDocumentMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeUpdate.Calendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeCreate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeCreate.Calendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textAuthor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textId)).BeginInit();
            this.SuspendLayout();
            // 
            // editorToolStrip1
            // 
            this.editorToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonSave,
            this.buttonSaveAndClose,
            this.buttonAccept,
            this.buttonAcceptAndClose,
            this.toolStripSeparator2,
            this.buttonRefresh,
            this.buttonPrint});
            this.editorToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.editorToolStrip1.Name = "editorToolStrip1";
            this.editorToolStrip1.Size = new System.Drawing.Size(1088, 52);
            this.editorToolStrip1.TabIndex = 3;
            this.editorToolStrip1.Text = "editorToolStrip1";
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
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
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
            this.buttonSaveAndClose.Click += new System.EventHandler(this.ButtonSaveAndClose_Click);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Image = global::DocumentFlow.Properties.Resources.icons8_document_accept_30;
            this.buttonAccept.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAccept.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(64, 49);
            this.buttonAccept.Text = "Провести";
            this.buttonAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // buttonAcceptAndClose
            // 
            this.buttonAcceptAndClose.Image = global::DocumentFlow.Properties.Resources.icons8_doc_accept_close_30;
            this.buttonAcceptAndClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAcceptAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAcceptAndClose.Name = "buttonAcceptAndClose";
            this.buttonAcceptAndClose.Size = new System.Drawing.Size(121, 49);
            this.buttonAcceptAndClose.Text = "Провести и закрыть";
            this.buttonAcceptAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonAcceptAndClose.Click += new System.EventHandler(this.ButtonAcceptAndClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 52);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonRefresh.Image = global::DocumentFlow.Properties.Resources.icons8_refresh_30;
            this.buttonRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(65, 49);
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // buttonPrint
            // 
            this.buttonPrint.Image = global::DocumentFlow.Properties.Resources.icons8_print_30;
            this.buttonPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(59, 49);
            this.buttonPrint.Text = "Печать";
            this.buttonPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // container
            // 
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(0, 52);
            this.container.Name = "container";
            this.container.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // container.Panel1
            // 
            this.container.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.container.Panel1.Padding = new System.Windows.Forms.Padding(15);
            // 
            // container.Panel2
            // 
            this.container.Panel2.Controls.Add(this.tabControl1);
            this.container.Size = new System.Drawing.Size(1088, 560);
            this.container.SplitterDistance = 333;
            this.container.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageDoc);
            this.tabControl1.Controls.Add(this.tabPageInfo);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1088, 223);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageDoc
            // 
            this.tabPageDoc.Controls.Add(this.gridDocuments);
            this.tabPageDoc.Controls.Add(this.toolStrip1);
            this.tabPageDoc.Location = new System.Drawing.Point(4, 24);
            this.tabPageDoc.Name = "tabPageDoc";
            this.tabPageDoc.Size = new System.Drawing.Size(1080, 195);
            this.tabPageDoc.TabIndex = 0;
            this.tabPageDoc.Text = "Документы";
            this.tabPageDoc.UseVisualStyleBackColor = true;
            // 
            // gridDocuments
            // 
            this.gridDocuments.AccessibleName = "Table";
            this.gridDocuments.AllowEditing = false;
            this.gridDocuments.AllowGrouping = false;
            this.gridDocuments.AllowResizingColumns = true;
            this.gridDocuments.AllowTriStateSorting = true;
            this.gridDocuments.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.LastColumnFill;
            this.gridDocuments.BackColor = System.Drawing.SystemColors.Window;
            this.gridDocuments.ContextMenuStrip = this.contextDocumentMenu;
            this.gridDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDocuments.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gridDocuments.Location = new System.Drawing.Point(0, 25);
            this.gridDocuments.Name = "gridDocuments";
            this.gridDocuments.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridDocuments.Size = new System.Drawing.Size(1080, 170);
            this.gridDocuments.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridDocuments.Style.GroupDropAreaStyle.BackColor = System.Drawing.SystemColors.Window;
            this.gridDocuments.Style.TableSummaryRowStyle.Font.Size = 9F;
            this.gridDocuments.TabIndex = 4;
            this.gridDocuments.AutoGeneratingColumn += new Syncfusion.WinForms.DataGrid.Events.AutoGeneratingColumnEventHandler(this.GridDocuments_AutoGeneratingColumn);
            this.gridDocuments.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.GridDocuments_CellDoubleClick);
            // 
            // contextDocumentMenu
            // 
            this.contextDocumentMenu.DropShadowEnabled = false;
            this.contextDocumentMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSaveDocument,
            this.toolStripSeparator5,
            this.menuCreateDocument,
            this.menuEditDocument,
            this.menuDeleteDocument,
            this.toolStripSeparator3,
            this.menuOpenFileDocument});
            this.contextDocumentMenu.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextDocumentMenu.Name = "contextMenuStripEx1";
            this.contextDocumentMenu.Size = new System.Drawing.Size(187, 126);
            this.contextDocumentMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextDocumentMenu.ThemeName = "Metro";
            // 
            // menuSaveDocument
            // 
            this.menuSaveDocument.Name = "menuSaveDocument";
            this.menuSaveDocument.Size = new System.Drawing.Size(186, 22);
            this.menuSaveDocument.Text = "Сохранить на диск...";
            this.menuSaveDocument.Click += new System.EventHandler(this.ButtonSaveDoc_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(183, 6);
            // 
            // menuCreateDocument
            // 
            this.menuCreateDocument.Image = global::DocumentFlow.Properties.Resources.icons8_file_add_16;
            this.menuCreateDocument.Name = "menuCreateDocument";
            this.menuCreateDocument.Size = new System.Drawing.Size(186, 22);
            this.menuCreateDocument.Text = "Создать";
            this.menuCreateDocument.Click += new System.EventHandler(this.ButtonAddDoc_Click);
            // 
            // menuEditDocument
            // 
            this.menuEditDocument.Image = global::DocumentFlow.Properties.Resources.icons8_file_edit_16;
            this.menuEditDocument.Name = "menuEditDocument";
            this.menuEditDocument.Size = new System.Drawing.Size(186, 22);
            this.menuEditDocument.Text = "Изменить";
            this.menuEditDocument.Click += new System.EventHandler(this.ButtonEditDoc_Click);
            // 
            // menuDeleteDocument
            // 
            this.menuDeleteDocument.Image = global::DocumentFlow.Properties.Resources.icons8_file_delete_16;
            this.menuDeleteDocument.Name = "menuDeleteDocument";
            this.menuDeleteDocument.Size = new System.Drawing.Size(186, 22);
            this.menuDeleteDocument.Text = "Удалить";
            this.menuDeleteDocument.Click += new System.EventHandler(this.ButtonDeleteDoc_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(183, 6);
            // 
            // menuOpenFileDocument
            // 
            this.menuOpenFileDocument.Name = "menuOpenFileDocument";
            this.menuOpenFileDocument.Size = new System.Drawing.Size(186, 22);
            this.menuOpenFileDocument.Text = "Открыть файл";
            this.menuOpenFileDocument.Click += new System.EventHandler(this.ButtonOpenDoc_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonSaveDoc,
            this.toolStripSeparator4,
            this.buttonAddDoc,
            this.buttonEditDoc,
            this.buttonDeleteDoc,
            this.toolStripSeparator1,
            this.buttonOpenDoc,
            this.buttonDocumentRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1080, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonSaveDoc
            // 
            this.buttonSaveDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSaveDoc.Image = global::DocumentFlow.Properties.Resources.icons8_save_16;
            this.buttonSaveDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSaveDoc.Name = "buttonSaveDoc";
            this.buttonSaveDoc.Size = new System.Drawing.Size(23, 22);
            this.buttonSaveDoc.Text = "Сохранить на диск...";
            this.buttonSaveDoc.Click += new System.EventHandler(this.ButtonSaveDoc_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonAddDoc
            // 
            this.buttonAddDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAddDoc.Image = global::DocumentFlow.Properties.Resources.icons8_file_add_16;
            this.buttonAddDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAddDoc.Name = "buttonAddDoc";
            this.buttonAddDoc.Size = new System.Drawing.Size(23, 22);
            this.buttonAddDoc.Text = "Создать...";
            this.buttonAddDoc.Click += new System.EventHandler(this.ButtonAddDoc_Click);
            // 
            // buttonEditDoc
            // 
            this.buttonEditDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEditDoc.Image = global::DocumentFlow.Properties.Resources.icons8_file_edit_16;
            this.buttonEditDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEditDoc.Name = "buttonEditDoc";
            this.buttonEditDoc.Size = new System.Drawing.Size(23, 22);
            this.buttonEditDoc.Text = "Изменить...";
            this.buttonEditDoc.Click += new System.EventHandler(this.ButtonEditDoc_Click);
            // 
            // buttonDeleteDoc
            // 
            this.buttonDeleteDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonDeleteDoc.Image = global::DocumentFlow.Properties.Resources.icons8_file_delete_16;
            this.buttonDeleteDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDeleteDoc.Name = "buttonDeleteDoc";
            this.buttonDeleteDoc.Size = new System.Drawing.Size(23, 22);
            this.buttonDeleteDoc.Text = "Удалить";
            this.buttonDeleteDoc.Click += new System.EventHandler(this.ButtonDeleteDoc_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonOpenDoc
            // 
            this.buttonOpenDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonOpenDoc.Image = global::DocumentFlow.Properties.Resources.icons8_open_document_16;
            this.buttonOpenDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpenDoc.Name = "buttonOpenDoc";
            this.buttonOpenDoc.Size = new System.Drawing.Size(23, 22);
            this.buttonOpenDoc.Text = "Открыть файл";
            this.buttonOpenDoc.Click += new System.EventHandler(this.ButtonOpenDoc_Click);
            // 
            // buttonDocumentRefresh
            // 
            this.buttonDocumentRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonDocumentRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonDocumentRefresh.Image = global::DocumentFlow.Properties.Resources.icons8_refresh_16;
            this.buttonDocumentRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDocumentRefresh.Name = "buttonDocumentRefresh";
            this.buttonDocumentRefresh.Size = new System.Drawing.Size(23, 22);
            this.buttonDocumentRefresh.Text = "Обновить";
            this.buttonDocumentRefresh.Click += new System.EventHandler(this.ButtonDocumentRefresh_Click);
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.dateTimeUpdate);
            this.tabPageInfo.Controls.Add(this.dateTimeCreate);
            this.tabPageInfo.Controls.Add(this.textAuthor);
            this.tabPageInfo.Controls.Add(this.textEditor);
            this.tabPageInfo.Controls.Add(this.textId);
            this.tabPageInfo.Controls.Add(this.label5);
            this.tabPageInfo.Controls.Add(this.label4);
            this.tabPageInfo.Controls.Add(this.label3);
            this.tabPageInfo.Controls.Add(this.label2);
            this.tabPageInfo.Controls.Add(this.label1);
            this.tabPageInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 24);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfo.Size = new System.Drawing.Size(1080, 195);
            this.tabPageInfo.TabIndex = 1;
            this.tabPageInfo.Text = "Информация";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // dateTimeUpdate
            // 
            this.dateTimeUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimeUpdate.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.dateTimeUpdate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.dateTimeUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dateTimeUpdate.Calendar.AllowMultipleSelection = false;
            this.dateTimeUpdate.Calendar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimeUpdate.Calendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimeUpdate.Calendar.BottomHeight = 23;
            this.dateTimeUpdate.Calendar.Culture = new System.Globalization.CultureInfo("ru-RU");
            this.dateTimeUpdate.Calendar.DayNamesColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.dateTimeUpdate.Calendar.DayNamesFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimeUpdate.Calendar.DaysColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeUpdate.Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimeUpdate.Calendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimeUpdate.Calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeUpdate.Calendar.GridBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimeUpdate.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None;
            this.dateTimeUpdate.Calendar.HeaderFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dateTimeUpdate.Calendar.HeaderStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dateTimeUpdate.Calendar.HeadForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeUpdate.Calendar.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.dateTimeUpdate.Calendar.InactiveMonthColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeUpdate.Calendar.Iso8601CalenderFormat = false;
            this.dateTimeUpdate.Calendar.Location = new System.Drawing.Point(0, 0);
            this.dateTimeUpdate.Calendar.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeUpdate.Calendar.Name = "monthCalendar";
            // 
            // 
            // 
            this.dateTimeUpdate.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimeUpdate.Calendar.NoneButton.AutoSize = true;
            this.dateTimeUpdate.Calendar.NoneButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeUpdate.Calendar.NoneButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeUpdate.Calendar.NoneButton.KeepFocusRectangle = false;
            this.dateTimeUpdate.Calendar.NoneButton.Location = new System.Drawing.Point(85, 0);
            this.dateTimeUpdate.Calendar.NoneButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimeUpdate.Calendar.NoneButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimeUpdate.Calendar.NoneButton.Size = new System.Drawing.Size(62, 23);
            this.dateTimeUpdate.Calendar.NoneButton.Text = "None";
            this.dateTimeUpdate.Calendar.NoneButton.ThemeName = "Metro";
            this.dateTimeUpdate.Calendar.NoneButton.UseVisualStyle = true;
            this.dateTimeUpdate.Calendar.ScrollButtonSize = new System.Drawing.Size(24, 24);
            this.dateTimeUpdate.Calendar.Size = new System.Drawing.Size(147, 174);
            this.dateTimeUpdate.Calendar.SizeToFit = true;
            this.dateTimeUpdate.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.dateTimeUpdate.Calendar.TabIndex = 0;
            // 
            // 
            // 
            this.dateTimeUpdate.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimeUpdate.Calendar.TodayButton.AutoSize = true;
            this.dateTimeUpdate.Calendar.TodayButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeUpdate.Calendar.TodayButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeUpdate.Calendar.TodayButton.KeepFocusRectangle = false;
            this.dateTimeUpdate.Calendar.TodayButton.Location = new System.Drawing.Point(0, 0);
            this.dateTimeUpdate.Calendar.TodayButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimeUpdate.Calendar.TodayButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimeUpdate.Calendar.TodayButton.Size = new System.Drawing.Size(85, 23);
            this.dateTimeUpdate.Calendar.TodayButton.Text = "Today";
            this.dateTimeUpdate.Calendar.TodayButton.ThemeName = "Metro";
            this.dateTimeUpdate.Calendar.TodayButton.UseVisualStyle = true;
            this.dateTimeUpdate.Calendar.WeekFont = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimeUpdate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimeUpdate.CalendarSize = new System.Drawing.Size(189, 176);
            this.dateTimeUpdate.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dateTimeUpdate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeUpdate.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeUpdate.DropDownImage = null;
            this.dateTimeUpdate.DropDownNormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimeUpdate.Enabled = false;
            this.dateTimeUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimeUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeUpdate.Location = new System.Drawing.Point(120, 72);
            this.dateTimeUpdate.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeUpdate.MinValue = new System.DateTime(((long)(0)));
            this.dateTimeUpdate.Name = "dateTimeUpdate";
            this.dateTimeUpdate.ShowCheckBox = false;
            this.dateTimeUpdate.ShowDropButton = false;
            this.dateTimeUpdate.Size = new System.Drawing.Size(210, 25);
            this.dateTimeUpdate.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.dateTimeUpdate.TabIndex = 14;
            this.dateTimeUpdate.Value = new System.DateTime(2021, 9, 14, 20, 6, 40, 506);
            // 
            // dateTimeCreate
            // 
            this.dateTimeCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimeCreate.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.dateTimeCreate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.dateTimeCreate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dateTimeCreate.Calendar.AllowMultipleSelection = false;
            this.dateTimeCreate.Calendar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimeCreate.Calendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimeCreate.Calendar.BottomHeight = 23;
            this.dateTimeCreate.Calendar.Culture = new System.Globalization.CultureInfo("ru-RU");
            this.dateTimeCreate.Calendar.DayNamesColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.dateTimeCreate.Calendar.DayNamesFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimeCreate.Calendar.DaysColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeCreate.Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimeCreate.Calendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimeCreate.Calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeCreate.Calendar.GridBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimeCreate.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None;
            this.dateTimeCreate.Calendar.HeaderFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dateTimeCreate.Calendar.HeaderStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dateTimeCreate.Calendar.HeadForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeCreate.Calendar.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.dateTimeCreate.Calendar.InactiveMonthColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeCreate.Calendar.Iso8601CalenderFormat = false;
            this.dateTimeCreate.Calendar.Location = new System.Drawing.Point(0, 0);
            this.dateTimeCreate.Calendar.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeCreate.Calendar.Name = "monthCalendar";
            // 
            // 
            // 
            this.dateTimeCreate.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimeCreate.Calendar.NoneButton.AutoSize = true;
            this.dateTimeCreate.Calendar.NoneButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeCreate.Calendar.NoneButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeCreate.Calendar.NoneButton.KeepFocusRectangle = false;
            this.dateTimeCreate.Calendar.NoneButton.Location = new System.Drawing.Point(85, 0);
            this.dateTimeCreate.Calendar.NoneButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimeCreate.Calendar.NoneButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimeCreate.Calendar.NoneButton.Size = new System.Drawing.Size(62, 23);
            this.dateTimeCreate.Calendar.NoneButton.Text = "None";
            this.dateTimeCreate.Calendar.NoneButton.ThemeName = "Metro";
            this.dateTimeCreate.Calendar.NoneButton.UseVisualStyle = true;
            this.dateTimeCreate.Calendar.ScrollButtonSize = new System.Drawing.Size(24, 24);
            this.dateTimeCreate.Calendar.Size = new System.Drawing.Size(147, 174);
            this.dateTimeCreate.Calendar.SizeToFit = true;
            this.dateTimeCreate.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.dateTimeCreate.Calendar.TabIndex = 0;
            // 
            // 
            // 
            this.dateTimeCreate.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimeCreate.Calendar.TodayButton.AutoSize = true;
            this.dateTimeCreate.Calendar.TodayButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeCreate.Calendar.TodayButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeCreate.Calendar.TodayButton.KeepFocusRectangle = false;
            this.dateTimeCreate.Calendar.TodayButton.Location = new System.Drawing.Point(0, 0);
            this.dateTimeCreate.Calendar.TodayButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimeCreate.Calendar.TodayButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimeCreate.Calendar.TodayButton.Size = new System.Drawing.Size(85, 23);
            this.dateTimeCreate.Calendar.TodayButton.Text = "Today";
            this.dateTimeCreate.Calendar.TodayButton.ThemeName = "Metro";
            this.dateTimeCreate.Calendar.TodayButton.UseVisualStyle = true;
            this.dateTimeCreate.Calendar.WeekFont = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimeCreate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimeCreate.CalendarSize = new System.Drawing.Size(189, 176);
            this.dateTimeCreate.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dateTimeCreate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeCreate.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeCreate.DropDownImage = null;
            this.dateTimeCreate.DropDownNormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimeCreate.Enabled = false;
            this.dateTimeCreate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimeCreate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimeCreate.Location = new System.Drawing.Point(120, 41);
            this.dateTimeCreate.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimeCreate.MinValue = new System.DateTime(((long)(0)));
            this.dateTimeCreate.Name = "dateTimeCreate";
            this.dateTimeCreate.ShowCheckBox = false;
            this.dateTimeCreate.ShowDropButton = false;
            this.dateTimeCreate.Size = new System.Drawing.Size(210, 25);
            this.dateTimeCreate.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.dateTimeCreate.TabIndex = 13;
            this.dateTimeCreate.Value = new System.DateTime(2021, 9, 14, 20, 6, 36, 610);
            // 
            // textAuthor
            // 
            this.textAuthor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textAuthor.BeforeTouchSize = new System.Drawing.Size(450, 25);
            this.textAuthor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textAuthor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textAuthor.Enabled = false;
            this.textAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textAuthor.Location = new System.Drawing.Point(420, 41);
            this.textAuthor.Name = "textAuthor";
            this.textAuthor.Size = new System.Drawing.Size(150, 25);
            this.textAuthor.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textAuthor.TabIndex = 12;
            this.textAuthor.ThemeName = "Office2016Colorful";
            // 
            // textEditor
            // 
            this.textEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textEditor.BeforeTouchSize = new System.Drawing.Size(450, 25);
            this.textEditor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textEditor.Enabled = false;
            this.textEditor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textEditor.Location = new System.Drawing.Point(420, 72);
            this.textEditor.Name = "textEditor";
            this.textEditor.Size = new System.Drawing.Size(150, 25);
            this.textEditor.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textEditor.TabIndex = 11;
            this.textEditor.ThemeName = "Office2016Colorful";
            // 
            // textId
            // 
            this.textId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textId.BeforeTouchSize = new System.Drawing.Size(450, 25);
            this.textId.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textId.Location = new System.Drawing.Point(120, 10);
            this.textId.Name = "textId";
            this.textId.ReadOnly = true;
            this.textId.Size = new System.Drawing.Size(450, 25);
            this.textId.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textId.TabIndex = 10;
            this.textId.ThemeName = "Office2016Colorful";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(351, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Редактор";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Автор";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Дата изменения";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Дата создания";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Идентификатор";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.container);
            this.Controls.Add(this.editorToolStrip1);
            this.Name = "Editor";
            this.Size = new System.Drawing.Size(1088, 612);
            this.editorToolStrip1.ResumeLayout(false);
            this.editorToolStrip1.PerformLayout();
            this.container.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.container)).EndInit();
            this.container.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageDoc.ResumeLayout(false);
            this.tabPageDoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments)).EndInit();
            this.contextDocumentMenu.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeUpdate.Calendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeCreate.Calendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeCreate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textAuthor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip editorToolStrip1;
        private System.Windows.Forms.SplitContainer container;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDoc;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textAuthor;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textEditor;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textId;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dateTimeUpdate;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dateTimeCreate;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonAddDoc;
        private System.Windows.Forms.ToolStripButton buttonEditDoc;
        private System.Windows.Forms.ToolStripButton buttonDeleteDoc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonOpenDoc;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridDocuments;
        private System.Windows.Forms.ToolStripButton buttonDocumentRefresh;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextDocumentMenu;
        private System.Windows.Forms.ToolStripMenuItem menuCreateDocument;
        private System.Windows.Forms.ToolStripMenuItem menuEditDocument;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteDocument;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuOpenFileDocument;
        private ToolStripButton buttonSave;
        private ToolStripButton buttonSaveAndClose;
        private ToolStripButton buttonAccept;
        private ToolStripButton buttonAcceptAndClose;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton buttonRefresh;
        private ToolStripDropDownButton buttonPrint;
        private ToolStripButton buttonSaveDoc;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem menuSaveDocument;
        private ToolStripSeparator toolStripSeparator5;
        private SaveFileDialog saveFileDialog1;
    }
}
