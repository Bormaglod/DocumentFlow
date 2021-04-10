
namespace DocumentFlow
{
    partial class TwainForm
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
            this.buttonAdd = new Syncfusion.WinForms.Controls.SfButton();
            this.buttonDelete = new Syncfusion.WinForms.Controls.SfButton();
            this.panelImages = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleName = "Button";
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancel.Location = new System.Drawing.Point(266, 493);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 28);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отменить";
            // 
            // buttonOk
            // 
            this.buttonOk.AccessibleName = "Button";
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonOk.Location = new System.Drawing.Point(266, 459);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(112, 28);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Сохранить";
            this.buttonOk.UseVisualStyleBackColor = false;
            // 
            // buttonAdd
            // 
            this.buttonAdd.AccessibleName = "Button";
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAdd.Location = new System.Drawing.Point(266, 12);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(112, 28);
            this.buttonAdd.TabIndex = 14;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.AccessibleName = "Button";
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonDelete.Location = new System.Drawing.Point(266, 46);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(112, 28);
            this.buttonDelete.TabIndex = 15;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // panelImages
            // 
            this.panelImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelImages.AutoScroll = true;
            this.panelImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelImages.Location = new System.Drawing.Point(12, 12);
            this.panelImages.Name = "panelImages";
            this.panelImages.Size = new System.Drawing.Size(248, 509);
            this.panelImages.TabIndex = 16;
            // 
            // TwainForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(390, 533);
            this.Controls.Add(this.panelImages);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TwainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сканирование документов";
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Syncfusion.WinForms.Controls.SfButton buttonAdd;
        private Syncfusion.WinForms.Controls.SfButton buttonDelete;
        private System.Windows.Forms.Panel panelImages;
    }
}
