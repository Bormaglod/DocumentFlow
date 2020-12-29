
namespace DocumentFlow
{
    partial class ScanningWindow
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
            this.components = new System.ComponentModel.Container();
            Syncfusion.WinForms.DataGrid.GridImageColumn gridImageColumn1 = new Syncfusion.WinForms.DataGrid.GridImageColumn();
            this.buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.buttonOk = new Syncfusion.WinForms.Controls.SfButton();
            this.gridImages = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.buttonAdd = new Syncfusion.WinForms.Controls.SfButton();
            this.buttonDelete = new Syncfusion.WinForms.Controls.SfButton();
            this.twain32 = new Saraff.Twain.Twain32(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridImages)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleName = "Button";
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(261, 500);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 28);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отменить";
            // 
            // buttonOk
            // 
            this.buttonOk.AccessibleName = "Button";
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(261, 466);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(112, 28);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Сохранить";
            this.buttonOk.UseVisualStyleBackColor = false;
            // 
            // gridImages
            // 
            this.gridImages.AccessibleName = "Table";
            this.gridImages.AllowEditing = false;
            this.gridImages.AllowFiltering = true;
            this.gridImages.AllowResizingColumns = true;
            this.gridImages.AllowSorting = false;
            this.gridImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridImages.AutoGenerateColumns = false;
            this.gridImages.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
            this.gridImages.BackColor = System.Drawing.SystemColors.Window;
            gridImageColumn1.AllowEditing = false;
            gridImageColumn1.AllowGrouping = false;
            gridImageColumn1.AllowResizing = true;
            gridImageColumn1.AllowSorting = false;
            gridImageColumn1.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
            gridImageColumn1.HeaderText = "Страницы";
            gridImageColumn1.MappingName = "Image";
            this.gridImages.Columns.Add(gridImageColumn1);
            this.gridImages.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridImages.Location = new System.Drawing.Point(12, 12);
            this.gridImages.Name = "gridImages";
            this.gridImages.RowHeight = 100;
            this.gridImages.ShowBusyIndicator = true;
            this.gridImages.Size = new System.Drawing.Size(242, 516);
            this.gridImages.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.gridImages.Style.GroupDropAreaStyle.BackColor = System.Drawing.SystemColors.Window;
            this.gridImages.Style.TableSummaryRowStyle.Font.Facename = "Segoe UI";
            this.gridImages.Style.TableSummaryRowStyle.Font.Size = 9F;
            this.gridImages.TabIndex = 13;
            // 
            // buttonAdd
            // 
            this.buttonAdd.AccessibleName = "Button";
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdd.Location = new System.Drawing.Point(261, 12);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(112, 28);
            this.buttonAdd.TabIndex = 14;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.AccessibleName = "Button";
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDelete.Location = new System.Drawing.Point(261, 46);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(112, 28);
            this.buttonDelete.TabIndex = 15;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = false;
            // 
            // twain32
            // 
            this.twain32.AppProductName = "Saraff.Twain.NET";
            this.twain32.Country = Saraff.Twain.TwCountry.USSR;
            this.twain32.Parent = null;
            this.twain32.AcquireCompleted += new System.EventHandler(this.twain32_AcquireCompleted);
            // 
            // ScanningWindow
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(385, 540);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.gridImages);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScanningWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Сканирование документов";
            ((System.ComponentModel.ISupportInitialize)(this.gridImages)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridImages;
        private Syncfusion.WinForms.Controls.SfButton buttonAdd;
        private Syncfusion.WinForms.Controls.SfButton buttonDelete;
        private Saraff.Twain.Twain32 twain32;
    }
}
