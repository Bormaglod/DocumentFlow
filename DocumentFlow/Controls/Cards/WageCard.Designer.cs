namespace DocumentFlow.Controls.Cards
{
    partial class WageCard
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
            this.gridEmps = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmps)).BeginInit();
            this.SuspendLayout();
            // 
            // gridEmps
            // 
            this.gridEmps.AccessibleName = "Table";
            this.gridEmps.AllowEditing = false;
            this.gridEmps.AllowGrouping = false;
            this.gridEmps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEmps.HeaderRowHeight = 28;
            this.gridEmps.Location = new System.Drawing.Point(6, 6);
            this.gridEmps.Name = "gridEmps";
            this.gridEmps.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridEmps.RowHeight = 26;
            this.gridEmps.Size = new System.Drawing.Size(424, 224);
            this.gridEmps.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridEmps.Style.CellStyle.Font.Size = 8F;
            this.gridEmps.Style.CheckBoxStyle.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridEmps.Style.CheckBoxStyle.CheckedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridEmps.Style.CheckBoxStyle.IndeterminateBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridEmps.Style.HyperlinkStyle.DefaultLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridEmps.Style.RowHeaderStyle.Font.Size = 8F;
            this.gridEmps.TabIndex = 1;
            this.gridEmps.AutoGeneratingColumn += new Syncfusion.WinForms.DataGrid.Events.AutoGeneratingColumnEventHandler(this.GridEmps_AutoGeneratingColumn);
            // 
            // WageCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridEmps);
            this.Name = "WageCard";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Size = new System.Drawing.Size(436, 236);
            ((System.ComponentModel.ISupportInitialize)(this.gridEmps)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.DataGrid.SfDataGrid gridEmps;
    }
}
