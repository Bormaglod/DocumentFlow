namespace DocumentFlow
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
            this.selectFile = new DocumentFlow.Controls.Editor.L_FileSelectBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textNote = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textNote)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleName = "Button";
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(433, 159);
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
            this.buttonOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(331, 159);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(96, 28);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Сохранить";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Имя файла";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.selectFile);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(541, 45);
            this.panel1.TabIndex = 8;
            // 
            // selectFile
            // 
            this.selectFile.AutoSizeLabel = false;
            this.selectFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectFile.EditWidth = 436;
            this.selectFile.FileName = null;
            this.selectFile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectFile.LabelText = "Text";
            this.selectFile.LabelWidth = 117;
            this.selectFile.Location = new System.Drawing.Point(95, 10);
            this.selectFile.Name = "selectFile";
            this.selectFile.Nullable = true;
            this.selectFile.ShowLabel = false;
            this.selectFile.Size = new System.Drawing.Size(436, 25);
            this.selectFile.TabIndex = 7;
            this.selectFile.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textNote);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 45);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(541, 113);
            this.panel2.TabIndex = 9;
            // 
            // textNote
            // 
            this.textNote.BeforeTouchSize = new System.Drawing.Size(436, 93);
            this.textNote.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.textNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textNote.Location = new System.Drawing.Point(95, 10);
            this.textNote.Multiline = true;
            this.textNote.Name = "textNote";
            this.textNote.Size = new System.Drawing.Size(436, 93);
            this.textNote.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.textNote.TabIndex = 1;
            this.textNote.ThemeName = "Metro";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 93);
            this.label2.TabIndex = 0;
            this.label2.Text = "Описание";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DocumentRefEditor
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(541, 199);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DocumentRefEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Документ";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textNote)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private Controls.Editor.L_FileSelectBox selectFile;
        private System.Windows.Forms.Panel panel2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textNote;
        private System.Windows.Forms.Label label2;
    }
}
