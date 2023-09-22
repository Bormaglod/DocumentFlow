namespace DocumentFlow.Controls.Editors
{
    partial class DfPropertyList
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
            toolStrip1 = new ToolStrip();
            buttonAdd = new ToolStripButton();
            buttonEdit = new ToolStripButton();
            buttonDelete = new ToolStripButton();
            gridParams = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            panelEdit = new Panel();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridParams).BeginInit();
            panelEdit.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { buttonAdd, buttonEdit, buttonDelete });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(351, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // buttonAdd
            // 
            buttonAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonAdd.Image = DocumentFlow.Properties.Resources.icons8_add_16;
            buttonAdd.ImageTransparentColor = Color.Magenta;
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(23, 22);
            buttonAdd.Text = "toolStripButton1";
            // 
            // buttonEdit
            // 
            buttonEdit.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonEdit.Image = DocumentFlow.Properties.Resources.pencil_16;
            buttonEdit.ImageTransparentColor = Color.Magenta;
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(23, 22);
            buttonEdit.Text = "toolStripButton2";
            // 
            // buttonDelete
            // 
            buttonDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonDelete.Image = DocumentFlow.Properties.Resources.icons8_delete_16;
            buttonDelete.ImageTransparentColor = Color.Magenta;
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(23, 22);
            buttonDelete.Text = "toolStripButton3";
            // 
            // gridParams
            // 
            gridParams.AccessibleName = "Table";
            gridParams.AllowEditing = false;
            gridParams.AllowGrouping = false;
            gridParams.Dock = DockStyle.Fill;
            gridParams.HeaderRowHeight = 0;
            gridParams.Location = new Point(0, 25);
            gridParams.Name = "gridParams";
            gridParams.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            gridParams.Size = new Size(351, 0);
            gridParams.Style.BorderColor = Color.FromArgb(204, 204, 204);
            gridParams.Style.CheckBoxStyle.CheckedBackColor = Color.FromArgb(0, 120, 215);
            gridParams.Style.CheckBoxStyle.CheckedBorderColor = Color.FromArgb(0, 120, 215);
            gridParams.Style.CheckBoxStyle.IndeterminateBorderColor = Color.FromArgb(0, 120, 215);
            gridParams.Style.HyperlinkStyle.DefaultLinkColor = Color.FromArgb(0, 120, 215);
            gridParams.TabIndex = 2;
            gridParams.Text = "sfDataGrid1";
            gridParams.AutoGeneratingColumn += GridParams_AutoGeneratingColumn;
            // 
            // panelEdit
            // 
            panelEdit.Controls.Add(gridParams);
            panelEdit.Controls.Add(toolStrip1);
            panelEdit.Dock = DockStyle.Left;
            panelEdit.Location = new Point(100, 0);
            panelEdit.Name = "panelEdit";
            panelEdit.Size = new Size(351, 24);
            panelEdit.TabIndex = 3;
            // 
            // DfPropertyList
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelEdit);
            Name = "DfPropertyList";
            Padding = new Padding(0, 0, 0, 8);
            Size = new Size(532, 32);
            Controls.SetChildIndex(panelEdit, 0);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridParams).EndInit();
            panelEdit.ResumeLayout(false);
            panelEdit.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton buttonAdd;
        private ToolStripButton buttonEdit;
        private ToolStripButton buttonDelete;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridParams;
        private Panel panelEdit;
    }
}
