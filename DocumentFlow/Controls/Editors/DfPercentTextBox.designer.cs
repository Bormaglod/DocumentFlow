namespace DocumentFlow.Controls.Editors
{
    partial class DfPercentTextBox
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
            percentTextBox1 = new Syncfusion.Windows.Forms.Tools.PercentTextBox();
            ((System.ComponentModel.ISupportInitialize)percentTextBox1).BeginInit();
            SuspendLayout();
            // 
            // percentTextBox1
            // 
            percentTextBox1.BackGroundColor = Color.FromArgb(255, 255, 255);
            percentTextBox1.BeforeTouchSize = new Size(100, 25);
            percentTextBox1.BorderColor = Color.FromArgb(197, 197, 197);
            percentTextBox1.BorderStyle = BorderStyle.FixedSingle;
            percentTextBox1.Dock = DockStyle.Left;
            percentTextBox1.ForeColor = Color.FromArgb(68, 68, 68);
            percentTextBox1.Location = new Point(100, 0);
            percentTextBox1.Name = "percentTextBox1";
            percentTextBox1.PositiveColor = Color.FromArgb(68, 68, 68);
            percentTextBox1.Size = new Size(100, 25);
            percentTextBox1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            percentTextBox1.TabIndex = 2;
            percentTextBox1.Text = "0,00 %";
            percentTextBox1.ThemeName = "Office2016Colorful";
            percentTextBox1.ZeroColor = Color.FromArgb(68, 68, 68);
            // 
            // DfPercentTextBox
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(percentTextBox1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "DfPercentTextBox";
            Size = new Size(389, 32);
            Controls.SetChildIndex(percentTextBox1, 0);
            ((System.ComponentModel.ISupportInitialize)percentTextBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.PercentTextBox percentTextBox1;
    }
}
