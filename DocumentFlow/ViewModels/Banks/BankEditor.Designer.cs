namespace DocumentFlow.ViewModels
{
    partial class BankEditor
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
            textBik = new Controls.Editors.DfMaskedTextBox();
            textAccount = new Controls.Editors.DfMaskedTextBox();
            textTown = new Controls.Editors.DfTextBox();
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
            textName.HeaderTextAlign = ContentAlignment.TopLeft;
            textName.HeaderVisible = true;
            textName.HeaderWidth = 100;
            textName.Location = new Point(0, 0);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(826, 32);
            textName.TabIndex = 0;
            textName.TextValue = "";
            // 
            // textBik
            // 
            textBik.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textBik.Dock = DockStyle.Top;
            textBik.EditorFitToSize = false;
            textBik.EditorWidth = 100;
            textBik.EnabledEditor = true;
            textBik.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBik.Header = "БИК";
            textBik.HeaderAutoSize = false;
            textBik.HeaderTextAlign = ContentAlignment.TopLeft;
            textBik.HeaderVisible = true;
            textBik.HeaderWidth = 100;
            textBik.LeadingZero = true;
            textBik.Location = new Point(0, 32);
            textBik.Margin = new Padding(3, 4, 3, 4);
            textBik.Mask = "## ## ## ###";
            textBik.MaxLength = 9;
            textBik.Name = "textBik";
            textBik.Padding = new Padding(0, 0, 0, 7);
            textBik.PromptCharacter = ' ';
            textBik.ShowSuffix = false;
            textBik.Size = new Size(826, 32);
            textBik.TabIndex = 1;
            // 
            // textAccount
            // 
            textAccount.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textAccount.Dock = DockStyle.Top;
            textAccount.EditorFitToSize = false;
            textAccount.EditorWidth = 200;
            textAccount.EnabledEditor = true;
            textAccount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textAccount.Header = "Корр. счёт";
            textAccount.HeaderAutoSize = false;
            textAccount.HeaderTextAlign = ContentAlignment.TopLeft;
            textAccount.HeaderVisible = true;
            textAccount.HeaderWidth = 100;
            textAccount.LeadingZero = true;
            textAccount.Location = new Point(0, 64);
            textAccount.Margin = new Padding(3, 4, 3, 4);
            textAccount.Mask = "### ## ### # ######## ###";
            textAccount.MaxLength = 20;
            textAccount.Name = "textAccount";
            textAccount.Padding = new Padding(0, 0, 0, 7);
            textAccount.PromptCharacter = ' ';
            textAccount.ShowSuffix = false;
            textAccount.Size = new Size(826, 32);
            textAccount.TabIndex = 2;
            // 
            // textTown
            // 
            textTown.Dock = DockStyle.Top;
            textTown.EditorFitToSize = false;
            textTown.EditorWidth = 484;
            textTown.EnabledEditor = true;
            textTown.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textTown.Header = "Город";
            textTown.HeaderAutoSize = false;
            textTown.HeaderTextAlign = ContentAlignment.TopLeft;
            textTown.HeaderVisible = true;
            textTown.HeaderWidth = 100;
            textTown.Location = new Point(0, 96);
            textTown.Margin = new Padding(3, 4, 3, 4);
            textTown.Multiline = false;
            textTown.Name = "textTown";
            textTown.Padding = new Padding(0, 0, 0, 7);
            textTown.Size = new Size(826, 32);
            textTown.TabIndex = 3;
            textTown.TextValue = "";
            // 
            // BankEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textTown);
            Controls.Add(textAccount);
            Controls.Add(textBik);
            Controls.Add(textName);
            Name = "BankEditor";
            Size = new Size(826, 279);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfMaskedTextBox textBik;
        private Controls.Editors.DfMaskedTextBox textAccount;
        private Controls.Editors.DfTextBox textTown;
    }
}
