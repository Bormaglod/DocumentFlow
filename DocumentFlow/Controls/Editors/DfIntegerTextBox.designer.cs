namespace DocumentFlow.Controls.Editors
{
    partial class DfIntegerTextBox
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
            integerTextBox1 = new Syncfusion.Windows.Forms.Tools.IntegerTextBox();
            labelSuffix = new Label();
            ((System.ComponentModel.ISupportInitialize)integerTextBox1).BeginInit();
            SuspendLayout();
            // 
            // integerTextBox1
            // 
            integerTextBox1.BackGroundColor = Color.FromArgb(255, 255, 255);
            integerTextBox1.BeforeTouchSize = new Size(100, 25);
            integerTextBox1.BorderColor = Color.FromArgb(197, 197, 197);
            integerTextBox1.BorderStyle = BorderStyle.FixedSingle;
            integerTextBox1.Dock = DockStyle.Left;
            integerTextBox1.ForeColor = Color.FromArgb(68, 68, 68);
            integerTextBox1.IntegerValue = 0L;
            integerTextBox1.Location = new Point(100, 0);
            integerTextBox1.Name = "integerTextBox1";
            integerTextBox1.PositiveColor = Color.FromArgb(68, 68, 68);
            integerTextBox1.Size = new Size(100, 25);
            integerTextBox1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            integerTextBox1.TabIndex = 0;
            integerTextBox1.ThemeName = "Office2016Colorful";
            integerTextBox1.ZeroColor = Color.FromArgb(68, 68, 68);
            // 
            // labelSuffix
            // 
            labelSuffix.AutoSize = true;
            labelSuffix.Dock = DockStyle.Left;
            labelSuffix.Location = new Point(200, 0);
            labelSuffix.Name = "labelSuffix";
            labelSuffix.Size = new Size(59, 17);
            labelSuffix.TabIndex = 1;
            labelSuffix.Text = "Суффикс";
            labelSuffix.Visible = false;
            // 
            // DfIntegerTextBox
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelSuffix);
            Controls.Add(integerTextBox1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "DfIntegerTextBox";
            Size = new Size(389, 32);
            Controls.SetChildIndex(integerTextBox1, 0);
            Controls.SetChildIndex(labelSuffix, 0);
            ((System.ComponentModel.ISupportInitialize)integerTextBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.IntegerTextBox integerTextBox1;
        private Label labelSuffix;
    }
}
