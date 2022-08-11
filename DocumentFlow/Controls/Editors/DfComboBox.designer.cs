namespace DocumentFlow.Controls.Editors
{
    partial class DfComboBox<T>
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
            this.comboBoxAdv1 = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.panelEdit = new System.Windows.Forms.Panel();
            this.panelSeparator1 = new System.Windows.Forms.Panel();
            this.buttonDelete = new DocumentFlow.Controls.ToolButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxAdv1)).BeginInit();
            this.panelEdit.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Text";
            // 
            // comboBoxAdv1
            // 
            this.comboBoxAdv1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboBoxAdv1.BeforeTouchSize = new System.Drawing.Size(172, 23);
            this.comboBoxAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxAdv1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAdv1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.comboBoxAdv1.Location = new System.Drawing.Point(0, 0);
            this.comboBoxAdv1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxAdv1.Name = "comboBoxAdv1";
            this.comboBoxAdv1.Size = new System.Drawing.Size(172, 23);
            this.comboBoxAdv1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.comboBoxAdv1.TabIndex = 2;
            this.comboBoxAdv1.ThemeName = "Office2016Colorful";
            this.comboBoxAdv1.SelectedIndexChanging += new Syncfusion.Windows.Forms.Tools.SelectedIndexChangingHandler(this.ComboBoxAdv1_SelectedIndexChanging);
            // 
            // panelEdit
            // 
            this.panelEdit.Controls.Add(this.comboBoxAdv1);
            this.panelEdit.Controls.Add(this.panelSeparator1);
            this.panelEdit.Controls.Add(this.buttonDelete);
            this.panelEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEdit.Location = new System.Drawing.Point(100, 0);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(200, 23);
            this.panelEdit.TabIndex = 3;
            // 
            // panelSeparator1
            // 
            this.panelSeparator1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSeparator1.Location = new System.Drawing.Point(172, 0);
            this.panelSeparator1.Name = "panelSeparator1";
            this.panelSeparator1.Size = new System.Drawing.Size(5, 23);
            this.panelSeparator1.TabIndex = 4;
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.buttonDelete.BackColor = System.Drawing.Color.White;
            this.buttonDelete.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.buttonDelete.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.buttonDelete.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.buttonDelete.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.buttonDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonDelete.Kind = DocumentFlow.Controls.ToolButtonKind.Delete;
            this.buttonDelete.Location = new System.Drawing.Point(177, 0);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(23, 23);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "toolButton2";
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelEdit);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 23);
            this.panel1.TabIndex = 4;
            // 
            // DfComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DfComboBox";
            this.Size = new System.Drawing.Size(363, 32);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxAdv1)).EndInit();
            this.panelEdit.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboBoxAdv1;
        private System.Windows.Forms.Panel panelEdit;
        private ToolButton buttonDelete;
        private System.Windows.Forms.Panel panelSeparator1;
        private System.Windows.Forms.Panel panel1;
    }
}
