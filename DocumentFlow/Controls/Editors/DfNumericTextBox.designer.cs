namespace DocumentFlow.Controls.Editors
{
    partial class DfNumericTextBox
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
            decimalTextBox1 = new DecimalTextBox();
            labelSuffix = new Label();
            ((System.ComponentModel.ISupportInitialize)decimalTextBox1).BeginInit();
            SuspendLayout();
            // 
            // decimalTextBox1
            // 
            decimalTextBox1.BackGroundColor = Color.FromArgb(255, 255, 255);
            decimalTextBox1.BeforeTouchSize = new Size(100, 25);
            decimalTextBox1.BorderColor = Color.FromArgb(197, 197, 197);
            decimalTextBox1.BorderStyle = BorderStyle.FixedSingle;
            decimalTextBox1.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            decimalTextBox1.Dock = DockStyle.Left;
            decimalTextBox1.ForeColor = Color.FromArgb(68, 68, 68);
            decimalTextBox1.Location = new Point(100, 0);
            decimalTextBox1.Name = "decimalTextBox1";
            decimalTextBox1.PositiveColor = Color.FromArgb(68, 68, 68);
            decimalTextBox1.Size = new Size(100, 25);
            decimalTextBox1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            decimalTextBox1.TabIndex = 1;
            decimalTextBox1.Text = "0,00";
            decimalTextBox1.ThemeName = "Office2016Colorful";
            decimalTextBox1.ZeroColor = Color.FromArgb(68, 68, 68);
            // 
            // labelSuffix
            // 
            labelSuffix.AutoSize = true;
            labelSuffix.Dock = DockStyle.Left;
            labelSuffix.Location = new Point(200, 0);
            labelSuffix.Name = "labelSuffix";
            labelSuffix.Size = new Size(59, 17);
            labelSuffix.TabIndex = 2;
            labelSuffix.Text = "Суффикс";
            labelSuffix.Visible = false;
            // 
            // DfNumericTextBox
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelSuffix);
            Controls.Add(decimalTextBox1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "DfNumericTextBox";
            Size = new Size(389, 32);
            Controls.SetChildIndex(decimalTextBox1, 0);
            Controls.SetChildIndex(labelSuffix, 0);
            ((System.ComponentModel.ISupportInitialize)decimalTextBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DecimalTextBox decimalTextBox1;
        private Label labelSuffix;
    }
}
