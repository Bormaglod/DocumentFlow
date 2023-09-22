namespace DocumentFlow.ViewModels
{
    partial class BaseEmployeeEditor
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
            textOrg = new Controls.Editors.DfTextBox();
            selectPerson = new Controls.Editors.DfDirectorySelectBox();
            selectPost = new Controls.Editors.DfDirectorySelectBox();
            textPhone = new Controls.Editors.DfTextBox();
            textEmail = new Controls.Editors.DfTextBox();
            choiceRole = new Controls.Editors.DfChoice();
            selectIncomeItems = new Controls.Editors.DfMultiSelectionComboBox();
            SuspendLayout();
            // 
            // textOrg
            // 
            textOrg.Dock = DockStyle.Top;
            textOrg.EditorFitToSize = false;
            textOrg.EditorWidth = 400;
            textOrg.EnabledEditor = false;
            textOrg.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textOrg.Header = "Организация";
            textOrg.HeaderAutoSize = false;
            textOrg.HeaderDock = DockStyle.Left;
            textOrg.HeaderTextAlign = ContentAlignment.TopLeft;
            textOrg.HeaderVisible = true;
            textOrg.HeaderWidth = 100;
            textOrg.Location = new Point(0, 0);
            textOrg.Margin = new Padding(3, 4, 3, 4);
            textOrg.Multiline = false;
            textOrg.Name = "textOrg";
            textOrg.Padding = new Padding(0, 0, 0, 7);
            textOrg.Size = new Size(871, 32);
            textOrg.TabIndex = 0;
            textOrg.TextValue = "";
            // 
            // selectPerson
            // 
            selectPerson.CanSelectFolder = false;
            selectPerson.Dock = DockStyle.Top;
            selectPerson.EditorFitToSize = false;
            selectPerson.EditorWidth = 300;
            selectPerson.EnabledEditor = true;
            selectPerson.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectPerson.Header = "Сотрудник";
            selectPerson.HeaderAutoSize = false;
            selectPerson.HeaderDock = DockStyle.Left;
            selectPerson.HeaderTextAlign = ContentAlignment.TopLeft;
            selectPerson.HeaderVisible = true;
            selectPerson.HeaderWidth = 100;
            selectPerson.Location = new Point(0, 32);
            selectPerson.Margin = new Padding(3, 4, 3, 4);
            selectPerson.Name = "selectPerson";
            selectPerson.Padding = new Padding(0, 0, 0, 7);
            selectPerson.RemoveEmptyFolders = false;
            selectPerson.ShowDeleteButton = true;
            selectPerson.ShowOpenButton = true;
            selectPerson.Size = new Size(871, 32);
            selectPerson.TabIndex = 1;
            selectPerson.OpenButtonClick += SelectPerson_OpenButtonClick;
            // 
            // selectPost
            // 
            selectPost.CanSelectFolder = false;
            selectPost.Dock = DockStyle.Top;
            selectPost.EditorFitToSize = false;
            selectPost.EditorWidth = 300;
            selectPost.EnabledEditor = true;
            selectPost.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectPost.Header = "Должность";
            selectPost.HeaderAutoSize = false;
            selectPost.HeaderDock = DockStyle.Left;
            selectPost.HeaderTextAlign = ContentAlignment.TopLeft;
            selectPost.HeaderVisible = true;
            selectPost.HeaderWidth = 100;
            selectPost.Location = new Point(0, 64);
            selectPost.Margin = new Padding(3, 4, 3, 4);
            selectPost.Name = "selectPost";
            selectPost.Padding = new Padding(0, 0, 0, 7);
            selectPost.RemoveEmptyFolders = false;
            selectPost.ShowDeleteButton = true;
            selectPost.ShowOpenButton = true;
            selectPost.Size = new Size(871, 32);
            selectPost.TabIndex = 2;
            selectPost.OpenButtonClick += SelectPost_OpenButtonClick;
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
            textPhone.HeaderDock = DockStyle.Left;
            textPhone.HeaderTextAlign = ContentAlignment.TopLeft;
            textPhone.HeaderVisible = true;
            textPhone.HeaderWidth = 100;
            textPhone.Location = new Point(0, 96);
            textPhone.Margin = new Padding(3, 4, 3, 4);
            textPhone.Multiline = false;
            textPhone.Name = "textPhone";
            textPhone.Padding = new Padding(0, 0, 0, 7);
            textPhone.Size = new Size(871, 32);
            textPhone.TabIndex = 3;
            textPhone.TextValue = "";
            // 
            // textEmail
            // 
            textEmail.Dock = DockStyle.Top;
            textEmail.EditorFitToSize = false;
            textEmail.EditorWidth = 200;
            textEmail.EnabledEditor = true;
            textEmail.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textEmail.Header = "Эл. почта";
            textEmail.HeaderAutoSize = false;
            textEmail.HeaderDock = DockStyle.Left;
            textEmail.HeaderTextAlign = ContentAlignment.TopLeft;
            textEmail.HeaderVisible = true;
            textEmail.HeaderWidth = 100;
            textEmail.Location = new Point(0, 128);
            textEmail.Margin = new Padding(3, 4, 3, 4);
            textEmail.Multiline = false;
            textEmail.Name = "textEmail";
            textEmail.Padding = new Padding(0, 0, 0, 7);
            textEmail.Size = new Size(871, 32);
            textEmail.TabIndex = 4;
            textEmail.TextValue = "";
            // 
            // choiceRole
            // 
            choiceRole.Dock = DockStyle.Top;
            choiceRole.EditorFitToSize = false;
            choiceRole.EditorWidth = 150;
            choiceRole.EnabledEditor = true;
            choiceRole.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            choiceRole.Header = "Роль";
            choiceRole.HeaderAutoSize = false;
            choiceRole.HeaderDock = DockStyle.Left;
            choiceRole.HeaderTextAlign = ContentAlignment.TopLeft;
            choiceRole.HeaderVisible = true;
            choiceRole.HeaderWidth = 100;
            choiceRole.Location = new Point(0, 160);
            choiceRole.Margin = new Padding(3, 4, 3, 4);
            choiceRole.Name = "choiceRole";
            choiceRole.Padding = new Padding(0, 0, 0, 7);
            choiceRole.ShowDeleteButton = true;
            choiceRole.Size = new Size(871, 32);
            choiceRole.TabIndex = 5;
            // 
            // selectIncomeItems
            // 
            selectIncomeItems.Dock = DockStyle.Top;
            selectIncomeItems.EditorFitToSize = false;
            selectIncomeItems.EditorWidth = 500;
            selectIncomeItems.EnabledEditor = true;
            selectIncomeItems.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectIncomeItems.Header = "Статьи дохода";
            selectIncomeItems.HeaderAutoSize = false;
            selectIncomeItems.HeaderDock = DockStyle.Left;
            selectIncomeItems.HeaderTextAlign = ContentAlignment.TopLeft;
            selectIncomeItems.HeaderVisible = true;
            selectIncomeItems.HeaderWidth = 100;
            selectIncomeItems.Location = new Point(0, 192);
            selectIncomeItems.Margin = new Padding(3, 4, 3, 4);
            selectIncomeItems.MinimumSize = new Size(0, 32);
            selectIncomeItems.Name = "selectIncomeItems";
            selectIncomeItems.Padding = new Padding(0, 0, 0, 7);
            selectIncomeItems.Size = new Size(871, 32);
            selectIncomeItems.TabIndex = 6;
            selectIncomeItems.Visible = false;
            // 
            // BaseEmployeeEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(selectIncomeItems);
            Controls.Add(choiceRole);
            Controls.Add(textEmail);
            Controls.Add(textPhone);
            Controls.Add(selectPost);
            Controls.Add(selectPerson);
            Controls.Add(textOrg);
            Name = "BaseEmployeeEditor";
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textOrg;
        private Controls.Editors.DfDirectorySelectBox selectPerson;
        private Controls.Editors.DfDirectorySelectBox selectPost;
        private Controls.Editors.DfTextBox textPhone;
        private Controls.Editors.DfTextBox textEmail;
        private Controls.Editors.DfChoice choiceRole;
        private Controls.Editors.DfMultiSelectionComboBox selectIncomeItems;
    }
}
