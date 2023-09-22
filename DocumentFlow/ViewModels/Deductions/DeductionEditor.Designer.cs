namespace DocumentFlow.ViewModels
{
    partial class DeductionEditor
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
            textName = new Controls.Editors.DfTextBox();
            choiceBase = new Controls.Editors.DfChoice();
            comboPerson = new Controls.Editors.DfComboBox();
            textPercent = new Controls.Editors.DfPercentTextBox();
            textSumma = new Controls.Editors.DfCurrencyTextBox();
            SuspendLayout();
            // 
            // textName
            // 
            textName.Dock = DockStyle.Top;
            textName.EditorFitToSize = false;
            textName.EditorWidth = 400;
            textName.EnabledEditor = true;
            textName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textName.Header = "Наименование";
            textName.HeaderAutoSize = false;
            textName.HeaderDock = DockStyle.Left;
            textName.HeaderTextAlign = ContentAlignment.TopLeft;
            textName.HeaderVisible = true;
            textName.HeaderWidth = 160;
            textName.Location = new Point(0, 0);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(690, 32);
            textName.TabIndex = 0;
            textName.TextValue = "";
            // 
            // choiceBase
            // 
            choiceBase.Dock = DockStyle.Top;
            choiceBase.EditorFitToSize = false;
            choiceBase.EditorWidth = 300;
            choiceBase.EnabledEditor = true;
            choiceBase.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            choiceBase.Header = "База для начисления";
            choiceBase.HeaderAutoSize = false;
            choiceBase.HeaderDock = DockStyle.Left;
            choiceBase.HeaderTextAlign = ContentAlignment.TopLeft;
            choiceBase.HeaderVisible = true;
            choiceBase.HeaderWidth = 160;
            choiceBase.Location = new Point(0, 32);
            choiceBase.Margin = new Padding(3, 4, 3, 4);
            choiceBase.Name = "choiceBase";
            choiceBase.Padding = new Padding(0, 0, 0, 7);
            choiceBase.ShowDeleteButton = false;
            choiceBase.Size = new Size(690, 32);
            choiceBase.TabIndex = 1;
            choiceBase.ChoiceValueChanged += ChoiceBase_ChoiceValueChanged;
            // 
            // comboPerson
            // 
            comboPerson.Dock = DockStyle.Top;
            comboPerson.EditorFitToSize = false;
            comboPerson.EditorWidth = 300;
            comboPerson.EnabledEditor = true;
            comboPerson.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboPerson.Header = "Получатель фикс. суммы";
            comboPerson.HeaderAutoSize = false;
            comboPerson.HeaderDock = DockStyle.Left;
            comboPerson.HeaderTextAlign = ContentAlignment.TopLeft;
            comboPerson.HeaderVisible = true;
            comboPerson.HeaderWidth = 160;
            comboPerson.Location = new Point(0, 64);
            comboPerson.Margin = new Padding(3, 4, 3, 4);
            comboPerson.Name = "comboPerson";
            comboPerson.Padding = new Padding(0, 0, 0, 7);
            comboPerson.ShowDeleteButton = true;
            comboPerson.ShowOpenButton = true;
            comboPerson.Size = new Size(690, 32);
            comboPerson.TabIndex = 2;
            comboPerson.OpenButtonClick += ComboPerson_OpenButtonClick;
            // 
            // textPercent
            // 
            textPercent.Dock = DockStyle.Top;
            textPercent.EditorFitToSize = false;
            textPercent.EditorWidth = 100;
            textPercent.EnabledEditor = true;
            textPercent.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPercent.Header = "Процент от базы";
            textPercent.HeaderAutoSize = false;
            textPercent.HeaderDock = DockStyle.Left;
            textPercent.HeaderTextAlign = ContentAlignment.TopLeft;
            textPercent.HeaderVisible = true;
            textPercent.HeaderWidth = 160;
            textPercent.Location = new Point(0, 96);
            textPercent.Margin = new Padding(3, 4, 3, 4);
            textPercent.Name = "textPercent";
            textPercent.NumberDecimalDigits = 2;
            textPercent.Padding = new Padding(0, 0, 0, 7);
            textPercent.PercentValue = new decimal(new int[] { 0, 0, 0, 0 });
            textPercent.Size = new Size(690, 32);
            textPercent.TabIndex = 3;
            // 
            // textSumma
            // 
            textSumma.CurrencyDecimalDigits = 2;
            textSumma.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textSumma.Dock = DockStyle.Top;
            textSumma.EditorFitToSize = false;
            textSumma.EditorWidth = 100;
            textSumma.EnabledEditor = true;
            textSumma.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textSumma.Header = "Сумма";
            textSumma.HeaderAutoSize = false;
            textSumma.HeaderDock = DockStyle.Left;
            textSumma.HeaderTextAlign = ContentAlignment.TopLeft;
            textSumma.HeaderVisible = true;
            textSumma.HeaderWidth = 160;
            textSumma.Location = new Point(0, 128);
            textSumma.Margin = new Padding(3, 4, 3, 4);
            textSumma.Name = "textSumma";
            textSumma.Padding = new Padding(0, 0, 0, 7);
            textSumma.Size = new Size(690, 32);
            textSumma.TabIndex = 4;
            // 
            // DeductionEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textSumma);
            Controls.Add(textPercent);
            Controls.Add(comboPerson);
            Controls.Add(choiceBase);
            Controls.Add(textName);
            Name = "DeductionEditor";
            Size = new Size(690, 403);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfChoice choiceBase;
        private Controls.Editors.DfComboBox comboPerson;
        private Controls.Editors.DfPercentTextBox textPercent;
        private Controls.Editors.DfCurrencyTextBox textSumma;
    }
}
