namespace DocumentFlow.Dialogs
{
    partial class DocumentRefDialog
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
            buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            buttonOk = new Syncfusion.WinForms.Controls.SfButton();
            label1 = new Label();
            panel1 = new Panel();
            buttonSelectFile = new Syncfusion.WinForms.Controls.SfButton();
            textFileName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            panel2 = new Panel();
            textNote = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            label2 = new Label();
            openFileDialog1 = new OpenFileDialog();
            panel3 = new Panel();
            checkBoxThumbnail = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            label3 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textFileName).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textNote).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checkBoxThumbnail).BeginInit();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            buttonCancel.AccessibleName = "Button";
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancel.Location = new Point(420, 211);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(96, 28);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Отменить";
            // 
            // buttonOk
            // 
            buttonOk.AccessibleName = "Button";
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonOk.Location = new Point(318, 211);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(96, 28);
            buttonOk.TabIndex = 4;
            buttonOk.Text = "Сохранить";
            buttonOk.UseVisualStyleBackColor = false;
            buttonOk.Click += ButtonOk_Click;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Left;
            label1.Location = new Point(10, 10);
            label1.Name = "label1";
            label1.Size = new Size(95, 23);
            label1.TabIndex = 6;
            label1.Text = "Имя файла";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonSelectFile);
            panel1.Controls.Add(textFileName);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10);
            panel1.Size = new Size(528, 43);
            panel1.TabIndex = 8;
            // 
            // buttonSelectFile
            // 
            buttonSelectFile.AccessibleName = "Button";
            buttonSelectFile.Dock = DockStyle.Right;
            buttonSelectFile.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonSelectFile.Location = new Point(495, 10);
            buttonSelectFile.Name = "buttonSelectFile";
            buttonSelectFile.Size = new Size(23, 23);
            buttonSelectFile.Style.Image = Properties.Resources.icons8_select_16;
            buttonSelectFile.TabIndex = 8;
            buttonSelectFile.UseVisualStyleBackColor = true;
            buttonSelectFile.Click += ButtonSelectFile_Click;
            // 
            // textFileName
            // 
            textFileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textFileName.BackColor = Color.FromArgb(255, 255, 255);
            textFileName.BeforeTouchSize = new Size(413, 93);
            textFileName.BorderColor = Color.FromArgb(197, 197, 197);
            textFileName.BorderStyle = BorderStyle.FixedSingle;
            textFileName.ForeColor = Color.FromArgb(68, 68, 68);
            textFileName.Location = new Point(105, 10);
            textFileName.Name = "textFileName";
            textFileName.ReadOnly = true;
            textFileName.Size = new Size(384, 23);
            textFileName.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            textFileName.TabIndex = 7;
            textFileName.ThemeName = "Office2016Colorful";
            // 
            // panel2
            // 
            panel2.Controls.Add(textNote);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 43);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(10);
            panel2.Size = new Size(528, 113);
            panel2.TabIndex = 9;
            // 
            // textNote
            // 
            textNote.BackColor = Color.FromArgb(255, 255, 255);
            textNote.BeforeTouchSize = new Size(413, 93);
            textNote.BorderColor = Color.FromArgb(197, 197, 197);
            textNote.BorderStyle = BorderStyle.FixedSingle;
            textNote.Dock = DockStyle.Fill;
            textNote.ForeColor = Color.FromArgb(68, 68, 68);
            textNote.Location = new Point(105, 10);
            textNote.Multiline = true;
            textNote.Name = "textNote";
            textNote.Size = new Size(413, 93);
            textNote.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            textNote.TabIndex = 1;
            textNote.ThemeName = "Office2016Colorful";
            // 
            // label2
            // 
            label2.Dock = DockStyle.Left;
            label2.Location = new Point(10, 10);
            label2.Name = "label2";
            label2.Size = new Size(95, 93);
            label2.TabIndex = 0;
            label2.Text = "Описание";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            panel3.Controls.Add(checkBoxThumbnail);
            panel3.Controls.Add(label3);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 156);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(10);
            panel3.Size = new Size(528, 51);
            panel3.TabIndex = 10;
            // 
            // checkBoxThumbnail
            // 
            checkBoxThumbnail.BackColor = Color.FromArgb(255, 255, 255);
            checkBoxThumbnail.BeforeTouchSize = new Size(413, 31);
            checkBoxThumbnail.BorderColor = Color.FromArgb(197, 197, 197);
            checkBoxThumbnail.Dock = DockStyle.Fill;
            checkBoxThumbnail.ForeColor = Color.FromArgb(43, 43, 43);
            checkBoxThumbnail.HotBorderColor = Color.FromArgb(150, 150, 150);
            checkBoxThumbnail.Location = new Point(105, 10);
            checkBoxThumbnail.Name = "checkBoxThumbnail";
            checkBoxThumbnail.Size = new Size(413, 31);
            checkBoxThumbnail.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Office2016Colorful;
            checkBoxThumbnail.TabIndex = 1;
            checkBoxThumbnail.ThemeName = "Office2016Colorful";
            // 
            // label3
            // 
            label3.Dock = DockStyle.Left;
            label3.Location = new Point(10, 10);
            label3.Name = "label3";
            label3.Size = new Size(95, 31);
            label3.TabIndex = 0;
            label3.Text = "Предпросмотр";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // DocumentRefDialog
            // 
            AcceptButton = buttonOk;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.Window;
            CancelButton = buttonCancel;
            ClientSize = new Size(528, 251);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DocumentRefDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Документ";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)textFileName).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)textNote).EndInit();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checkBoxThumbnail).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Label label1;
        private Panel panel1;

        private Panel panel2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textNote;
        private Label label2;
        private Syncfusion.WinForms.Controls.SfButton buttonSelectFile;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textFileName;
        private OpenFileDialog openFileDialog1;
        private Panel panel3;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv checkBoxThumbnail;
        private Label label3;
    }
}
