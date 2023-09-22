using DocumentFlow.Controls.Interfaces;

namespace DocumentFlow.Controls.Editors
{
    partial class DfComboBox
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
            panelEdit = new Panel();
            comboBox = new Syncfusion.WinForms.ListView.SfComboBox();
            panelDelete = new Panel();
            buttonDelete = new ToolButton();
            panelOpen = new Panel();
            buttonOpen = new ToolButton();
            panelEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)comboBox).BeginInit();
            panelDelete.SuspendLayout();
            panelOpen.SuspendLayout();
            SuspendLayout();
            // 
            // panelEdit
            // 
            panelEdit.Controls.Add(comboBox);
            panelEdit.Controls.Add(panelDelete);
            panelEdit.Controls.Add(panelOpen);
            panelEdit.Dock = DockStyle.Left;
            panelEdit.Location = new Point(100, 0);
            panelEdit.Name = "panelEdit";
            panelEdit.Size = new Size(300, 25);
            panelEdit.TabIndex = 3;
            // 
            // comboBox
            // 
            comboBox.Dock = DockStyle.Fill;
            comboBox.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            comboBox.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            comboBox.Location = new Point(0, 0);
            comboBox.Name = "comboBox";
            comboBox.Size = new Size(240, 25);
            comboBox.Style.EditorStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox.Style.ReadOnlyEditorStyle.BackColor = Color.White;
            comboBox.Style.ReadOnlyEditorStyle.BorderColor = Color.FromArgb(197, 197, 197);
            comboBox.Style.ReadOnlyEditorStyle.DisabledBackColor = Color.White;
            comboBox.Style.ReadOnlyEditorStyle.DisabledBorderColor = Color.FromArgb(197, 197, 197);
            comboBox.Style.ReadOnlyEditorStyle.DisabledForeColor = Color.FromArgb(152, 152, 152);
            comboBox.Style.ReadOnlyEditorStyle.FocusedBorderColor = Color.FromArgb(150, 150, 150);
            comboBox.Style.ReadOnlyEditorStyle.FocusedCuesColor = Color.FromArgb(68, 68, 68);
            comboBox.Style.TokenStyle.CloseButtonBackColor = Color.FromArgb(255, 255, 255);
            comboBox.TabIndex = 7;
            comboBox.DropDownOpened += ComboBox_DropDownOpened;
            comboBox.DropDownClosed += ComboBox_DropDownClosed;
            // 
            // panelDelete
            // 
            panelDelete.Controls.Add(buttonDelete);
            panelDelete.Dock = DockStyle.Right;
            panelDelete.Location = new Point(240, 0);
            panelDelete.Name = "panelDelete";
            panelDelete.Size = new Size(30, 25);
            panelDelete.TabIndex = 5;
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
            buttonDelete.Kind = Enums.ToolButtonKind.Delete;
            buttonDelete.Location = new Point(5, 0);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(25, 25);
            buttonDelete.TabIndex = 3;
            buttonDelete.TabStop = false;
            buttonDelete.Text = "toolButton2";
            buttonDelete.Click += ButtonDelete_Click;
            // 
            // panelOpen
            // 
            panelOpen.Controls.Add(buttonOpen);
            panelOpen.Dock = DockStyle.Right;
            panelOpen.Location = new Point(270, 0);
            panelOpen.Name = "panelOpen";
            panelOpen.Size = new Size(30, 25);
            panelOpen.TabIndex = 6;
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
            buttonOpen.Kind = Enums.ToolButtonKind.Open;
            buttonOpen.Location = new Point(5, 0);
            buttonOpen.Name = "buttonOpen";
            buttonOpen.Size = new Size(25, 25);
            buttonOpen.TabIndex = 9;
            buttonOpen.TabStop = false;
            buttonOpen.Text = "toolButton1";
            buttonOpen.Click += ButtonOpen_Click;
            // 
            // DfComboBox
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelEdit);
            Margin = new Padding(3, 4, 3, 4);
            Name = "DfComboBox";
            Size = new Size(500, 32);
            Controls.SetChildIndex(panelEdit, 0);
            panelEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)comboBox).EndInit();
            panelDelete.ResumeLayout(false);
            panelOpen.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panelEdit;
        private ToolButton buttonDelete;
        private ToolButton buttonOpen;
        private Panel panelDelete;
        private Panel panelOpen;
        private Syncfusion.WinForms.ListView.SfComboBox comboBox;
    }
}
