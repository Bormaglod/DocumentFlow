namespace DocumentFlow.Dialogs
{
    partial class DocumentRefEditorForm
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
            this.buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.buttonOk = new Syncfusion.WinForms.Controls.SfButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSelectFile = new Syncfusion.WinForms.Controls.SfButton();
            this.textFileName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textNote = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBoxThumbnail = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textFileName)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textNote)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxThumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleName = "Button";
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancel.Location = new System.Drawing.Point(420, 211);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 28);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отменить";
            // 
            // buttonOk
            // 
            this.buttonOk.AccessibleName = "Button";
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonOk.Location = new System.Drawing.Point(318, 211);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(96, 28);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Сохранить";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Имя файла";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSelectFile);
            this.panel1.Controls.Add(this.textFileName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(528, 43);
            this.panel1.TabIndex = 8;
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.AccessibleName = "Button";
            this.buttonSelectFile.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSelectFile.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSelectFile.Location = new System.Drawing.Point(495, 10);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(23, 23);
            this.buttonSelectFile.Style.Image = global::DocumentFlow.Properties.Resources.icons8_select_16;
            this.buttonSelectFile.TabIndex = 8;
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            this.buttonSelectFile.Click += new System.EventHandler(this.ButtonSelectFile_Click);
            // 
            // textFileName
            // 
            this.textFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textFileName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textFileName.BeforeTouchSize = new System.Drawing.Size(413, 93);
            this.textFileName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textFileName.Location = new System.Drawing.Point(105, 10);
            this.textFileName.Name = "textFileName";
            this.textFileName.Size = new System.Drawing.Size(384, 23);
            this.textFileName.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textFileName.TabIndex = 7;
            this.textFileName.ThemeName = "Office2016Colorful";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textNote);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 43);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(528, 113);
            this.panel2.TabIndex = 9;
            // 
            // textNote
            // 
            this.textNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textNote.BeforeTouchSize = new System.Drawing.Size(413, 93);
            this.textNote.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textNote.Location = new System.Drawing.Point(105, 10);
            this.textNote.Multiline = true;
            this.textNote.Name = "textNote";
            this.textNote.Size = new System.Drawing.Size(413, 93);
            this.textNote.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textNote.TabIndex = 1;
            this.textNote.ThemeName = "Office2016Colorful";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 93);
            this.label2.TabIndex = 0;
            this.label2.Text = "Описание";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.checkBoxThumbnail);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 156);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(528, 51);
            this.panel3.TabIndex = 10;
            // 
            // checkBoxThumbnail
            // 
            this.checkBoxThumbnail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.checkBoxThumbnail.BeforeTouchSize = new System.Drawing.Size(413, 31);
            this.checkBoxThumbnail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.checkBoxThumbnail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxThumbnail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.checkBoxThumbnail.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.checkBoxThumbnail.Location = new System.Drawing.Point(105, 10);
            this.checkBoxThumbnail.Name = "checkBoxThumbnail";
            this.checkBoxThumbnail.Size = new System.Drawing.Size(413, 31);
            this.checkBoxThumbnail.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Office2016Colorful;
            this.checkBoxThumbnail.TabIndex = 1;
            this.checkBoxThumbnail.ThemeName = "Office2016Colorful";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "Предпросмотр";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocumentRefEditorForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(528, 251);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DocumentRefEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Документ";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textFileName)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textNote)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxThumbnail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        
        private System.Windows.Forms.Panel panel2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textNote;
        private System.Windows.Forms.Label label2;
        private Syncfusion.WinForms.Controls.SfButton buttonSelectFile;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textFileName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Panel panel3;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv checkBoxThumbnail;
        private Label label3;
    }
}
