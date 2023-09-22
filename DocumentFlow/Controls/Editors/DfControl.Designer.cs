
namespace DocumentFlow.Controls
{
    partial class DfControl
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
            labelHeader = new Label();
            SuspendLayout();
            // 
            // labelHeader
            // 
            labelHeader.Dock = DockStyle.Left;
            labelHeader.Location = new Point(0, 0);
            labelHeader.Name = "labelHeader";
            labelHeader.Size = new Size(100, 25);
            labelHeader.TabIndex = 0;
            labelHeader.Text = "Заголовок";
            // 
            // DfControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelHeader);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "DfControl";
            Padding = new Padding(0, 0, 0, 7);
            Size = new Size(610, 32);
            ResumeLayout(false);
        }

        #endregion

        private Label labelHeader;
    }
}
