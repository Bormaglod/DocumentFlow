
namespace DocumentFlow.Controls.Editors
{
    partial class DfLine
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
            this.panelColored = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelColored
            // 
            this.panelColored.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.panelColored.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelColored.Location = new System.Drawing.Point(0, 0);
            this.panelColored.Name = "panelColored";
            this.panelColored.Size = new System.Drawing.Size(893, 2);
            this.panelColored.TabIndex = 3;
            // 
            // L_Line
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelColored);
            this.Name = "L_Line";
            this.Size = new System.Drawing.Size(893, 125);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelColored;
    }
}
