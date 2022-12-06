namespace DocumentFlow.Controls
{
    partial class BalanceContractorFilter
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
            this.sfComboBox1 = new Syncfusion.WinForms.ListView.SfComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolButton1 = new DocumentFlow.Controls.ToolButton();
            ((System.ComponentModel.ISupportInitialize)(this.sfComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(4, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.label1.Size = new System.Drawing.Size(68, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Договор";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sfComboBox1
            // 
            this.sfComboBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.sfComboBox1.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.sfComboBox1.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.sfComboBox1.Location = new System.Drawing.Point(72, 3);
            this.sfComboBox1.Name = "sfComboBox1";
            this.sfComboBox1.Size = new System.Drawing.Size(331, 24);
            this.sfComboBox1.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sfComboBox1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(403, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(7, 24);
            this.panel2.TabIndex = 17;
            // 
            // toolButton1
            // 
            this.toolButton1.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.toolButton1.BackColor = System.Drawing.Color.White;
            this.toolButton1.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.toolButton1.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.toolButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.toolButton1.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.toolButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolButton1.Kind = DocumentFlow.Controls.ToolButtonKind.Delete;
            this.toolButton1.Location = new System.Drawing.Point(410, 3);
            this.toolButton1.Name = "toolButton1";
            this.toolButton1.Size = new System.Drawing.Size(24, 24);
            this.toolButton1.TabIndex = 18;
            this.toolButton1.TabStop = false;
            this.toolButton1.Text = "toolButton1";
            this.toolButton1.Click += new System.EventHandler(this.ToolButton1_Click);
            // 
            // BalanceContractorFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolButton1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.sfComboBox1);
            this.Controls.Add(this.label1);
            this.Name = "BalanceContractorFilter";
            this.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Size = new System.Drawing.Size(773, 30);
            ((System.ComponentModel.ISupportInitialize)(this.sfComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Label label1;
        private Syncfusion.WinForms.ListView.SfComboBox sfComboBox1;
        private Panel panel2;
        private ToolButton toolButton1;
    }
}
