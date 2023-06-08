namespace DocumentFlow.Dialogs
{
    partial class InputCurrencyDialog
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
            components = new System.ComponentModel.Container();
            buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            buttonOk = new Syncfusion.WinForms.Controls.SfButton();
            label1 = new Label();
            currencyText = new Syncfusion.Windows.Forms.Tools.CurrencyTextBox();
            ((System.ComponentModel.ISupportInitialize)currencyText).BeginInit();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            buttonCancel.AccessibleName = "Button";
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancel.Location = new Point(251, 47);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(96, 32);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Отменить";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            buttonOk.AccessibleName = "Button";
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.BackColor = Color.FromArgb(242, 242, 242);
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonOk.Location = new Point(149, 47);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(96, 32);
            buttonOk.Style.BackColor = Color.FromArgb(242, 242, 242);
            buttonOk.TabIndex = 4;
            buttonOk.Text = "Сохранить";
            buttonOk.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(95, 17);
            label1.TabIndex = 6;
            label1.Text = "Введите сумму";
            // 
            // currencyText
            // 
            currencyText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            currencyText.BackGroundColor = Color.FromArgb(255, 255, 255);
            currencyText.BeforeTouchSize = new Size(234, 25);
            currencyText.BorderColor = Color.FromArgb(197, 197, 197);
            currencyText.BorderStyle = BorderStyle.FixedSingle;
            currencyText.DecimalValue = new decimal(new int[] { 0, 0, 0, 131072 });
            currencyText.ForeColor = Color.FromArgb(68, 68, 68);
            currencyText.Location = new Point(113, 9);
            currencyText.Name = "currencyText";
            currencyText.PositiveColor = Color.FromArgb(68, 68, 68);
            currencyText.Size = new Size(234, 25);
            currencyText.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            currencyText.TabIndex = 7;
            currencyText.Text = "0,00 ₽";
            currencyText.ThemeName = "Office2016Colorful";
            currencyText.ZeroColor = Color.FromArgb(68, 68, 68);
            // 
            // InputCurrencyDialog
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            CancelButton = buttonCancel;
            ClientSize = new Size(359, 91);
            Controls.Add(currencyText);
            Controls.Add(label1);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputCurrencyDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Цена";
            ((System.ComponentModel.ISupportInitialize)currencyText).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Label label1;
        private Syncfusion.Windows.Forms.Tools.CurrencyTextBox currencyText;
    }
}