namespace DocumentFlow.Controls
{
    partial class CardRow
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
            this.labelAmount = new System.Windows.Forms.Label();
            this.linkLabelContractor = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // labelAmount
            // 
            this.labelAmount.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelAmount.Location = new System.Drawing.Point(107, 0);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(65, 30);
            this.labelAmount.TabIndex = 0;
            this.labelAmount.Text = "Сумма";
            this.labelAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabelContractor
            // 
            this.linkLabelContractor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabelContractor.Location = new System.Drawing.Point(0, 0);
            this.linkLabelContractor.Name = "linkLabelContractor";
            this.linkLabelContractor.Size = new System.Drawing.Size(107, 30);
            this.linkLabelContractor.TabIndex = 1;
            this.linkLabelContractor.TabStop = true;
            this.linkLabelContractor.Text = "Контрагент";
            this.linkLabelContractor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabelContractor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelContractor_LinkClicked);
            // 
            // CardRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabelContractor);
            this.Controls.Add(this.labelAmount);
            this.Name = "CardRow";
            this.Size = new System.Drawing.Size(172, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private Label labelAmount;
        private LinkLabel linkLabelContractor;
    }
}
