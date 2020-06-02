namespace DocumentFlow.Controls.Editor.Forms
{
    partial class SelectListWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxExt1 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolButton1 = new DocumentFlow.Controls.ToolButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.buttonSelect = new Syncfusion.WinForms.Controls.SfButton();
            this.gridMain = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.textBoxExt1);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.toolButton1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(6);
            this.panel1.Size = new System.Drawing.Size(800, 37);
            this.panel1.TabIndex = 3;
            // 
            // textBoxExt1
            // 
            this.textBoxExt1.BeforeTouchSize = new System.Drawing.Size(553, 25);
            this.textBoxExt1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.textBoxExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBoxExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxExt1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxExt1.Location = new System.Drawing.Point(108, 6);
            this.textBoxExt1.Name = "textBoxExt1";
            this.textBoxExt1.NearImage = global::DocumentFlow.Controls.Properties.Resources.icons8_find_filled_16;
            this.textBoxExt1.Size = new System.Drawing.Size(553, 25);
            this.textBoxExt1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.textBoxExt1.TabIndex = 2;
            this.textBoxExt1.ThemeName = "Metro";
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(102, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(6, 25);
            this.panel4.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(661, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(6, 25);
            this.panel3.TabIndex = 5;
            // 
            // toolButton1
            // 
            this.toolButton1.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.toolButton1.BackColor = System.Drawing.Color.White;
            this.toolButton1.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.toolButton1.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.toolButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.toolButton1.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.toolButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolButton1.Kind = DocumentFlow.Controls.ToolButtonKind.Delete;
            this.toolButton1.Location = new System.Drawing.Point(667, 6);
            this.toolButton1.Name = "toolButton1";
            this.toolButton1.Size = new System.Drawing.Size(25, 25);
            this.toolButton1.TabIndex = 3;
            this.toolButton1.Text = "toolButton1";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(692, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(6, 25);
            this.panel2.TabIndex = 4;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleName = "Button";
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(698, 6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 25);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Отмена";
            // 
            // buttonSelect
            // 
            this.buttonSelect.AccessibleName = "Button";
            this.buttonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonSelect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSelect.Location = new System.Drawing.Point(6, 6);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(96, 25);
            this.buttonSelect.TabIndex = 0;
            this.buttonSelect.Text = "Выбор";
            // 
            // gridMain
            // 
            this.gridMain.AccessibleName = "Table";
            this.gridMain.AllowEditing = false;
            this.gridMain.AllowGrouping = false;
            this.gridMain.AllowResizingColumns = true;
            this.gridMain.AllowTriStateSorting = true;
            this.gridMain.AutoGenerateColumns = false;
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridMain.Location = new System.Drawing.Point(0, 37);
            this.gridMain.Name = "gridMain";
            this.gridMain.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridMain.Size = new System.Drawing.Size(800, 413);
            this.gridMain.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridMain.TabIndex = 4;
            // 
            // SelectListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridMain);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectListWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Выбор";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private ToolButton toolButton1;
        private System.Windows.Forms.Panel panel2;
        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonSelect;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridMain;
    }
}