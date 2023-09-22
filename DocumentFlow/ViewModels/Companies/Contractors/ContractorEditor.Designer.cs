namespace DocumentFlow.ViewModels
{
    partial class ContractorEditor
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
            selectGroup = new Controls.Editors.DfDirectorySelectBox();
            textName = new Controls.Editors.DfTextBox();
            textFullName = new Controls.Editors.DfTextBox();
            choiceSubject = new Controls.Editors.DfChoice();
            textInn = new Controls.Editors.DfMaskedTextBox();
            textKpp = new Controls.Editors.DfMaskedTextBox();
            textOgrn = new Controls.Editors.DfMaskedTextBox();
            textOkpo = new Controls.Editors.DfMaskedTextBox();
            comboOkopf = new Controls.Editors.DfComboBox();
            comboAccount = new Controls.Editors.DfComboBox();
            selectPerson = new Controls.Editors.DfDirectorySelectBox();
            SuspendLayout();
            // 
            // textCode
            // 
            textCode.Dock = DockStyle.Top;
            textCode.EditorFitToSize = false;
            textCode.EditorWidth = 400;
            textCode.EnabledEditor = true;
            textCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textCode.Header = "Наименование";
            textCode.HeaderAutoSize = false;
            textCode.HeaderDock = DockStyle.Left;
            textCode.HeaderTextAlign = ContentAlignment.TopLeft;
            textCode.HeaderVisible = true;
            textCode.HeaderWidth = 160;
            textCode.Location = new Point(0, 0);
            textCode.Margin = new Padding(3, 4, 3, 4);
            textCode.Multiline = false;
            textCode.Name = "textCode";
            textCode.Padding = new Padding(0, 0, 0, 7);
            textCode.Size = new Size(871, 32);
            textCode.TabIndex = 0;
            textCode.TextValue = "";
            // 
            // selectGroup
            // 
            selectGroup.CanSelectFolder = false;
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
            selectGroup.HeaderWidth = 160;
            selectGroup.Location = new Point(0, 32);
            selectGroup.Margin = new Padding(3, 4, 3, 4);
            selectGroup.Name = "selectGroup";
            selectGroup.Padding = new Padding(0, 0, 0, 7);
            selectGroup.RemoveEmptyFolders = false;
            selectGroup.ShowDeleteButton = false;
            selectGroup.ShowOpenButton = false;
            selectGroup.Size = new Size(871, 32);
            selectGroup.TabIndex = 1;
            // 
            // textName
            // 
            textName.Dock = DockStyle.Top;
            textName.EditorFitToSize = false;
            textName.EditorWidth = 500;
            textName.EnabledEditor = true;
            textName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textName.Header = "Короткое наименование";
            textName.HeaderAutoSize = false;
            textName.HeaderDock = DockStyle.Left;
            textName.HeaderTextAlign = ContentAlignment.TopLeft;
            textName.HeaderVisible = true;
            textName.HeaderWidth = 160;
            textName.Location = new Point(0, 64);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(871, 32);
            textName.TabIndex = 2;
            textName.TextValue = "";
            // 
            // textFullName
            // 
            textFullName.Dock = DockStyle.Top;
            textFullName.EditorFitToSize = false;
            textFullName.EditorWidth = 500;
            textFullName.EnabledEditor = true;
            textFullName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textFullName.Header = "Полное наименование";
            textFullName.HeaderAutoSize = false;
            textFullName.HeaderDock = DockStyle.Left;
            textFullName.HeaderTextAlign = ContentAlignment.TopLeft;
            textFullName.HeaderVisible = true;
            textFullName.HeaderWidth = 160;
            textFullName.Location = new Point(0, 96);
            textFullName.Margin = new Padding(3, 4, 3, 4);
            textFullName.Multiline = true;
            textFullName.Name = "textFullName";
            textFullName.Padding = new Padding(0, 0, 0, 7);
            textFullName.Size = new Size(871, 75);
            textFullName.TabIndex = 3;
            textFullName.TextValue = "";
            // 
            // choiceSubject
            // 
            choiceSubject.Dock = DockStyle.Top;
            choiceSubject.EditorFitToSize = false;
            choiceSubject.EditorWidth = 200;
            choiceSubject.EnabledEditor = true;
            choiceSubject.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            choiceSubject.Header = "Субъект права";
            choiceSubject.HeaderAutoSize = false;
            choiceSubject.HeaderDock = DockStyle.Left;
            choiceSubject.HeaderTextAlign = ContentAlignment.TopLeft;
            choiceSubject.HeaderVisible = true;
            choiceSubject.HeaderWidth = 160;
            choiceSubject.Location = new Point(0, 171);
            choiceSubject.Margin = new Padding(3, 4, 3, 4);
            choiceSubject.Name = "choiceSubject";
            choiceSubject.Padding = new Padding(0, 0, 0, 7);
            choiceSubject.ShowDeleteButton = false;
            choiceSubject.Size = new Size(871, 32);
            choiceSubject.TabIndex = 4;
            // 
            // textInn
            // 
            textInn.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textInn.Dock = DockStyle.Top;
            textInn.EditorFitToSize = false;
            textInn.EditorWidth = 170;
            textInn.EnabledEditor = true;
            textInn.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textInn.Header = "ИНН";
            textInn.HeaderAutoSize = false;
            textInn.HeaderDock = DockStyle.Left;
            textInn.HeaderTextAlign = ContentAlignment.TopLeft;
            textInn.HeaderVisible = true;
            textInn.HeaderWidth = 160;
            textInn.LeadingZero = true;
            textInn.Location = new Point(0, 203);
            textInn.Margin = new Padding(3, 4, 3, 4);
            textInn.Mask = "";
            textInn.MaxLength = 0;
            textInn.Name = "textInn";
            textInn.Padding = new Padding(0, 0, 0, 7);
            textInn.PromptCharacter = ' ';
            textInn.ShowSuffix = false;
            textInn.Size = new Size(871, 32);
            textInn.Suffix = "суффикс";
            textInn.TabIndex = 5;
            // 
            // textKpp
            // 
            textKpp.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textKpp.Dock = DockStyle.Top;
            textKpp.EditorFitToSize = false;
            textKpp.EditorWidth = 170;
            textKpp.EnabledEditor = true;
            textKpp.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textKpp.Header = "КПП";
            textKpp.HeaderAutoSize = false;
            textKpp.HeaderDock = DockStyle.Left;
            textKpp.HeaderTextAlign = ContentAlignment.TopLeft;
            textKpp.HeaderVisible = true;
            textKpp.HeaderWidth = 160;
            textKpp.LeadingZero = true;
            textKpp.Location = new Point(0, 235);
            textKpp.Margin = new Padding(3, 4, 3, 4);
            textKpp.Mask = "#### ## ###";
            textKpp.MaxLength = 9;
            textKpp.Name = "textKpp";
            textKpp.Padding = new Padding(0, 0, 0, 7);
            textKpp.PromptCharacter = ' ';
            textKpp.ShowSuffix = false;
            textKpp.Size = new Size(871, 32);
            textKpp.Suffix = "суффикс";
            textKpp.TabIndex = 6;
            // 
            // textOgrn
            // 
            textOgrn.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textOgrn.Dock = DockStyle.Top;
            textOgrn.EditorFitToSize = false;
            textOgrn.EditorWidth = 170;
            textOgrn.EnabledEditor = true;
            textOgrn.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textOgrn.Header = "ОГРН";
            textOgrn.HeaderAutoSize = false;
            textOgrn.HeaderDock = DockStyle.Left;
            textOgrn.HeaderTextAlign = ContentAlignment.TopLeft;
            textOgrn.HeaderVisible = true;
            textOgrn.HeaderWidth = 160;
            textOgrn.LeadingZero = true;
            textOgrn.Location = new Point(0, 267);
            textOgrn.Margin = new Padding(3, 4, 3, 4);
            textOgrn.Mask = "# ## ## ## ##### #";
            textOgrn.MaxLength = 13;
            textOgrn.Name = "textOgrn";
            textOgrn.Padding = new Padding(0, 0, 0, 7);
            textOgrn.PromptCharacter = ' ';
            textOgrn.ShowSuffix = false;
            textOgrn.Size = new Size(871, 32);
            textOgrn.Suffix = "суффикс";
            textOgrn.TabIndex = 7;
            // 
            // textOkpo
            // 
            textOkpo.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textOkpo.Dock = DockStyle.Top;
            textOkpo.EditorFitToSize = false;
            textOkpo.EditorWidth = 170;
            textOkpo.EnabledEditor = true;
            textOkpo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textOkpo.Header = "ОКПО";
            textOkpo.HeaderAutoSize = false;
            textOkpo.HeaderDock = DockStyle.Left;
            textOkpo.HeaderTextAlign = ContentAlignment.TopLeft;
            textOkpo.HeaderVisible = true;
            textOkpo.HeaderWidth = 160;
            textOkpo.LeadingZero = true;
            textOkpo.Location = new Point(0, 299);
            textOkpo.Margin = new Padding(3, 4, 3, 4);
            textOkpo.Mask = "## ##### #";
            textOkpo.MaxLength = 8;
            textOkpo.Name = "textOkpo";
            textOkpo.Padding = new Padding(0, 0, 0, 7);
            textOkpo.PromptCharacter = ' ';
            textOkpo.ShowSuffix = false;
            textOkpo.Size = new Size(871, 32);
            textOkpo.Suffix = "суффикс";
            textOkpo.TabIndex = 8;
            // 
            // comboOkopf
            // 
            comboOkopf.Dock = DockStyle.Top;
            comboOkopf.EditorFitToSize = false;
            comboOkopf.EditorWidth = 450;
            comboOkopf.EnabledEditor = true;
            comboOkopf.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboOkopf.Header = "ОКОПФ";
            comboOkopf.HeaderAutoSize = false;
            comboOkopf.HeaderDock = DockStyle.Left;
            comboOkopf.HeaderTextAlign = ContentAlignment.TopLeft;
            comboOkopf.HeaderVisible = true;
            comboOkopf.HeaderWidth = 160;
            comboOkopf.Location = new Point(0, 331);
            comboOkopf.Margin = new Padding(3, 4, 3, 4);
            comboOkopf.Name = "comboOkopf";
            comboOkopf.Padding = new Padding(0, 0, 0, 7);
            comboOkopf.ShowDeleteButton = true;
            comboOkopf.ShowOpenButton = true;
            comboOkopf.Size = new Size(871, 32);
            comboOkopf.TabIndex = 9;
            comboOkopf.OpenButtonClick += ComboOkopf_OpenButtonClick;
            // 
            // comboAccount
            // 
            comboAccount.Dock = DockStyle.Top;
            comboAccount.EditorFitToSize = false;
            comboAccount.EditorWidth = 450;
            comboAccount.EnabledEditor = true;
            comboAccount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboAccount.Header = "Расчётный счёт";
            comboAccount.HeaderAutoSize = false;
            comboAccount.HeaderDock = DockStyle.Left;
            comboAccount.HeaderTextAlign = ContentAlignment.TopLeft;
            comboAccount.HeaderVisible = true;
            comboAccount.HeaderWidth = 160;
            comboAccount.Location = new Point(0, 363);
            comboAccount.Margin = new Padding(3, 4, 3, 4);
            comboAccount.Name = "comboAccount";
            comboAccount.Padding = new Padding(0, 0, 0, 7);
            comboAccount.ShowDeleteButton = true;
            comboAccount.ShowOpenButton = true;
            comboAccount.Size = new Size(871, 32);
            comboAccount.TabIndex = 10;
            // 
            // selectPerson
            // 
            selectPerson.CanSelectFolder = false;
            selectPerson.Dock = DockStyle.Top;
            selectPerson.EditorFitToSize = false;
            selectPerson.EditorWidth = 300;
            selectPerson.EnabledEditor = true;
            selectPerson.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectPerson.Header = "Физ. лицо";
            selectPerson.HeaderAutoSize = false;
            selectPerson.HeaderDock = DockStyle.Left;
            selectPerson.HeaderTextAlign = ContentAlignment.TopLeft;
            selectPerson.HeaderVisible = true;
            selectPerson.HeaderWidth = 160;
            selectPerson.Location = new Point(0, 395);
            selectPerson.Margin = new Padding(3, 4, 3, 4);
            selectPerson.Name = "selectPerson";
            selectPerson.Padding = new Padding(0, 0, 0, 7);
            selectPerson.RemoveEmptyFolders = false;
            selectPerson.ShowDeleteButton = true;
            selectPerson.ShowOpenButton = true;
            selectPerson.Size = new Size(871, 32);
            selectPerson.TabIndex = 11;
            selectPerson.OpenButtonClick += SelectPerson_OpenButtonClick;
            // 
            // ContractorEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(selectPerson);
            Controls.Add(comboAccount);
            Controls.Add(comboOkopf);
            Controls.Add(textOkpo);
            Controls.Add(textOgrn);
            Controls.Add(textKpp);
            Controls.Add(textInn);
            Controls.Add(choiceSubject);
            Controls.Add(textFullName);
            Controls.Add(textName);
            Controls.Add(selectGroup);
            Controls.Add(textCode);
            Name = "ContractorEditor";
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textCode;
        private Controls.Editors.DfDirectorySelectBox selectGroup;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfTextBox textFullName;
        private Controls.Editors.DfChoice choiceSubject;
        private Controls.Editors.DfMaskedTextBox textInn;
        private Controls.Editors.DfMaskedTextBox textKpp;
        private Controls.Editors.DfMaskedTextBox textOgrn;
        private Controls.Editors.DfMaskedTextBox textOkpo;
        private Controls.Editors.DfComboBox comboOkopf;
        private Controls.Editors.DfComboBox comboAccount;
        private Controls.Editors.DfDirectorySelectBox selectPerson;
    }
}
