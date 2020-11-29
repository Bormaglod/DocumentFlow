namespace DocumentFlow.Controls.Editor
{
    partial class L_SelectBox
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.textValue)).BeginInit();
            this.panelEdit.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // textValue
            // 
            this.textValue.BeforeTouchSize = new System.Drawing.Size(180, 25);
            this.textValue.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.textValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textValue.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textValue.Location = new System.Drawing.Point(0, 0);
            this.textValue.Name = "textValue";
            this.textValue.ReadOnly = true;
            this.textValue.Size = new System.Drawing.Size(180, 25);
            this.textValue.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.textValue.TabIndex = 0;
            this.textValue.ThemeName = "Metro";
            // 
            // panelSeparator1
            // 
            this.panelSeparator1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSeparator1.Location = new System.Drawing.Point(180, 0);
            this.panelSeparator1.Name = "panelSeparator1";
            this.panelSeparator1.Size = new System.Drawing.Size(5, 25);
            this.panelSeparator1.TabIndex = 3;
            // 
            // panelSeparator2
            // 
            this.panelSeparator2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSeparator2.Location = new System.Drawing.Point(210, 0);
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
            this.panelEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEdit.Location = new System.Drawing.Point(117, 0);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(240, 25);
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
            this.buttonDelete.Location = new System.Drawing.Point(185, 0);
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
            this.buttonSelect.Location = new System.Drawing.Point(215, 0);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(25, 25);
            this.buttonSelect.TabIndex = 1;
            this.buttonSelect.Text = "toolButton1";
            this.buttonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelEdit);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(394, 25);
            this.panel4.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Text";
            // 
            // L_SelectBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "L_SelectBox";
            this.Size = new System.Drawing.Size(394, 32);
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
    }
}
