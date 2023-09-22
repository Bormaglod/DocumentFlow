namespace DocumentFlow.Controls.Editors
{
    partial class DfCheckBox
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
            checkBoxAdv1 = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            ((System.ComponentModel.ISupportInitialize)checkBoxAdv1).BeginInit();
            SuspendLayout();
            // 
            // checkBoxAdv1
            // 
            checkBoxAdv1.BackColor = Color.FromArgb(255, 255, 255);
            checkBoxAdv1.BeforeTouchSize = new Size(20, 23);
            checkBoxAdv1.BorderColor = Color.FromArgb(197, 197, 197);
            checkBoxAdv1.CheckAlign = ContentAlignment.TopLeft;
            checkBoxAdv1.Dock = DockStyle.Left;
            checkBoxAdv1.ForeColor = Color.FromArgb(43, 43, 43);
            checkBoxAdv1.HotBorderColor = Color.FromArgb(150, 150, 150);
            checkBoxAdv1.Location = new Point(100, 0);
            checkBoxAdv1.Margin = new Padding(4, 3, 4, 3);
            checkBoxAdv1.Name = "checkBoxAdv1";
            checkBoxAdv1.Size = new Size(20, 23);
            checkBoxAdv1.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Office2016Colorful;
            checkBoxAdv1.TabIndex = 2;
            checkBoxAdv1.ThemeName = "Office2016Colorful";
            // 
            // DfCheckBox
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(checkBoxAdv1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "DfCheckBox";
            Padding = new Padding(0, 0, 0, 9);
            Size = new Size(666, 32);
            Controls.SetChildIndex(checkBoxAdv1, 0);
            ((System.ComponentModel.ISupportInitialize)checkBoxAdv1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv checkBoxAdv1;
    }
}
