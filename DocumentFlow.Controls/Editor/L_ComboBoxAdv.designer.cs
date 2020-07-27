namespace DocumentFlow.Controls
{
    partial class L_ComboBoxAdv
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
            this.comboBoxAdv1 = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.panelEdit = new System.Windows.Forms.Panel();
            this.toolButton1 = new DocumentFlow.Controls.ToolButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxAdv1)).BeginInit();
            this.panelEdit.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxAdv1
            // 
            this.comboBoxAdv1.BackColor = System.Drawing.Color.White;
            this.comboBoxAdv1.BeforeTouchSize = new System.Drawing.Size(410, 23);
            this.comboBoxAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxAdv1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAdv1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxAdv1.Location = new System.Drawing.Point(0, 0);
            this.comboBoxAdv1.Name = "comboBoxAdv1";
            this.comboBoxAdv1.Size = new System.Drawing.Size(410, 23);
            this.comboBoxAdv1.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.comboBoxAdv1.TabIndex = 0;
            this.comboBoxAdv1.ThemeName = "Metro";
            this.comboBoxAdv1.SelectedIndexChanging += new Syncfusion.Windows.Forms.Tools.SelectedIndexChangingHandler(this.ComboBoxAdv1_SelectedIndexChanging);
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.Transparent;
            this.panelSeparator.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSeparator.Location = new System.Drawing.Point(410, 0);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(5, 25);
            this.panelSeparator.TabIndex = 2;
            // 
            // panelEdit
            // 
            this.panelEdit.Controls.Add(this.comboBoxAdv1);
            this.panelEdit.Controls.Add(this.panelSeparator);
            this.panelEdit.Controls.Add(this.toolButton1);
            this.panelEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEdit.Location = new System.Drawing.Point(117, 0);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(440, 25);
            this.panelEdit.TabIndex = 3;
            // 
            // toolButton1
            // 
            this.toolButton1.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.toolButton1.BackColor = System.Drawing.Color.White;
            this.toolButton1.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.toolButton1.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.toolButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.toolButton1.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.toolButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolButton1.Kind = DocumentFlow.Controls.ToolButtonKind.Delete;
            this.toolButton1.Location = new System.Drawing.Point(415, 0);
            this.toolButton1.Name = "toolButton1";
            this.toolButton1.Size = new System.Drawing.Size(25, 25);
            this.toolButton1.TabIndex = 1;
            this.toolButton1.Click += new System.EventHandler(this.ToolButton1_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panelEdit);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(846, 25);
            this.panel3.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Text";
            // 
            // L_ComboBoxAdv
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel3);
            this.Name = "L_ComboBoxAdv";
            this.Size = new System.Drawing.Size(846, 32);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxAdv1)).EndInit();
            this.panelEdit.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboBoxAdv1;
        private ToolButton toolButton1;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.Panel panelEdit;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
    }
}
