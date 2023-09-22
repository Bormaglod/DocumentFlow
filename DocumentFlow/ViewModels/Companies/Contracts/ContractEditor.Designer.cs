namespace DocumentFlow.ViewModels
{
    partial class ContractEditor
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
            textContractor = new Controls.Editors.DfTextBox();
            textNumber = new Controls.Editors.DfTextBox();
            textName = new Controls.Editors.DfTextBox();
            dateContract = new Controls.Editors.DfDateTimePicker();
            selectSignatory = new Controls.Editors.DfDirectorySelectBox();
            selectOrgSignatory = new Controls.Editors.DfDirectorySelectBox();
            toggleTax = new Controls.Editors.DfToggleButton();
            textPaymentPeriod = new Controls.Editors.DfIntegerTextBox();
            toggleDefault = new Controls.Editors.DfToggleButton();
            choiceType = new Controls.Editors.DfChoice();
            dateEnd = new Controls.Editors.DfDateTimePicker();
            dateStart = new Controls.Editors.DfDateTimePicker();
            SuspendLayout();
            // 
            // textContractor
            // 
            textContractor.Dock = DockStyle.Top;
            textContractor.EditorFitToSize = false;
            textContractor.EditorWidth = 400;
            textContractor.EnabledEditor = false;
            textContractor.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textContractor.Header = "Контрагент";
            textContractor.HeaderAutoSize = false;
            textContractor.HeaderTextAlign = ContentAlignment.TopLeft;
            textContractor.HeaderVisible = true;
            textContractor.HeaderWidth = 160;
            textContractor.Location = new Point(0, 0);
            textContractor.Margin = new Padding(3, 4, 3, 4);
            textContractor.Multiline = false;
            textContractor.Name = "textContractor";
            textContractor.Padding = new Padding(0, 0, 0, 7);
            textContractor.Size = new Size(765, 32);
            textContractor.TabIndex = 0;
            textContractor.TextValue = "";
            // 
            // textNumber
            // 
            textNumber.Dock = DockStyle.Top;
            textNumber.EditorFitToSize = false;
            textNumber.EditorWidth = 200;
            textNumber.EnabledEditor = true;
            textNumber.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textNumber.Header = "Номер договора";
            textNumber.HeaderAutoSize = false;
            textNumber.HeaderTextAlign = ContentAlignment.TopLeft;
            textNumber.HeaderVisible = true;
            textNumber.HeaderWidth = 160;
            textNumber.Location = new Point(0, 64);
            textNumber.Margin = new Padding(3, 4, 3, 4);
            textNumber.Multiline = false;
            textNumber.Name = "textNumber";
            textNumber.Padding = new Padding(0, 0, 0, 7);
            textNumber.Size = new Size(765, 32);
            textNumber.TabIndex = 2;
            textNumber.TextValue = "";
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
            textName.HeaderTextAlign = ContentAlignment.TopLeft;
            textName.HeaderVisible = true;
            textName.HeaderWidth = 160;
            textName.Location = new Point(0, 96);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(765, 32);
            textName.TabIndex = 3;
            textName.TextValue = "";
            // 
            // dateContract
            // 
            dateContract.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateContract.Dock = DockStyle.Top;
            dateContract.EditorFitToSize = false;
            dateContract.EditorWidth = 120;
            dateContract.EnabledEditor = true;
            dateContract.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateContract.Format = DateTimePickerFormat.Short;
            dateContract.Header = "Дата договора";
            dateContract.HeaderAutoSize = false;
            dateContract.HeaderTextAlign = ContentAlignment.TopLeft;
            dateContract.HeaderVisible = true;
            dateContract.HeaderWidth = 160;
            dateContract.Location = new Point(0, 128);
            dateContract.Margin = new Padding(3, 5, 3, 5);
            dateContract.Name = "dateContract";
            dateContract.Padding = new Padding(0, 0, 0, 7);
            dateContract.Required = true;
            dateContract.Size = new Size(765, 32);
            dateContract.TabIndex = 4;
            // 
            // selectSignatory
            // 
            selectSignatory.Dock = DockStyle.Top;
            selectSignatory.EditorFitToSize = false;
            selectSignatory.EditorWidth = 400;
            selectSignatory.EnabledEditor = true;
            selectSignatory.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectSignatory.Header = "Подпись контрагента";
            selectSignatory.HeaderAutoSize = false;
            selectSignatory.HeaderTextAlign = ContentAlignment.TopLeft;
            selectSignatory.HeaderVisible = true;
            selectSignatory.HeaderWidth = 160;
            selectSignatory.Location = new Point(0, 192);
            selectSignatory.Margin = new Padding(3, 4, 3, 4);
            selectSignatory.Name = "selectSignatory";
            selectSignatory.Padding = new Padding(0, 0, 0, 7);
            selectSignatory.RemoveEmptyFolders = false;
            selectSignatory.ShowDeleteButton = true;
            selectSignatory.ShowOpenButton = true;
            selectSignatory.Size = new Size(765, 32);
            selectSignatory.TabIndex = 5;
            selectSignatory.OpenButtonClick += SelectSignatory_OpenButtonClick;
            // 
            // selectOrgSignatory
            // 
            selectOrgSignatory.Dock = DockStyle.Top;
            selectOrgSignatory.EditorFitToSize = false;
            selectOrgSignatory.EditorWidth = 400;
            selectOrgSignatory.EnabledEditor = true;
            selectOrgSignatory.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectOrgSignatory.Header = "Подпись организации";
            selectOrgSignatory.HeaderAutoSize = false;
            selectOrgSignatory.HeaderTextAlign = ContentAlignment.TopLeft;
            selectOrgSignatory.HeaderVisible = true;
            selectOrgSignatory.HeaderWidth = 160;
            selectOrgSignatory.Location = new Point(0, 224);
            selectOrgSignatory.Margin = new Padding(3, 4, 3, 4);
            selectOrgSignatory.Name = "selectOrgSignatory";
            selectOrgSignatory.Padding = new Padding(0, 0, 0, 7);
            selectOrgSignatory.RemoveEmptyFolders = false;
            selectOrgSignatory.ShowDeleteButton = true;
            selectOrgSignatory.ShowOpenButton = true;
            selectOrgSignatory.Size = new Size(765, 32);
            selectOrgSignatory.TabIndex = 6;
            selectOrgSignatory.OpenButtonClick += SelectOrgSignatory_OpenButtonClick;
            // 
            // toggleTax
            // 
            toggleTax.Dock = DockStyle.Top;
            toggleTax.EditorFitToSize = false;
            toggleTax.EditorWidth = 90;
            toggleTax.EnabledEditor = true;
            toggleTax.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            toggleTax.Header = "Плательщик НДС";
            toggleTax.HeaderAutoSize = false;
            toggleTax.HeaderTextAlign = ContentAlignment.TopLeft;
            toggleTax.HeaderVisible = true;
            toggleTax.HeaderWidth = 160;
            toggleTax.Location = new Point(0, 160);
            toggleTax.Margin = new Padding(4, 3, 4, 3);
            toggleTax.Name = "toggleTax";
            toggleTax.Padding = new Padding(0, 0, 0, 7);
            toggleTax.Size = new Size(765, 32);
            toggleTax.TabIndex = 7;
            toggleTax.ToggleValue = false;
            // 
            // textPaymentPeriod
            // 
            textPaymentPeriod.Dock = DockStyle.Top;
            textPaymentPeriod.EditorFitToSize = false;
            textPaymentPeriod.EditorWidth = 100;
            textPaymentPeriod.EnabledEditor = true;
            textPaymentPeriod.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPaymentPeriod.Header = "Срок оплаты, дней";
            textPaymentPeriod.HeaderAutoSize = false;
            textPaymentPeriod.HeaderTextAlign = ContentAlignment.TopLeft;
            textPaymentPeriod.HeaderVisible = true;
            textPaymentPeriod.HeaderWidth = 160;
            textPaymentPeriod.IntegerValue = 0L;
            textPaymentPeriod.Location = new Point(0, 320);
            textPaymentPeriod.Margin = new Padding(3, 4, 3, 4);
            textPaymentPeriod.Name = "textPaymentPeriod";
            textPaymentPeriod.Padding = new Padding(0, 0, 0, 7);
            textPaymentPeriod.Size = new Size(765, 32);
            textPaymentPeriod.TabIndex = 10;
            // 
            // toggleDefault
            // 
            toggleDefault.Dock = DockStyle.Top;
            toggleDefault.EditorFitToSize = false;
            toggleDefault.EditorWidth = 90;
            toggleDefault.EnabledEditor = true;
            toggleDefault.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            toggleDefault.Header = "Основной";
            toggleDefault.HeaderAutoSize = false;
            toggleDefault.HeaderTextAlign = ContentAlignment.TopLeft;
            toggleDefault.HeaderVisible = true;
            toggleDefault.HeaderWidth = 160;
            toggleDefault.Location = new Point(0, 352);
            toggleDefault.Margin = new Padding(4, 3, 4, 3);
            toggleDefault.Name = "toggleDefault";
            toggleDefault.Padding = new Padding(0, 0, 0, 7);
            toggleDefault.Size = new Size(765, 32);
            toggleDefault.TabIndex = 11;
            toggleDefault.ToggleValue = false;
            // 
            // choiceType
            // 
            choiceType.Dock = DockStyle.Top;
            choiceType.EditorFitToSize = false;
            choiceType.EditorWidth = 200;
            choiceType.EnabledEditor = true;
            choiceType.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            choiceType.Header = "Вид договора";
            choiceType.HeaderAutoSize = false;
            choiceType.HeaderTextAlign = ContentAlignment.TopLeft;
            choiceType.HeaderVisible = true;
            choiceType.HeaderWidth = 160;
            choiceType.Location = new Point(0, 32);
            choiceType.Margin = new Padding(3, 4, 3, 4);
            choiceType.Name = "choiceType";
            choiceType.Padding = new Padding(0, 0, 0, 7);
            choiceType.ShowDeleteButton = false;
            choiceType.Size = new Size(765, 32);
            choiceType.TabIndex = 12;
            // 
            // dateEnd
            // 
            dateEnd.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateEnd.Dock = DockStyle.Top;
            dateEnd.EditorFitToSize = false;
            dateEnd.EditorWidth = 120;
            dateEnd.EnabledEditor = true;
            dateEnd.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateEnd.Format = DateTimePickerFormat.Short;
            dateEnd.Header = "Окончание действия";
            dateEnd.HeaderAutoSize = false;
            dateEnd.HeaderTextAlign = ContentAlignment.TopLeft;
            dateEnd.HeaderVisible = true;
            dateEnd.HeaderWidth = 160;
            dateEnd.Location = new Point(0, 288);
            dateEnd.Margin = new Padding(3, 5, 3, 5);
            dateEnd.Name = "dateEnd";
            dateEnd.Padding = new Padding(0, 0, 0, 7);
            dateEnd.Required = false;
            dateEnd.Size = new Size(765, 32);
            dateEnd.TabIndex = 13;
            // 
            // dateStart
            // 
            dateStart.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateStart.Dock = DockStyle.Top;
            dateStart.EditorFitToSize = false;
            dateStart.EditorWidth = 120;
            dateStart.EnabledEditor = true;
            dateStart.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateStart.Format = DateTimePickerFormat.Short;
            dateStart.Header = "Начало действия";
            dateStart.HeaderAutoSize = false;
            dateStart.HeaderTextAlign = ContentAlignment.TopLeft;
            dateStart.HeaderVisible = true;
            dateStart.HeaderWidth = 160;
            dateStart.Location = new Point(0, 256);
            dateStart.Margin = new Padding(3, 5, 3, 5);
            dateStart.Name = "dateStart";
            dateStart.Padding = new Padding(0, 0, 0, 7);
            dateStart.Required = true;
            dateStart.Size = new Size(765, 32);
            dateStart.TabIndex = 14;
            // 
            // ContractEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(toggleDefault);
            Controls.Add(textPaymentPeriod);
            Controls.Add(dateEnd);
            Controls.Add(dateStart);
            Controls.Add(selectOrgSignatory);
            Controls.Add(selectSignatory);
            Controls.Add(toggleTax);
            Controls.Add(dateContract);
            Controls.Add(textName);
            Controls.Add(textNumber);
            Controls.Add(choiceType);
            Controls.Add(textContractor);
            Name = "ContractEditor";
            Size = new Size(765, 423);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textContractor;
        private Controls.Editors.DfTextBox textNumber;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfDateTimePicker dateContract;
        private Controls.Editors.DfDirectorySelectBox selectSignatory;
        private Controls.Editors.DfDirectorySelectBox selectOrgSignatory;
        private Controls.Editors.DfToggleButton toggleTax;
        private Controls.Editors.DfIntegerTextBox textPaymentPeriod;
        private Controls.Editors.DfToggleButton toggleDefault;
        private Controls.Editors.DfChoice choiceType;
        private Controls.Editors.DfDateTimePicker dateEnd;
        private Controls.Editors.DfDateTimePicker dateStart;
    }
}
