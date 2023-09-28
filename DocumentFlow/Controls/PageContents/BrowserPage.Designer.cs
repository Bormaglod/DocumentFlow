namespace DocumentFlow.Controls.PageContents
{
    partial class BrowserPage<T>
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
            toolStripSeparator7 = new ToolStripSeparator();
            buttonWipe = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            buttonCopy = new ToolStripButton();
            buttonCreateGroup = new ToolStripButton();
            buttonRefresh = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            buttonPrint = new ToolStripSplitButton();
            toolStripPrintList = new ToolStripMenuItem();
            buttonSettings = new ToolStripButton();
            gridContent = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            contextGridMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            menuAddRecord = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            menuCreateGroup2 = new ToolStripMenuItem();
            contextHeaderMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            menuSortAsc = new ToolStripMenuItem();
            menuSortDesc = new ToolStripMenuItem();
            menuClearSort = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            menuVisibleColumns = new ToolStripMenuItem();
            contextRowMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            menuDeleteRow = new ToolStripMenuItem();
            separatorAcceptanceRow = new ToolStripSeparator();
            menuAcceptRow = new ToolStripMenuItem();
            menuCancelAccepanceRow = new ToolStripMenuItem();
            contextRecordMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            menuId = new ToolStripMenuItem();
            separatorId = new ToolStripSeparator();
            menuCopyClipboard = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            menuCreate = new ToolStripMenuItem();
            menuEdit = new ToolStripMenuItem();
            menuDelete = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            menuAccept = new ToolStripMenuItem();
            menuCancelAcceptance = new ToolStripMenuItem();
            separatorAcceptance = new ToolStripSeparator();
            menuCopy = new ToolStripMenuItem();
            menuCreateBasedOn = new ToolStripMenuItem();
            menuCreateGroup = new ToolStripMenuItem();
            separatorDocuments = new ToolStripSeparator();
            menuDocuments = new ToolStripMenuItem();
            contextGroupMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            menuHideGroupArea = new ToolStripMenuItem();
            menuExpandAll = new ToolStripMenuItem();
            menuCollapseAll = new ToolStripMenuItem();
            menuClearGroups = new ToolStripMenuItem();
            contextGroupItemMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            menuExpandItem = new ToolStripMenuItem();
            menuCollapseItem = new ToolStripMenuItem();
            menuClearGroupItems = new ToolStripMenuItem();
            contextGroupCaptionMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            menuGroupCaptionExpand = new ToolStripMenuItem();
            menuGroupCaptionCollapse = new ToolStripMenuItem();
            panelTools = new Panel();
            toolStripSeparator8 = new ToolStripSeparator();
            menuGroupsSettings = new ToolStripMenuItem();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridContent).BeginInit();
            contextGridMenu.SuspendLayout();
            contextHeaderMenu.SuspendLayout();
            contextRowMenu.SuspendLayout();
            contextRecordMenu.SuspendLayout();
            contextGroupMenu.SuspendLayout();
            contextGroupItemMenu.SuspendLayout();
            contextGroupCaptionMenu.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { buttonCreate, buttonEdit, buttonDelete, toolStripSeparator7, buttonWipe, toolStripSeparator1, buttonCopy, buttonCreateGroup, buttonRefresh, toolStripSeparator4, buttonPrint, buttonSettings });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1067, 52);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // buttonCreate
            // 
            buttonCreate.Image = DocumentFlow.Properties.Resources.icons8_file_add_30;
            buttonCreate.ImageScaling = ToolStripItemImageScaling.None;
            buttonCreate.ImageTransparentColor = Color.Magenta;
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(54, 49);
            buttonCreate.Text = "Создать";
            buttonCreate.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonCreate.Click += ButtonCreate_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Image = DocumentFlow.Properties.Resources.icons8_file_edit_30;
            buttonEdit.ImageScaling = ToolStripItemImageScaling.None;
            buttonEdit.ImageTransparentColor = Color.Magenta;
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(65, 49);
            buttonEdit.Text = "Изменить";
            buttonEdit.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonEdit.Click += ButtonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Image = DocumentFlow.Properties.Resources.icons8_file_delete_30;
            buttonDelete.ImageScaling = ToolStripItemImageScaling.None;
            buttonDelete.ImageTransparentColor = Color.Magenta;
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(65, 49);
            buttonDelete.Text = "Пометить";
            buttonDelete.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonDelete.ToolTipText = "Пометить на удаление / снять отметку";
            buttonDelete.Click += ButtonDelete_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(6, 52);
            // 
            // buttonWipe
            // 
            buttonWipe.Image = DocumentFlow.Properties.Resources.trash_32;
            buttonWipe.ImageScaling = ToolStripItemImageScaling.None;
            buttonWipe.ImageTransparentColor = SystemColors.Window;
            buttonWipe.Name = "buttonWipe";
            buttonWipe.Size = new Size(55, 49);
            buttonWipe.Text = "Удалить";
            buttonWipe.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonWipe.Click += ButtonWipe_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 52);
            // 
            // buttonCopy
            // 
            buttonCopy.Image = DocumentFlow.Properties.Resources.icons8_copy_edit_30;
            buttonCopy.ImageScaling = ToolStripItemImageScaling.None;
            buttonCopy.ImageTransparentColor = Color.Magenta;
            buttonCopy.Name = "buttonCopy";
            buttonCopy.Size = new Size(45, 49);
            buttonCopy.Text = "Копия";
            buttonCopy.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonCopy.Click += ButtonCopy_Click;
            // 
            // buttonCreateGroup
            // 
            buttonCreateGroup.Image = DocumentFlow.Properties.Resources.icons8_folder_add_30;
            buttonCreateGroup.ImageScaling = ToolStripItemImageScaling.None;
            buttonCreateGroup.ImageTransparentColor = Color.Magenta;
            buttonCreateGroup.Name = "buttonCreateGroup";
            buttonCreateGroup.Size = new Size(50, 49);
            buttonCreateGroup.Text = "Группа";
            buttonCreateGroup.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonCreateGroup.Click += ButtonCreateGroup_Click;
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
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 52);
            // 
            // buttonPrint
            // 
            buttonPrint.DropDownItems.AddRange(new ToolStripItem[] { toolStripPrintList });
            buttonPrint.Image = DocumentFlow.Properties.Resources.icons8_print_30;
            buttonPrint.ImageScaling = ToolStripItemImageScaling.None;
            buttonPrint.ImageTransparentColor = Color.Magenta;
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new Size(62, 49);
            buttonPrint.Text = "Печать";
            buttonPrint.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // toolStripPrintList
            // 
            toolStripPrintList.Name = "toolStripPrintList";
            toolStripPrintList.Size = new Size(115, 22);
            toolStripPrintList.Text = "Список";
            toolStripPrintList.Click += ToolStripPrintList_Click;
            // 
            // buttonSettings
            // 
            buttonSettings.Image = DocumentFlow.Properties.Resources.icons8_settings_30;
            buttonSettings.ImageScaling = ToolStripItemImageScaling.None;
            buttonSettings.ImageTransparentColor = Color.Magenta;
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new Size(71, 49);
            buttonSettings.Text = "Настройки";
            buttonSettings.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonSettings.Click += ButtonSettings_Click;
            // 
            // gridContent
            // 
            gridContent.AccessibleName = "Table";
            gridContent.AllowEditing = false;
            gridContent.AllowFiltering = true;
            gridContent.AllowGrouping = false;
            gridContent.AllowResizingColumns = true;
            gridContent.AllowTriStateSorting = true;
            gridContent.AutoExpandGroups = true;
            gridContent.AutoGenerateColumns = false;
            gridContent.BackColor = SystemColors.Window;
            gridContent.ContextMenuStrip = contextGridMenu;
            gridContent.Dock = DockStyle.Fill;
            gridContent.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            gridContent.LiveDataUpdateMode = Syncfusion.Data.LiveDataUpdateMode.AllowSummaryUpdate;
            gridContent.Location = new Point(0, 152);
            gridContent.Margin = new Padding(4, 3, 4, 3);
            gridContent.Name = "gridContent";
            gridContent.ShowBusyIndicator = true;
            gridContent.ShowRowHeader = true;
            gridContent.ShowSortNumbers = true;
            gridContent.Size = new Size(1067, 404);
            gridContent.Style.BorderColor = Color.FromArgb(204, 204, 204);
            gridContent.Style.GroupDropAreaStyle.BackColor = SystemColors.Window;
            gridContent.Style.ProgressBarStyle.ForegroundStyle = Syncfusion.WinForms.DataGrid.Enums.GridProgressBarStyle.Tube;
            gridContent.Style.ProgressBarStyle.ProgressTextColor = Color.FromArgb(68, 68, 68);
            gridContent.Style.ProgressBarStyle.TubeForegroundEndColor = Color.White;
            gridContent.Style.ProgressBarStyle.TubeForegroundStartColor = Color.SkyBlue;
            gridContent.Style.TableSummaryRowStyle.Font.Size = 9F;
            gridContent.TabIndex = 14;
            gridContent.ToolTipOpening += GridContent_ToolTipOpening;
            gridContent.ContextMenuOpening += GridContent_ContextMenuOpening;
            gridContent.QueryCellStyle += GridContent_QueryCellStyle;
            gridContent.CellDoubleClick += GridContent_CellDoubleClick;
            gridContent.QueryImageCellStyle += GridContent_QueryImageCellStyle;
            // 
            // contextGridMenu
            // 
            contextGridMenu.DropShadowEnabled = false;
            contextGridMenu.Items.AddRange(new ToolStripItem[] { menuAddRecord, toolStripSeparator2, menuCreateGroup2 });
            contextGridMenu.MetroColor = Color.FromArgb(204, 236, 249);
            contextGridMenu.Name = "contextGridMenu";
            contextGridMenu.Size = new Size(184, 54);
            contextGridMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            contextGridMenu.ThemeName = "Metro";
            // 
            // menuAddRecord
            // 
            menuAddRecord.Image = DocumentFlow.Properties.Resources.icons8_file_add_16;
            menuAddRecord.Name = "menuAddRecord";
            menuAddRecord.ShortcutKeys = Keys.Alt | Keys.Insert;
            menuAddRecord.Size = new Size(183, 22);
            menuAddRecord.Text = "Создать";
            menuAddRecord.Click += ButtonCreate_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(180, 6);
            // 
            // menuCreateGroup2
            // 
            menuCreateGroup2.Image = DocumentFlow.Properties.Resources.icons8_folder_add_16;
            menuCreateGroup2.Name = "menuCreateGroup2";
            menuCreateGroup2.Size = new Size(183, 22);
            menuCreateGroup2.Text = "Группа";
            menuCreateGroup2.Click += ButtonCreateGroup_Click;
            // 
            // contextHeaderMenu
            // 
            contextHeaderMenu.DropShadowEnabled = false;
            contextHeaderMenu.Items.AddRange(new ToolStripItem[] { menuSortAsc, menuSortDesc, menuClearSort, toolStripSeparator3, menuVisibleColumns });
            contextHeaderMenu.MetroColor = Color.FromArgb(204, 236, 249);
            contextHeaderMenu.Name = "contextHeaderMenu";
            contextHeaderMenu.Size = new Size(233, 98);
            contextHeaderMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            contextHeaderMenu.ThemeName = "Metro";
            // 
            // menuSortAsc
            // 
            menuSortAsc.Image = DocumentFlow.Properties.Resources.icons8_sort_by_asc_16;
            menuSortAsc.Name = "menuSortAsc";
            menuSortAsc.Size = new Size(232, 22);
            menuSortAsc.Text = "Сортировка по возрастанию";
            menuSortAsc.Click += MenuSortAsc_Click;
            // 
            // menuSortDesc
            // 
            menuSortDesc.Image = DocumentFlow.Properties.Resources.icons8_sort_by_desc_16;
            menuSortDesc.Name = "menuSortDesc";
            menuSortDesc.Size = new Size(232, 22);
            menuSortDesc.Text = "Сортировка по убыванию";
            menuSortDesc.Click += MenuSortDesc_Click;
            // 
            // menuClearSort
            // 
            menuClearSort.Name = "menuClearSort";
            menuClearSort.Size = new Size(232, 22);
            menuClearSort.Text = "Убрать сортировку";
            menuClearSort.Click += MenuClearSort_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(229, 6);
            // 
            // menuVisibleColumns
            // 
            menuVisibleColumns.Name = "menuVisibleColumns";
            menuVisibleColumns.Size = new Size(232, 22);
            menuVisibleColumns.Text = "Видимые столбцы";
            // 
            // contextRowMenu
            // 
            contextRowMenu.DropShadowEnabled = false;
            contextRowMenu.Items.AddRange(new ToolStripItem[] { menuDeleteRow, separatorAcceptanceRow, menuAcceptRow, menuCancelAccepanceRow });
            contextRowMenu.MetroColor = Color.FromArgb(204, 236, 249);
            contextRowMenu.Name = "contextRowMenu";
            contextRowMenu.Size = new Size(355, 76);
            contextRowMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            contextRowMenu.ThemeName = "Metro";
            // 
            // menuDeleteRow
            // 
            menuDeleteRow.Image = DocumentFlow.Properties.Resources.icons8_file_delete_16;
            menuDeleteRow.Name = "menuDeleteRow";
            menuDeleteRow.ShortcutKeys = Keys.Alt | Keys.Delete;
            menuDeleteRow.Size = new Size(354, 22);
            menuDeleteRow.Text = "Пометить на удаление / снять отметку";
            menuDeleteRow.Click += ButtonDelete_Click;
            // 
            // separatorAcceptanceRow
            // 
            separatorAcceptanceRow.Name = "separatorAcceptanceRow";
            separatorAcceptanceRow.Size = new Size(351, 6);
            // 
            // menuAcceptRow
            // 
            menuAcceptRow.Image = DocumentFlow.Properties.Resources.icons8_accept_16;
            menuAcceptRow.Name = "menuAcceptRow";
            menuAcceptRow.Size = new Size(354, 22);
            menuAcceptRow.Text = "Провести";
            menuAcceptRow.Click += MenuAcceptRow_Click;
            // 
            // menuCancelAccepanceRow
            // 
            menuCancelAccepanceRow.Image = DocumentFlow.Properties.Resources.icons8_cancel_16;
            menuCancelAccepanceRow.Name = "menuCancelAccepanceRow";
            menuCancelAccepanceRow.Size = new Size(354, 22);
            menuCancelAccepanceRow.Text = "Отменить проведение";
            menuCancelAccepanceRow.Click += MenuCancelAccepanceRow_Click;
            // 
            // contextRecordMenu
            // 
            contextRecordMenu.DropShadowEnabled = false;
            contextRecordMenu.Items.AddRange(new ToolStripItem[] { menuId, separatorId, menuCopyClipboard, toolStripSeparator6, menuCreate, menuEdit, menuDelete, toolStripSeparator5, menuAccept, menuCancelAcceptance, separatorAcceptance, menuCopy, menuCreateBasedOn, menuCreateGroup, separatorDocuments, menuDocuments });
            contextRecordMenu.MetroColor = Color.FromArgb(204, 236, 249);
            contextRecordMenu.Name = "contextGridMenu";
            contextRecordMenu.Size = new Size(355, 276);
            contextRecordMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            contextRecordMenu.ThemeName = "Metro";
            contextRecordMenu.Opening += ContextRecordMenu_Opening;
            // 
            // menuId
            // 
            menuId.Name = "menuId";
            menuId.Size = new Size(354, 22);
            menuId.Text = "id";
            menuId.Click += MenuId_Click;
            // 
            // separatorId
            // 
            separatorId.Name = "separatorId";
            separatorId.Size = new Size(351, 6);
            // 
            // menuCopyClipboard
            // 
            menuCopyClipboard.Image = DocumentFlow.Properties.Resources.icons8_copy_16;
            menuCopyClipboard.Name = "menuCopyClipboard";
            menuCopyClipboard.ShortcutKeys = Keys.Control | Keys.C;
            menuCopyClipboard.Size = new Size(354, 22);
            menuCopyClipboard.Text = "Копировать";
            menuCopyClipboard.Click += MenuCopyClipboard_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(351, 6);
            // 
            // menuCreate
            // 
            menuCreate.Image = DocumentFlow.Properties.Resources.icons8_file_add_16;
            menuCreate.Name = "menuCreate";
            menuCreate.ShortcutKeys = Keys.Alt | Keys.Insert;
            menuCreate.Size = new Size(354, 22);
            menuCreate.Text = "Создать";
            menuCreate.Click += ButtonCreate_Click;
            // 
            // menuEdit
            // 
            menuEdit.Image = DocumentFlow.Properties.Resources.icons8_file_edit_16;
            menuEdit.Name = "menuEdit";
            menuEdit.Size = new Size(354, 22);
            menuEdit.Text = "Изменить";
            menuEdit.Click += ButtonEdit_Click;
            // 
            // menuDelete
            // 
            menuDelete.Image = DocumentFlow.Properties.Resources.icons8_file_delete_16;
            menuDelete.Name = "menuDelete";
            menuDelete.ShortcutKeys = Keys.Alt | Keys.Delete;
            menuDelete.Size = new Size(354, 22);
            menuDelete.Text = "Пометить на удаление / снять отметку";
            menuDelete.Click += ButtonDelete_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(351, 6);
            // 
            // menuAccept
            // 
            menuAccept.Image = DocumentFlow.Properties.Resources.icons8_accept_16;
            menuAccept.Name = "menuAccept";
            menuAccept.Size = new Size(354, 22);
            menuAccept.Text = "Провести";
            menuAccept.Click += MenuAcceptRow_Click;
            // 
            // menuCancelAcceptance
            // 
            menuCancelAcceptance.Image = DocumentFlow.Properties.Resources.icons8_cancel_16;
            menuCancelAcceptance.Name = "menuCancelAcceptance";
            menuCancelAcceptance.Size = new Size(354, 22);
            menuCancelAcceptance.Text = "Отменить проведение";
            menuCancelAcceptance.Click += MenuCancelAccepanceRow_Click;
            // 
            // separatorAcceptance
            // 
            separatorAcceptance.Name = "separatorAcceptance";
            separatorAcceptance.Size = new Size(351, 6);
            // 
            // menuCopy
            // 
            menuCopy.Image = DocumentFlow.Properties.Resources.icons8_copy_edit_16;
            menuCopy.Name = "menuCopy";
            menuCopy.ShortcutKeys = Keys.Control | Keys.Alt | Keys.Insert;
            menuCopy.Size = new Size(354, 22);
            menuCopy.Text = "Создать копию";
            menuCopy.Click += ButtonCopy_Click;
            // 
            // menuCreateBasedOn
            // 
            menuCreateBasedOn.Name = "menuCreateBasedOn";
            menuCreateBasedOn.Size = new Size(354, 22);
            menuCreateBasedOn.Text = "Создать на основании";
            // 
            // menuCreateGroup
            // 
            menuCreateGroup.Image = DocumentFlow.Properties.Resources.icons8_folder_add_16;
            menuCreateGroup.Name = "menuCreateGroup";
            menuCreateGroup.Size = new Size(354, 22);
            menuCreateGroup.Text = "Группа";
            menuCreateGroup.Click += ButtonCreateGroup_Click;
            // 
            // separatorDocuments
            // 
            separatorDocuments.Name = "separatorDocuments";
            separatorDocuments.Size = new Size(351, 6);
            // 
            // menuDocuments
            // 
            menuDocuments.Name = "menuDocuments";
            menuDocuments.Size = new Size(354, 22);
            menuDocuments.Text = "Документы";
            // 
            // contextGroupMenu
            // 
            contextGroupMenu.DropShadowEnabled = false;
            contextGroupMenu.Items.AddRange(new ToolStripItem[] { menuHideGroupArea, menuExpandAll, menuCollapseAll, menuClearGroups, toolStripSeparator8, menuGroupsSettings });
            contextGroupMenu.MetroColor = Color.FromArgb(204, 236, 249);
            contextGroupMenu.Name = "contextGroupMenu";
            contextGroupMenu.Size = new Size(238, 142);
            contextGroupMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            contextGroupMenu.ThemeName = "Metro";
            // 
            // menuHideGroupArea
            // 
            menuHideGroupArea.Name = "menuHideGroupArea";
            menuHideGroupArea.Size = new Size(237, 22);
            menuHideGroupArea.Text = "Скрыть область группировки";
            menuHideGroupArea.Click += MenuHideGroupArea_Click;
            // 
            // menuExpandAll
            // 
            menuExpandAll.Name = "menuExpandAll";
            menuExpandAll.Size = new Size(237, 22);
            menuExpandAll.Text = "Развернуть все";
            menuExpandAll.Click += MenuExpandAll_Click;
            // 
            // menuCollapseAll
            // 
            menuCollapseAll.Name = "menuCollapseAll";
            menuCollapseAll.Size = new Size(237, 22);
            menuCollapseAll.Text = "Свернуть все";
            menuCollapseAll.Click += MenuCollapseAll_Click;
            // 
            // menuClearGroups
            // 
            menuClearGroups.Name = "menuClearGroups";
            menuClearGroups.Size = new Size(237, 22);
            menuClearGroups.Text = "Очистить";
            menuClearGroups.Click += MenuClearGroups_Click;
            // 
            // contextGroupItemMenu
            // 
            contextGroupItemMenu.DropShadowEnabled = false;
            contextGroupItemMenu.Items.AddRange(new ToolStripItem[] { menuExpandItem, menuCollapseItem, menuClearGroupItems });
            contextGroupItemMenu.MetroColor = Color.FromArgb(204, 236, 249);
            contextGroupItemMenu.Name = "contextGroupItemMenu";
            contextGroupItemMenu.Size = new Size(157, 70);
            contextGroupItemMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            contextGroupItemMenu.ThemeName = "Metro";
            // 
            // menuExpandItem
            // 
            menuExpandItem.Name = "menuExpandItem";
            menuExpandItem.Size = new Size(156, 22);
            menuExpandItem.Text = "Развернуть все";
            menuExpandItem.Click += MenuExpandItem_Click;
            // 
            // menuCollapseItem
            // 
            menuCollapseItem.Name = "menuCollapseItem";
            menuCollapseItem.Size = new Size(156, 22);
            menuCollapseItem.Text = "Свернуть все";
            menuCollapseItem.Click += MenuCollapseItem_Click;
            // 
            // menuClearGroupItems
            // 
            menuClearGroupItems.Name = "menuClearGroupItems";
            menuClearGroupItems.Size = new Size(156, 22);
            menuClearGroupItems.Text = "Очистить";
            menuClearGroupItems.Click += MenuClearGroupItems_Click;
            // 
            // contextGroupCaptionMenu
            // 
            contextGroupCaptionMenu.DropShadowEnabled = false;
            contextGroupCaptionMenu.Items.AddRange(new ToolStripItem[] { menuGroupCaptionExpand, menuGroupCaptionCollapse });
            contextGroupCaptionMenu.MetroColor = Color.FromArgb(204, 236, 249);
            contextGroupCaptionMenu.Name = "contextGroupCaptionMenu";
            contextGroupCaptionMenu.Size = new Size(136, 48);
            contextGroupCaptionMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            contextGroupCaptionMenu.ThemeName = "Metro";
            // 
            // menuGroupCaptionExpand
            // 
            menuGroupCaptionExpand.Name = "menuGroupCaptionExpand";
            menuGroupCaptionExpand.Size = new Size(135, 22);
            menuGroupCaptionExpand.Text = "Развернуть";
            menuGroupCaptionExpand.Click += MenuGroupCaptionExpand_Click;
            // 
            // menuGroupCaptionCollapse
            // 
            menuGroupCaptionCollapse.Name = "menuGroupCaptionCollapse";
            menuGroupCaptionCollapse.Size = new Size(135, 22);
            menuGroupCaptionCollapse.Text = "Свернуть";
            menuGroupCaptionCollapse.Click += MenuGroupCaptionCollapse_Click;
            // 
            // panelTools
            // 
            panelTools.Dock = DockStyle.Top;
            panelTools.Location = new Point(0, 52);
            panelTools.Name = "panelTools";
            panelTools.Size = new Size(1067, 100);
            panelTools.TabIndex = 15;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(234, 6);
            // 
            // menuGroupsSettings
            // 
            menuGroupsSettings.Name = "menuGroupsSettings";
            menuGroupsSettings.Size = new Size(237, 22);
            menuGroupsSettings.Text = "Настройка...";
            menuGroupsSettings.Click += MenuGroupsSettings_Click;
            // 
            // BrowserPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridContent);
            Controls.Add(panelTools);
            Controls.Add(toolStrip1);
            Name = "BrowserPage";
            Size = new Size(1067, 556);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridContent).EndInit();
            contextGridMenu.ResumeLayout(false);
            contextHeaderMenu.ResumeLayout(false);
            contextRowMenu.ResumeLayout(false);
            contextRecordMenu.ResumeLayout(false);
            contextGroupMenu.ResumeLayout(false);
            contextGroupItemMenu.ResumeLayout(false);
            contextGroupCaptionMenu.ResumeLayout(false);
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
        private ToolStripMenuItem menuCreateBasedOn;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem menuGroupsSettings;
    }
}
