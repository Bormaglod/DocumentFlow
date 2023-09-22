namespace DocumentFlow.Controls.Editors
{
    partial class DfTextBox
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
            textBoxExt = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            ((System.ComponentModel.ISupportInitialize)textBoxExt).BeginInit();
            SuspendLayout();
            // 
            // textBoxExt
            // 
            textBoxExt.BackColor = Color.FromArgb(255, 255, 255);
            textBoxExt.BeforeTouchSize = new Size(484, 25);
            textBoxExt.BorderColor = Color.FromArgb(197, 197, 197);
            textBoxExt.BorderStyle = BorderStyle.FixedSingle;
            textBoxExt.Dock = DockStyle.Left;
            textBoxExt.ForeColor = Color.FromArgb(68, 68, 68);
            textBoxExt.Location = new Point(90, 0);
            textBoxExt.Margin = new Padding(3, 4, 3, 4);
            textBoxExt.Name = "textBoxExt";
            textBoxExt.Size = new Size(484, 25);
            textBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            textBoxExt.TabIndex = 1;
            textBoxExt.ThemeName = "Office2016Colorful";
            // 
            // DfTextBox
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textBoxExt);
            Header = "Заголовок1";
            HeaderWidth = 90;
            Margin = new Padding(3, 4, 3, 4);
            Name = "DfTextBox";
            Size = new Size(663, 32);
            Controls.SetChildIndex(textBoxExt, 0);
            ((System.ComponentModel.ISupportInitialize)textBoxExt).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt;
    }
}
