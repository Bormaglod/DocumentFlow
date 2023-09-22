namespace DocumentFlow
{
    partial class AboutForm
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
            panel1 = new Panel();
            labelDatabase = new Label();
            textDescr = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            label5 = new Label();
            listLibs = new Syncfusion.WinForms.ListView.SfListView();
            label4 = new Label();
            labelVersion = new Label();
            label2 = new Label();
            panel2 = new Panel();
            linkLabel1 = new LinkLabel();
            buttonOk = new Syncfusion.WinForms.Controls.SfButton();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textDescr).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(17, 158, 218);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 0, 0, 10);
            label1.Size = new Size(216, 50);
            label1.TabIndex = 0;
            label1.Text = "Document Flow";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(labelDatabase);
            panel1.Controls.Add(textDescr);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(listLibs);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(labelVersion);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 50);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 400);
            panel1.TabIndex = 1;
            // 
            // labelDatabase
            // 
            labelDatabase.AutoSize = true;
            labelDatabase.Location = new Point(10, 62);
            labelDatabase.Name = "labelDatabase";
            labelDatabase.Size = new Size(90, 13);
            labelDatabase.TabIndex = 7;
            labelDatabase.Text = "База данных: {0}";
            // 
            // textDescr
            // 
            textDescr.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textDescr.BeforeTouchSize = new Size(775, 58);
            textDescr.BorderColor = Color.FromArgb(209, 211, 212);
            textDescr.BorderStyle = BorderStyle.FixedSingle;
            textDescr.Location = new Point(10, 291);
            textDescr.Multiline = true;
            textDescr.Name = "textDescr";
            textDescr.Size = new Size(775, 58);
            textDescr.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            textDescr.TabIndex = 6;
            textDescr.ThemeName = "Metro";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(10, 275);
            label5.Name = "label5";
            label5.Size = new Size(138, 13);
            label5.TabIndex = 5;
            label5.Text = "Сведения о библиотеке:";
            // 
            // listLibs
            // 
            listLibs.AccessibleName = "ScrollControl";
            listLibs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listLibs.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            listLibs.Location = new Point(10, 102);
            listLibs.Name = "listLibs";
            listLibs.Size = new Size(775, 170);
            listLibs.TabIndex = 4;
            listLibs.Text = "sfListView1";
            listLibs.SelectionChanged += ListLibs_SelectionChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 86);
            label4.Name = "label4";
            label4.Size = new Size(160, 13);
            label4.TabIndex = 3;
            label4.Text = "Установленные библиотеки:";
            // 
            // labelVersion
            // 
            labelVersion.AutoSize = true;
            labelVersion.Location = new Point(10, 38);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(59, 13);
            labelVersion.TabIndex = 2;
            labelVersion.Text = "Версия {0}";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 12);
            label2.Name = "label2";
            label2.Size = new Size(159, 13);
            label2.TabIndex = 1;
            label2.Text = "© 2010-2023 ООО \"Автоком\"";
            // 
            // panel2
            // 
            panel2.Controls.Add(linkLabel1);
            panel2.Controls.Add(buttonOk);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 355);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 45);
            panel2.TabIndex = 0;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(10, 5);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(92, 13);
            linkLabel1.TabIndex = 1;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://icons8.ru";
            // 
            // buttonOk
            // 
            buttonOk.AccessibleName = "Button";
            buttonOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.FlatStyle = FlatStyle.System;
            buttonOk.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonOk.Location = new Point(692, 5);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(96, 28);
            buttonOk.TabIndex = 0;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = false;
            // 
            // AboutForm
            // 
            AcceptButton = buttonOk;
            AutoScaleMode = AutoScaleMode.None;
            CancelButton = buttonOk;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "О программе";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)textDescr).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textDescr;
        private Label label5;
        private Syncfusion.WinForms.ListView.SfListView listLibs;
        private Label label4;
        private Label labelVersion;
        private Label label2;
        private LinkLabel linkLabel1;
        private Label labelDatabase;
    }
}