namespace DocumentFlow.Controls.Editors
{
    partial class DfWireStripping
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxSweep = new Syncfusion.Windows.Forms.Tools.IntegerTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCleaning = new DocumentFlow.Controls.DecimalTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxSweep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCleaning)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Text";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxSweep);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxCleaning);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(117, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(211, 22);
            this.panel1.TabIndex = 2;
            // 
            // textBoxSweep
            // 
            this.textBoxSweep.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxSweep.BeforeTouchSize = new System.Drawing.Size(65, 23);
            this.textBoxSweep.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textBoxSweep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSweep.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textBoxSweep.IntegerValue = ((long)(0));
            this.textBoxSweep.Location = new System.Drawing.Point(90, 0);
            this.textBoxSweep.Name = "textBoxSweep";
            this.textBoxSweep.PositiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textBoxSweep.Size = new System.Drawing.Size(65, 23);
            this.textBoxSweep.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textBoxSweep.TabIndex = 2;
            this.textBoxSweep.Text = "0";
            this.textBoxSweep.ThemeName = "Office2016Colorful";
            this.textBoxSweep.ZeroColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textBoxSweep.IntegerValueChanged += new System.EventHandler(this.TextBoxSweep_IntegerValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxCleaning
            // 
            this.textBoxCleaning.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxCleaning.BeforeTouchSize = new System.Drawing.Size(65, 23);
            this.textBoxCleaning.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textBoxCleaning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCleaning.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxCleaning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textBoxCleaning.Location = new System.Drawing.Point(0, 0);
            this.textBoxCleaning.Name = "textBoxCleaning";
            this.textBoxCleaning.PositiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textBoxCleaning.Size = new System.Drawing.Size(65, 23);
            this.textBoxCleaning.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textBoxCleaning.TabIndex = 0;
            this.textBoxCleaning.Text = "0,00";
            this.textBoxCleaning.ThemeName = "Office2016Colorful";
            this.textBoxCleaning.ZeroColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textBoxCleaning.DecimalValueChanged += new System.EventHandler(this.TextBoxCleaning_DecimalValueChanged);
            // 
            // DfWireStripping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DfWireStripping";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.Size = new System.Drawing.Size(328, 28);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxSweep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCleaning)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Syncfusion.Windows.Forms.Tools.IntegerTextBox textBoxSweep;
        private Label label2;
        private DocumentFlow.Controls.DecimalTextBox textBoxCleaning;
    }
}
