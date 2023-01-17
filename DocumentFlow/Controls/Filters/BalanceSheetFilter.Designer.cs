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
            this.label1 = new System.Windows.Forms.Label();
            this.comboView = new Syncfusion.WinForms.ListView.SfComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dateRangeControl1 = new DocumentFlow.Controls.DateRangeControl();
            this.label2 = new System.Windows.Forms.Label();
            this.checkAmount = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.checkSumma = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.comboView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSumma)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Вид номенклатуры";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboView
            // 
            this.comboView.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboView.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.comboView.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboView.Location = new System.Drawing.Point(124, 3);
            this.comboView.Name = "comboView";
            this.comboView.Size = new System.Drawing.Size(206, 24);
            this.comboView.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboView.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(330, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(7, 24);
            this.panel2.TabIndex = 16;
            // 
            // dateRangeControl1
            // 
            this.dateRangeControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateRangeControl1.From = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            this.dateRangeControl1.FromEnabled = true;
            this.dateRangeControl1.Location = new System.Drawing.Point(579, 3);
            this.dateRangeControl1.Name = "dateRangeControl1";
            this.dateRangeControl1.Size = new System.Drawing.Size(413, 24);
            this.dateRangeControl1.TabIndex = 17;
            this.dateRangeControl1.To = new System.DateTime(2022, 12, 31, 23, 59, 59, 999);
            this.dateRangeControl1.ToEnabled = true;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(337, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 18;
            this.label2.Text = "Показывать:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkAmount
            // 
            this.checkAmount.BeforeTouchSize = new System.Drawing.Size(91, 24);
            this.checkAmount.Checked = true;
            this.checkAmount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAmount.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkAmount.Location = new System.Drawing.Point(419, 3);
            this.checkAmount.Name = "checkAmount";
            this.checkAmount.Size = new System.Drawing.Size(91, 24);
            this.checkAmount.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            this.checkAmount.TabIndex = 19;
            this.checkAmount.Text = "Количество";
            this.checkAmount.ThemeName = "Metro";
            this.checkAmount.ThemeStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // checkSumma
            // 
            this.checkSumma.BeforeTouchSize = new System.Drawing.Size(62, 24);
            this.checkSumma.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkSumma.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkSumma.Location = new System.Drawing.Point(510, 3);
            this.checkSumma.Name = "checkSumma";
            this.checkSumma.Size = new System.Drawing.Size(62, 24);
            this.checkSumma.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            this.checkSumma.TabIndex = 20;
            this.checkSumma.Text = "Сумму";
            this.checkSumma.ThemeName = "Metro";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(572, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(7, 24);
            this.panel1.TabIndex = 21;
            // 
            // BalanceSheetFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateRangeControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkSumma);
            this.Controls.Add(this.checkAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.comboView);
            this.Controls.Add(this.label1);
            this.Name = "BalanceSheetFilter";
            this.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Size = new System.Drawing.Size(1153, 30);
            ((System.ComponentModel.ISupportInitialize)(this.comboView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSumma)).EndInit();
            this.ResumeLayout(false);

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
    }
}
