namespace DocumentFlow.Controls.Editor
{
    partial class L_NumericTextBox
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
            this.decimalText = new DocumentFlow.Controls.DecimalTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.decimalText)).BeginInit();
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
            // decimalText
            // 
            this.decimalText.BeforeTouchSize = new System.Drawing.Size(185, 25);
            this.decimalText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.decimalText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.decimalText.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.decimalText.Dock = System.Windows.Forms.DockStyle.Left;
            this.decimalText.Location = new System.Drawing.Point(136, 0);
            this.decimalText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.decimalText.Name = "decimalText";
            this.decimalText.Size = new System.Drawing.Size(509, 25);
            this.decimalText.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.decimalText.TabIndex = 2;
            this.decimalText.Text = "0,00";
            this.decimalText.ThemeName = "Metro";
            this.decimalText.DecimalValueChanged += new System.EventHandler(this.DecimalText_DecimalValueChanged);
            // 
            // L_NumericTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.decimalText);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "L_NumericTextBox";
            this.Size = new System.Drawing.Size(719, 32);
            ((System.ComponentModel.ISupportInitialize)(this.decimalText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DocumentFlow.Controls.DecimalTextBox decimalText;
    }
}
