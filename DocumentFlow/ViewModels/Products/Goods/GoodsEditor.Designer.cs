namespace DocumentFlow.ViewModels
{
    partial class GoodsEditor
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
            textCode = new Controls.Editors.DfTextBox();
            textName = new Controls.Editors.DfTextBox();
            textDocName = new Controls.Editors.DfTextBox();
            selectGroup = new Controls.Editors.DfDirectorySelectBox();
            textPrice = new Controls.Editors.DfCurrencyTextBox();
            comboCalculation = new Controls.Editors.DfComboBox();
            toggleService = new Controls.Editors.DfToggleButton();
            textNote = new Controls.Editors.DfTextBox();
            comboMeasurement = new Controls.Editors.DfComboBox();
            textWeight = new Controls.Editors.DfNumericTextBox();
            choiceVat = new Controls.Editors.DfChoice();
            panel1 = new Panel();
            buttonChangeCode = new Syncfusion.WinForms.Controls.SfButton();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // textCode
            // 
            textCode.Dock = DockStyle.Left;
            textCode.EditorFitToSize = false;
            textCode.EditorWidth = 180;
            textCode.EnabledEditor = true;
            textCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textCode.Header = "Код";
            textCode.HeaderAutoSize = false;
            textCode.HeaderDock = DockStyle.Left;
            textCode.HeaderTextAlign = ContentAlignment.TopLeft;
            textCode.HeaderVisible = true;
            textCode.HeaderWidth = 200;
            textCode.Location = new Point(0, 0);
            textCode.Margin = new Padding(3, 4, 3, 4);
            textCode.Multiline = false;
            textCode.Name = "textCode";
            textCode.Padding = new Padding(0, 0, 0, 7);
            textCode.Size = new Size(385, 32);
            textCode.TabIndex = 0;
            textCode.TextValue = "";
            // 
            // textName
            // 
            textName.Dock = DockStyle.Top;
            textName.EditorFitToSize = false;
            textName.EditorWidth = 600;
            textName.EnabledEditor = true;
            textName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textName.Header = "Наименование";
            textName.HeaderAutoSize = false;
            textName.HeaderDock = DockStyle.Left;
            textName.HeaderTextAlign = ContentAlignment.TopLeft;
            textName.HeaderVisible = true;
            textName.HeaderWidth = 200;
            textName.Location = new Point(0, 32);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(857, 32);
            textName.TabIndex = 1;
            textName.TextValue = "";
            // 
            // textDocName
            // 
            textDocName.Dock = DockStyle.Top;
            textDocName.EditorFitToSize = false;
            textDocName.EditorWidth = 600;
            textDocName.EnabledEditor = true;
            textDocName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textDocName.Header = "Наименование для документов";
            textDocName.HeaderAutoSize = false;
            textDocName.HeaderDock = DockStyle.Left;
            textDocName.HeaderTextAlign = ContentAlignment.TopLeft;
            textDocName.HeaderVisible = true;
            textDocName.HeaderWidth = 200;
            textDocName.Location = new Point(0, 64);
            textDocName.Margin = new Padding(3, 4, 3, 4);
            textDocName.Multiline = false;
            textDocName.Name = "textDocName";
            textDocName.Padding = new Padding(0, 0, 0, 7);
            textDocName.Size = new Size(857, 32);
            textDocName.TabIndex = 2;
            textDocName.TextValue = "";
            // 
            // selectGroup
            // 
            selectGroup.CanSelectFolder = true;
            selectGroup.Dock = DockStyle.Top;
            selectGroup.EditorFitToSize = false;
            selectGroup.EditorWidth = 400;
            selectGroup.EnabledEditor = true;
            selectGroup.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectGroup.Header = "Группа";
            selectGroup.HeaderAutoSize = false;
            selectGroup.HeaderDock = DockStyle.Left;
            selectGroup.HeaderTextAlign = ContentAlignment.TopLeft;
            selectGroup.HeaderVisible = true;
            selectGroup.HeaderWidth = 200;
            selectGroup.Location = new Point(0, 96);
            selectGroup.Margin = new Padding(3, 4, 3, 4);
            selectGroup.Name = "selectGroup";
            selectGroup.Padding = new Padding(0, 0, 0, 7);
            selectGroup.RemoveEmptyFolders = false;
            selectGroup.ShowDeleteButton = true;
            selectGroup.ShowOpenButton = false;
            selectGroup.Size = new Size(857, 32);
            selectGroup.TabIndex = 3;
            // 
            // textPrice
            // 
            textPrice.CurrencyDecimalDigits = 2;
            textPrice.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textPrice.Dock = DockStyle.Top;
            textPrice.EditorFitToSize = false;
            textPrice.EditorWidth = 120;
            textPrice.EnabledEditor = true;
            textPrice.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPrice.Header = "Цена без НДС";
            textPrice.HeaderAutoSize = false;
            textPrice.HeaderDock = DockStyle.Left;
            textPrice.HeaderTextAlign = ContentAlignment.TopLeft;
            textPrice.HeaderVisible = true;
            textPrice.HeaderWidth = 200;
            textPrice.Location = new Point(0, 192);
            textPrice.Margin = new Padding(3, 4, 3, 4);
            textPrice.Name = "textPrice";
            textPrice.Padding = new Padding(0, 0, 0, 7);
            textPrice.Size = new Size(857, 32);
            textPrice.TabIndex = 4;
            // 
            // comboCalculation
            // 
            comboCalculation.DisplayMember = "ItemName";
            comboCalculation.Dock = DockStyle.Top;
            comboCalculation.EditorFitToSize = false;
            comboCalculation.EditorWidth = 400;
            comboCalculation.EnabledEditor = true;
            comboCalculation.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboCalculation.Header = "Калькуляция";
            comboCalculation.HeaderAutoSize = false;
            comboCalculation.HeaderDock = DockStyle.Left;
            comboCalculation.HeaderTextAlign = ContentAlignment.TopLeft;
            comboCalculation.HeaderVisible = true;
            comboCalculation.HeaderWidth = 200;
            comboCalculation.Location = new Point(0, 256);
            comboCalculation.Margin = new Padding(3, 4, 3, 4);
            comboCalculation.Name = "comboCalculation";
            comboCalculation.Padding = new Padding(0, 0, 0, 7);
            comboCalculation.ShowDeleteButton = true;
            comboCalculation.ShowOpenButton = true;
            comboCalculation.Size = new Size(857, 32);
            comboCalculation.TabIndex = 5;
            comboCalculation.OpenButtonClick += ComboCalculation_OpenButtonClick;
            // 
            // toggleService
            // 
            toggleService.Dock = DockStyle.Top;
            toggleService.EditorFitToSize = false;
            toggleService.EditorWidth = 90;
            toggleService.EnabledEditor = true;
            toggleService.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            toggleService.Header = "Услуга";
            toggleService.HeaderAutoSize = false;
            toggleService.HeaderDock = DockStyle.Left;
            toggleService.HeaderTextAlign = ContentAlignment.TopLeft;
            toggleService.HeaderVisible = true;
            toggleService.HeaderWidth = 200;
            toggleService.Location = new Point(0, 288);
            toggleService.Margin = new Padding(4, 3, 4, 3);
            toggleService.Name = "toggleService";
            toggleService.Padding = new Padding(0, 0, 0, 7);
            toggleService.Size = new Size(857, 32);
            toggleService.TabIndex = 6;
            toggleService.ToggleValue = false;
            toggleService.ToggleValueChanged += ToggleService_ToggleValueChanged;
            // 
            // textNote
            // 
            textNote.Dock = DockStyle.Fill;
            textNote.EditorFitToSize = false;
            textNote.EditorWidth = 500;
            textNote.EnabledEditor = true;
            textNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textNote.Header = "Описание";
            textNote.HeaderAutoSize = false;
            textNote.HeaderDock = DockStyle.Left;
            textNote.HeaderTextAlign = ContentAlignment.TopLeft;
            textNote.HeaderVisible = true;
            textNote.HeaderWidth = 200;
            textNote.Location = new Point(0, 320);
            textNote.Margin = new Padding(3, 4, 3, 4);
            textNote.Multiline = true;
            textNote.Name = "textNote";
            textNote.Padding = new Padding(0, 0, 0, 7);
            textNote.Size = new Size(857, 127);
            textNote.TabIndex = 7;
            textNote.TextValue = "";
            // 
            // comboMeasurement
            // 
            comboMeasurement.DisplayMember = "ItemName";
            comboMeasurement.Dock = DockStyle.Top;
            comboMeasurement.EditorFitToSize = false;
            comboMeasurement.EditorWidth = 250;
            comboMeasurement.EnabledEditor = true;
            comboMeasurement.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboMeasurement.Header = "Единица измерения";
            comboMeasurement.HeaderAutoSize = false;
            comboMeasurement.HeaderDock = DockStyle.Left;
            comboMeasurement.HeaderTextAlign = ContentAlignment.TopLeft;
            comboMeasurement.HeaderVisible = true;
            comboMeasurement.HeaderWidth = 200;
            comboMeasurement.Location = new Point(0, 128);
            comboMeasurement.Margin = new Padding(3, 4, 3, 4);
            comboMeasurement.Name = "comboMeasurement";
            comboMeasurement.Padding = new Padding(0, 0, 0, 7);
            comboMeasurement.ShowDeleteButton = true;
            comboMeasurement.ShowOpenButton = true;
            comboMeasurement.Size = new Size(857, 32);
            comboMeasurement.TabIndex = 8;
            comboMeasurement.OpenButtonClick += ComboMeasurement_OpenButtonClick;
            // 
            // textWeight
            // 
            textWeight.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textWeight.Dock = DockStyle.Top;
            textWeight.EditorFitToSize = false;
            textWeight.EditorWidth = 120;
            textWeight.EnabledEditor = true;
            textWeight.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textWeight.Header = "Вес, г";
            textWeight.HeaderAutoSize = false;
            textWeight.HeaderDock = DockStyle.Left;
            textWeight.HeaderTextAlign = ContentAlignment.TopLeft;
            textWeight.HeaderVisible = true;
            textWeight.HeaderWidth = 200;
            textWeight.Location = new Point(0, 160);
            textWeight.Margin = new Padding(3, 4, 3, 4);
            textWeight.Name = "textWeight";
            textWeight.NumberDecimalDigits = 3;
            textWeight.Padding = new Padding(0, 0, 0, 7);
            textWeight.ShowSuffix = false;
            textWeight.Size = new Size(857, 32);
            textWeight.Suffix = "суффикс";
            textWeight.TabIndex = 9;
            // 
            // choiceVat
            // 
            choiceVat.Dock = DockStyle.Top;
            choiceVat.EditorFitToSize = false;
            choiceVat.EditorWidth = 150;
            choiceVat.EnabledEditor = true;
            choiceVat.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            choiceVat.Header = "НДС";
            choiceVat.HeaderAutoSize = false;
            choiceVat.HeaderDock = DockStyle.Left;
            choiceVat.HeaderTextAlign = ContentAlignment.TopLeft;
            choiceVat.HeaderVisible = true;
            choiceVat.HeaderWidth = 200;
            choiceVat.Location = new Point(0, 224);
            choiceVat.Margin = new Padding(3, 4, 3, 4);
            choiceVat.Name = "choiceVat";
            choiceVat.Padding = new Padding(0, 0, 0, 7);
            choiceVat.ShowDeleteButton = true;
            choiceVat.Size = new Size(857, 32);
            choiceVat.TabIndex = 10;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonChangeCode);
            panel1.Controls.Add(textCode);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(857, 32);
            panel1.TabIndex = 11;
            // 
            // buttonChangeCode
            // 
            buttonChangeCode.AccessibleName = "Button";
            buttonChangeCode.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonChangeCode.Location = new Point(385, 0);
            buttonChangeCode.Name = "buttonChangeCode";
            buttonChangeCode.Size = new Size(96, 24);
            buttonChangeCode.TabIndex = 1;
            buttonChangeCode.Text = "Изменить";
            buttonChangeCode.UseVisualStyleBackColor = true;
            buttonChangeCode.Click += ButtonChangeCode_Click;
            // 
            // GoodsEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textNote);
            Controls.Add(toggleService);
            Controls.Add(comboCalculation);
            Controls.Add(choiceVat);
            Controls.Add(textPrice);
            Controls.Add(textWeight);
            Controls.Add(comboMeasurement);
            Controls.Add(selectGroup);
            Controls.Add(textDocName);
            Controls.Add(textName);
            Controls.Add(panel1);
            Name = "GoodsEditor";
            Size = new Size(857, 447);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textCode;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfTextBox textDocName;
        private Controls.Editors.DfDirectorySelectBox selectGroup;
        private Controls.Editors.DfCurrencyTextBox textPrice;
        private Controls.Editors.DfComboBox comboCalculation;
        private Controls.Editors.DfToggleButton toggleService;
        private Controls.Editors.DfTextBox textNote;
        private Controls.Editors.DfComboBox comboMeasurement;
        private Controls.Editors.DfNumericTextBox textWeight;
        private Controls.Editors.DfChoice choiceVat;
        private Panel panel1;
        private Syncfusion.WinForms.Controls.SfButton buttonChangeCode;
    }
}
