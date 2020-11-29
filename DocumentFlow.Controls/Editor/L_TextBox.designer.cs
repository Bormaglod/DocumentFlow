namespace DocumentFlow.Controls.Editor
{
    partial class L_TextBox
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
            this.textBoxExt = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text";
            // 
            // textBoxExt
            // 
            this.textBoxExt.BeforeTouchSize = new System.Drawing.Size(484, 25);
            this.textBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.textBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxExt.Location = new System.Drawing.Point(117, 0);
            this.textBoxExt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxExt.Name = "textBoxExt";
            this.textBoxExt.Size = new System.Drawing.Size(484, 25);
            this.textBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.textBoxExt.TabIndex = 1;
            this.textBoxExt.ThemeName = "Metro";
            this.textBoxExt.TextChanged += new System.EventHandler(this.TextBoxExt_TextChanged);
            // 
            // L_TextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxExt);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "L_TextBox";
            this.Size = new System.Drawing.Size(716, 32);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt;
    }
}
