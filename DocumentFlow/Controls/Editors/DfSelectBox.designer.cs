using DocumentFlow.Controls.Interfaces;

namespace DocumentFlow.Controls.Editors
{
    partial class DfSelectBox
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
            textValue = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            panelEdit = new Panel();
            panelDelete = new Panel();
            buttonDelete = new ToolButton();
            panelSelect = new Panel();
            buttonSelect = new ToolButton();
            panelOpen = new Panel();
            buttonOpen = new ToolButton();
            ((System.ComponentModel.ISupportInitialize)textValue).BeginInit();
            panelEdit.SuspendLayout();
            panelDelete.SuspendLayout();
            panelSelect.SuspendLayout();
            panelOpen.SuspendLayout();
            SuspendLayout();
            // 
            // textValue
            // 
            textValue.BackColor = Color.FromArgb(255, 255, 255);
            textValue.BeforeTouchSize = new Size(310, 25);
            textValue.BorderColor = Color.FromArgb(197, 197, 197);
            textValue.BorderStyle = BorderStyle.FixedSingle;
            textValue.Dock = DockStyle.Fill;
            textValue.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textValue.ForeColor = Color.FromArgb(68, 68, 68);
            textValue.Location = new Point(0, 0);
            textValue.Name = "textValue";
            textValue.ReadOnly = true;
            textValue.Size = new Size(310, 25);
            textValue.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            textValue.TabIndex = 0;
            textValue.ThemeName = "Office2016Colorful";
            // 
            // panelEdit
            // 
            panelEdit.Controls.Add(textValue);
            panelEdit.Controls.Add(panelDelete);
            panelEdit.Controls.Add(panelSelect);
            panelEdit.Controls.Add(panelOpen);
            panelEdit.Dock = DockStyle.Left;
            panelEdit.Location = new Point(75, 0);
            panelEdit.Name = "panelEdit";
            panelEdit.Size = new Size(400, 25);
            panelEdit.TabIndex = 5;
            // 
            // panelDelete
            // 
            panelDelete.Controls.Add(buttonDelete);
            panelDelete.Dock = DockStyle.Right;
            panelDelete.Location = new Point(310, 0);
            panelDelete.Name = "panelDelete";
            panelDelete.Size = new Size(30, 25);
            panelDelete.TabIndex = 3;
            // 
            // buttonDelete
            // 
            buttonDelete.BackClickedColor = Color.FromArgb(204, 232, 255);
            buttonDelete.BackColor = Color.White;
            buttonDelete.BackHoveredColor = Color.FromArgb(229, 243, 255);
            buttonDelete.BorderClickedColor = Color.FromArgb(153, 209, 255);
            buttonDelete.BorderColor = Color.FromArgb(217, 217, 217);
            buttonDelete.BorderHoveredColor = Color.FromArgb(204, 232, 235);
            buttonDelete.Dock = DockStyle.Right;
            buttonDelete.Kind = DocumentFlow.Controls.Enums.ToolButtonKind.Delete;
            buttonDelete.Location = new Point(5, 0);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(25, 25);
            buttonDelete.TabIndex = 2;
            buttonDelete.TabStop = false;
            buttonDelete.Text = "toolButton2";
            buttonDelete.Click += ButtonDelete_Click;
            // 
            // panelSelect
            // 
            panelSelect.Controls.Add(buttonSelect);
            panelSelect.Dock = DockStyle.Right;
            panelSelect.Location = new Point(340, 0);
            panelSelect.Name = "panelSelect";
            panelSelect.Size = new Size(30, 25);
            panelSelect.TabIndex = 2;
            // 
            // buttonSelect
            // 
            buttonSelect.BackClickedColor = Color.FromArgb(204, 232, 255);
            buttonSelect.BackColor = Color.White;
            buttonSelect.BackHoveredColor = Color.FromArgb(229, 243, 255);
            buttonSelect.BorderClickedColor = Color.FromArgb(153, 209, 255);
            buttonSelect.BorderColor = Color.FromArgb(217, 217, 217);
            buttonSelect.BorderHoveredColor = Color.FromArgb(204, 232, 235);
            buttonSelect.Dock = DockStyle.Right;
            buttonSelect.Kind = DocumentFlow.Controls.Enums.ToolButtonKind.Select;
            buttonSelect.Location = new Point(5, 0);
            buttonSelect.Name = "buttonSelect";
            buttonSelect.Size = new Size(25, 25);
            buttonSelect.TabIndex = 1;
            buttonSelect.TabStop = false;
            buttonSelect.Text = "toolButton1";
            buttonSelect.Click += ButtonSelect_Click;
            // 
            // panelOpen
            // 
            panelOpen.Controls.Add(buttonOpen);
            panelOpen.Dock = DockStyle.Right;
            panelOpen.Location = new Point(370, 0);
            panelOpen.Name = "panelOpen";
            panelOpen.Size = new Size(30, 25);
            panelOpen.TabIndex = 1;
            // 
            // buttonOpen
            // 
            buttonOpen.BackClickedColor = Color.FromArgb(204, 232, 255);
            buttonOpen.BackColor = Color.White;
            buttonOpen.BackHoveredColor = Color.FromArgb(229, 243, 255);
            buttonOpen.BorderClickedColor = Color.FromArgb(153, 209, 255);
            buttonOpen.BorderColor = Color.FromArgb(217, 217, 217);
            buttonOpen.BorderHoveredColor = Color.FromArgb(204, 232, 235);
            buttonOpen.Dock = DockStyle.Right;
            buttonOpen.Kind = DocumentFlow.Controls.Enums.ToolButtonKind.Open;
            buttonOpen.Location = new Point(5, 0);
            buttonOpen.Name = "buttonOpen";
            buttonOpen.Size = new Size(25, 25);
            buttonOpen.TabIndex = 1;
            buttonOpen.TabStop = false;
            buttonOpen.Text = "toolButton1";
            buttonOpen.Click += ButtonOpen_Click;
            // 
            // DfSelectBox
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(panelEdit);
            Margin = new Padding(3, 4, 3, 4);
            Name = "DfSelectBox";
            Size = new Size(500, 32);
            Controls.SetChildIndex(panelEdit, 0);
            ((System.ComponentModel.ISupportInitialize)textValue).EndInit();
            panelEdit.ResumeLayout(false);
            panelEdit.PerformLayout();
            panelDelete.ResumeLayout(false);
            panelSelect.ResumeLayout(false);
            panelOpen.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TextBoxExt textValue;
        private ToolButton buttonSelect;
        private ToolButton buttonDelete;
        private Panel panelEdit;
        private ToolButton buttonOpen;
        private Panel panelDelete;
        private Panel panelSelect;
        private Panel panelOpen;
    }
}
