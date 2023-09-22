namespace DocumentFlow.ViewModels
{
    partial class FinishedGoodsEditor
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
            selectLot = new Controls.Editors.DfDocumentSelectBox();
            selectGoods = new Controls.Editors.DfDirectorySelectBox();
            textQuantity = new Controls.Editors.DfNumericTextBox();
            textPrice = new Controls.Editors.DfCurrencyTextBox();
            textProductCost = new Controls.Editors.DfCurrencyTextBox();
            panel1.SuspendLayout();
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
            panel1.Size = new Size(685, 32);
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
            lineSplitter1.Size = new Size(685, 12);
            lineSplitter1.TabIndex = 5;
            lineSplitter1.Text = "lineSplitter1";
            // 
            // selectLot
            // 
            selectLot.DisableCurrentItem = true;
            selectLot.Dock = DockStyle.Top;
            selectLot.EditorFitToSize = false;
            selectLot.EditorWidth = 300;
            selectLot.EnabledEditor = true;
            selectLot.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectLot.Header = "Партия";
            selectLot.HeaderAutoSize = false;
            selectLot.HeaderDock = DockStyle.Left;
            selectLot.HeaderTextAlign = ContentAlignment.TopLeft;
            selectLot.HeaderVisible = true;
            selectLot.HeaderWidth = 160;
            selectLot.Location = new Point(0, 44);
            selectLot.Margin = new Padding(3, 4, 3, 4);
            selectLot.Name = "selectLot";
            selectLot.Padding = new Padding(0, 0, 0, 7);
            selectLot.ShowDeleteButton = true;
            selectLot.ShowOpenButton = true;
            selectLot.Size = new Size(685, 32);
            selectLot.TabIndex = 6;
            selectLot.DocumentDialogColumns += SelectLot_DocumentDialogColumns;
            selectLot.OpenButtonClick += SelectLot_OpenButtonClick;
            selectLot.SelectedItemChanged += SelectLot_SelectedItemChanged;
            selectLot.UserDocumentModified += SelectLot_UserDocumentModified;
            // 
            // selectGoods
            // 
            selectGoods.CanSelectFolder = false;
            selectGoods.Dock = DockStyle.Top;
            selectGoods.EditorFitToSize = false;
            selectGoods.EditorWidth = 500;
            selectGoods.EnabledEditor = true;
            selectGoods.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectGoods.Header = "Изделие";
            selectGoods.HeaderAutoSize = false;
            selectGoods.HeaderDock = DockStyle.Left;
            selectGoods.HeaderTextAlign = ContentAlignment.TopLeft;
            selectGoods.HeaderVisible = true;
            selectGoods.HeaderWidth = 160;
            selectGoods.Location = new Point(0, 76);
            selectGoods.Margin = new Padding(3, 4, 3, 4);
            selectGoods.Name = "selectGoods";
            selectGoods.Padding = new Padding(0, 0, 0, 7);
            selectGoods.RemoveEmptyFolders = false;
            selectGoods.ShowDeleteButton = false;
            selectGoods.ShowOpenButton = true;
            selectGoods.Size = new Size(685, 32);
            selectGoods.TabIndex = 7;
            selectGoods.OpenButtonClick += SelectGoods_OpenButtonClick;
            selectGoods.SelectedItemChanged += SelectGoods_SelectedItemChanged;
            selectGoods.UserDocumentModified += SelectGoods_UserDocumentModified;
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
            textQuantity.HeaderWidth = 160;
            textQuantity.Location = new Point(0, 108);
            textQuantity.Margin = new Padding(3, 4, 3, 4);
            textQuantity.Name = "textQuantity";
            textQuantity.NumberDecimalDigits = 2;
            textQuantity.Padding = new Padding(0, 0, 0, 7);
            textQuantity.ShowSuffix = true;
            textQuantity.Size = new Size(685, 32);
            textQuantity.Suffix = "шт.";
            textQuantity.TabIndex = 8;
            // 
            // textPrice
            // 
            textPrice.CurrencyDecimalDigits = 2;
            textPrice.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textPrice.Dock = DockStyle.Top;
            textPrice.EditorFitToSize = false;
            textPrice.EditorWidth = 100;
            textPrice.EnabledEditor = true;
            textPrice.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPrice.Header = "Себестоимость 1 ед. изм.";
            textPrice.HeaderAutoSize = false;
            textPrice.HeaderDock = DockStyle.Left;
            textPrice.HeaderTextAlign = ContentAlignment.TopLeft;
            textPrice.HeaderVisible = true;
            textPrice.HeaderWidth = 160;
            textPrice.Location = new Point(0, 140);
            textPrice.Margin = new Padding(3, 4, 3, 4);
            textPrice.Name = "textPrice";
            textPrice.Padding = new Padding(0, 0, 0, 7);
            textPrice.Size = new Size(685, 32);
            textPrice.TabIndex = 9;
            // 
            // textProductCost
            // 
            textProductCost.CurrencyDecimalDigits = 2;
            textProductCost.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textProductCost.Dock = DockStyle.Top;
            textProductCost.EditorFitToSize = false;
            textProductCost.EditorWidth = 100;
            textProductCost.EnabledEditor = true;
            textProductCost.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textProductCost.Header = "Себестоимость (всего)";
            textProductCost.HeaderAutoSize = false;
            textProductCost.HeaderDock = DockStyle.Left;
            textProductCost.HeaderTextAlign = ContentAlignment.TopLeft;
            textProductCost.HeaderVisible = true;
            textProductCost.HeaderWidth = 160;
            textProductCost.Location = new Point(0, 172);
            textProductCost.Margin = new Padding(3, 4, 3, 4);
            textProductCost.Name = "textProductCost";
            textProductCost.Padding = new Padding(0, 0, 0, 7);
            textProductCost.Size = new Size(685, 32);
            textProductCost.TabIndex = 10;
            // 
            // FinishedGoodsEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textProductCost);
            Controls.Add(textPrice);
            Controls.Add(textQuantity);
            Controls.Add(selectGoods);
            Controls.Add(selectLot);
            Controls.Add(lineSplitter1);
            Controls.Add(panel1);
            Name = "FinishedGoodsEditor";
            Size = new Size(685, 218);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Controls.Editors.DfComboBox comboOrg;
        private Controls.Editors.DfDateTimePicker dateDocument;
        private Controls.Editors.DfIntegerTextBox textDocNumber;
        private Controls.LineSplitter lineSplitter1;
        private Controls.Editors.DfDocumentSelectBox selectLot;
        private Controls.Editors.DfDirectorySelectBox selectGoods;
        private Controls.Editors.DfNumericTextBox textQuantity;
        private Controls.Editors.DfCurrencyTextBox textPrice;
        private Controls.Editors.DfCurrencyTextBox textProductCost;
    }
}
