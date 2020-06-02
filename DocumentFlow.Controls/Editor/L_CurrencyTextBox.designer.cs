namespace DocumentFlow.Controls
{
    partial class L_CurrencyTextBox
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
            this.label1 = new System.Windows.Forms.Label();
            this.currencyTextBox1 = new Syncfusion.Windows.Forms.Tools.CurrencyTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.currencyTextBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Text";
            // 
            // currencyTextBox1
            // 
            this.currencyTextBox1.BeforeTouchSize = new System.Drawing.Size(227, 25);
            this.currencyTextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.currencyTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currencyTextBox1.DecimalValue = new decimal(new int[] {
            100,
            0,
            0,
            -2147352576});
            this.currencyTextBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.currencyTextBox1.ForeColor = System.Drawing.Color.Red;
            this.currencyTextBox1.Location = new System.Drawing.Point(136, 0);
            this.currencyTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.currencyTextBox1.Name = "currencyTextBox1";
            this.currencyTextBox1.Size = new System.Drawing.Size(227, 25);
            this.currencyTextBox1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.currencyTextBox1.TabIndex = 2;
            this.currencyTextBox1.Text = "-1,00 ₽";
            this.currencyTextBox1.ThemeName = "Metro";
            this.currencyTextBox1.DecimalValueChanged += new System.EventHandler(this.CurrencyTextBox1_DecimalValueChanged);
            // 
            // L_CurrencyTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.currencyTextBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "L_CurrencyTextBox";
            this.Size = new System.Drawing.Size(407, 32);
            ((System.ComponentModel.ISupportInitialize)(this.currencyTextBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.CurrencyTextBox currencyTextBox1;
    }
}
