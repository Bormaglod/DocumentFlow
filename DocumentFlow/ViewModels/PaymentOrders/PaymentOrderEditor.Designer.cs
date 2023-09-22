namespace DocumentFlow.ViewModels
{
    partial class PaymentOrderEditor
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
            panel2 = new Panel();
            dateOperation = new Controls.Editors.DfDateTimePicker();
            textPaymentNumber = new Controls.Editors.DfTextBox();
            selectContractor = new Controls.Editors.DfDirectorySelectBox();
            choiceDirection = new Controls.Editors.DfChoice();
            textSumma = new Controls.Editors.DfCurrencyTextBox();
            toggleNotDistrib = new Controls.Editors.DfToggleButton();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
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
            panel1.Size = new Size(731, 32);
            panel1.TabIndex = 2;
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
            lineSplitter1.Size = new Size(731, 12);
            lineSplitter1.TabIndex = 6;
            lineSplitter1.Text = "lineSplitter1";
            // 
            // panel2
            // 
            panel2.Controls.Add(dateOperation);
            panel2.Controls.Add(textPaymentNumber);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 44);
            panel2.Name = "panel2";
            panel2.Size = new Size(731, 32);
            panel2.TabIndex = 9;
            // 
            // dateOperation
            // 
            dateOperation.CustomFormat = "dd.MM.yyyy";
            dateOperation.Dock = DockStyle.Left;
            dateOperation.EditorFitToSize = false;
            dateOperation.EditorWidth = 110;
            dateOperation.EnabledEditor = true;
            dateOperation.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateOperation.Format = DateTimePickerFormat.Custom;
            dateOperation.Header = "от";
            dateOperation.HeaderAutoSize = false;
            dateOperation.HeaderDock = DockStyle.Left;
            dateOperation.HeaderTextAlign = ContentAlignment.TopCenter;
            dateOperation.HeaderVisible = true;
            dateOperation.HeaderWidth = 30;
            dateOperation.Location = new Point(220, 0);
            dateOperation.Margin = new Padding(3, 5, 3, 5);
            dateOperation.Name = "dateOperation";
            dateOperation.Padding = new Padding(0, 0, 0, 7);
            dateOperation.Required = true;
            dateOperation.Size = new Size(149, 32);
            dateOperation.TabIndex = 1;
            // 
            // textPaymentNumber
            // 
            textPaymentNumber.Dock = DockStyle.Left;
            textPaymentNumber.EditorFitToSize = false;
            textPaymentNumber.EditorWidth = 100;
            textPaymentNumber.EnabledEditor = true;
            textPaymentNumber.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPaymentNumber.Header = "Номер пл. ордера";
            textPaymentNumber.HeaderAutoSize = false;
            textPaymentNumber.HeaderDock = DockStyle.Left;
            textPaymentNumber.HeaderTextAlign = ContentAlignment.TopLeft;
            textPaymentNumber.HeaderVisible = true;
            textPaymentNumber.HeaderWidth = 120;
            textPaymentNumber.Location = new Point(0, 0);
            textPaymentNumber.Margin = new Padding(3, 4, 3, 4);
            textPaymentNumber.Multiline = false;
            textPaymentNumber.Name = "textPaymentNumber";
            textPaymentNumber.Padding = new Padding(0, 0, 0, 7);
            textPaymentNumber.Size = new Size(220, 32);
            textPaymentNumber.TabIndex = 0;
            textPaymentNumber.TextValue = "";
            // 
            // selectContractor
            // 
            selectContractor.CanSelectFolder = false;
            selectContractor.Dock = DockStyle.Top;
            selectContractor.EditorFitToSize = false;
            selectContractor.EditorWidth = 400;
            selectContractor.EnabledEditor = true;
            selectContractor.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectContractor.Header = "Контрагент";
            selectContractor.HeaderAutoSize = false;
            selectContractor.HeaderDock = DockStyle.Left;
            selectContractor.HeaderTextAlign = ContentAlignment.TopLeft;
            selectContractor.HeaderVisible = true;
            selectContractor.HeaderWidth = 120;
            selectContractor.Location = new Point(0, 76);
            selectContractor.Margin = new Padding(3, 4, 3, 4);
            selectContractor.Name = "selectContractor";
            selectContractor.Padding = new Padding(0, 0, 0, 7);
            selectContractor.RemoveEmptyFolders = false;
            selectContractor.ShowDeleteButton = true;
            selectContractor.ShowOpenButton = true;
            selectContractor.Size = new Size(731, 32);
            selectContractor.TabIndex = 10;
            selectContractor.OpenButtonClick += SelectContractor_OpenButtonClick;
            // 
            // choiceDirection
            // 
            choiceDirection.Dock = DockStyle.Top;
            choiceDirection.EditorFitToSize = false;
            choiceDirection.EditorWidth = 120;
            choiceDirection.EnabledEditor = true;
            choiceDirection.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            choiceDirection.Header = "Тип платежа";
            choiceDirection.HeaderAutoSize = false;
            choiceDirection.HeaderDock = DockStyle.Left;
            choiceDirection.HeaderTextAlign = ContentAlignment.TopLeft;
            choiceDirection.HeaderVisible = true;
            choiceDirection.HeaderWidth = 120;
            choiceDirection.Location = new Point(0, 108);
            choiceDirection.Margin = new Padding(3, 4, 3, 4);
            choiceDirection.Name = "choiceDirection";
            choiceDirection.Padding = new Padding(0, 0, 0, 7);
            choiceDirection.ShowDeleteButton = false;
            choiceDirection.Size = new Size(731, 32);
            choiceDirection.TabIndex = 11;
            // 
            // textSumma
            // 
            textSumma.CurrencyDecimalDigits = 2;
            textSumma.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textSumma.Dock = DockStyle.Top;
            textSumma.EditorFitToSize = false;
            textSumma.EditorWidth = 120;
            textSumma.EnabledEditor = true;
            textSumma.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textSumma.Header = "Сумма";
            textSumma.HeaderAutoSize = false;
            textSumma.HeaderDock = DockStyle.Left;
            textSumma.HeaderTextAlign = ContentAlignment.TopLeft;
            textSumma.HeaderVisible = true;
            textSumma.HeaderWidth = 120;
            textSumma.Location = new Point(0, 140);
            textSumma.Margin = new Padding(3, 4, 3, 4);
            textSumma.Name = "textSumma";
            textSumma.Padding = new Padding(0, 0, 0, 7);
            textSumma.Size = new Size(731, 32);
            textSumma.TabIndex = 12;
            // 
            // toggleNotDistrib
            // 
            toggleNotDistrib.Dock = DockStyle.Top;
            toggleNotDistrib.EditorFitToSize = false;
            toggleNotDistrib.EditorWidth = 90;
            toggleNotDistrib.EnabledEditor = true;
            toggleNotDistrib.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            toggleNotDistrib.Header = "Не распределять";
            toggleNotDistrib.HeaderAutoSize = false;
            toggleNotDistrib.HeaderDock = DockStyle.Left;
            toggleNotDistrib.HeaderTextAlign = ContentAlignment.TopLeft;
            toggleNotDistrib.HeaderVisible = true;
            toggleNotDistrib.HeaderWidth = 120;
            toggleNotDistrib.Location = new Point(0, 172);
            toggleNotDistrib.Margin = new Padding(4, 3, 4, 3);
            toggleNotDistrib.Name = "toggleNotDistrib";
            toggleNotDistrib.Padding = new Padding(0, 0, 0, 7);
            toggleNotDistrib.Size = new Size(731, 32);
            toggleNotDistrib.TabIndex = 13;
            toggleNotDistrib.ToggleValue = false;
            // 
            // PaymentOrderEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(toggleNotDistrib);
            Controls.Add(textSumma);
            Controls.Add(choiceDirection);
            Controls.Add(selectContractor);
            Controls.Add(panel2);
            Controls.Add(lineSplitter1);
            Controls.Add(panel1);
            Name = "PaymentOrderEditor";
            Size = new Size(731, 553);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Controls.Editors.DfComboBox comboOrg;
        private Controls.Editors.DfDateTimePicker dateDocument;
        private Controls.Editors.DfIntegerTextBox textDocNumber;
        private Controls.LineSplitter lineSplitter1;
        private Panel panel2;
        private Controls.Editors.DfDateTimePicker dateOperation;
        private Controls.Editors.DfTextBox textPaymentNumber;
        private Controls.Editors.DfDirectorySelectBox selectContractor;
        private Controls.Editors.DfChoice choiceDirection;
        private Controls.Editors.DfCurrencyTextBox textSumma;
        private Controls.Editors.DfToggleButton toggleNotDistrib;
    }
}
