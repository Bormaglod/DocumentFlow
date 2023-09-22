namespace DocumentFlow.ViewModels
{
    partial class CalculationDeductionEditor
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
            textCalcName = new Controls.Editors.DfTextBox();
            textPrice = new Controls.Editors.DfCurrencyTextBox();
            comboDeduction = new Controls.Editors.DfComboBox();
            textPercent = new Controls.Editors.DfPercentTextBox();
            textItemCost = new Controls.Editors.DfCurrencyTextBox();
            SuspendLayout();
            // 
            // textCalcName
            // 
            textCalcName.Dock = DockStyle.Top;
            textCalcName.EditorFitToSize = false;
            textCalcName.EditorWidth = 600;
            textCalcName.EnabledEditor = false;
            textCalcName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textCalcName.Header = "Калькуляция";
            textCalcName.HeaderAutoSize = false;
            textCalcName.HeaderDock = DockStyle.Left;
            textCalcName.HeaderTextAlign = ContentAlignment.TopLeft;
            textCalcName.HeaderVisible = true;
            textCalcName.HeaderWidth = 210;
            textCalcName.Location = new Point(0, 0);
            textCalcName.Margin = new Padding(3, 4, 3, 4);
            textCalcName.Multiline = false;
            textCalcName.Name = "textCalcName";
            textCalcName.Padding = new Padding(0, 0, 0, 7);
            textCalcName.Size = new Size(930, 32);
            textCalcName.TabIndex = 0;
            textCalcName.TextValue = "";
            // 
            // textPrice
            // 
            textPrice.CurrencyDecimalDigits = 2;
            textPrice.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textPrice.Dock = DockStyle.Top;
            textPrice.EditorFitToSize = false;
            textPrice.EditorWidth = 150;
            textPrice.EnabledEditor = false;
            textPrice.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPrice.Header = "Сумма с которой произв. удерж.";
            textPrice.HeaderAutoSize = false;
            textPrice.HeaderDock = DockStyle.Left;
            textPrice.HeaderTextAlign = ContentAlignment.TopLeft;
            textPrice.HeaderVisible = true;
            textPrice.HeaderWidth = 210;
            textPrice.Location = new Point(0, 64);
            textPrice.Margin = new Padding(3, 4, 3, 4);
            textPrice.Name = "textPrice";
            textPrice.Padding = new Padding(0, 0, 0, 7);
            textPrice.Size = new Size(930, 32);
            textPrice.TabIndex = 4;
            // 
            // comboDeduction
            // 
            comboDeduction.Dock = DockStyle.Top;
            comboDeduction.EditorFitToSize = false;
            comboDeduction.EditorWidth = 400;
            comboDeduction.EnabledEditor = true;
            comboDeduction.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboDeduction.Header = "Удержание";
            comboDeduction.HeaderAutoSize = false;
            comboDeduction.HeaderDock = DockStyle.Left;
            comboDeduction.HeaderTextAlign = ContentAlignment.TopLeft;
            comboDeduction.HeaderVisible = true;
            comboDeduction.HeaderWidth = 210;
            comboDeduction.Location = new Point(0, 32);
            comboDeduction.Margin = new Padding(3, 4, 3, 4);
            comboDeduction.Name = "comboDeduction";
            comboDeduction.Padding = new Padding(0, 0, 0, 7);
            comboDeduction.ShowDeleteButton = true;
            comboDeduction.ShowOpenButton = true;
            comboDeduction.Size = new Size(930, 32);
            comboDeduction.TabIndex = 13;
            comboDeduction.OpenButtonClick += ComboDeduction_OpenButtonClick;
            comboDeduction.SelectedItemChanged += ComboDeduction_SelectedItemChanged;
            comboDeduction.DocumentSelectedChanged += ComboDeduction_DocumentSelectedChanged;
            // 
            // textPercent
            // 
            textPercent.Dock = DockStyle.Top;
            textPercent.EditorFitToSize = false;
            textPercent.EditorWidth = 150;
            textPercent.EnabledEditor = true;
            textPercent.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPercent.Header = "Процент";
            textPercent.HeaderAutoSize = false;
            textPercent.HeaderDock = DockStyle.Left;
            textPercent.HeaderTextAlign = ContentAlignment.TopLeft;
            textPercent.HeaderVisible = true;
            textPercent.HeaderWidth = 210;
            textPercent.Location = new Point(0, 96);
            textPercent.Margin = new Padding(3, 4, 3, 4);
            textPercent.Name = "textPercent";
            textPercent.NumberDecimalDigits = 2;
            textPercent.Padding = new Padding(0, 0, 0, 7);
            textPercent.PercentValue = new decimal(new int[] { 0, 0, 0, 0 });
            textPercent.Size = new Size(930, 32);
            textPercent.TabIndex = 14;
            // 
            // textItemCost
            // 
            textItemCost.CurrencyDecimalDigits = 2;
            textItemCost.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textItemCost.Dock = DockStyle.Top;
            textItemCost.EditorFitToSize = false;
            textItemCost.EditorWidth = 150;
            textItemCost.EnabledEditor = true;
            textItemCost.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textItemCost.Header = "Сумма удержания";
            textItemCost.HeaderAutoSize = false;
            textItemCost.HeaderDock = DockStyle.Left;
            textItemCost.HeaderTextAlign = ContentAlignment.TopLeft;
            textItemCost.HeaderVisible = true;
            textItemCost.HeaderWidth = 210;
            textItemCost.Location = new Point(0, 128);
            textItemCost.Margin = new Padding(3, 4, 3, 4);
            textItemCost.Name = "textItemCost";
            textItemCost.Padding = new Padding(0, 0, 0, 7);
            textItemCost.Size = new Size(930, 32);
            textItemCost.TabIndex = 15;
            // 
            // CalculationDeductionEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textItemCost);
            Controls.Add(textPercent);
            Controls.Add(textPrice);
            Controls.Add(comboDeduction);
            Controls.Add(textCalcName);
            Name = "CalculationDeductionEditor";
            Size = new Size(930, 509);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textCalcName;
        private Controls.Editors.DfCurrencyTextBox textPrice;
        private Controls.Editors.DfComboBox comboDeduction;
        private Controls.Editors.DfPercentTextBox textPercent;
        private Controls.Editors.DfCurrencyTextBox textItemCost;
    }
}
