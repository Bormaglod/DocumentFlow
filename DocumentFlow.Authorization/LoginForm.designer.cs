namespace DocumentFlow.Authorization
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLogin = new Syncfusion.WinForms.Controls.SfButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textPassword = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.comboDatabase = new DocumentFlow.Controls.ComboBoxAdvImage();
            this.comboUsers = new DocumentFlow.Controls.ComboBoxAdvImage();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 30);
            this.label1.TabIndex = 6;
            this.label1.Text = "Авторизация";
            // 
            // buttonLogin
            // 
            this.buttonLogin.AccessibleName = "Button";
            this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.buttonLogin.Location = new System.Drawing.Point(392, 141);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(96, 28);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "Вход";
            this.buttonLogin.Click += new System.EventHandler(this.ButtonLogin_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DocumentFlow.Authorization.Properties.Resources.login_manager;
            this.pictureBox1.Location = new System.Drawing.Point(12, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // textPassword
            // 
            this.textPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textPassword.BeforeTouchSize = new System.Drawing.Size(327, 25);
            this.textPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textPassword.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textPassword.Location = new System.Drawing.Point(161, 104);
            this.textPassword.Name = "textPassword";
            this.textPassword.NearImage = global::DocumentFlow.Authorization.Properties.Resources.lock_16;
            this.textPassword.PasswordChar = '●';
            this.textPassword.Size = new System.Drawing.Size(327, 25);
            this.textPassword.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textPassword.TabIndex = 2;
            this.textPassword.ThemeName = "Office2016Colorful";
            this.textPassword.UseSystemPasswordChar = true;
            this.textPassword.WordWrap = false;
            // 
            // comboDatabase
            // 
            this.comboDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboDatabase.BeforeTouchSize = new System.Drawing.Size(327, 23);
            this.comboDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDatabase.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.comboDatabase.Location = new System.Drawing.Point(161, 42);
            this.comboDatabase.Name = "comboDatabase";
            this.comboDatabase.NearImage = global::DocumentFlow.Authorization.Properties.Resources.database_16;
            this.comboDatabase.Size = new System.Drawing.Size(327, 23);
            this.comboDatabase.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.comboDatabase.TabIndex = 0;
            this.comboDatabase.ThemeName = "Office2016Colorful";
            this.comboDatabase.SelectedValueChanged += new System.EventHandler(this.ComboDatabase_SelectedValueChanged);
            // 
            // comboUsers
            // 
            this.comboUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboUsers.BeforeTouchSize = new System.Drawing.Size(327, 23);
            this.comboUsers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.comboUsers.Location = new System.Drawing.Point(161, 73);
            this.comboUsers.Name = "comboUsers";
            this.comboUsers.NearImage = global::DocumentFlow.Authorization.Properties.Resources.user_16;
            this.comboUsers.Size = new System.Drawing.Size(327, 23);
            this.comboUsers.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.comboUsers.TabIndex = 1;
            this.comboUsers.ThemeName = "Office2016Colorful";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(514, 182);
            this.Controls.Add(this.comboDatabase);
            this.Controls.Add(this.comboUsers);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.VisibleChanged += new System.EventHandler(this.LoginWindow_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textPassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private Syncfusion.WinForms.Controls.SfButton buttonLogin;
        private Controls.ComboBoxAdvImage comboUsers;
        private Controls.ComboBoxAdvImage comboDatabase;
    }
}