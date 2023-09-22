using Syncfusion.Windows.Forms.Tools;

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
            multiSelectionComboBox1 = new Syncfusion.Windows.Forms.Tools.MultiSelectionComboBox();
            ((System.ComponentModel.ISupportInitialize)multiSelectionComboBox1).BeginInit();
            SuspendLayout();
            // 
            // multiSelectionComboBox1
            // 
            multiSelectionComboBox1.AutoSizeMode = Syncfusion.Windows.Forms.Tools.AutoSizeModes.None;
            multiSelectionComboBox1.BackColor = Color.FromArgb(255, 255, 255);
            multiSelectionComboBox1.BeforeTouchSize = new Size(361, 26);
            multiSelectionComboBox1.ButtonStyle = Syncfusion.Windows.Forms.ButtonAppearance.Office2016Colorful;
            multiSelectionComboBox1.DataSource = resources.GetObject("multiSelectionComboBox1.DataSource");
            multiSelectionComboBox1.Dock = DockStyle.Left;
            multiSelectionComboBox1.FlatBorderColor = Color.FromArgb(197, 197, 197);
            multiSelectionComboBox1.FlatStyle = FlatStyle.Flat;
            multiSelectionComboBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            multiSelectionComboBox1.GroupHeaderBackColor = Color.FromArgb(197, 197, 197);
            multiSelectionComboBox1.GroupHeaderForeColor = Color.FromArgb(68, 68, 68);
            multiSelectionComboBox1.Location = new Point(75, 0);
            multiSelectionComboBox1.MetroColor = Color.FromArgb(197, 197, 197);
            multiSelectionComboBox1.Name = "multiSelectionComboBox1";
            multiSelectionComboBox1.SelectionStart = 23;
            multiSelectionComboBox1.Size = new Size(361, 26);
            multiSelectionComboBox1.Style = Syncfusion.Windows.Forms.Tools.MultiSelectionComboBoxStyle.Office2016Colorful;
            multiSelectionComboBox1.TabIndex = 1;
            multiSelectionComboBox1.ThemeName = "Office2016Colorful";
            multiSelectionComboBox1.TickColor = Color.FromArgb(150, 150, 150);
            multiSelectionComboBox1.UseVisualStyle = true;
            multiSelectionComboBox1.VisualItemBackColor = Color.FromArgb(225, 225, 225);
            multiSelectionComboBox1.VisualItemSelectionColor = Color.FromArgb(155, 202, 239);
            multiSelectionComboBox1.VisualItemSelectionForeColor = Color.FromArgb(255, 255, 255);
            multiSelectionComboBox1.VisualItemsCollectionChanged += MultiSelectionComboBox1_VisualItemsCollectionChanged;
            // 
            // DfMultiSelectionComboBox
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(multiSelectionComboBox1);
            Name = "DfMultiSelectionComboBox";
            Controls.SetChildIndex(multiSelectionComboBox1, 0);
            ((System.ComponentModel.ISupportInitialize)multiSelectionComboBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.MultiSelectionComboBox multiSelectionComboBox1;
    }
}
