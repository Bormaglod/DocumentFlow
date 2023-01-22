namespace DocumentFlow.Dialogs
{
    partial class SelectDocumentForm<T>
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
            this.gridContent = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridContent)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(742, 37);
            this.panel1.TabIndex = 3;
            // 
            // textBoxExt1
            // 
            this.textBoxExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxExt1.BeforeTouchSize = new System.Drawing.Size(495, 25);
            this.textBoxExt1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textBoxExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxExt1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxExt1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textBoxExt1.Location = new System.Drawing.Point(108, 6);
            this.textBoxExt1.Name = "textBoxExt1";
            this.textBoxExt1.NearImage = global::DocumentFlow.Properties.Resources.icons8_find_filled_16;
            this.textBoxExt1.Size = new System.Drawing.Size(495, 25);
            this.textBoxExt1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textBoxExt1.TabIndex = 2;
            this.textBoxExt1.ThemeName = "Office2016Colorful";
            this.textBoxExt1.TextChanged += new System.EventHandler(this.TextBoxExt1_TextChanged);
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
            this.panel3.Location = new System.Drawing.Point(603, 6);
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
            this.toolButton1.Kind = DocumentFlow.Infrastructure.Controls.ToolButtonKind.Delete;
            this.toolButton1.Location = new System.Drawing.Point(609, 6);
            this.toolButton1.Name = "toolButton1";
            this.toolButton1.Size = new System.Drawing.Size(25, 25);
            this.toolButton1.TabIndex = 3;
            this.toolButton1.TabStop = false;
            this.toolButton1.Text = "toolButton1";
            this.toolButton1.Click += new System.EventHandler(this.ToolButton1_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(634, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(6, 25);
            this.panel2.TabIndex = 4;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleName = "Button";
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancel.Location = new System.Drawing.Point(640, 6);
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
            this.buttonSelect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSelect.Location = new System.Drawing.Point(6, 6);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(96, 25);
            this.buttonSelect.TabIndex = 0;
            this.buttonSelect.Text = "Выбор";
            // 
            // gridContent
            // 
            this.gridContent.AccessibleName = "Table";
            this.gridContent.AllowEditing = false;
            this.gridContent.AllowFiltering = true;
            this.gridContent.AllowGrouping = false;
            this.gridContent.AllowResizingColumns = true;
            this.gridContent.AllowTriStateSorting = true;
            this.gridContent.AutoExpandGroups = true;
            this.gridContent.AutoGenerateColumns = false;
            this.gridContent.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
            this.gridContent.BackColor = System.Drawing.SystemColors.Window;
            this.gridContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridContent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gridContent.LiveDataUpdateMode = Syncfusion.Data.LiveDataUpdateMode.AllowSummaryUpdate;
            this.gridContent.Location = new System.Drawing.Point(0, 37);
            this.gridContent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridContent.Name = "gridContent";
            this.gridContent.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridContent.ShowBusyIndicator = true;
            this.gridContent.ShowRowHeader = true;
            this.gridContent.ShowSortNumbers = true;
            this.gridContent.Size = new System.Drawing.Size(742, 386);
            this.gridContent.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridContent.Style.GroupDropAreaStyle.BackColor = System.Drawing.SystemColors.Window;
            this.gridContent.Style.ProgressBarStyle.ForegroundStyle = Syncfusion.WinForms.DataGrid.Enums.GridProgressBarStyle.Tube;
            this.gridContent.Style.ProgressBarStyle.ProgressTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.gridContent.Style.ProgressBarStyle.TubeForegroundEndColor = System.Drawing.Color.White;
            this.gridContent.Style.ProgressBarStyle.TubeForegroundStartColor = System.Drawing.Color.SkyBlue;
            this.gridContent.Style.TableSummaryRowStyle.Font.Size = 9F;
            this.gridContent.TabIndex = 15;
            this.gridContent.QueryCellStyle += new Syncfusion.WinForms.DataGrid.Events.QueryCellStyleEventHandler(this.GridContent_QueryCellStyle);
            this.gridContent.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.GridContent_CellDoubleClick);
            // 
            // SelectDocumentForm
            // 
            this.AcceptButton = this.buttonSelect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(742, 423);
            this.Controls.Add(this.gridContent);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectDocumentForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt1;
        private Panel panel4;
        private Panel panel3;
        private Controls.ToolButton toolButton1;
        private Panel panel2;
        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonSelect;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridContent;
    }
}