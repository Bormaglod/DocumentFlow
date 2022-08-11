namespace DocumentFlow.Controls.Editors
{
    partial class DfProductionLot
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
            this.gridContent = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.gridContent)).BeginInit();
            this.SuspendLayout();
            // 
            // gridContent
            // 
            this.gridContent.AccessibleName = "Table";
            this.gridContent.AllowEditing = false;
            this.gridContent.AllowGrouping = false;
            this.gridContent.AllowResizingColumns = true;
            this.gridContent.AllowSorting = false;
            this.gridContent.AutoGenerateColumns = false;
            this.gridContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridContent.Location = new System.Drawing.Point(0, 0);
            this.gridContent.Name = "gridContent";
            this.gridContent.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridContent.Size = new System.Drawing.Size(765, 451);
            this.gridContent.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridContent.Style.CheckBoxStyle.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridContent.Style.CheckBoxStyle.CheckedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridContent.Style.CheckBoxStyle.IndeterminateBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridContent.Style.HyperlinkStyle.DefaultLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridContent.TabIndex = 1;
            this.gridContent.QueryCellStyle += new Syncfusion.WinForms.DataGrid.Events.QueryCellStyleEventHandler(this.GridContent_QueryCellStyle);
            // 
            // DfProductionLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridContent);
            this.Name = "DfProductionLot";
            this.Size = new System.Drawing.Size(765, 451);
            ((System.ComponentModel.ISupportInitialize)(this.gridContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridContent;
    }
}
