namespace DocumentFlow.Controls.PageContents
{
    partial class Browser<T>
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
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonWipe = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonCopy = new System.Windows.Forms.ToolStripButton();
            this.buttonCreateGroup = new System.Windows.Forms.ToolStripButton();
            this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonPrint = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripPrintList = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSettings = new System.Windows.Forms.ToolStripButton();
            this.gridContent = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.contextGridMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuAddRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCreateGroup2 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextHeaderMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuSortAsc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSortDesc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClearSort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuVisibleColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.contextRowMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorAcceptanceRow = new System.Windows.Forms.ToolStripSeparator();
            this.menuAcceptRow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCancelAccepanceRow = new System.Windows.Forms.ToolStripMenuItem();
            this.contextRecordMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuId = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorId = new System.Windows.Forms.ToolStripSeparator();
            this.menuCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuAccept = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCancelAcceptance = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorAcceptance = new System.Windows.Forms.ToolStripSeparator();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCreateGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorDocuments = new System.Windows.Forms.ToolStripSeparator();
            this.menuDocuments = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGroupMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuHideGroupArea = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClearGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGroupItemMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuExpandItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCollapseItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClearGroupItems = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGroupCaptionMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuGroupCaptionExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGroupCaptionCollapse = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTools = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridContent)).BeginInit();
            this.contextGridMenu.SuspendLayout();
            this.contextHeaderMenu.SuspendLayout();
            this.contextRowMenu.SuspendLayout();
            this.contextRecordMenu.SuspendLayout();
            this.contextGroupMenu.SuspendLayout();
            this.contextGroupItemMenu.SuspendLayout();
            this.contextGroupCaptionMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonCreate,
            this.buttonEdit,
            this.buttonDelete,
            this.toolStripSeparator7,
            this.buttonWipe,
            this.toolStripSeparator1,
            this.buttonCopy,
            this.buttonCreateGroup,
            this.buttonRefresh,
            this.toolStripSeparator4,
            this.buttonPrint,
            this.buttonSettings});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1067, 52);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Image = global::DocumentFlow.Properties.Resources.icons8_file_add_30;
            this.buttonCreate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(54, 49);
            this.buttonCreate.Text = "Создать";
            this.buttonCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonCreate.Click += new System.EventHandler(this.ButtonCreate_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Image = global::DocumentFlow.Properties.Resources.icons8_file_edit_30;
            this.buttonEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(65, 49);
            this.buttonEdit.Text = "Изменить";
            this.buttonEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Image = global::DocumentFlow.Properties.Resources.icons8_file_delete_30;
            this.buttonDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(65, 49);
            this.buttonDelete.Text = "Пометить";
            this.buttonDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonDelete.ToolTipText = "Пометить на удаление / снять отметку";
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 52);
            // 
            // buttonWipe
            // 
            this.buttonWipe.Image = global::DocumentFlow.Properties.Resources.trash_32;
            this.buttonWipe.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonWipe.ImageTransparentColor = System.Drawing.SystemColors.Window;
            this.buttonWipe.Name = "buttonWipe";
            this.buttonWipe.Size = new System.Drawing.Size(55, 49);
            this.buttonWipe.Text = "Удалить";
            this.buttonWipe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonWipe.Click += new System.EventHandler(this.ButtonWipe_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Image = global::DocumentFlow.Properties.Resources.icons8_copy_edit_30;
            this.buttonCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(45, 49);
            this.buttonCopy.Text = "Копия";
            this.buttonCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonCopy.Click += new System.EventHandler(this.ButtonCopy_Click);
            // 
            // buttonCreateGroup
            // 
            this.buttonCreateGroup.Image = global::DocumentFlow.Properties.Resources.icons8_folder_add_30;
            this.buttonCreateGroup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonCreateGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCreateGroup.Name = "buttonCreateGroup";
            this.buttonCreateGroup.Size = new System.Drawing.Size(50, 49);
            this.buttonCreateGroup.Text = "Группа";
            this.buttonCreateGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonCreateGroup.Click += new System.EventHandler(this.ButtonCreateGroup_Click);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 52);
            // 
            // buttonPrint
            // 
            this.buttonPrint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPrintList});
            this.buttonPrint.Image = global::DocumentFlow.Properties.Resources.icons8_print_30;
            this.buttonPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(62, 49);
            this.buttonPrint.Text = "Печать";
            this.buttonPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripPrintList
            // 
            this.toolStripPrintList.Name = "toolStripPrintList";
            this.toolStripPrintList.Size = new System.Drawing.Size(115, 22);
            this.toolStripPrintList.Text = "Список";
            this.toolStripPrintList.Click += new System.EventHandler(this.ToolStripPrintList_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Image = global::DocumentFlow.Properties.Resources.icons8_settings_30;
            this.buttonSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(71, 49);
            this.buttonSettings.Text = "Настройки";
            this.buttonSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonSettings.Click += new System.EventHandler(this.ButtonSettings_Click);
            // 
            // gridContent
            // 
            this.gridContent.AccessibleName = "Table";
            this.gridContent.AllowEditing = false;
            this.gridContent.AllowFiltering = true;
            this.gridContent.AllowGrouping = false;
            this.gridContent.AllowResizingColumns = true;
            this.gridContent.AllowTriStateSorting = true;
            this.gridContent.AutoExpandGroups = true;
            this.gridContent.AutoGenerateColumns = false;
            this.gridContent.BackColor = System.Drawing.SystemColors.Window;
            this.gridContent.ContextMenuStrip = this.contextGridMenu;
            this.gridContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridContent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gridContent.LiveDataUpdateMode = Syncfusion.Data.LiveDataUpdateMode.AllowSummaryUpdate;
            this.gridContent.Location = new System.Drawing.Point(0, 152);
            this.gridContent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridContent.Name = "gridContent";
            this.gridContent.ShowBusyIndicator = true;
            this.gridContent.ShowRowHeader = true;
            this.gridContent.ShowSortNumbers = true;
            this.gridContent.Size = new System.Drawing.Size(1067, 404);
            this.gridContent.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridContent.Style.GroupDropAreaStyle.BackColor = System.Drawing.SystemColors.Window;
            this.gridContent.Style.ProgressBarStyle.ForegroundStyle = Syncfusion.WinForms.DataGrid.Enums.GridProgressBarStyle.Tube;
            this.gridContent.Style.ProgressBarStyle.ProgressTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.gridContent.Style.ProgressBarStyle.TubeForegroundEndColor = System.Drawing.Color.White;
            this.gridContent.Style.ProgressBarStyle.TubeForegroundStartColor = System.Drawing.Color.SkyBlue;
            this.gridContent.Style.TableSummaryRowStyle.Font.Size = 9F;
            this.gridContent.TabIndex = 14;
            this.gridContent.ToolTipOpening += new Syncfusion.WinForms.DataGrid.Events.ToolTipOpeningEventHandler(this.GridContent_ToolTipOpening);
            this.gridContent.ContextMenuOpening += new Syncfusion.WinForms.DataGrid.Events.ContextMenuOpeningEventHandler(this.GridContent_ContextMenuOpening);
            this.gridContent.QueryCellStyle += new Syncfusion.WinForms.DataGrid.Events.QueryCellStyleEventHandler(this.GridContent_QueryCellStyle);
            this.gridContent.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.GridContent_CellDoubleClick);
            this.gridContent.QueryImageCellStyle += new Syncfusion.WinForms.DataGrid.Events.QueryImageCellStyleEventHandler(this.GridContent_QueryImageCellStyle);
            // 
            // contextGridMenu
            // 
            this.contextGridMenu.DropShadowEnabled = false;
            this.contextGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddRecord,
            this.toolStripSeparator2,
            this.menuCreateGroup2});
            this.contextGridMenu.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextGridMenu.Name = "contextGridMenu";
            this.contextGridMenu.Size = new System.Drawing.Size(184, 54);
            this.contextGridMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextGridMenu.ThemeName = "Metro";
            // 
            // menuAddRecord
            // 
            this.menuAddRecord.Image = global::DocumentFlow.Properties.Resources.icons8_file_add_16;
            this.menuAddRecord.Name = "menuAddRecord";
            this.menuAddRecord.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Insert)));
            this.menuAddRecord.Size = new System.Drawing.Size(183, 22);
            this.menuAddRecord.Text = "Создать";
            this.menuAddRecord.Click += new System.EventHandler(this.ButtonCreate_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
            // 
            // menuCreateGroup2
            // 
            this.menuCreateGroup2.Image = global::DocumentFlow.Properties.Resources.icons8_folder_add_16;
            this.menuCreateGroup2.Name = "menuCreateGroup2";
            this.menuCreateGroup2.Size = new System.Drawing.Size(183, 22);
            this.menuCreateGroup2.Text = "Группа";
            this.menuCreateGroup2.Click += new System.EventHandler(this.ButtonCreateGroup_Click);
            // 
            // contextHeaderMenu
            // 
            this.contextHeaderMenu.DropShadowEnabled = false;
            this.contextHeaderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSortAsc,
            this.menuSortDesc,
            this.menuClearSort,
            this.toolStripSeparator3,
            this.menuVisibleColumns});
            this.contextHeaderMenu.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextHeaderMenu.Name = "contextHeaderMenu";
            this.contextHeaderMenu.Size = new System.Drawing.Size(233, 98);
            this.contextHeaderMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextHeaderMenu.ThemeName = "Metro";
            // 
            // menuSortAsc
            // 
            this.menuSortAsc.Image = global::DocumentFlow.Properties.Resources.icons8_sort_by_asc_16;
            this.menuSortAsc.Name = "menuSortAsc";
            this.menuSortAsc.Size = new System.Drawing.Size(232, 22);
            this.menuSortAsc.Text = "Сортировка по возрастанию";
            this.menuSortAsc.Click += new System.EventHandler(this.MenuSortAsc_Click);
            // 
            // menuSortDesc
            // 
            this.menuSortDesc.Image = global::DocumentFlow.Properties.Resources.icons8_sort_by_desc_16;
            this.menuSortDesc.Name = "menuSortDesc";
            this.menuSortDesc.Size = new System.Drawing.Size(232, 22);
            this.menuSortDesc.Text = "Сортировка по убыванию";
            this.menuSortDesc.Click += new System.EventHandler(this.MenuSortDesc_Click);
            // 
            // menuClearSort
            // 
            this.menuClearSort.Name = "menuClearSort";
            this.menuClearSort.Size = new System.Drawing.Size(232, 22);
            this.menuClearSort.Text = "Убрать сортировку";
            this.menuClearSort.Click += new System.EventHandler(this.MenuClearSort_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(229, 6);
            // 
            // menuVisibleColumns
            // 
            this.menuVisibleColumns.Name = "menuVisibleColumns";
            this.menuVisibleColumns.Size = new System.Drawing.Size(232, 22);
            this.menuVisibleColumns.Text = "Видимые столбцы";
            // 
            // contextRowMenu
            // 
            this.contextRowMenu.DropShadowEnabled = false;
            this.contextRowMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDeleteRow,
            this.separatorAcceptanceRow,
            this.menuAcceptRow,
            this.menuCancelAccepanceRow});
            this.contextRowMenu.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextRowMenu.Name = "contextRowMenu";
            this.contextRowMenu.Size = new System.Drawing.Size(355, 76);
            this.contextRowMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextRowMenu.ThemeName = "Metro";
            // 
            // menuDeleteRow
            // 
            this.menuDeleteRow.Image = global::DocumentFlow.Properties.Resources.icons8_file_delete_16;
            this.menuDeleteRow.Name = "menuDeleteRow";
            this.menuDeleteRow.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Delete)));
            this.menuDeleteRow.Size = new System.Drawing.Size(354, 22);
            this.menuDeleteRow.Text = "Пометить на удаление / снять отметку";
            this.menuDeleteRow.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // separatorAcceptanceRow
            // 
            this.separatorAcceptanceRow.Name = "separatorAcceptanceRow";
            this.separatorAcceptanceRow.Size = new System.Drawing.Size(351, 6);
            // 
            // menuAcceptRow
            // 
            this.menuAcceptRow.Image = global::DocumentFlow.Properties.Resources.icons8_accept_16;
            this.menuAcceptRow.Name = "menuAcceptRow";
            this.menuAcceptRow.Size = new System.Drawing.Size(354, 22);
            this.menuAcceptRow.Text = "Провести";
            this.menuAcceptRow.Click += new System.EventHandler(this.MenuAcceptRow_Click);
            // 
            // menuCancelAccepanceRow
            // 
            this.menuCancelAccepanceRow.Image = global::DocumentFlow.Properties.Resources.icons8_cancel_16;
            this.menuCancelAccepanceRow.Name = "menuCancelAccepanceRow";
            this.menuCancelAccepanceRow.Size = new System.Drawing.Size(354, 22);
            this.menuCancelAccepanceRow.Text = "Отменить проведение";
            this.menuCancelAccepanceRow.Click += new System.EventHandler(this.MenuCancelAccepanceRow_Click);
            // 
            // contextRecordMenu
            // 
            this.contextRecordMenu.DropShadowEnabled = false;
            this.contextRecordMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuId,
            this.separatorId,
            this.menuCopyClipboard,
            this.toolStripSeparator6,
            this.menuCreate,
            this.menuEdit,
            this.menuDelete,
            this.toolStripSeparator5,
            this.menuAccept,
            this.menuCancelAcceptance,
            this.separatorAcceptance,
            this.menuCopy,
            this.menuCreateGroup,
            this.separatorDocuments,
            this.menuDocuments});
            this.contextRecordMenu.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextRecordMenu.Name = "contextGridMenu";
            this.contextRecordMenu.Size = new System.Drawing.Size(355, 254);
            this.contextRecordMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextRecordMenu.ThemeName = "Metro";
            this.contextRecordMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextRecordMenu_Opening);
            // 
            // menuId
            // 
            this.menuId.Name = "menuId";
            this.menuId.Size = new System.Drawing.Size(354, 22);
            this.menuId.Text = "id";
            this.menuId.Click += new System.EventHandler(this.MenuId_Click);
            // 
            // separatorId
            // 
            this.separatorId.Name = "separatorId";
            this.separatorId.Size = new System.Drawing.Size(351, 6);
            // 
            // menuCopyClipboard
            // 
            this.menuCopyClipboard.Image = global::DocumentFlow.Properties.Resources.icons8_copy_16;
            this.menuCopyClipboard.Name = "menuCopyClipboard";
            this.menuCopyClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuCopyClipboard.Size = new System.Drawing.Size(354, 22);
            this.menuCopyClipboard.Text = "Копировать";
            this.menuCopyClipboard.Click += new System.EventHandler(this.MenuCopyClipboard_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(351, 6);
            // 
            // menuCreate
            // 
            this.menuCreate.Image = global::DocumentFlow.Properties.Resources.icons8_file_add_16;
            this.menuCreate.Name = "menuCreate";
            this.menuCreate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Insert)));
            this.menuCreate.Size = new System.Drawing.Size(354, 22);
            this.menuCreate.Text = "Создать";
            this.menuCreate.Click += new System.EventHandler(this.ButtonCreate_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.Image = global::DocumentFlow.Properties.Resources.icons8_file_edit_16;
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(354, 22);
            this.menuEdit.Text = "Изменить";
            this.menuEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // menuDelete
            // 
            this.menuDelete.Image = global::DocumentFlow.Properties.Resources.icons8_file_delete_16;
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Delete)));
            this.menuDelete.Size = new System.Drawing.Size(354, 22);
            this.menuDelete.Text = "Пометить на удаление / снять отметку";
            this.menuDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(351, 6);
            // 
            // menuAccept
            // 
            this.menuAccept.Image = global::DocumentFlow.Properties.Resources.icons8_accept_16;
            this.menuAccept.Name = "menuAccept";
            this.menuAccept.Size = new System.Drawing.Size(354, 22);
            this.menuAccept.Text = "Провести";
            this.menuAccept.Click += new System.EventHandler(this.MenuAcceptRow_Click);
            // 
            // menuCancelAcceptance
            // 
            this.menuCancelAcceptance.Image = global::DocumentFlow.Properties.Resources.icons8_cancel_16;
            this.menuCancelAcceptance.Name = "menuCancelAcceptance";
            this.menuCancelAcceptance.Size = new System.Drawing.Size(354, 22);
            this.menuCancelAcceptance.Text = "Отменить проведение";
            this.menuCancelAcceptance.Click += new System.EventHandler(this.MenuCancelAccepanceRow_Click);
            // 
            // separatorAcceptance
            // 
            this.separatorAcceptance.Name = "separatorAcceptance";
            this.separatorAcceptance.Size = new System.Drawing.Size(351, 6);
            // 
            // menuCopy
            // 
            this.menuCopy.Image = global::DocumentFlow.Properties.Resources.icons8_copy_edit_16;
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Insert)));
            this.menuCopy.Size = new System.Drawing.Size(354, 22);
            this.menuCopy.Text = "Создать копию";
            this.menuCopy.Click += new System.EventHandler(this.ButtonCopy_Click);
            // 
            // menuCreateGroup
            // 
            this.menuCreateGroup.Image = global::DocumentFlow.Properties.Resources.icons8_folder_add_16;
            this.menuCreateGroup.Name = "menuCreateGroup";
            this.menuCreateGroup.Size = new System.Drawing.Size(354, 22);
            this.menuCreateGroup.Text = "Группа";
            this.menuCreateGroup.Click += new System.EventHandler(this.ButtonCreateGroup_Click);
            // 
            // separatorDocuments
            // 
            this.separatorDocuments.Name = "separatorDocuments";
            this.separatorDocuments.Size = new System.Drawing.Size(351, 6);
            // 
            // menuDocuments
            // 
            this.menuDocuments.Name = "menuDocuments";
            this.menuDocuments.Size = new System.Drawing.Size(354, 22);
            this.menuDocuments.Text = "Документы";
            // 
            // contextGroupMenu
            // 
            this.contextGroupMenu.DropShadowEnabled = false;
            this.contextGroupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHideGroupArea,
            this.menuExpandAll,
            this.menuCollapseAll,
            this.menuClearGroups});
            this.contextGroupMenu.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextGroupMenu.Name = "contextGroupMenu";
            this.contextGroupMenu.Size = new System.Drawing.Size(238, 92);
            this.contextGroupMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextGroupMenu.ThemeName = "Metro";
            // 
            // menuHideGroupArea
            // 
            this.menuHideGroupArea.Name = "menuHideGroupArea";
            this.menuHideGroupArea.Size = new System.Drawing.Size(237, 22);
            this.menuHideGroupArea.Text = "Скрыть область группировки";
            this.menuHideGroupArea.Click += new System.EventHandler(this.MenuHideGroupArea_Click);
            // 
            // menuExpandAll
            // 
            this.menuExpandAll.Name = "menuExpandAll";
            this.menuExpandAll.Size = new System.Drawing.Size(237, 22);
            this.menuExpandAll.Text = "Развернуть все";
            this.menuExpandAll.Click += new System.EventHandler(this.MenuExpandAll_Click);
            // 
            // menuCollapseAll
            // 
            this.menuCollapseAll.Name = "menuCollapseAll";
            this.menuCollapseAll.Size = new System.Drawing.Size(237, 22);
            this.menuCollapseAll.Text = "Свернуть все";
            this.menuCollapseAll.Click += new System.EventHandler(this.MenuCollapseAll_Click);
            // 
            // menuClearGroups
            // 
            this.menuClearGroups.Name = "menuClearGroups";
            this.menuClearGroups.Size = new System.Drawing.Size(237, 22);
            this.menuClearGroups.Text = "Очистить";
            this.menuClearGroups.Click += new System.EventHandler(this.MenuClearGroups_Click);
            // 
            // contextGroupItemMenu
            // 
            this.contextGroupItemMenu.DropShadowEnabled = false;
            this.contextGroupItemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExpandItem,
            this.menuCollapseItem,
            this.menuClearGroupItems});
            this.contextGroupItemMenu.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextGroupItemMenu.Name = "contextGroupItemMenu";
            this.contextGroupItemMenu.Size = new System.Drawing.Size(157, 70);
            this.contextGroupItemMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextGroupItemMenu.ThemeName = "Metro";
            // 
            // menuExpandItem
            // 
            this.menuExpandItem.Name = "menuExpandItem";
            this.menuExpandItem.Size = new System.Drawing.Size(156, 22);
            this.menuExpandItem.Text = "Развернуть все";
            this.menuExpandItem.Click += new System.EventHandler(this.MenuExpandItem_Click);
            // 
            // menuCollapseItem
            // 
            this.menuCollapseItem.Name = "menuCollapseItem";
            this.menuCollapseItem.Size = new System.Drawing.Size(156, 22);
            this.menuCollapseItem.Text = "Свернуть все";
            this.menuCollapseItem.Click += new System.EventHandler(this.MenuCollapseItem_Click);
            // 
            // menuClearGroupItems
            // 
            this.menuClearGroupItems.Name = "menuClearGroupItems";
            this.menuClearGroupItems.Size = new System.Drawing.Size(156, 22);
            this.menuClearGroupItems.Text = "Очистить";
            this.menuClearGroupItems.Click += new System.EventHandler(this.MenuClearGroupItems_Click);
            // 
            // contextGroupCaptionMenu
            // 
            this.contextGroupCaptionMenu.DropShadowEnabled = false;
            this.contextGroupCaptionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGroupCaptionExpand,
            this.menuGroupCaptionCollapse});
            this.contextGroupCaptionMenu.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextGroupCaptionMenu.Name = "contextGroupCaptionMenu";
            this.contextGroupCaptionMenu.Size = new System.Drawing.Size(136, 48);
            this.contextGroupCaptionMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextGroupCaptionMenu.ThemeName = "Metro";
            // 
            // menuGroupCaptionExpand
            // 
            this.menuGroupCaptionExpand.Name = "menuGroupCaptionExpand";
            this.menuGroupCaptionExpand.Size = new System.Drawing.Size(135, 22);
            this.menuGroupCaptionExpand.Text = "Развернуть";
            this.menuGroupCaptionExpand.Click += new System.EventHandler(this.MenuGroupCaptionExpand_Click);
            // 
            // menuGroupCaptionCollapse
            // 
            this.menuGroupCaptionCollapse.Name = "menuGroupCaptionCollapse";
            this.menuGroupCaptionCollapse.Size = new System.Drawing.Size(135, 22);
            this.menuGroupCaptionCollapse.Text = "Свернуть";
            this.menuGroupCaptionCollapse.Click += new System.EventHandler(this.MenuGroupCaptionCollapse_Click);
            // 
            // panelTools
            // 
            this.panelTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTools.Location = new System.Drawing.Point(0, 52);
            this.panelTools.Name = "panelTools";
            this.panelTools.Size = new System.Drawing.Size(1067, 100);
            this.panelTools.TabIndex = 15;
            // 
            // Browser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridContent);
            this.Controls.Add(this.panelTools);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Browser";
            this.Size = new System.Drawing.Size(1067, 556);
            this.Load += new System.EventHandler(this.Browser_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridContent)).EndInit();
            this.contextGridMenu.ResumeLayout(false);
            this.contextHeaderMenu.ResumeLayout(false);
            this.contextRowMenu.ResumeLayout(false);
            this.contextRecordMenu.ResumeLayout(false);
            this.contextGroupMenu.ResumeLayout(false);
            this.contextGroupItemMenu.ResumeLayout(false);
            this.contextGroupCaptionMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton buttonCreate;
        private ToolStripButton buttonEdit;
        private ToolStripButton buttonDelete;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton buttonCopy;
        private ToolStripButton buttonCreateGroup;
        private ToolStripButton buttonRefresh;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridContent;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextGridMenu;
        private ToolStripMenuItem menuAddRecord;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuCreateGroup2;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextHeaderMenu;
        private ToolStripMenuItem menuSortAsc;
        private ToolStripMenuItem menuSortDesc;
        private ToolStripMenuItem menuClearSort;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem menuVisibleColumns;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextRowMenu;
        private ToolStripMenuItem menuDeleteRow;
        private ToolStripSeparator separatorAcceptanceRow;
        private ToolStripMenuItem menuAcceptRow;
        private ToolStripMenuItem menuCancelAccepanceRow;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextRecordMenu;
        private ToolStripMenuItem menuCopyClipboard;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem menuCreate;
        private ToolStripMenuItem menuEdit;
        private ToolStripMenuItem menuDelete;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem menuAccept;
        private ToolStripMenuItem menuCancelAcceptance;
        private ToolStripSeparator separatorAcceptance;
        private ToolStripMenuItem menuCopy;
        private ToolStripMenuItem menuCreateGroup;
        private ToolStripSeparator separatorDocuments;
        private ToolStripMenuItem menuDocuments;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextGroupMenu;
        private ToolStripMenuItem menuHideGroupArea;
        private ToolStripMenuItem menuExpandAll;
        private ToolStripMenuItem menuCollapseAll;
        private ToolStripMenuItem menuClearGroups;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextGroupItemMenu;
        private ToolStripMenuItem menuExpandItem;
        private ToolStripMenuItem menuCollapseItem;
        private ToolStripMenuItem menuClearGroupItems;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextGroupCaptionMenu;
        private ToolStripMenuItem menuGroupCaptionExpand;
        private ToolStripMenuItem menuGroupCaptionCollapse;
        private Panel panelTools;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton buttonSettings;
        private ToolStripSplitButton buttonPrint;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton buttonWipe;
        private ToolStripMenuItem toolStripPrintList;
        private ToolStripMenuItem menuId;
        private ToolStripSeparator separatorId;
    }
}
