namespace DocumentFlow.Controls
{
    partial class L_PercentTextBox
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
            this.percentTextBox1 = new Syncfusion.Windows.Forms.Tools.PercentTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.percentTextBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Text";
            // 
            // percentTextBox1
            // 
            this.percentTextBox1.BeforeTouchSize = new System.Drawing.Size(160, 25);
            this.percentTextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.percentTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.percentTextBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.percentTextBox1.Location = new System.Drawing.Point(117, 0);
            this.percentTextBox1.Name = "percentTextBox1";
            this.percentTextBox1.Size = new System.Drawing.Size(160, 25);
            this.percentTextBox1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.percentTextBox1.TabIndex = 2;
            this.percentTextBox1.Text = "0,00%";
            this.percentTextBox1.ThemeName = "Metro";
            // 
            // L_PercentTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.percentTextBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "L_PercentTextBox";
            this.Size = new System.Drawing.Size(661, 32);
            ((System.ComponentModel.ISupportInitialize)(this.percentTextBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.PercentTextBox percentTextBox1;
    }
}
