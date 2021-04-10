namespace DocumentFlow
{
    partial class GroupEditorForm
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
            this.buttonOk = new Syncfusion.WinForms.Controls.SfButton();
            this.buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.textCode = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            ((System.ComponentModel.ISupportInitialize)(this.textName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCode)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.AccessibleName = "Button";
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(268, 85);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(112, 28);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Сохранить";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleName = "Button";
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(387, 85);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 28);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отменить";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(19, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Наименование группы";
            // 
            // textName
            // 
            this.textName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textName.BeforeTouchSize = new System.Drawing.Size(340, 23);
            this.textName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.textName.Location = new System.Drawing.Point(159, 47);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(340, 23);
            this.textName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(19, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Код";
            // 
            // textCode
            // 
            this.textCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textCode.BeforeTouchSize = new System.Drawing.Size(340, 23);
            this.textCode.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.textCode.Location = new System.Drawing.Point(159, 18);
            this.textCode.Name = "textCode";
            this.textCode.Size = new System.Drawing.Size(340, 23);
            this.textCode.TabIndex = 0;
            // 
            // GroupEditor
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(514, 125);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Группа";
            ((System.ComponentModel.ISupportInitialize)(this.textName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textName;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textCode;
    }
}