namespace DocumentFlow
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            buttonLogin = new Syncfusion.WinForms.Controls.SfButton();
            pictureBox1 = new PictureBox();
            textPassword = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            comboDatabase = new Controls.ComboBoxAdvImage();
            comboUsers = new Controls.ComboBoxAdvImage();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textPassword).BeginInit();
            ((System.ComponentModel.ISupportInitialize)comboDatabase).BeginInit();
            ((System.ComponentModel.ISupportInitialize)comboUsers).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(14, 10);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(139, 30);
            label1.TabIndex = 6;
            label1.Text = "Авторизация";
            // 
            // buttonLogin
            // 
            buttonLogin.AccessibleName = "Button";
            buttonLogin.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonLogin.DialogResult = DialogResult.OK;
            buttonLogin.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonLogin.Location = new Point(457, 163);
            buttonLogin.Margin = new Padding(4, 3, 4, 3);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(112, 32);
            buttonLogin.TabIndex = 3;
            buttonLogin.Text = "Вход";
            buttonLogin.Click += ButtonLogin_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.login_manager;
            pictureBox1.Location = new Point(14, 48);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(149, 148);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // textPassword
            // 
            textPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textPassword.BackColor = Color.FromArgb(255, 255, 255);
            textPassword.BeforeTouchSize = new Size(381, 25);
            textPassword.BorderColor = Color.FromArgb(197, 197, 197);
            textPassword.BorderStyle = BorderStyle.FixedSingle;
            textPassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPassword.ForeColor = Color.FromArgb(68, 68, 68);
            textPassword.Location = new Point(188, 120);
            textPassword.Margin = new Padding(4, 3, 4, 3);
            textPassword.Name = "textPassword";
            textPassword.NearImage = Properties.Resources.lock_16;
            textPassword.PasswordChar = '●';
            textPassword.Size = new Size(381, 25);
            textPassword.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            textPassword.TabIndex = 2;
            textPassword.ThemeName = "Office2016Colorful";
            textPassword.UseSystemPasswordChar = true;
            textPassword.WordWrap = false;
            // 
            // comboDatabase
            // 
            comboDatabase.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboDatabase.BackColor = Color.FromArgb(255, 255, 255);
            comboDatabase.BeforeTouchSize = new Size(381, 23);
            comboDatabase.DropDownStyle = ComboBoxStyle.DropDownList;
            comboDatabase.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboDatabase.ForeColor = Color.FromArgb(68, 68, 68);
            comboDatabase.Location = new Point(188, 48);
            comboDatabase.Margin = new Padding(4, 3, 4, 3);
            comboDatabase.Name = "comboDatabase";
            comboDatabase.NearImage = Properties.Resources.database_16;
            comboDatabase.Size = new Size(381, 23);
            comboDatabase.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            comboDatabase.TabIndex = 0;
            comboDatabase.ThemeName = "Office2016Colorful";
            comboDatabase.SelectedValueChanged += ComboDatabase_SelectedValueChanged;
            // 
            // comboUsers
            // 
            comboUsers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboUsers.BackColor = Color.FromArgb(255, 255, 255);
            comboUsers.BeforeTouchSize = new Size(381, 23);
            comboUsers.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboUsers.ForeColor = Color.FromArgb(68, 68, 68);
            comboUsers.Location = new Point(188, 84);
            comboUsers.Margin = new Padding(4, 3, 4, 3);
            comboUsers.Name = "comboUsers";
            comboUsers.NearImage = Properties.Resources.user_16;
            comboUsers.Size = new Size(381, 23);
            comboUsers.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            comboUsers.TabIndex = 1;
            comboUsers.ThemeName = "Office2016Colorful";
            // 
            // LoginForm
            // 
            AcceptButton = buttonLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(600, 210);
            Controls.Add(comboDatabase);
            Controls.Add(comboUsers);
            Controls.Add(buttonLogin);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(textPassword);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            VisibleChanged += LoginWindow_VisibleChanged;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)textPassword).EndInit();
            ((System.ComponentModel.ISupportInitialize)comboDatabase).EndInit();
            ((System.ComponentModel.ISupportInitialize)comboUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textPassword;
        private PictureBox pictureBox1;
        private Label label1;
        private Syncfusion.WinForms.Controls.SfButton buttonLogin;
        private Controls.ComboBoxAdvImage comboUsers;
        private Controls.ComboBoxAdvImage comboDatabase;
    }
}