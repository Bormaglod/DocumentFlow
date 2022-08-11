namespace DocumentFlow.Dialogs.Settings
{
    partial class BrowserColumnsPage
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonChangeColumn = new Syncfusion.WinForms.Controls.SfButton();
            this.gridColumns = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonChangeColumn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(612, 8);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.panel1.Size = new System.Drawing.Size(114, 468);
            this.panel1.TabIndex = 4;
            // 
            // buttonChangeColumn
            // 
            this.buttonChangeColumn.AccessibleName = "Button";
            this.buttonChangeColumn.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonChangeColumn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonChangeColumn.Location = new System.Drawing.Point(5, 0);
            this.buttonChangeColumn.Name = "buttonChangeColumn";
            this.buttonChangeColumn.Size = new System.Drawing.Size(109, 28);
            this.buttonChangeColumn.TabIndex = 1;
            this.buttonChangeColumn.Text = "Изменить";
            this.buttonChangeColumn.UseVisualStyleBackColor = true;
            // 
            // gridColumns
            // 
            this.gridColumns.AccessibleName = "Table";
            this.gridColumns.AllowEditing = false;
            this.gridColumns.AllowGrouping = false;
            this.gridColumns.AllowSorting = false;
            this.gridColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridColumns.Location = new System.Drawing.Point(8, 8);
            this.gridColumns.Name = "gridColumns";
            this.gridColumns.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridColumns.Size = new System.Drawing.Size(604, 468);
            this.gridColumns.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(144)))));
            this.gridColumns.Style.CheckBoxStyle.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridColumns.Style.CheckBoxStyle.CheckedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridColumns.Style.CheckBoxStyle.IndeterminateBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridColumns.Style.HyperlinkStyle.DefaultLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridColumns.TabIndex = 5;
            this.gridColumns.Text = "sfDataGrid1";
            // 
            // BrowserColumnsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.gridColumns);
            this.Controls.Add(this.panel1);
            this.Name = "BrowserColumnsPage";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Size = new System.Drawing.Size(734, 484);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridColumns)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Syncfusion.WinForms.Controls.SfButton buttonChangeColumn;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridColumns;
    }
}
