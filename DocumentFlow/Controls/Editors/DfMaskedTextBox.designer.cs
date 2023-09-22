namespace DocumentFlow.Controls.Editors
{
    partial class DfMaskedTextBox
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
            textBox = new Syncfusion.Windows.Forms.Tools.MaskedEditBox();
            labelSuffix = new Label();
            ((System.ComponentModel.ISupportInitialize)textBox).BeginInit();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.BackColor = Color.FromArgb(255, 255, 255);
            textBox.BeforeTouchSize = new Size(200, 25);
            textBox.BorderColor = Color.FromArgb(197, 197, 197);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Dock = DockStyle.Left;
            textBox.ForeColor = Color.FromArgb(68, 68, 68);
            textBox.Location = new Point(100, 0);
            textBox.Name = "textBox";
            textBox.Size = new Size(200, 25);
            textBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            textBox.TabIndex = 1;
            textBox.ThemeName = "Office2016Colorful";
            // 
            // labelSuffix
            // 
            labelSuffix.AutoSize = true;
            labelSuffix.Dock = DockStyle.Left;
            labelSuffix.Location = new Point(300, 0);
            labelSuffix.Name = "labelSuffix";
            labelSuffix.Size = new Size(59, 17);
            labelSuffix.TabIndex = 2;
            labelSuffix.Text = "Суффикс";
            // 
            // DfMaskedTextBox
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelSuffix);
            Controls.Add(textBox);
            Margin = new Padding(3, 4, 3, 4);
            Name = "DfMaskedTextBox";
            Size = new Size(719, 32);
            Controls.SetChildIndex(textBox, 0);
            Controls.SetChildIndex(labelSuffix, 0);
            ((System.ComponentModel.ISupportInitialize)textBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.MaskedEditBox textBox;
        private Label labelSuffix;
    }
}
