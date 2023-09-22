namespace DocumentFlow.ViewModels
{
    partial class OrganizationEditor
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
            textFullName = new Controls.Editors.DfTextBox();
            textInn = new Controls.Editors.DfMaskedTextBox();
            textKpp = new Controls.Editors.DfMaskedTextBox();
            textOgrn = new Controls.Editors.DfMaskedTextBox();
            textOkpo = new Controls.Editors.DfMaskedTextBox();
            comboOkopf = new Controls.Editors.DfComboBox();
            comboAccount = new Controls.Editors.DfComboBox();
            textAddress = new Controls.Editors.DfTextBox();
            textPhone = new Controls.Editors.DfTextBox();
            textEmail = new Controls.Editors.DfTextBox();
            toggleMainOrg = new Controls.Editors.DfToggleButton();
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
            // textName
            // 
            textName.Dock = DockStyle.Top;
            textName.EditorFitToSize = false;
            textName.EditorWidth = 500;
            textName.EnabledEditor = true;
            textName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textName.Header = "Короткое наименование";
            textName.HeaderAutoSize = false;
            textName.HeaderTextAlign = ContentAlignment.TopLeft;
            textName.HeaderVisible = true;
            textName.HeaderWidth = 160;
            textName.Location = new Point(0, 32);
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
            textFullName.HeaderTextAlign = ContentAlignment.TopLeft;
            textFullName.HeaderVisible = true;
            textFullName.HeaderWidth = 160;
            textFullName.Location = new Point(0, 64);
            textFullName.Margin = new Padding(3, 4, 3, 4);
            textFullName.Multiline = true;
            textFullName.Name = "textFullName";
            textFullName.Padding = new Padding(0, 0, 0, 7);
            textFullName.Size = new Size(871, 75);
            textFullName.TabIndex = 3;
            textFullName.TextValue = "";
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
            textInn.HeaderTextAlign = ContentAlignment.TopLeft;
            textInn.HeaderVisible = true;
            textInn.HeaderWidth = 160;
            textInn.LeadingZero = true;
            textInn.Location = new Point(0, 139);
            textInn.Margin = new Padding(3, 4, 3, 4);
            textInn.Mask = "#### ##### #";
            textInn.MaxLength = 10;
            textInn.Name = "textInn";
            textInn.Padding = new Padding(0, 0, 0, 7);
            textInn.PromptCharacter = ' ';
            textInn.ShowSuffix = false;
            textInn.Size = new Size(871, 32);
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
            textKpp.HeaderTextAlign = ContentAlignment.TopLeft;
            textKpp.HeaderVisible = true;
            textKpp.HeaderWidth = 160;
            textKpp.LeadingZero = true;
            textKpp.Location = new Point(0, 171);
            textKpp.Margin = new Padding(3, 4, 3, 4);
            textKpp.Mask = "#### ## ###";
            textKpp.MaxLength = 9;
            textKpp.Name = "textKpp";
            textKpp.Padding = new Padding(0, 0, 0, 7);
            textKpp.PromptCharacter = ' ';
            textKpp.ShowSuffix = false;
            textKpp.Size = new Size(871, 32);
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
            textOgrn.HeaderTextAlign = ContentAlignment.TopLeft;
            textOgrn.HeaderVisible = true;
            textOgrn.HeaderWidth = 160;
            textOgrn.LeadingZero = true;
            textOgrn.Location = new Point(0, 203);
            textOgrn.Margin = new Padding(3, 4, 3, 4);
            textOgrn.Mask = "# ## ## ## ##### #";
            textOgrn.MaxLength = 13;
            textOgrn.Name = "textOgrn";
            textOgrn.Padding = new Padding(0, 0, 0, 7);
            textOgrn.PromptCharacter = ' ';
            textOgrn.ShowSuffix = false;
            textOgrn.Size = new Size(871, 32);
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
            textOkpo.HeaderTextAlign = ContentAlignment.TopLeft;
            textOkpo.HeaderVisible = true;
            textOkpo.HeaderWidth = 160;
            textOkpo.LeadingZero = true;
            textOkpo.Location = new Point(0, 235);
            textOkpo.Margin = new Padding(3, 4, 3, 4);
            textOkpo.Mask = "## ##### #";
            textOkpo.MaxLength = 8;
            textOkpo.Name = "textOkpo";
            textOkpo.Padding = new Padding(0, 0, 0, 7);
            textOkpo.PromptCharacter = ' ';
            textOkpo.ShowSuffix = false;
            textOkpo.Size = new Size(871, 32);
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
            comboOkopf.HeaderTextAlign = ContentAlignment.TopLeft;
            comboOkopf.HeaderVisible = true;
            comboOkopf.HeaderWidth = 160;
            comboOkopf.Location = new Point(0, 267);
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
            comboAccount.HeaderTextAlign = ContentAlignment.TopLeft;
            comboAccount.HeaderVisible = true;
            comboAccount.HeaderWidth = 160;
            comboAccount.Location = new Point(0, 299);
            comboAccount.Margin = new Padding(3, 4, 3, 4);
            comboAccount.Name = "comboAccount";
            comboAccount.Padding = new Padding(0, 0, 0, 7);
            comboAccount.ShowDeleteButton = true;
            comboAccount.ShowOpenButton = true;
            comboAccount.Size = new Size(871, 32);
            comboAccount.TabIndex = 10;
            comboAccount.OpenButtonClick += ComboAccount_OpenButtonClick;
            // 
            // textAddress
            // 
            textAddress.Dock = DockStyle.Top;
            textAddress.EditorFitToSize = false;
            textAddress.EditorWidth = 500;
            textAddress.EnabledEditor = true;
            textAddress.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textAddress.Header = "Адрес";
            textAddress.HeaderAutoSize = false;
            textAddress.HeaderTextAlign = ContentAlignment.TopLeft;
            textAddress.HeaderVisible = true;
            textAddress.HeaderWidth = 160;
            textAddress.Location = new Point(0, 331);
            textAddress.Margin = new Padding(3, 4, 3, 4);
            textAddress.Multiline = true;
            textAddress.Name = "textAddress";
            textAddress.Padding = new Padding(0, 0, 0, 7);
            textAddress.Size = new Size(871, 75);
            textAddress.TabIndex = 11;
            textAddress.TextValue = "";
            // 
            // textPhone
            // 
            textPhone.Dock = DockStyle.Top;
            textPhone.EditorFitToSize = false;
            textPhone.EditorWidth = 250;
            textPhone.EnabledEditor = true;
            textPhone.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPhone.Header = "Телефон";
            textPhone.HeaderAutoSize = false;
            textPhone.HeaderTextAlign = ContentAlignment.TopLeft;
            textPhone.HeaderVisible = true;
            textPhone.HeaderWidth = 160;
            textPhone.Location = new Point(0, 406);
            textPhone.Margin = new Padding(3, 4, 3, 4);
            textPhone.Multiline = false;
            textPhone.Name = "textPhone";
            textPhone.Padding = new Padding(0, 0, 0, 7);
            textPhone.Size = new Size(871, 32);
            textPhone.TabIndex = 12;
            textPhone.TextValue = "";
            // 
            // textEmail
            // 
            textEmail.Dock = DockStyle.Top;
            textEmail.EditorFitToSize = false;
            textEmail.EditorWidth = 250;
            textEmail.EnabledEditor = true;
            textEmail.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textEmail.Header = "Эл. почта";
            textEmail.HeaderAutoSize = false;
            textEmail.HeaderTextAlign = ContentAlignment.TopLeft;
            textEmail.HeaderVisible = true;
            textEmail.HeaderWidth = 160;
            textEmail.Location = new Point(0, 438);
            textEmail.Margin = new Padding(3, 4, 3, 4);
            textEmail.Multiline = false;
            textEmail.Name = "textEmail";
            textEmail.Padding = new Padding(0, 0, 0, 7);
            textEmail.Size = new Size(871, 32);
            textEmail.TabIndex = 13;
            textEmail.TextValue = "";
            // 
            // toggleMainOrg
            // 
            toggleMainOrg.Dock = DockStyle.Top;
            toggleMainOrg.EditorFitToSize = false;
            toggleMainOrg.EditorWidth = 90;
            toggleMainOrg.EnabledEditor = true;
            toggleMainOrg.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            toggleMainOrg.Header = "Основная организация";
            toggleMainOrg.HeaderAutoSize = false;
            toggleMainOrg.HeaderTextAlign = ContentAlignment.TopLeft;
            toggleMainOrg.HeaderVisible = true;
            toggleMainOrg.HeaderWidth = 160;
            toggleMainOrg.Location = new Point(0, 470);
            toggleMainOrg.Margin = new Padding(4, 3, 4, 3);
            toggleMainOrg.Name = "toggleMainOrg";
            toggleMainOrg.Padding = new Padding(0, 0, 0, 7);
            toggleMainOrg.Size = new Size(871, 32);
            toggleMainOrg.TabIndex = 14;
            toggleMainOrg.ToggleValue = false;
            // 
            // OrganizationEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(toggleMainOrg);
            Controls.Add(textEmail);
            Controls.Add(textPhone);
            Controls.Add(textAddress);
            Controls.Add(comboAccount);
            Controls.Add(comboOkopf);
            Controls.Add(textOkpo);
            Controls.Add(textOgrn);
            Controls.Add(textKpp);
            Controls.Add(textInn);
            Controls.Add(textFullName);
            Controls.Add(textName);
            Controls.Add(textCode);
            Name = "OrganizationEditor";
            Size = new Size(871, 512);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textCode;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfTextBox textFullName;
        private Controls.Editors.DfMaskedTextBox textInn;
        private Controls.Editors.DfMaskedTextBox textKpp;
        private Controls.Editors.DfMaskedTextBox textOgrn;
        private Controls.Editors.DfMaskedTextBox textOkpo;
        private Controls.Editors.DfComboBox comboOkopf;
        private Controls.Editors.DfComboBox comboAccount;
        private Controls.Editors.DfTextBox textAddress;
        private Controls.Editors.DfTextBox textPhone;
        private Controls.Editors.DfTextBox textEmail;
        private Controls.Editors.DfToggleButton toggleMainOrg;
    }
}
