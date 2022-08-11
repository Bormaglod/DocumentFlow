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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxExt = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text";
            // 
            // textBoxExt
            // 
            this.textBoxExt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxExt.BeforeTouchSize = new System.Drawing.Size(484, 25);
            this.textBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxExt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textBoxExt.Location = new System.Drawing.Point(117, 0);
            this.textBoxExt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxExt.Name = "textBoxExt";
            this.textBoxExt.Size = new System.Drawing.Size(484, 25);
            this.textBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textBoxExt.TabIndex = 1;
            this.textBoxExt.ThemeName = "Office2016Colorful";
            this.textBoxExt.TextChanged += new System.EventHandler(this.TextBoxExt_TextChanged);
            // 
            // DfTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxExt);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DfTextBox";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
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
