namespace DocumentFlow.ViewModels
{
    partial class ProductionLotEditor
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
            panel1 = new Panel();
            comboOrg = new Controls.Editors.DfComboBox();
            dateDocument = new Controls.Editors.DfDateTimePicker();
            textDocNumber = new Controls.Editors.DfIntegerTextBox();
            lineSplitter1 = new Controls.LineSplitter();
            textQuantity = new Controls.Editors.DfNumericTextBox();
            comboGoods = new Controls.Editors.DfComboBox();
            contextMenuStripEx1 = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            menuCreateOperation = new ToolStripMenuItem();
            gridContent = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            selectOrder = new Controls.Editors.DfDocumentSelectBox();
            comboCalc = new Controls.Editors.DfComboBox();
            panel1.SuspendLayout();
            contextMenuStripEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridContent).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(comboOrg);
            panel1.Controls.Add(dateDocument);
            panel1.Controls.Add(textDocNumber);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(937, 32);
            panel1.TabIndex = 1;
            // 
            // comboOrg
            // 
            comboOrg.DisplayMember = "ItemName";
            comboOrg.Dock = DockStyle.Left;
            comboOrg.EditorFitToSize = false;
            comboOrg.EditorWidth = 200;
            comboOrg.EnabledEditor = true;
            comboOrg.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboOrg.Header = "Организация";
            comboOrg.HeaderAutoSize = false;
            comboOrg.HeaderDock = DockStyle.Left;
            comboOrg.HeaderTextAlign = ContentAlignment.TopLeft;
            comboOrg.HeaderVisible = true;
            comboOrg.HeaderWidth = 100;
            comboOrg.Location = new Point(365, 0);
            comboOrg.Margin = new Padding(3, 4, 3, 4);
            comboOrg.Name = "comboOrg";
            comboOrg.Padding = new Padding(0, 0, 0, 7);
            comboOrg.ShowDeleteButton = true;
            comboOrg.ShowOpenButton = false;
            comboOrg.Size = new Size(305, 32);
            comboOrg.TabIndex = 2;
            // 
            // dateDocument
            // 
            dateDocument.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateDocument.Dock = DockStyle.Left;
            dateDocument.EditorFitToSize = false;
            dateDocument.EditorWidth = 170;
            dateDocument.EnabledEditor = true;
            dateDocument.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateDocument.Format = DateTimePickerFormat.Custom;
            dateDocument.Header = "от";
            dateDocument.HeaderAutoSize = false;
            dateDocument.HeaderDock = DockStyle.Left;
            dateDocument.HeaderTextAlign = ContentAlignment.TopLeft;
            dateDocument.HeaderVisible = true;
            dateDocument.HeaderWidth = 25;
            dateDocument.Location = new Point(165, 0);
            dateDocument.Margin = new Padding(3, 5, 3, 5);
            dateDocument.Name = "dateDocument";
            dateDocument.Padding = new Padding(0, 0, 0, 7);
            dateDocument.Required = true;
            dateDocument.Size = new Size(200, 32);
            dateDocument.TabIndex = 1;
            // 
            // textDocNumber
            // 
            textDocNumber.Dock = DockStyle.Left;
            textDocNumber.EditorFitToSize = false;
            textDocNumber.EditorWidth = 100;
            textDocNumber.EnabledEditor = true;
            textDocNumber.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textDocNumber.Header = "Номер";
            textDocNumber.HeaderAutoSize = false;
            textDocNumber.HeaderDock = DockStyle.Left;
            textDocNumber.HeaderTextAlign = ContentAlignment.TopLeft;
            textDocNumber.HeaderVisible = true;
            textDocNumber.HeaderWidth = 60;
            textDocNumber.IntegerValue = 0L;
            textDocNumber.Location = new Point(0, 0);
            textDocNumber.Margin = new Padding(3, 4, 3, 4);
            textDocNumber.Name = "textDocNumber";
            textDocNumber.NumberGroupSeparator = " ";
            textDocNumber.NumberGroupSizes = new int[] { 0 };
            textDocNumber.Padding = new Padding(0, 0, 0, 7);
            textDocNumber.ShowSuffix = false;
            textDocNumber.Size = new Size(165, 32);
            textDocNumber.Suffix = "суффикс";
            textDocNumber.TabIndex = 0;
            // 
            // lineSplitter1
            // 
            lineSplitter1.Dock = DockStyle.Top;
            lineSplitter1.LineColor = Color.FromArgb(22, 165, 220);
            lineSplitter1.LinePlacement = DocumentFlow.Controls.LinePlacement.HorizontalTop;
            lineSplitter1.LineSize = 3;
            lineSplitter1.Location = new Point(0, 32);
            lineSplitter1.Name = "lineSplitter1";
            lineSplitter1.Size = new Size(937, 12);
            lineSplitter1.TabIndex = 5;
            lineSplitter1.Text = "lineSplitter1";
            // 
            // textQuantity
            // 
            textQuantity.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textQuantity.Dock = DockStyle.Top;
            textQuantity.EditorFitToSize = false;
            textQuantity.EditorWidth = 100;
            textQuantity.EnabledEditor = true;
            textQuantity.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textQuantity.Header = "Количество";
            textQuantity.HeaderAutoSize = false;
            textQuantity.HeaderDock = DockStyle.Left;
            textQuantity.HeaderTextAlign = ContentAlignment.TopLeft;
            textQuantity.HeaderVisible = true;
            textQuantity.HeaderWidth = 100;
            textQuantity.Location = new Point(0, 140);
            textQuantity.Margin = new Padding(3, 4, 3, 4);
            textQuantity.Name = "textQuantity";
            textQuantity.NumberDecimalDigits = 3;
            textQuantity.Padding = new Padding(0, 0, 0, 7);
            textQuantity.ShowSuffix = false;
            textQuantity.Size = new Size(937, 32);
            textQuantity.Suffix = "суффикс";
            textQuantity.TabIndex = 7;
            // 
            // comboGoods
            // 
            comboGoods.DisplayMember = "ItemName";
            comboGoods.Dock = DockStyle.Top;
            comboGoods.EditorFitToSize = false;
            comboGoods.EditorWidth = 500;
            comboGoods.EnabledEditor = true;
            comboGoods.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboGoods.Header = "Изделие";
            comboGoods.HeaderAutoSize = false;
            comboGoods.HeaderDock = DockStyle.Left;
            comboGoods.HeaderTextAlign = ContentAlignment.TopLeft;
            comboGoods.HeaderVisible = true;
            comboGoods.HeaderWidth = 100;
            comboGoods.Location = new Point(0, 76);
            comboGoods.Margin = new Padding(3, 4, 3, 4);
            comboGoods.Name = "comboGoods";
            comboGoods.Padding = new Padding(0, 0, 0, 7);
            comboGoods.ShowDeleteButton = true;
            comboGoods.ShowOpenButton = true;
            comboGoods.Size = new Size(937, 32);
            comboGoods.TabIndex = 8;
            comboGoods.OpenButtonClick += ComboGoods_OpenButtonClick;
            comboGoods.SelectedItemChanged += ComboGoods_SelectedItemChanged;
            // 
            // contextMenuStripEx1
            // 
            contextMenuStripEx1.DropShadowEnabled = false;
            contextMenuStripEx1.Items.AddRange(new ToolStripItem[] { menuCreateOperation });
            contextMenuStripEx1.MetroColor = Color.FromArgb(204, 236, 249);
            contextMenuStripEx1.Name = "contextMenuStripEx1";
            contextMenuStripEx1.Size = new Size(265, 26);
            contextMenuStripEx1.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Office2016Colorful;
            contextMenuStripEx1.ThemeName = "Office2016Colorful";
            // 
            // menuCreateOperation
            // 
            menuCreateOperation.Image = Properties.Resources.icons8_robot_16;
            menuCreateOperation.Name = "menuCreateOperation";
            menuCreateOperation.Size = new Size(264, 22);
            menuCreateOperation.Text = "Добавить выполненные операции";
            menuCreateOperation.Click += MenuCreateOperation_Click;
            // 
            // gridContent
            // 
            gridContent.AccessibleName = "Table";
            gridContent.AllowEditing = false;
            gridContent.AllowGrouping = false;
            gridContent.AllowResizingColumns = true;
            gridContent.AllowSorting = false;
            gridContent.AutoGenerateColumns = false;
            gridContent.ContextMenuStrip = contextMenuStripEx1;
            gridContent.Dock = DockStyle.Fill;
            gridContent.Location = new Point(0, 172);
            gridContent.Name = "gridContent";
            gridContent.Size = new Size(937, 345);
            gridContent.Style.BorderColor = Color.FromArgb(204, 204, 204);
            gridContent.Style.CheckBoxStyle.CheckedBackColor = Color.FromArgb(0, 120, 215);
            gridContent.Style.CheckBoxStyle.CheckedBorderColor = Color.FromArgb(0, 120, 215);
            gridContent.Style.CheckBoxStyle.IndeterminateBorderColor = Color.FromArgb(0, 120, 215);
            gridContent.Style.HyperlinkStyle.DefaultLinkColor = Color.FromArgb(0, 120, 215);
            gridContent.TabIndex = 10;
            gridContent.QueryCellStyle += GridContent_QueryCellStyle;
            gridContent.CellDoubleClick += GridContent_CellDoubleClick;
            // 
            // selectOrder
            // 
            selectOrder.DisableCurrentItem = false;
            selectOrder.Dock = DockStyle.Top;
            selectOrder.EditorFitToSize = false;
            selectOrder.EditorWidth = 300;
            selectOrder.EnabledEditor = true;
            selectOrder.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectOrder.Header = "Заказ";
            selectOrder.HeaderAutoSize = false;
            selectOrder.HeaderDock = DockStyle.Left;
            selectOrder.HeaderTextAlign = ContentAlignment.TopLeft;
            selectOrder.HeaderVisible = true;
            selectOrder.HeaderWidth = 100;
            selectOrder.Location = new Point(0, 44);
            selectOrder.Margin = new Padding(3, 4, 3, 4);
            selectOrder.Name = "selectOrder";
            selectOrder.Padding = new Padding(0, 0, 0, 7);
            selectOrder.ShowDeleteButton = true;
            selectOrder.ShowOpenButton = true;
            selectOrder.Size = new Size(937, 32);
            selectOrder.TabIndex = 11;
            selectOrder.DocumentDialogColumns += SelectOrder_DocumentDialogColumns;
            selectOrder.OpenButtonClick += SelectOrder_OpenButtonClick;
            selectOrder.SelectedItemChanged += SelectOrder_SelectedItemChanged;
            // 
            // comboCalc
            // 
            comboCalc.DisplayMember = "Code";
            comboCalc.Dock = DockStyle.Top;
            comboCalc.EditorFitToSize = false;
            comboCalc.EditorWidth = 300;
            comboCalc.EnabledEditor = true;
            comboCalc.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboCalc.Header = "Калькуляция";
            comboCalc.HeaderAutoSize = false;
            comboCalc.HeaderDock = DockStyle.Left;
            comboCalc.HeaderTextAlign = ContentAlignment.TopLeft;
            comboCalc.HeaderVisible = true;
            comboCalc.HeaderWidth = 100;
            comboCalc.Location = new Point(0, 108);
            comboCalc.Margin = new Padding(3, 4, 3, 4);
            comboCalc.Name = "comboCalc";
            comboCalc.Padding = new Padding(0, 0, 0, 7);
            comboCalc.ShowDeleteButton = false;
            comboCalc.ShowOpenButton = true;
            comboCalc.Size = new Size(937, 32);
            comboCalc.TabIndex = 12;
            comboCalc.OpenButtonClick += ComboCalc_OpenButtonClick;
            // 
            // ProductionLotEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ContextMenuStrip = contextMenuStripEx1;
            Controls.Add(gridContent);
            Controls.Add(textQuantity);
            Controls.Add(comboCalc);
            Controls.Add(comboGoods);
            Controls.Add(selectOrder);
            Controls.Add(lineSplitter1);
            Controls.Add(panel1);
            Name = "ProductionLotEditor";
            Padding = new Padding(0, 0, 0, 7);
            Size = new Size(937, 524);
            panel1.ResumeLayout(false);
            contextMenuStripEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridContent).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Controls.Editors.DfComboBox comboOrg;
        private Controls.Editors.DfDateTimePicker dateDocument;
        private Controls.Editors.DfIntegerTextBox textDocNumber;
        private Controls.LineSplitter lineSplitter1;
        private Controls.Editors.DfNumericTextBox textQuantity;
        private Controls.Editors.DfComboBox comboGoods;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextMenuStripEx1;
        private ToolStripMenuItem menuCreateOperation;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridContent;
        private Controls.Editors.DfDocumentSelectBox selectOrder;
        private Controls.Editors.DfComboBox comboCalc;
    }
}
