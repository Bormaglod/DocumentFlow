namespace DocumentFlow.Controls.Cards
{
    partial class ApplicatorCard
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
            this.gridApps = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.gridApps)).BeginInit();
            this.SuspendLayout();
            // 
            // gridApps
            // 
            this.gridApps.AccessibleName = "Table";
            this.gridApps.AllowEditing = false;
            this.gridApps.AllowGrouping = false;
            this.gridApps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridApps.HeaderRowHeight = 28;
            this.gridApps.Location = new System.Drawing.Point(6, 6);
            this.gridApps.Name = "gridApps";
            this.gridApps.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridApps.RowHeight = 26;
            this.gridApps.Size = new System.Drawing.Size(505, 213);
            this.gridApps.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridApps.Style.CellStyle.Font.Size = 8F;
            this.gridApps.Style.CheckBoxStyle.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridApps.Style.CheckBoxStyle.CheckedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridApps.Style.CheckBoxStyle.IndeterminateBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridApps.Style.HyperlinkStyle.DefaultLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridApps.Style.RowHeaderStyle.Font.Size = 8F;
            this.gridApps.TabIndex = 2;
            this.gridApps.AutoGeneratingColumn += new Syncfusion.WinForms.DataGrid.Events.AutoGeneratingColumnEventHandler(this.GridApps_AutoGeneratingColumn);
            // 
            // ApplicatorCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridApps);
            this.Name = "ApplicatorCard";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Size = new System.Drawing.Size(517, 225);
            ((System.ComponentModel.ISupportInitialize)(this.gridApps)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.DataGrid.SfDataGrid gridApps;
    }
}
