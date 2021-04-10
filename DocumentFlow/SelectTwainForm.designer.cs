namespace DocumentFlow
{
    partial class SelectTwainForm
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
            this.comboBoxData = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxData)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.AccessibleName = "Button";
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonOk.Location = new System.Drawing.Point(268, 45);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(112, 28);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Сохранить";
            this.buttonOk.UseVisualStyleBackColor = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleName = "Button";
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancel.Location = new System.Drawing.Point(387, 45);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 28);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отменить";
            // 
            // comboBoxData
            // 
            this.comboBoxData.BackColor = System.Drawing.Color.White;
            this.comboBoxData.BeforeTouchSize = new System.Drawing.Size(378, 21);
            this.comboBoxData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxData.Location = new System.Drawing.Point(121, 12);
            this.comboBoxData.Name = "comboBoxData";
            this.comboBoxData.Size = new System.Drawing.Size(378, 21);
            this.comboBoxData.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.comboBoxData.TabIndex = 4;
            this.comboBoxData.ThemeName = "Metro";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Источник данных";
            // 
            // SelectScannerForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(514, 85);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxData);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectScannerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор источника";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboBoxData;
        private System.Windows.Forms.Label label1;
    }
}