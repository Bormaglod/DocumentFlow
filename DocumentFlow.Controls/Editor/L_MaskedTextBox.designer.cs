namespace DocumentFlow.Controls.Editor
{
    partial class L_MaskedTextBox<T>
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
            this.maskedEditBox1 = new Syncfusion.Windows.Forms.Tools.MaskedEditBox();
            ((System.ComponentModel.ISupportInitialize)(this.maskedEditBox1)).BeginInit();
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
            // maskedEditBox1
            // 
            this.maskedEditBox1.BeforeTouchSize = new System.Drawing.Size(181, 25);
            this.maskedEditBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.maskedEditBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedEditBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.maskedEditBox1.Location = new System.Drawing.Point(136, 0);
            this.maskedEditBox1.Name = "maskedEditBox1";
            this.maskedEditBox1.Size = new System.Drawing.Size(181, 25);
            this.maskedEditBox1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.maskedEditBox1.TabIndex = 3;
            this.maskedEditBox1.ThemeName = "Metro";
            this.maskedEditBox1.TextChanged += new System.EventHandler(this.maskedEditBox1_TextChanged);
            // 
            // L_MaskedTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.maskedEditBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "L_MaskedTextBox";
            this.Size = new System.Drawing.Size(719, 32);
            ((System.ComponentModel.ISupportInitialize)(this.maskedEditBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.MaskedEditBox maskedEditBox1;
    }
}
