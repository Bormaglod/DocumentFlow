namespace DocumentFlow.ViewModels
{
    partial class CalculationEditor
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
            textState = new Controls.Editors.DfTextBox();
            textGoods = new Controls.Editors.DfTextBox();
            textCode = new Controls.Editors.DfTextBox();
            choiceType = new Controls.Editors.DfChoice();
            textStimul = new Controls.Editors.DfNumericTextBox();
            textCost = new Controls.Editors.DfCurrencyTextBox();
            textPercent = new Controls.Editors.DfPercentTextBox();
            textProfit = new Controls.Editors.DfCurrencyTextBox();
            textPrice = new Controls.Editors.DfCurrencyTextBox();
            dateApproval = new Controls.Editors.DfDateTimePicker();
            textNote = new Controls.Editors.DfTextBox();
            SuspendLayout();
            // 
            // textState
            // 
            textState.Dock = DockStyle.Top;
            textState.EditorFitToSize = false;
            textState.EditorWidth = 150;
            textState.EnabledEditor = false;
            textState.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textState.Header = "Состояние";
            textState.HeaderAutoSize = false;
            textState.HeaderDock = DockStyle.Left;
            textState.HeaderTextAlign = ContentAlignment.TopLeft;
            textState.HeaderVisible = true;
            textState.HeaderWidth = 170;
            textState.Location = new Point(0, 0);
            textState.Margin = new Padding(3, 4, 3, 4);
            textState.Multiline = false;
            textState.Name = "textState";
            textState.Padding = new Padding(0, 0, 0, 7);
            textState.Size = new Size(919, 32);
            textState.TabIndex = 0;
            textState.TextValue = "";
            // 
            // textGoods
            // 
            textGoods.Dock = DockStyle.Top;
            textGoods.EditorFitToSize = false;
            textGoods.EditorWidth = 500;
            textGoods.EnabledEditor = false;
            textGoods.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textGoods.Header = "Изделие / услуга";
            textGoods.HeaderAutoSize = false;
            textGoods.HeaderDock = DockStyle.Left;
            textGoods.HeaderTextAlign = ContentAlignment.TopLeft;
            textGoods.HeaderVisible = true;
            textGoods.HeaderWidth = 170;
            textGoods.Location = new Point(0, 32);
            textGoods.Margin = new Padding(3, 4, 3, 4);
            textGoods.Multiline = false;
            textGoods.Name = "textGoods";
            textGoods.Padding = new Padding(0, 0, 0, 7);
            textGoods.Size = new Size(919, 32);
            textGoods.TabIndex = 1;
            textGoods.TextValue = "";
            // 
            // textCode
            // 
            textCode.Dock = DockStyle.Top;
            textCode.EditorFitToSize = false;
            textCode.EditorWidth = 150;
            textCode.EnabledEditor = true;
            textCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textCode.Header = "Код";
            textCode.HeaderAutoSize = false;
            textCode.HeaderDock = DockStyle.Left;
            textCode.HeaderTextAlign = ContentAlignment.TopLeft;
            textCode.HeaderVisible = true;
            textCode.HeaderWidth = 170;
            textCode.Location = new Point(0, 64);
            textCode.Margin = new Padding(3, 4, 3, 4);
            textCode.Multiline = false;
            textCode.Name = "textCode";
            textCode.Padding = new Padding(0, 0, 0, 7);
            textCode.Size = new Size(919, 32);
            textCode.TabIndex = 2;
            textCode.TextValue = "";
            // 
            // choiceType
            // 
            choiceType.Dock = DockStyle.Top;
            choiceType.EditorFitToSize = false;
            choiceType.EditorWidth = 150;
            choiceType.EnabledEditor = true;
            choiceType.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            choiceType.Header = "Способ";
            choiceType.HeaderAutoSize = false;
            choiceType.HeaderDock = DockStyle.Left;
            choiceType.HeaderTextAlign = ContentAlignment.TopLeft;
            choiceType.HeaderVisible = true;
            choiceType.HeaderWidth = 170;
            choiceType.Location = new Point(0, 96);
            choiceType.Margin = new Padding(3, 4, 3, 4);
            choiceType.Name = "choiceType";
            choiceType.Padding = new Padding(0, 0, 0, 7);
            choiceType.ShowDeleteButton = false;
            choiceType.Size = new Size(919, 32);
            choiceType.TabIndex = 3;
            // 
            // textStimul
            // 
            textStimul.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textStimul.Dock = DockStyle.Top;
            textStimul.EditorFitToSize = false;
            textStimul.EditorWidth = 100;
            textStimul.EnabledEditor = true;
            textStimul.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textStimul.Header = "Стимул. выплата";
            textStimul.HeaderAutoSize = false;
            textStimul.HeaderDock = DockStyle.Left;
            textStimul.HeaderTextAlign = ContentAlignment.TopLeft;
            textStimul.HeaderVisible = true;
            textStimul.HeaderWidth = 170;
            textStimul.Location = new Point(0, 128);
            textStimul.Margin = new Padding(3, 4, 3, 4);
            textStimul.Name = "textStimul";
            textStimul.NumberDecimalDigits = 2;
            textStimul.Padding = new Padding(0, 0, 0, 7);
            textStimul.Size = new Size(919, 32);
            textStimul.TabIndex = 4;
            // 
            // textCost
            // 
            textCost.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textCost.Dock = DockStyle.Top;
            textCost.EditorFitToSize = false;
            textCost.EditorWidth = 100;
            textCost.EnabledEditor = false;
            textCost.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textCost.Header = "Себестоимость";
            textCost.HeaderAutoSize = false;
            textCost.HeaderDock = DockStyle.Left;
            textCost.HeaderTextAlign = ContentAlignment.TopLeft;
            textCost.HeaderVisible = true;
            textCost.HeaderWidth = 170;
            textCost.Location = new Point(0, 160);
            textCost.Margin = new Padding(3, 4, 3, 4);
            textCost.Name = "textCost";
            textCost.Padding = new Padding(0, 0, 0, 7);
            textCost.Size = new Size(919, 32);
            textCost.TabIndex = 5;
            // 
            // textPercent
            // 
            textPercent.Dock = DockStyle.Top;
            textPercent.EditorFitToSize = false;
            textPercent.EditorWidth = 100;
            textPercent.EnabledEditor = true;
            textPercent.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPercent.Header = "Рентабельность";
            textPercent.HeaderAutoSize = false;
            textPercent.HeaderDock = DockStyle.Left;
            textPercent.HeaderTextAlign = ContentAlignment.TopLeft;
            textPercent.HeaderVisible = true;
            textPercent.HeaderWidth = 170;
            textPercent.Location = new Point(0, 192);
            textPercent.Margin = new Padding(3, 4, 3, 4);
            textPercent.Name = "textPercent";
            textPercent.NumberDecimalDigits = 2;
            textPercent.Padding = new Padding(0, 0, 0, 7);
            textPercent.PercentValue = new decimal(new int[] { 0, 0, 0, 0 });
            textPercent.Size = new Size(919, 32);
            textPercent.TabIndex = 6;
            // 
            // textProfit
            // 
            textProfit.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textProfit.Dock = DockStyle.Top;
            textProfit.EditorFitToSize = false;
            textProfit.EditorWidth = 100;
            textProfit.EnabledEditor = true;
            textProfit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textProfit.Header = "Прибыль";
            textProfit.HeaderAutoSize = false;
            textProfit.HeaderDock = DockStyle.Left;
            textProfit.HeaderTextAlign = ContentAlignment.TopLeft;
            textProfit.HeaderVisible = true;
            textProfit.HeaderWidth = 170;
            textProfit.Location = new Point(0, 224);
            textProfit.Margin = new Padding(3, 4, 3, 4);
            textProfit.Name = "textProfit";
            textProfit.Padding = new Padding(0, 0, 0, 7);
            textProfit.Size = new Size(919, 32);
            textProfit.TabIndex = 7;
            // 
            // textPrice
            // 
            textPrice.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textPrice.Dock = DockStyle.Top;
            textPrice.EditorFitToSize = false;
            textPrice.EditorWidth = 100;
            textPrice.EnabledEditor = true;
            textPrice.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPrice.Header = "Цена";
            textPrice.HeaderAutoSize = false;
            textPrice.HeaderDock = DockStyle.Left;
            textPrice.HeaderTextAlign = ContentAlignment.TopLeft;
            textPrice.HeaderVisible = true;
            textPrice.HeaderWidth = 170;
            textPrice.Location = new Point(0, 256);
            textPrice.Margin = new Padding(3, 4, 3, 4);
            textPrice.Name = "textPrice";
            textPrice.Padding = new Padding(0, 0, 0, 7);
            textPrice.Size = new Size(919, 32);
            textPrice.TabIndex = 8;
            // 
            // dateApproval
            // 
            dateApproval.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateApproval.Dock = DockStyle.Top;
            dateApproval.EditorFitToSize = false;
            dateApproval.EditorWidth = 150;
            dateApproval.EnabledEditor = true;
            dateApproval.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateApproval.Format = DateTimePickerFormat.Long;
            dateApproval.Header = "Дата утверждения";
            dateApproval.HeaderAutoSize = false;
            dateApproval.HeaderDock = DockStyle.Left;
            dateApproval.HeaderTextAlign = ContentAlignment.TopLeft;
            dateApproval.HeaderVisible = true;
            dateApproval.HeaderWidth = 170;
            dateApproval.Location = new Point(0, 288);
            dateApproval.Margin = new Padding(3, 5, 3, 5);
            dateApproval.Name = "dateApproval";
            dateApproval.Padding = new Padding(0, 0, 0, 7);
            dateApproval.Required = false;
            dateApproval.Size = new Size(919, 32);
            dateApproval.TabIndex = 9;
            // 
            // textNote
            // 
            textNote.Dock = DockStyle.Top;
            textNote.EditorFitToSize = false;
            textNote.EditorWidth = 500;
            textNote.EnabledEditor = true;
            textNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textNote.Header = "Полное наименование";
            textNote.HeaderAutoSize = false;
            textNote.HeaderDock = DockStyle.Left;
            textNote.HeaderTextAlign = ContentAlignment.TopLeft;
            textNote.HeaderVisible = true;
            textNote.HeaderWidth = 170;
            textNote.Location = new Point(0, 320);
            textNote.Margin = new Padding(3, 4, 3, 4);
            textNote.Multiline = true;
            textNote.Name = "textNote";
            textNote.Padding = new Padding(0, 0, 0, 7);
            textNote.Size = new Size(919, 75);
            textNote.TabIndex = 10;
            textNote.TextValue = "";
            // 
            // CalculationEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textNote);
            Controls.Add(dateApproval);
            Controls.Add(textPrice);
            Controls.Add(textProfit);
            Controls.Add(textPercent);
            Controls.Add(textCost);
            Controls.Add(textStimul);
            Controls.Add(choiceType);
            Controls.Add(textCode);
            Controls.Add(textGoods);
            Controls.Add(textState);
            Name = "CalculationEditor";
            Size = new Size(919, 541);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textState;
        private Controls.Editors.DfTextBox textGoods;
        private Controls.Editors.DfTextBox textCode;
        private Controls.Editors.DfChoice choiceType;
        private Controls.Editors.DfNumericTextBox textStimul;
        private Controls.Editors.DfCurrencyTextBox textCost;
        private Controls.Editors.DfPercentTextBox textPercent;
        private Controls.Editors.DfCurrencyTextBox textProfit;
        private Controls.Editors.DfCurrencyTextBox textPrice;
        private Controls.Editors.DfDateTimePicker dateApproval;
        private Controls.Editors.DfTextBox textNote;
    }
}
