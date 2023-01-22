
namespace DocumentFlow.Controls.Filters
{
    partial class DocumentFilter
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
            this.comboOrg = new Syncfusion.WinForms.ListView.SfComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClearOrg = new DocumentFlow.Controls.ToolButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dateRangeControl1 = new DocumentFlow.Controls.DateRangeControl();
            ((System.ComponentModel.ISupportInitialize)(this.comboOrg)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(4, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Организация";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboOrg
            // 
            this.comboOrg.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboOrg.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.comboOrg.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboOrg.Location = new System.Drawing.Point(108, 3);
            this.comboOrg.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboOrg.Name = "comboOrg";
            this.comboOrg.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.comboOrg.Size = new System.Drawing.Size(265, 24);
            this.comboOrg.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboOrg.TabIndex = 11;
            this.comboOrg.ThemeName = "Metro";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(373, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(7, 24);
            this.panel1.TabIndex = 13;
            // 
            // buttonClearOrg
            // 
            this.buttonClearOrg.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.buttonClearOrg.BackColor = System.Drawing.Color.White;
            this.buttonClearOrg.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.buttonClearOrg.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.buttonClearOrg.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.buttonClearOrg.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.buttonClearOrg.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonClearOrg.Kind = DocumentFlow.Infrastructure.Controls.ToolButtonKind.Delete;
            this.buttonClearOrg.Location = new System.Drawing.Point(380, 3);
            this.buttonClearOrg.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonClearOrg.Name = "buttonClearOrg";
            this.buttonClearOrg.Size = new System.Drawing.Size(28, 24);
            this.buttonClearOrg.TabIndex = 14;
            this.buttonClearOrg.Text = "toolButton1";
            this.buttonClearOrg.Click += new System.EventHandler(this.ButtonClearOrg_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(408, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(7, 24);
            this.panel2.TabIndex = 15;
            // 
            // dateRangeControl1
            // 
            this.dateRangeControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateRangeControl1.From = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            this.dateRangeControl1.FromEnabled = true;
            this.dateRangeControl1.Location = new System.Drawing.Point(415, 3);
            this.dateRangeControl1.Name = "dateRangeControl1";
            this.dateRangeControl1.Size = new System.Drawing.Size(413, 24);
            this.dateRangeControl1.TabIndex = 22;
            this.dateRangeControl1.To = new System.DateTime(2022, 12, 31, 23, 59, 59, 999);
            this.dateRangeControl1.ToEnabled = true;
            // 
            // DocumentFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.dateRangeControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonClearOrg);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboOrg);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DocumentFilter";
            this.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Size = new System.Drawing.Size(1087, 30);
            ((System.ComponentModel.ISupportInitialize)(this.comboOrg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Syncfusion.WinForms.ListView.SfComboBox comboOrg;
        private System.Windows.Forms.Panel panel1;
        private ToolButton buttonClearOrg;
        private System.Windows.Forms.Panel panel2;
        private DateRangeControl dateRangeControl1;
    }
}
