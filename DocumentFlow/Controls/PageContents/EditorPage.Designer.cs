
namespace DocumentFlow.Controls.PageContents
{
    partial class EditorPage
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
            components = new System.ComponentModel.Container();
            editorToolStrip1 = new ToolStrip();
            buttonSave = new ToolStripButton();
            buttonSaveAndClose = new ToolStripButton();
            buttonAccept = new ToolStripButton();
            buttonAcceptAndClose = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            buttonRefresh = new ToolStripButton();
            buttonPrint = new ToolStripDropDownButton();
            container = new SplitContainer();
            tabControl1 = new TabControl();
            tabPageDoc = new TabPage();
            gridDocuments = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            contextDocumentMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            menuSaveDocument = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            menuCreateDocument = new ToolStripMenuItem();
            menuEditDocument = new ToolStripMenuItem();
            menuDeleteDocument = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            menuOpenFileDocument = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            buttonSaveDoc = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            buttonAddDoc = new ToolStripButton();
            buttonEditDoc = new ToolStripButton();
            buttonDeleteDoc = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            buttonOpenDoc = new ToolStripButton();
            buttonDocumentRefresh = new ToolStripButton();
            tabPageInfo = new TabPage();
            dateTimeUpdate = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            dateTimeCreate = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            textAuthor = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            textEditor = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            textId = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            saveFileDialog1 = new SaveFileDialog();
            buttonScan = new ToolStripSplitButton();
            buttonSingleScan = new ToolStripMenuItem();
            buttonMultipleScan = new ToolStripMenuItem();
            editorToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)container).BeginInit();
            container.Panel2.SuspendLayout();
            container.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageDoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridDocuments).BeginInit();
            contextDocumentMenu.SuspendLayout();
            toolStrip1.SuspendLayout();
            tabPageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dateTimeUpdate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateTimeUpdate.Calendar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateTimeCreate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateTimeCreate.Calendar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textAuthor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEditor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textId).BeginInit();
            SuspendLayout();
            // 
            // editorToolStrip1
            // 
            editorToolStrip1.Items.AddRange(new ToolStripItem[] { buttonSave, buttonSaveAndClose, buttonAccept, buttonAcceptAndClose, toolStripSeparator2, buttonRefresh, buttonPrint });
            editorToolStrip1.Location = new Point(0, 0);
            editorToolStrip1.Name = "editorToolStrip1";
            editorToolStrip1.Size = new Size(1088, 52);
            editorToolStrip1.TabIndex = 3;
            editorToolStrip1.Text = "editorToolStrip1";
            // 
            // buttonSave
            // 
            buttonSave.Image = DocumentFlow.Properties.Resources.icons8_save_30;
            buttonSave.ImageScaling = ToolStripItemImageScaling.None;
            buttonSave.ImageTransparentColor = Color.Magenta;
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(70, 49);
            buttonSave.Text = "Сохранить";
            buttonSave.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonSave.Click += ButtonSave_Click;
            // 
            // buttonSaveAndClose
            // 
            buttonSaveAndClose.Image = DocumentFlow.Properties.Resources.icons8_save_close_30;
            buttonSaveAndClose.ImageScaling = ToolStripItemImageScaling.None;
            buttonSaveAndClose.ImageTransparentColor = Color.Magenta;
            buttonSaveAndClose.Name = "buttonSaveAndClose";
            buttonSaveAndClose.Size = new Size(127, 49);
            buttonSaveAndClose.Text = "Сохранить и закрыть";
            buttonSaveAndClose.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonSaveAndClose.Click += ButtonSaveAndClose_Click;
            // 
            // buttonAccept
            // 
            buttonAccept.Image = DocumentFlow.Properties.Resources.icons8_document_accept_30;
            buttonAccept.ImageScaling = ToolStripItemImageScaling.None;
            buttonAccept.ImageTransparentColor = Color.Magenta;
            buttonAccept.Name = "buttonAccept";
            buttonAccept.Size = new Size(64, 49);
            buttonAccept.Text = "Провести";
            buttonAccept.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonAccept.Click += ButtonAccept_Click;
            // 
            // buttonAcceptAndClose
            // 
            buttonAcceptAndClose.Image = DocumentFlow.Properties.Resources.icons8_doc_accept_close_30;
            buttonAcceptAndClose.ImageScaling = ToolStripItemImageScaling.None;
            buttonAcceptAndClose.ImageTransparentColor = Color.Magenta;
            buttonAcceptAndClose.Name = "buttonAcceptAndClose";
            buttonAcceptAndClose.Size = new Size(121, 49);
            buttonAcceptAndClose.Text = "Провести и закрыть";
            buttonAcceptAndClose.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonAcceptAndClose.Click += ButtonAcceptAndClose_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 52);
            // 
            // buttonRefresh
            // 
            buttonRefresh.Alignment = ToolStripItemAlignment.Right;
            buttonRefresh.Image = DocumentFlow.Properties.Resources.icons8_refresh_30;
            buttonRefresh.ImageScaling = ToolStripItemImageScaling.None;
            buttonRefresh.ImageTransparentColor = Color.Magenta;
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(65, 49);
            buttonRefresh.Text = "Обновить";
            buttonRefresh.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonRefresh.Click += ButtonRefresh_Click;
            // 
            // buttonPrint
            // 
            buttonPrint.Image = DocumentFlow.Properties.Resources.icons8_print_30;
            buttonPrint.ImageScaling = ToolStripItemImageScaling.None;
            buttonPrint.ImageTransparentColor = Color.Magenta;
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new Size(59, 49);
            buttonPrint.Text = "Печать";
            buttonPrint.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // container
            // 
            container.Dock = DockStyle.Fill;
            container.Location = new Point(0, 52);
            container.Name = "container";
            container.Orientation = Orientation.Horizontal;
            // 
            // container.Panel1
            // 
            container.Panel1.BackColor = SystemColors.Window;
            container.Panel1.Padding = new Padding(15);
            // 
            // container.Panel2
            // 
            container.Panel2.Controls.Add(tabControl1);
            container.Size = new Size(1088, 560);
            container.SplitterDistance = 333;
            container.TabIndex = 5;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageDoc);
            tabControl1.Controls.Add(tabPageInfo);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1088, 223);
            tabControl1.TabIndex = 0;
            // 
            // tabPageDoc
            // 
            tabPageDoc.Controls.Add(gridDocuments);
            tabPageDoc.Controls.Add(toolStrip1);
            tabPageDoc.Location = new Point(4, 24);
            tabPageDoc.Name = "tabPageDoc";
            tabPageDoc.Size = new Size(1080, 195);
            tabPageDoc.TabIndex = 0;
            tabPageDoc.Text = "Документы";
            tabPageDoc.UseVisualStyleBackColor = true;
            // 
            // gridDocuments
            // 
            gridDocuments.AccessibleName = "Table";
            gridDocuments.AllowEditing = false;
            gridDocuments.AllowGrouping = false;
            gridDocuments.AllowResizingColumns = true;
            gridDocuments.AllowTriStateSorting = true;
            gridDocuments.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.LastColumnFill;
            gridDocuments.BackColor = SystemColors.Window;
            gridDocuments.ContextMenuStrip = contextDocumentMenu;
            gridDocuments.Dock = DockStyle.Fill;
            gridDocuments.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            gridDocuments.Location = new Point(0, 25);
            gridDocuments.Name = "gridDocuments";
            gridDocuments.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            gridDocuments.Size = new Size(1080, 170);
            gridDocuments.Style.BorderColor = Color.FromArgb(204, 204, 204);
            gridDocuments.Style.GroupDropAreaStyle.BackColor = SystemColors.Window;
            gridDocuments.Style.TableSummaryRowStyle.Font.Size = 9F;
            gridDocuments.TabIndex = 4;
            gridDocuments.AutoGeneratingColumn += GridDocuments_AutoGeneratingColumn;
            gridDocuments.CellDoubleClick += GridDocuments_CellDoubleClick;
            // 
            // contextDocumentMenu
            // 
            contextDocumentMenu.DropShadowEnabled = false;
            contextDocumentMenu.Items.AddRange(new ToolStripItem[] { menuSaveDocument, toolStripSeparator5, menuCreateDocument, menuEditDocument, menuDeleteDocument, toolStripSeparator3, menuOpenFileDocument });
            contextDocumentMenu.MetroColor = Color.FromArgb(204, 236, 249);
            contextDocumentMenu.Name = "contextMenuStripEx1";
            contextDocumentMenu.Size = new Size(187, 126);
            contextDocumentMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            contextDocumentMenu.ThemeName = "Metro";
            // 
            // menuSaveDocument
            // 
            menuSaveDocument.Name = "menuSaveDocument";
            menuSaveDocument.Size = new Size(186, 22);
            menuSaveDocument.Text = "Сохранить на диск...";
            menuSaveDocument.Click += ButtonSaveDoc_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(183, 6);
            // 
            // menuCreateDocument
            // 
            menuCreateDocument.Image = DocumentFlow.Properties.Resources.icons8_file_add_16;
            menuCreateDocument.Name = "menuCreateDocument";
            menuCreateDocument.Size = new Size(186, 22);
            menuCreateDocument.Text = "Создать";
            menuCreateDocument.Click += ButtonAddDoc_Click;
            // 
            // menuEditDocument
            // 
            menuEditDocument.Image = DocumentFlow.Properties.Resources.icons8_file_edit_16;
            menuEditDocument.Name = "menuEditDocument";
            menuEditDocument.Size = new Size(186, 22);
            menuEditDocument.Text = "Изменить";
            menuEditDocument.Click += ButtonEditDoc_Click;
            // 
            // menuDeleteDocument
            // 
            menuDeleteDocument.Image = DocumentFlow.Properties.Resources.icons8_file_delete_16;
            menuDeleteDocument.Name = "menuDeleteDocument";
            menuDeleteDocument.Size = new Size(186, 22);
            menuDeleteDocument.Text = "Удалить";
            menuDeleteDocument.Click += ButtonDeleteDoc_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(183, 6);
            // 
            // menuOpenFileDocument
            // 
            menuOpenFileDocument.Name = "menuOpenFileDocument";
            menuOpenFileDocument.Size = new Size(186, 22);
            menuOpenFileDocument.Text = "Открыть файл";
            menuOpenFileDocument.Click += ButtonOpenDoc_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { buttonSaveDoc, buttonOpenDoc, toolStripSeparator4, buttonAddDoc, buttonEditDoc, buttonDeleteDoc, toolStripSeparator1, buttonDocumentRefresh, buttonScan });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1080, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // buttonSaveDoc
            // 
            buttonSaveDoc.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonSaveDoc.Image = DocumentFlow.Properties.Resources.icons8_save_16;
            buttonSaveDoc.ImageTransparentColor = Color.Magenta;
            buttonSaveDoc.Name = "buttonSaveDoc";
            buttonSaveDoc.Size = new Size(23, 22);
            buttonSaveDoc.Text = "Сохранить на диск...";
            buttonSaveDoc.Click += ButtonSaveDoc_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // buttonAddDoc
            // 
            buttonAddDoc.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonAddDoc.Image = DocumentFlow.Properties.Resources.icons8_file_add_16;
            buttonAddDoc.ImageTransparentColor = Color.Magenta;
            buttonAddDoc.Name = "buttonAddDoc";
            buttonAddDoc.Size = new Size(23, 22);
            buttonAddDoc.Text = "Создать...";
            buttonAddDoc.Click += ButtonAddDoc_Click;
            // 
            // buttonEditDoc
            // 
            buttonEditDoc.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonEditDoc.Image = DocumentFlow.Properties.Resources.icons8_file_edit_16;
            buttonEditDoc.ImageTransparentColor = Color.Magenta;
            buttonEditDoc.Name = "buttonEditDoc";
            buttonEditDoc.Size = new Size(23, 22);
            buttonEditDoc.Text = "Изменить...";
            buttonEditDoc.Click += ButtonEditDoc_Click;
            // 
            // buttonDeleteDoc
            // 
            buttonDeleteDoc.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonDeleteDoc.Image = DocumentFlow.Properties.Resources.icons8_file_delete_16;
            buttonDeleteDoc.ImageTransparentColor = Color.Magenta;
            buttonDeleteDoc.Name = "buttonDeleteDoc";
            buttonDeleteDoc.Size = new Size(23, 22);
            buttonDeleteDoc.Text = "Удалить";
            buttonDeleteDoc.Click += ButtonDeleteDoc_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // buttonOpenDoc
            // 
            buttonOpenDoc.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonOpenDoc.Image = DocumentFlow.Properties.Resources.icons8_open_document_16;
            buttonOpenDoc.ImageTransparentColor = Color.Magenta;
            buttonOpenDoc.Name = "buttonOpenDoc";
            buttonOpenDoc.Size = new Size(23, 22);
            buttonOpenDoc.Text = "Открыть файл";
            buttonOpenDoc.Click += ButtonOpenDoc_Click;
            // 
            // buttonDocumentRefresh
            // 
            buttonDocumentRefresh.Alignment = ToolStripItemAlignment.Right;
            buttonDocumentRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonDocumentRefresh.Image = DocumentFlow.Properties.Resources.icons8_refresh_16;
            buttonDocumentRefresh.ImageTransparentColor = Color.Magenta;
            buttonDocumentRefresh.Name = "buttonDocumentRefresh";
            buttonDocumentRefresh.Size = new Size(23, 22);
            buttonDocumentRefresh.Text = "Обновить";
            buttonDocumentRefresh.Click += ButtonDocumentRefresh_Click;
            // 
            // tabPageInfo
            // 
            tabPageInfo.Controls.Add(dateTimeUpdate);
            tabPageInfo.Controls.Add(dateTimeCreate);
            tabPageInfo.Controls.Add(textAuthor);
            tabPageInfo.Controls.Add(textEditor);
            tabPageInfo.Controls.Add(textId);
            tabPageInfo.Controls.Add(label5);
            tabPageInfo.Controls.Add(label4);
            tabPageInfo.Controls.Add(label3);
            tabPageInfo.Controls.Add(label2);
            tabPageInfo.Controls.Add(label1);
            tabPageInfo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tabPageInfo.Location = new Point(4, 24);
            tabPageInfo.Name = "tabPageInfo";
            tabPageInfo.Padding = new Padding(3);
            tabPageInfo.Size = new Size(1080, 195);
            tabPageInfo.TabIndex = 1;
            tabPageInfo.Text = "Информация";
            tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // dateTimeUpdate
            // 
            dateTimeUpdate.BackColor = Color.FromArgb(255, 255, 255);
            dateTimeUpdate.Border3DStyle = Border3DStyle.Flat;
            dateTimeUpdate.BorderColor = Color.FromArgb(171, 171, 171);
            dateTimeUpdate.BorderStyle = BorderStyle.FixedSingle;
            // 
            // 
            // 
            dateTimeUpdate.Calendar.AllowMultipleSelection = false;
            dateTimeUpdate.Calendar.BorderColor = Color.FromArgb(209, 211, 212);
            dateTimeUpdate.Calendar.BorderStyle = BorderStyle.FixedSingle;
            dateTimeUpdate.Calendar.BottomHeight = 27;
            dateTimeUpdate.Calendar.Culture = new System.Globalization.CultureInfo("ru-RU");
            dateTimeUpdate.Calendar.DayNamesColor = Color.FromArgb(102, 102, 102);
            dateTimeUpdate.Calendar.DayNamesFont = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimeUpdate.Calendar.DaysColor = Color.FromArgb(68, 68, 68);
            dateTimeUpdate.Calendar.Dock = DockStyle.Fill;
            dateTimeUpdate.Calendar.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimeUpdate.Calendar.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimeUpdate.Calendar.GridBackColor = Color.FromArgb(255, 255, 255);
            dateTimeUpdate.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None;
            dateTimeUpdate.Calendar.HeaderFont = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            dateTimeUpdate.Calendar.HeaderStartColor = Color.FromArgb(240, 240, 240);
            dateTimeUpdate.Calendar.HeadForeColor = Color.FromArgb(68, 68, 68);
            dateTimeUpdate.Calendar.HighlightColor = Color.FromArgb(205, 230, 247);
            dateTimeUpdate.Calendar.InactiveMonthColor = Color.FromArgb(68, 68, 68);
            dateTimeUpdate.Calendar.Iso8601CalenderFormat = false;
            dateTimeUpdate.Calendar.Location = new Point(0, 0);
            dateTimeUpdate.Calendar.MetroColor = Color.FromArgb(22, 165, 220);
            dateTimeUpdate.Calendar.Name = "monthCalendar";
            // 
            // 
            // 
            dateTimeUpdate.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            dateTimeUpdate.Calendar.NoneButton.AutoSize = true;
            dateTimeUpdate.Calendar.NoneButton.BackColor = Color.FromArgb(22, 165, 220);
            dateTimeUpdate.Calendar.NoneButton.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimeUpdate.Calendar.NoneButton.KeepFocusRectangle = false;
            dateTimeUpdate.Calendar.NoneButton.Location = new Point(115, 0);
            dateTimeUpdate.Calendar.NoneButton.Margin = new Padding(3, 2, 3, 2);
            dateTimeUpdate.Calendar.NoneButton.MetroColor = Color.FromArgb(255, 255, 255);
            dateTimeUpdate.Calendar.NoneButton.Size = new Size(62, 27);
            dateTimeUpdate.Calendar.NoneButton.Text = "None";
            dateTimeUpdate.Calendar.NoneButton.ThemeName = "Metro";
            dateTimeUpdate.Calendar.NoneButton.UseVisualStyle = true;
            dateTimeUpdate.Calendar.ScrollButtonSize = new Size(24, 24);
            dateTimeUpdate.Calendar.Size = new Size(177, 174);
            dateTimeUpdate.Calendar.SizeToFit = true;
            dateTimeUpdate.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            dateTimeUpdate.Calendar.TabIndex = 0;
            // 
            // 
            // 
            dateTimeUpdate.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            dateTimeUpdate.Calendar.TodayButton.AutoSize = true;
            dateTimeUpdate.Calendar.TodayButton.BackColor = Color.FromArgb(22, 165, 220);
            dateTimeUpdate.Calendar.TodayButton.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimeUpdate.Calendar.TodayButton.KeepFocusRectangle = false;
            dateTimeUpdate.Calendar.TodayButton.Location = new Point(0, 0);
            dateTimeUpdate.Calendar.TodayButton.Margin = new Padding(3, 2, 3, 2);
            dateTimeUpdate.Calendar.TodayButton.MetroColor = Color.FromArgb(255, 255, 255);
            dateTimeUpdate.Calendar.TodayButton.Size = new Size(115, 27);
            dateTimeUpdate.Calendar.TodayButton.Text = "Today";
            dateTimeUpdate.Calendar.TodayButton.ThemeName = "Metro";
            dateTimeUpdate.Calendar.TodayButton.UseVisualStyle = true;
            dateTimeUpdate.Calendar.WeekFont = new Font("Verdana", 8F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimeUpdate.CalendarMonthBackground = Color.FromArgb(255, 255, 255);
            dateTimeUpdate.CalendarSize = new Size(189, 176);
            dateTimeUpdate.CalendarTitleBackColor = Color.FromArgb(240, 240, 240);
            dateTimeUpdate.CalendarTitleForeColor = Color.FromArgb(68, 68, 68);
            dateTimeUpdate.CalendarTrailingForeColor = Color.FromArgb(68, 68, 68);
            dateTimeUpdate.DropDownImage = null;
            dateTimeUpdate.DropDownNormalColor = Color.FromArgb(255, 255, 255);
            dateTimeUpdate.Enabled = false;
            dateTimeUpdate.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimeUpdate.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimeUpdate.Location = new Point(120, 72);
            dateTimeUpdate.MetroColor = Color.FromArgb(22, 165, 220);
            dateTimeUpdate.MinValue = new DateTime(0L);
            dateTimeUpdate.Name = "dateTimeUpdate";
            dateTimeUpdate.ShowCheckBox = false;
            dateTimeUpdate.ShowDropButton = false;
            dateTimeUpdate.Size = new Size(210, 25);
            dateTimeUpdate.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            dateTimeUpdate.TabIndex = 14;
            dateTimeUpdate.Value = new DateTime(2021, 9, 14, 20, 6, 40, 506);
            // 
            // dateTimeCreate
            // 
            dateTimeCreate.BackColor = Color.FromArgb(255, 255, 255);
            dateTimeCreate.Border3DStyle = Border3DStyle.Flat;
            dateTimeCreate.BorderColor = Color.FromArgb(171, 171, 171);
            dateTimeCreate.BorderStyle = BorderStyle.FixedSingle;
            // 
            // 
            // 
            dateTimeCreate.Calendar.AllowMultipleSelection = false;
            dateTimeCreate.Calendar.BorderColor = Color.FromArgb(209, 211, 212);
            dateTimeCreate.Calendar.BorderStyle = BorderStyle.FixedSingle;
            dateTimeCreate.Calendar.BottomHeight = 27;
            dateTimeCreate.Calendar.Culture = new System.Globalization.CultureInfo("ru-RU");
            dateTimeCreate.Calendar.DayNamesColor = Color.FromArgb(102, 102, 102);
            dateTimeCreate.Calendar.DayNamesFont = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimeCreate.Calendar.DaysColor = Color.FromArgb(68, 68, 68);
            dateTimeCreate.Calendar.Dock = DockStyle.Fill;
            dateTimeCreate.Calendar.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimeCreate.Calendar.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimeCreate.Calendar.GridBackColor = Color.FromArgb(255, 255, 255);
            dateTimeCreate.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None;
            dateTimeCreate.Calendar.HeaderFont = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            dateTimeCreate.Calendar.HeaderStartColor = Color.FromArgb(240, 240, 240);
            dateTimeCreate.Calendar.HeadForeColor = Color.FromArgb(68, 68, 68);
            dateTimeCreate.Calendar.HighlightColor = Color.FromArgb(205, 230, 247);
            dateTimeCreate.Calendar.InactiveMonthColor = Color.FromArgb(68, 68, 68);
            dateTimeCreate.Calendar.Iso8601CalenderFormat = false;
            dateTimeCreate.Calendar.Location = new Point(0, 0);
            dateTimeCreate.Calendar.MetroColor = Color.FromArgb(22, 165, 220);
            dateTimeCreate.Calendar.Name = "monthCalendar";
            // 
            // 
            // 
            dateTimeCreate.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            dateTimeCreate.Calendar.NoneButton.AutoSize = true;
            dateTimeCreate.Calendar.NoneButton.BackColor = Color.FromArgb(22, 165, 220);
            dateTimeCreate.Calendar.NoneButton.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimeCreate.Calendar.NoneButton.KeepFocusRectangle = false;
            dateTimeCreate.Calendar.NoneButton.Location = new Point(115, 0);
            dateTimeCreate.Calendar.NoneButton.Margin = new Padding(3, 2, 3, 2);
            dateTimeCreate.Calendar.NoneButton.MetroColor = Color.FromArgb(255, 255, 255);
            dateTimeCreate.Calendar.NoneButton.Size = new Size(62, 27);
            dateTimeCreate.Calendar.NoneButton.Text = "None";
            dateTimeCreate.Calendar.NoneButton.ThemeName = "Metro";
            dateTimeCreate.Calendar.NoneButton.UseVisualStyle = true;
            dateTimeCreate.Calendar.ScrollButtonSize = new Size(24, 24);
            dateTimeCreate.Calendar.Size = new Size(177, 174);
            dateTimeCreate.Calendar.SizeToFit = true;
            dateTimeCreate.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            dateTimeCreate.Calendar.TabIndex = 0;
            // 
            // 
            // 
            dateTimeCreate.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            dateTimeCreate.Calendar.TodayButton.AutoSize = true;
            dateTimeCreate.Calendar.TodayButton.BackColor = Color.FromArgb(22, 165, 220);
            dateTimeCreate.Calendar.TodayButton.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimeCreate.Calendar.TodayButton.KeepFocusRectangle = false;
            dateTimeCreate.Calendar.TodayButton.Location = new Point(0, 0);
            dateTimeCreate.Calendar.TodayButton.Margin = new Padding(3, 2, 3, 2);
            dateTimeCreate.Calendar.TodayButton.MetroColor = Color.FromArgb(255, 255, 255);
            dateTimeCreate.Calendar.TodayButton.Size = new Size(115, 27);
            dateTimeCreate.Calendar.TodayButton.Text = "Today";
            dateTimeCreate.Calendar.TodayButton.ThemeName = "Metro";
            dateTimeCreate.Calendar.TodayButton.UseVisualStyle = true;
            dateTimeCreate.Calendar.WeekFont = new Font("Verdana", 8F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimeCreate.CalendarMonthBackground = Color.FromArgb(255, 255, 255);
            dateTimeCreate.CalendarSize = new Size(189, 176);
            dateTimeCreate.CalendarTitleBackColor = Color.FromArgb(240, 240, 240);
            dateTimeCreate.CalendarTitleForeColor = Color.FromArgb(68, 68, 68);
            dateTimeCreate.CalendarTrailingForeColor = Color.FromArgb(68, 68, 68);
            dateTimeCreate.DropDownImage = null;
            dateTimeCreate.DropDownNormalColor = Color.FromArgb(255, 255, 255);
            dateTimeCreate.Enabled = false;
            dateTimeCreate.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimeCreate.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimeCreate.Location = new Point(120, 41);
            dateTimeCreate.MetroColor = Color.FromArgb(22, 165, 220);
            dateTimeCreate.MinValue = new DateTime(0L);
            dateTimeCreate.Name = "dateTimeCreate";
            dateTimeCreate.ShowCheckBox = false;
            dateTimeCreate.ShowDropButton = false;
            dateTimeCreate.Size = new Size(210, 25);
            dateTimeCreate.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            dateTimeCreate.TabIndex = 13;
            dateTimeCreate.Value = new DateTime(2021, 9, 14, 20, 6, 36, 610);
            // 
            // textAuthor
            // 
            textAuthor.BackColor = Color.FromArgb(255, 255, 255);
            textAuthor.BeforeTouchSize = new Size(450, 25);
            textAuthor.BorderColor = Color.FromArgb(197, 197, 197);
            textAuthor.BorderStyle = BorderStyle.FixedSingle;
            textAuthor.Enabled = false;
            textAuthor.ForeColor = Color.FromArgb(68, 68, 68);
            textAuthor.Location = new Point(420, 41);
            textAuthor.Name = "textAuthor";
            textAuthor.Size = new Size(150, 25);
            textAuthor.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            textAuthor.TabIndex = 12;
            textAuthor.ThemeName = "Office2016Colorful";
            // 
            // textEditor
            // 
            textEditor.BackColor = Color.FromArgb(255, 255, 255);
            textEditor.BeforeTouchSize = new Size(450, 25);
            textEditor.BorderColor = Color.FromArgb(197, 197, 197);
            textEditor.BorderStyle = BorderStyle.FixedSingle;
            textEditor.Enabled = false;
            textEditor.ForeColor = Color.FromArgb(68, 68, 68);
            textEditor.Location = new Point(420, 72);
            textEditor.Name = "textEditor";
            textEditor.Size = new Size(150, 25);
            textEditor.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            textEditor.TabIndex = 11;
            textEditor.ThemeName = "Office2016Colorful";
            // 
            // textId
            // 
            textId.BackColor = Color.FromArgb(255, 255, 255);
            textId.BeforeTouchSize = new Size(450, 25);
            textId.BorderColor = Color.FromArgb(197, 197, 197);
            textId.BorderStyle = BorderStyle.FixedSingle;
            textId.ForeColor = Color.FromArgb(68, 68, 68);
            textId.Location = new Point(120, 10);
            textId.Name = "textId";
            textId.ReadOnly = true;
            textId.Size = new Size(450, 25);
            textId.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            textId.TabIndex = 10;
            textId.ThemeName = "Office2016Colorful";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(351, 72);
            label5.Name = "label5";
            label5.Size = new Size(63, 17);
            label5.TabIndex = 4;
            label5.Text = "Редактор";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(351, 41);
            label4.Name = "label4";
            label4.Size = new Size(44, 17);
            label4.TabIndex = 3;
            label4.Text = "Автор";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 72);
            label3.Name = "label3";
            label3.Size = new Size(104, 17);
            label3.TabIndex = 2;
            label3.Text = "Дата изменения";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 41);
            label2.Name = "label2";
            label2.Size = new Size(95, 17);
            label2.TabIndex = 1;
            label2.Text = "Дата создания";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 10);
            label1.Name = "label1";
            label1.Size = new Size(101, 17);
            label1.TabIndex = 0;
            label1.Text = "Идентификатор";
            // 
            // buttonScan
            // 
            buttonScan.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonScan.DropDownItems.AddRange(new ToolStripItem[] { buttonSingleScan, buttonMultipleScan });
            buttonScan.Image = DocumentFlow.Properties.Resources.icons8_rescan_document_16;
            buttonScan.ImageTransparentColor = Color.Magenta;
            buttonScan.Name = "buttonScan";
            buttonScan.Size = new Size(32, 22);
            buttonScan.Text = "Сканировать один лист/документ";
            buttonScan.ButtonClick += ButtonScan_ButtonClick;
            // 
            // buttonSingleScan
            // 
            buttonSingleScan.Name = "buttonSingleScan";
            buttonSingleScan.Size = new Size(317, 22);
            buttonSingleScan.Text = "Сканировать один лист/документ";
            buttonSingleScan.Click += ButtonScan_ButtonClick;
            // 
            // buttonMultipleScan
            // 
            buttonMultipleScan.Name = "buttonMultipleScan";
            buttonMultipleScan.Size = new Size(317, 22);
            buttonMultipleScan.Text = "Сканировать несколько листов/документов";
            buttonMultipleScan.Click += ButtonMultipleScan_Click;
            // 
            // EditorPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(container);
            Controls.Add(editorToolStrip1);
            Name = "EditorPage";
            Size = new Size(1088, 612);
            editorToolStrip1.ResumeLayout(false);
            editorToolStrip1.PerformLayout();
            container.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)container).EndInit();
            container.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPageDoc.ResumeLayout(false);
            tabPageDoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridDocuments).EndInit();
            contextDocumentMenu.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tabPageInfo.ResumeLayout(false);
            tabPageInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dateTimeUpdate.Calendar).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateTimeUpdate).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateTimeCreate.Calendar).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateTimeCreate).EndInit();
            ((System.ComponentModel.ISupportInitialize)textAuthor).EndInit();
            ((System.ComponentModel.ISupportInitialize)textEditor).EndInit();
            ((System.ComponentModel.ISupportInitialize)textId).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStrip editorToolStrip1;
        private SplitContainer container;
        private TabControl tabControl1;
        private TabPage tabPageDoc;
        private TabPage tabPageInfo;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textAuthor;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textEditor;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textId;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dateTimeUpdate;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dateTimeCreate;
        private ToolStrip toolStrip1;
        private ToolStripButton buttonAddDoc;
        private ToolStripButton buttonEditDoc;
        private ToolStripButton buttonDeleteDoc;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton buttonOpenDoc;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridDocuments;
        private ToolStripButton buttonDocumentRefresh;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextDocumentMenu;
        private ToolStripMenuItem menuCreateDocument;
        private ToolStripMenuItem menuEditDocument;
        private ToolStripMenuItem menuDeleteDocument;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem menuOpenFileDocument;
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
        private ToolStripSplitButton buttonScan;
        private ToolStripMenuItem buttonSingleScan;
        private ToolStripMenuItem buttonMultipleScan;
    }
}
