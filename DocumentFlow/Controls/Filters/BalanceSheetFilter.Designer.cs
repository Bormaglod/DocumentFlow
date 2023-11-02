namespace DocumentFlow.Controls.Filters
{
    partial class BalanceSheetFilter
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
            label1 = new Label();
            comboView = new Syncfusion.WinForms.ListView.SfComboBox();
            panel2 = new Panel();
            dateRangeControl1 = new DateRangeControl();
            label2 = new Label();
            checkAmount = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            checkSumma = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            panel1 = new Panel();
            checkGivingMaterial = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            ((System.ComponentModel.ISupportInitialize)comboView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)checkAmount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)checkSumma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)checkGivingMaterial).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Dock = DockStyle.Left;
            label1.Location = new Point(4, 3);
            label1.Name = "label1";
            label1.Size = new Size(120, 24);
            label1.TabIndex = 0;
            label1.Text = "Вид номенклатуры";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comboView
            // 
            comboView.Dock = DockStyle.Left;
            comboView.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            comboView.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            comboView.Location = new Point(124, 3);
            comboView.Name = "comboView";
            comboView.Size = new Size(206, 24);
            comboView.Style.TokenStyle.CloseButtonBackColor = Color.FromArgb(255, 255, 255);
            comboView.TabIndex = 2;
            comboView.SelectedValueChanged += ComboView_SelectedValueChanged;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(330, 3);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(7, 24);
            panel2.TabIndex = 16;
            // 
            // dateRangeControl1
            // 
            dateRangeControl1.DateFrom = new DateTime(2022, 1, 1, 0, 0, 0, 0);
            dateRangeControl1.DateFromEnabled = true;
            dateRangeControl1.DateTo = new DateTime(2022, 12, 31, 23, 59, 59, 999);
            dateRangeControl1.DateToEnabled = true;
            dateRangeControl1.Dock = DockStyle.Left;
            dateRangeControl1.Location = new Point(683, 3);
            dateRangeControl1.Name = "dateRangeControl1";
            dateRangeControl1.Size = new Size(413, 24);
            dateRangeControl1.TabIndex = 17;
            dateRangeControl1.DateFromEnabledChanged += DateRangeControl1_DateFromEnabledChanged;
            dateRangeControl1.DateToEnabledChanged += DateRangeControl1_DateToEnabledChanged;
            dateRangeControl1.DateFromChanged += DateRangeControl1_DateFromChanged;
            dateRangeControl1.DateToChanged += DateRangeControl1_DateToChanged;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Left;
            label2.Location = new Point(337, 3);
            label2.Name = "label2";
            label2.Size = new Size(82, 24);
            label2.TabIndex = 18;
            label2.Text = "Показывать:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // checkAmount
            // 
            checkAmount.BeforeTouchSize = new Size(90, 24);
            checkAmount.Checked = true;
            checkAmount.CheckState = CheckState.Checked;
            checkAmount.Dock = DockStyle.Left;
            checkAmount.ForeColor = SystemColors.ControlText;
            checkAmount.Location = new Point(419, 3);
            checkAmount.Name = "checkAmount";
            checkAmount.Size = new Size(90, 24);
            checkAmount.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            checkAmount.TabIndex = 19;
            checkAmount.Text = "Количество";
            checkAmount.ThemeName = "Metro";
            checkAmount.ThemeStyle.BackColor = Color.FromArgb(0, 0, 0, 0);
            checkAmount.CheckedChanged += CheckAmount_CheckedChanged;
            // 
            // checkSumma
            // 
            checkSumma.BeforeTouchSize = new Size(62, 24);
            checkSumma.Dock = DockStyle.Left;
            checkSumma.ForeColor = SystemColors.ControlText;
            checkSumma.Location = new Point(509, 3);
            checkSumma.Name = "checkSumma";
            checkSumma.Size = new Size(62, 24);
            checkSumma.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            checkSumma.TabIndex = 20;
            checkSumma.Text = "Сумму";
            checkSumma.ThemeName = "Metro";
            checkSumma.CheckedChanged += CheckSumma_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(676, 3);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(7, 24);
            panel1.TabIndex = 21;
            // 
            // checkGivingMaterial
            // 
            checkGivingMaterial.BeforeTouchSize = new Size(105, 24);
            checkGivingMaterial.Checked = true;
            checkGivingMaterial.CheckState = CheckState.Checked;
            checkGivingMaterial.Dock = DockStyle.Left;
            checkGivingMaterial.ForeColor = SystemColors.ControlText;
            checkGivingMaterial.Location = new Point(571, 3);
            checkGivingMaterial.Name = "checkGivingMaterial";
            checkGivingMaterial.Size = new Size(105, 24);
            checkGivingMaterial.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            checkGivingMaterial.TabIndex = 22;
            checkGivingMaterial.Text = "Дав. материал";
            checkGivingMaterial.ThemeName = "Metro";
            checkGivingMaterial.CheckedChanged += CheckGivingMaterial_CheckedChanged;
            // 
            // BalanceSheetFilter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dateRangeControl1);
            Controls.Add(panel1);
            Controls.Add(checkGivingMaterial);
            Controls.Add(checkSumma);
            Controls.Add(checkAmount);
            Controls.Add(label2);
            Controls.Add(panel2);
            Controls.Add(comboView);
            Controls.Add(label1);
            Name = "BalanceSheetFilter";
            Padding = new Padding(4, 3, 4, 3);
            Size = new Size(1153, 30);
            ((System.ComponentModel.ISupportInitialize)comboView).EndInit();
            ((System.ComponentModel.ISupportInitialize)checkAmount).EndInit();
            ((System.ComponentModel.ISupportInitialize)checkSumma).EndInit();
            ((System.ComponentModel.ISupportInitialize)checkGivingMaterial).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Syncfusion.WinForms.ListView.SfComboBox comboView;
        private Panel panel2;
        private DateRangeControl dateRangeControl1;
        private Label label2;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv checkAmount;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv checkSumma;
        private Panel panel1;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv checkGivingMaterial;
    }
}
