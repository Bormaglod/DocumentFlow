namespace DocumentFlow.ViewModels
{
    partial class BaseAccountEditor
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
            textName = new Controls.Editors.DfTextBox();
            textAccount = new Controls.Editors.DfMaskedTextBox();
            comboBank = new Controls.Editors.DfComboBox();
            comboBoxAdv1 = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            ((System.ComponentModel.ISupportInitialize)comboBoxAdv1).BeginInit();
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
            textContractor.HeaderWidth = 100;
            textContractor.Location = new Point(0, 0);
            textContractor.Margin = new Padding(3, 4, 3, 4);
            textContractor.Multiline = false;
            textContractor.Name = "textContractor";
            textContractor.Padding = new Padding(0, 0, 0, 7);
            textContractor.Size = new Size(816, 32);
            textContractor.TabIndex = 0;
            textContractor.TextValue = "";
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
            textName.HeaderWidth = 100;
            textName.Location = new Point(0, 32);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(816, 32);
            textName.TabIndex = 1;
            textName.TextValue = "";
            // 
            // textAccount
            // 
            textAccount.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textAccount.Dock = DockStyle.Top;
            textAccount.EditorFitToSize = false;
            textAccount.EditorWidth = 200;
            textAccount.EnabledEditor = true;
            textAccount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textAccount.Header = "Номер счёта";
            textAccount.HeaderAutoSize = false;
            textAccount.HeaderTextAlign = ContentAlignment.TopLeft;
            textAccount.HeaderVisible = true;
            textAccount.HeaderWidth = 100;
            textAccount.LeadingZero = true;
            textAccount.Location = new Point(0, 64);
            textAccount.Margin = new Padding(3, 4, 3, 4);
            textAccount.Mask = "### ## ### # #### #######";
            textAccount.MaxLength = 20;
            textAccount.Name = "textAccount";
            textAccount.Padding = new Padding(0, 0, 0, 7);
            textAccount.PromptCharacter = ' ';
            textAccount.ShowSuffix = false;
            textAccount.Size = new Size(816, 32);
            textAccount.TabIndex = 2;
            // 
            // comboBank
            // 
            comboBank.Dock = DockStyle.Top;
            comboBank.EditorFitToSize = false;
            comboBank.EditorWidth = 400;
            comboBank.EnabledEditor = true;
            comboBank.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboBank.Header = "Банк";
            comboBank.HeaderAutoSize = false;
            comboBank.HeaderTextAlign = ContentAlignment.TopLeft;
            comboBank.HeaderVisible = true;
            comboBank.HeaderWidth = 100;
            comboBank.Location = new Point(0, 96);
            comboBank.Margin = new Padding(3, 4, 3, 4);
            comboBank.Name = "comboBank";
            comboBank.Padding = new Padding(0, 0, 0, 7);
            comboBank.Size = new Size(816, 32);
            comboBank.TabIndex = 3;
            comboBank.OpenButtonClick += ComboBank_OpenButtonClick;
            // 
            // comboBoxAdv1
            // 
            comboBoxAdv1.BackColor = Color.FromArgb(255, 255, 255);
            comboBoxAdv1.BeforeTouchSize = new Size(200, 93);
            comboBoxAdv1.Dock = DockStyle.Fill;
            comboBoxAdv1.ForeColor = Color.FromArgb(68, 68, 68);
            comboBoxAdv1.Location = new Point(0, 0);
            comboBoxAdv1.Name = "comboBoxAdv1";
            comboBoxAdv1.Size = new Size(200, 93);
            comboBoxAdv1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            comboBoxAdv1.TabIndex = 4;
            comboBoxAdv1.TextBoxHeight = 24;
            comboBoxAdv1.ThemeName = "Office2016Colorful";
            // 
            // AccountEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(comboBank);
            Controls.Add(textAccount);
            Controls.Add(textName);
            Controls.Add(textContractor);
            Name = "AccountEditor";
            Size = new Size(816, 383);
            ((System.ComponentModel.ISupportInitialize)comboBoxAdv1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textContractor;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfMaskedTextBox textAccount;
        private Controls.Editors.DfComboBox comboBank;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboBoxAdv1;
    }
}
