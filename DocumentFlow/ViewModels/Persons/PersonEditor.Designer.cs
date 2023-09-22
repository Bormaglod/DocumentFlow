namespace DocumentFlow.ViewModels
{
    partial class PersonEditor
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
            textSurname = new Controls.Editors.DfTextBox();
            textFirstName = new Controls.Editors.DfTextBox();
            textMiddleName = new Controls.Editors.DfTextBox();
            textPhone = new Controls.Editors.DfTextBox();
            textEmail = new Controls.Editors.DfTextBox();
            SuspendLayout();
            // 
            // textName
            // 
            textName.Dock = DockStyle.Top;
            textName.EditorFitToSize = false;
            textName.EditorWidth = 250;
            textName.Enabled = false;
            textName.EnabledEditor = false;
            textName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textName.Header = "Фамилия И.О.";
            textName.HeaderAutoSize = false;
            textName.HeaderTextAlign = ContentAlignment.TopLeft;
            textName.HeaderVisible = true;
            textName.HeaderWidth = 100;
            textName.Location = new Point(0, 0);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(694, 32);
            textName.TabIndex = 0;
            textName.TextValue = "";
            // 
            // textSurname
            // 
            textSurname.Dock = DockStyle.Top;
            textSurname.EditorFitToSize = false;
            textSurname.EditorWidth = 200;
            textSurname.EnabledEditor = true;
            textSurname.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textSurname.Header = "Фамилия";
            textSurname.HeaderAutoSize = false;
            textSurname.HeaderTextAlign = ContentAlignment.TopLeft;
            textSurname.HeaderVisible = true;
            textSurname.HeaderWidth = 100;
            textSurname.Location = new Point(0, 32);
            textSurname.Margin = new Padding(3, 4, 3, 4);
            textSurname.Multiline = false;
            textSurname.Name = "textSurname";
            textSurname.Padding = new Padding(0, 0, 0, 7);
            textSurname.Size = new Size(694, 32);
            textSurname.TabIndex = 1;
            textSurname.TextValue = "";
            // 
            // textFirstName
            // 
            textFirstName.Dock = DockStyle.Top;
            textFirstName.EditorFitToSize = false;
            textFirstName.EditorWidth = 200;
            textFirstName.EnabledEditor = true;
            textFirstName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textFirstName.Header = "Имя";
            textFirstName.HeaderAutoSize = false;
            textFirstName.HeaderTextAlign = ContentAlignment.TopLeft;
            textFirstName.HeaderVisible = true;
            textFirstName.HeaderWidth = 100;
            textFirstName.Location = new Point(0, 64);
            textFirstName.Margin = new Padding(3, 4, 3, 4);
            textFirstName.Multiline = false;
            textFirstName.Name = "textFirstName";
            textFirstName.Padding = new Padding(0, 0, 0, 7);
            textFirstName.Size = new Size(694, 32);
            textFirstName.TabIndex = 2;
            textFirstName.TextValue = "";
            // 
            // textMiddleName
            // 
            textMiddleName.Dock = DockStyle.Top;
            textMiddleName.EditorFitToSize = false;
            textMiddleName.EditorWidth = 200;
            textMiddleName.EnabledEditor = true;
            textMiddleName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textMiddleName.Header = "Отчество";
            textMiddleName.HeaderAutoSize = false;
            textMiddleName.HeaderTextAlign = ContentAlignment.TopLeft;
            textMiddleName.HeaderVisible = true;
            textMiddleName.HeaderWidth = 100;
            textMiddleName.Location = new Point(0, 96);
            textMiddleName.Margin = new Padding(3, 4, 3, 4);
            textMiddleName.Multiline = false;
            textMiddleName.Name = "textMiddleName";
            textMiddleName.Padding = new Padding(0, 0, 0, 7);
            textMiddleName.Size = new Size(694, 32);
            textMiddleName.TabIndex = 3;
            textMiddleName.TextValue = "";
            // 
            // textPhone
            // 
            textPhone.Dock = DockStyle.Top;
            textPhone.EditorFitToSize = false;
            textPhone.EditorWidth = 200;
            textPhone.EnabledEditor = true;
            textPhone.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPhone.Header = "Телефон";
            textPhone.HeaderAutoSize = false;
            textPhone.HeaderTextAlign = ContentAlignment.TopLeft;
            textPhone.HeaderVisible = true;
            textPhone.HeaderWidth = 100;
            textPhone.Location = new Point(0, 128);
            textPhone.Margin = new Padding(3, 4, 3, 4);
            textPhone.Multiline = false;
            textPhone.Name = "textPhone";
            textPhone.Padding = new Padding(0, 0, 0, 7);
            textPhone.Size = new Size(694, 32);
            textPhone.TabIndex = 4;
            textPhone.TextValue = "";
            // 
            // textEmail
            // 
            textEmail.Dock = DockStyle.Top;
            textEmail.EditorFitToSize = false;
            textEmail.EditorWidth = 300;
            textEmail.EnabledEditor = true;
            textEmail.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textEmail.Header = "Эл. почта";
            textEmail.HeaderAutoSize = false;
            textEmail.HeaderTextAlign = ContentAlignment.TopLeft;
            textEmail.HeaderVisible = true;
            textEmail.HeaderWidth = 100;
            textEmail.Location = new Point(0, 160);
            textEmail.Margin = new Padding(3, 4, 3, 4);
            textEmail.Multiline = false;
            textEmail.Name = "textEmail";
            textEmail.Padding = new Padding(0, 0, 0, 7);
            textEmail.Size = new Size(694, 32);
            textEmail.TabIndex = 5;
            textEmail.TextValue = "";
            // 
            // PersonEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textEmail);
            Controls.Add(textPhone);
            Controls.Add(textMiddleName);
            Controls.Add(textFirstName);
            Controls.Add(textSurname);
            Controls.Add(textName);
            Name = "PersonEditor";
            Size = new Size(694, 320);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfTextBox textSurname;
        private Controls.Editors.DfTextBox textFirstName;
        private Controls.Editors.DfTextBox textMiddleName;
        private Controls.Editors.DfTextBox textPhone;
        private Controls.Editors.DfTextBox textEmail;
    }
}
