namespace DocumentFlow.Controls.Cards
{
    partial class GivingMaterialsCard
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
            gridMaterials = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            contextRecordMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            menuOpenMaterial = new ToolStripMenuItem();
            menuOpenContractor = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)gridMaterials).BeginInit();
            contextRecordMenu.SuspendLayout();
            SuspendLayout();
            // 
            // gridMaterials
            // 
            gridMaterials.AccessibleName = "Table";
            gridMaterials.AllowEditing = false;
            gridMaterials.AllowGrouping = false;
            gridMaterials.Dock = DockStyle.Fill;
            gridMaterials.HeaderRowHeight = 28;
            gridMaterials.Location = new Point(6, 6);
            gridMaterials.Name = "gridMaterials";
            gridMaterials.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            gridMaterials.RowHeight = 26;
            gridMaterials.Size = new Size(402, 196);
            gridMaterials.Style.BorderColor = Color.FromArgb(204, 204, 204);
            gridMaterials.Style.CellStyle.Font.Size = 8F;
            gridMaterials.Style.CheckBoxStyle.CheckedBackColor = Color.FromArgb(0, 120, 215);
            gridMaterials.Style.CheckBoxStyle.CheckedBorderColor = Color.FromArgb(0, 120, 215);
            gridMaterials.Style.CheckBoxStyle.IndeterminateBorderColor = Color.FromArgb(0, 120, 215);
            gridMaterials.Style.HyperlinkStyle.DefaultLinkColor = Color.FromArgb(0, 120, 215);
            gridMaterials.Style.RowHeaderStyle.Font.Size = 8F;
            gridMaterials.TabIndex = 0;
            gridMaterials.AutoGeneratingColumn += GridMaterials_AutoGeneratingColumn;
            // 
            // contextRecordMenu
            // 
            contextRecordMenu.DropShadowEnabled = false;
            contextRecordMenu.Items.AddRange(new ToolStripItem[] { menuOpenMaterial, menuOpenContractor });
            contextRecordMenu.MetroColor = Color.FromArgb(204, 236, 249);
            contextRecordMenu.Name = "contextRecordMenu";
            contextRecordMenu.Size = new Size(192, 70);
            contextRecordMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            contextRecordMenu.ThemeName = "Metro";
            // 
            // menuOpenMaterial
            // 
            menuOpenMaterial.Name = "menuOpenMaterial";
            menuOpenMaterial.Size = new Size(191, 22);
            menuOpenMaterial.Text = "Открыть материал";
            menuOpenMaterial.Click += MenuOpenMaterial_Click;
            // 
            // menuOpenContractor
            // 
            menuOpenContractor.Name = "menuOpenContractor";
            menuOpenContractor.Size = new Size(191, 22);
            menuOpenContractor.Text = "Открыть контрагента";
            menuOpenContractor.Click += MenuOpenContractor_Click;
            // 
            // GivingMaterialsCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridMaterials);
            Name = "GivingMaterialsCard";
            Padding = new Padding(6);
            Size = new Size(414, 208);
            ((System.ComponentModel.ISupportInitialize)gridMaterials).EndInit();
            contextRecordMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.WinForms.DataGrid.SfDataGrid gridMaterials;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextRecordMenu;
        private ToolStripMenuItem menuOpenMaterial;
        private ToolStripMenuItem menuOpenContractor;
    }
}
