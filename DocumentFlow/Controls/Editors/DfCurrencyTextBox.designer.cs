namespace DocumentFlow.Controls.Editors
{
    partial class DfCurrencyTextBox
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            currencyTextBox1 = new Syncfusion.Windows.Forms.Tools.CurrencyTextBox();
            ((System.ComponentModel.ISupportInitialize)currencyTextBox1).BeginInit();
            SuspendLayout();
            // 
            // currencyTextBox1
            // 
            currencyTextBox1.BackGroundColor = Color.FromArgb(255, 255, 255);
            currencyTextBox1.BeforeTouchSize = new Size(100, 25);
            currencyTextBox1.BorderColor = Color.FromArgb(197, 197, 197);
            currencyTextBox1.BorderStyle = BorderStyle.FixedSingle;
            currencyTextBox1.DecimalValue = new decimal(new int[] { 0, 0, 0, 131072 });
            currencyTextBox1.Dock = DockStyle.Left;
            currencyTextBox1.ForeColor = Color.FromArgb(68, 68, 68);
            currencyTextBox1.Location = new Point(75, 0);
            currencyTextBox1.Name = "currencyTextBox1";
            currencyTextBox1.PositiveColor = Color.FromArgb(68, 68, 68);
            currencyTextBox1.Size = new Size(100, 25);
            currencyTextBox1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            currencyTextBox1.TabIndex = 1;
            currencyTextBox1.Text = "0,00 ₽";
            currencyTextBox1.ThemeName = "Office2016Colorful";
            currencyTextBox1.ZeroColor = Color.FromArgb(68, 68, 68);
            // 
            // DfCurrencyTextBox
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(currencyTextBox1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "DfCurrencyTextBox";
            Size = new Size(389, 32);
            Controls.SetChildIndex(currencyTextBox1, 0);
            ((System.ComponentModel.ISupportInitialize)currencyTextBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.CurrencyTextBox currencyTextBox1;
    }
}
