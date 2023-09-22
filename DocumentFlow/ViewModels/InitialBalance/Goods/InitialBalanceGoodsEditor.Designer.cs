namespace DocumentFlow.ViewModels
{
    partial class InitialBalanceGoodsEditor
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
            selectGoods = new Controls.Editors.DfDirectorySelectBox();
            textAmount = new Controls.Editors.DfNumericTextBox();
            textOperationSumma = new Controls.Editors.DfCurrencyTextBox();
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
            panel1.Size = new Size(1054, 32);
            panel1.TabIndex = 1;
            // 
            // comboOrg
            // 
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
            lineSplitter1.Size = new Size(1054, 12);
            lineSplitter1.TabIndex = 5;
            lineSplitter1.Text = "lineSplitter1";
            // 
            // selectGoods
            // 
            selectGoods.CanSelectFolder = false;
            selectGoods.Dock = DockStyle.Top;
            selectGoods.EditorFitToSize = false;
            selectGoods.EditorWidth = 400;
            selectGoods.EnabledEditor = true;
            selectGoods.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectGoods.Header = "Изделие";
            selectGoods.HeaderAutoSize = false;
            selectGoods.HeaderDock = DockStyle.Left;
            selectGoods.HeaderTextAlign = ContentAlignment.TopLeft;
            selectGoods.HeaderVisible = true;
            selectGoods.HeaderWidth = 100;
            selectGoods.Location = new Point(0, 44);
            selectGoods.Margin = new Padding(3, 4, 3, 4);
            selectGoods.Name = "selectGoods";
            selectGoods.Padding = new Padding(0, 0, 0, 7);
            selectGoods.RemoveEmptyFolders = false;
            selectGoods.ShowDeleteButton = true;
            selectGoods.ShowOpenButton = true;
            selectGoods.Size = new Size(1054, 32);
            selectGoods.TabIndex = 6;
            selectGoods.OpenButtonClick += SelectGoods_OpenButtonClick;
            // 
            // textAmount
            // 
            textAmount.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textAmount.Dock = DockStyle.Top;
            textAmount.EditorFitToSize = false;
            textAmount.EditorWidth = 150;
            textAmount.EnabledEditor = true;
            textAmount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textAmount.Header = "Количество";
            textAmount.HeaderAutoSize = false;
            textAmount.HeaderDock = DockStyle.Left;
            textAmount.HeaderTextAlign = ContentAlignment.TopLeft;
            textAmount.HeaderVisible = true;
            textAmount.HeaderWidth = 100;
            textAmount.Location = new Point(0, 76);
            textAmount.Margin = new Padding(3, 4, 3, 4);
            textAmount.Name = "textAmount";
            textAmount.NumberDecimalDigits = 3;
            textAmount.Padding = new Padding(0, 0, 0, 7);
            textAmount.ShowSuffix = false;
            textAmount.Size = new Size(1054, 32);
            textAmount.Suffix = "суффикс";
            textAmount.TabIndex = 7;
            // 
            // textOperationSumma
            // 
            textOperationSumma.CurrencyDecimalDigits = 2;
            textOperationSumma.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textOperationSumma.Dock = DockStyle.Top;
            textOperationSumma.EditorFitToSize = false;
            textOperationSumma.EditorWidth = 150;
            textOperationSumma.EnabledEditor = true;
            textOperationSumma.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textOperationSumma.Header = "Сумма";
            textOperationSumma.HeaderAutoSize = false;
            textOperationSumma.HeaderDock = DockStyle.Left;
            textOperationSumma.HeaderTextAlign = ContentAlignment.TopLeft;
            textOperationSumma.HeaderVisible = true;
            textOperationSumma.HeaderWidth = 100;
            textOperationSumma.Location = new Point(0, 108);
            textOperationSumma.Margin = new Padding(3, 4, 3, 4);
            textOperationSumma.Name = "textOperationSumma";
            textOperationSumma.Padding = new Padding(0, 0, 0, 7);
            textOperationSumma.Size = new Size(1054, 32);
            textOperationSumma.TabIndex = 8;
            // 
            // InitialBalanceGoodsEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textOperationSumma);
            Controls.Add(textAmount);
            Controls.Add(selectGoods);
            Controls.Add(lineSplitter1);
            Controls.Add(panel1);
            Name = "InitialBalanceGoodsEditor";
            Size = new Size(1054, 527);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Controls.Editors.DfComboBox comboOrg;
        private Controls.Editors.DfDateTimePicker dateDocument;
        private Controls.Editors.DfIntegerTextBox textDocNumber;
        private Controls.LineSplitter lineSplitter1;
        private Controls.Editors.DfDirectorySelectBox selectGoods;
        private Controls.Editors.DfNumericTextBox textAmount;
        private Controls.Editors.DfCurrencyTextBox textOperationSumma;
    }
}
