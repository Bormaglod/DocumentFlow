namespace DocumentFlow.Controls.Editors
{
    partial class SelectBox<T>
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
            this.textValue = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panelSeparator1 = new System.Windows.Forms.Panel();
            this.panelSeparator2 = new System.Windows.Forms.Panel();
            this.panelEdit = new System.Windows.Forms.Panel();
            this.buttonDelete = new DocumentFlow.Controls.ToolButton();
            this.buttonSelect = new DocumentFlow.Controls.ToolButton();
            this.panelSeparator3 = new System.Windows.Forms.Panel();
            this.buttonOpen = new DocumentFlow.Controls.ToolButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.textValue)).BeginInit();
            this.panelEdit.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // textValue
            // 
            this.textValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textValue.BeforeTouchSize = new System.Drawing.Size(151, 25);
            this.textValue.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textValue.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textValue.Location = new System.Drawing.Point(0, 0);
            this.textValue.Name = "textValue";
            this.textValue.ReadOnly = true;
            this.textValue.Size = new System.Drawing.Size(151, 25);
            this.textValue.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textValue.TabIndex = 0;
            this.textValue.ThemeName = "Office2016Colorful";
            // 
            // panelSeparator1
            // 
            this.panelSeparator1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSeparator1.Location = new System.Drawing.Point(151, 0);
            this.panelSeparator1.Name = "panelSeparator1";
            this.panelSeparator1.Size = new System.Drawing.Size(5, 25);
            this.panelSeparator1.TabIndex = 3;
            // 
            // panelSeparator2
            // 
            this.panelSeparator2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSeparator2.Location = new System.Drawing.Point(181, 0);
            this.panelSeparator2.Name = "panelSeparator2";
            this.panelSeparator2.Size = new System.Drawing.Size(5, 25);
            this.panelSeparator2.TabIndex = 4;
            // 
            // panelEdit
            // 
            this.panelEdit.Controls.Add(this.textValue);
            this.panelEdit.Controls.Add(this.panelSeparator1);
            this.panelEdit.Controls.Add(this.buttonDelete);
            this.panelEdit.Controls.Add(this.panelSeparator2);
            this.panelEdit.Controls.Add(this.buttonSelect);
            this.panelEdit.Controls.Add(this.panelSeparator3);
            this.panelEdit.Controls.Add(this.buttonOpen);
            this.panelEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEdit.Location = new System.Drawing.Point(117, 0);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(241, 25);
            this.panelEdit.TabIndex = 5;
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
            this.buttonDelete.Location = new System.Drawing.Point(156, 0);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(25, 25);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "toolButton2";
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonSelect
            // 
            this.buttonSelect.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.buttonSelect.BackColor = System.Drawing.Color.White;
            this.buttonSelect.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.buttonSelect.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.buttonSelect.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.buttonSelect.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.buttonSelect.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSelect.Kind = DocumentFlow.Controls.ToolButtonKind.Select;
            this.buttonSelect.Location = new System.Drawing.Point(186, 0);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(25, 25);
            this.buttonSelect.TabIndex = 1;
            this.buttonSelect.Text = "toolButton1";
            this.buttonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // panelSeparator3
            // 
            this.panelSeparator3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSeparator3.Location = new System.Drawing.Point(211, 0);
            this.panelSeparator3.Name = "panelSeparator3";
            this.panelSeparator3.Size = new System.Drawing.Size(5, 25);
            this.panelSeparator3.TabIndex = 7;
            // 
            // buttonOpen
            // 
            this.buttonOpen.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.buttonOpen.BackColor = System.Drawing.Color.White;
            this.buttonOpen.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.buttonOpen.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.buttonOpen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.buttonOpen.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.buttonOpen.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOpen.Kind = DocumentFlow.Controls.ToolButtonKind.Open;
            this.buttonOpen.Location = new System.Drawing.Point(216, 0);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(25, 25);
            this.buttonOpen.TabIndex = 1;
            this.buttonOpen.Text = "toolButton1";
            this.buttonOpen.Click += new System.EventHandler(this.ButtonOpen_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelEdit);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(391, 25);
            this.panel4.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Text";
            // 
            // DfDirectorySelectBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DfDirectorySelectBox";
            this.Size = new System.Drawing.Size(391, 32);
            ((System.ComponentModel.ISupportInitialize)(this.textValue)).EndInit();
            this.panelEdit.ResumeLayout(false);
            this.panelEdit.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TextBoxExt textValue;
        private ToolButton buttonSelect;
        private ToolButton buttonDelete;
        private System.Windows.Forms.Panel panelSeparator1;
        private System.Windows.Forms.Panel panelSeparator2;
        private System.Windows.Forms.Panel panelEdit;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private Panel panelSeparator3;
        private ToolButton buttonOpen;
    }
}
