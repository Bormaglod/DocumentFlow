namespace DocumentFlow.Controls.Editors
{
    partial class DfMultiSelectionComboBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DfMultiSelectionComboBox));
            this.label1 = new System.Windows.Forms.Label();
            this.multiSelectionComboBox1 = new Syncfusion.Windows.Forms.Tools.MultiSelectionComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.multiSelectionComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Text";
            // 
            // multiSelectionComboBox1
            // 
            this.multiSelectionComboBox1.AutoSizeMode = Syncfusion.Windows.Forms.Tools.AutoSizeModes.None;
            this.multiSelectionComboBox1.BackColor = System.Drawing.Color.White;
            this.multiSelectionComboBox1.BeforeTouchSize = new System.Drawing.Size(500, 26);
            this.multiSelectionComboBox1.ButtonStyle = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.multiSelectionComboBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.multiSelectionComboBox1.FlatBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.multiSelectionComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.multiSelectionComboBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.multiSelectionComboBox1.Location = new System.Drawing.Point(117, 0);
            this.multiSelectionComboBox1.Name = "multiSelectionComboBox1";
            this.multiSelectionComboBox1.Size = new System.Drawing.Size(500, 26);
            this.multiSelectionComboBox1.TabIndex = 2;
            this.multiSelectionComboBox1.ThemeName = "Metro";
            this.multiSelectionComboBox1.UseVisualStyle = true;
            this.multiSelectionComboBox1.SelectedItemCollectionChanged += new Syncfusion.Windows.Forms.Tools.SelectedItemCollectionChangedHandler(this.MultiSelectionComboBox1_SelectedItemCollectionChanged);
            // 
            // DfMultiSelectionComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.Controls.Add(this.multiSelectionComboBox1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(0, 32);
            this.Name = "DfMultiSelectionComboBox";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.Size = new System.Drawing.Size(858, 32);
            ((System.ComponentModel.ISupportInitialize)(this.multiSelectionComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.MultiSelectionComboBox multiSelectionComboBox1;
    }
}
