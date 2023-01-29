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
            this.gridMaterials = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.gridMaterials)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMaterials
            // 
            this.gridMaterials.AccessibleName = "Table";
            this.gridMaterials.AllowEditing = false;
            this.gridMaterials.AllowGrouping = false;
            this.gridMaterials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMaterials.HeaderRowHeight = 28;
            this.gridMaterials.Location = new System.Drawing.Point(6, 6);
            this.gridMaterials.Name = "gridMaterials";
            this.gridMaterials.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridMaterials.RowHeight = 26;
            this.gridMaterials.Size = new System.Drawing.Size(402, 196);
            this.gridMaterials.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridMaterials.Style.CellStyle.Font.Size = 8F;
            this.gridMaterials.Style.CheckBoxStyle.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridMaterials.Style.CheckBoxStyle.CheckedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridMaterials.Style.CheckBoxStyle.IndeterminateBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridMaterials.Style.HyperlinkStyle.DefaultLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridMaterials.Style.RowHeaderStyle.Font.Size = 8F;
            this.gridMaterials.TabIndex = 0;
            this.gridMaterials.AutoGeneratingColumn += new Syncfusion.WinForms.DataGrid.Events.AutoGeneratingColumnEventHandler(this.GridMaterials_AutoGeneratingColumn);
            // 
            // GivingMaterialsCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridMaterials);
            this.Name = "GivingMaterialsCard";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Size = new System.Drawing.Size(414, 208);
            ((System.ComponentModel.ISupportInitialize)(this.gridMaterials)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.DataGrid.SfDataGrid gridMaterials;
    }
}
