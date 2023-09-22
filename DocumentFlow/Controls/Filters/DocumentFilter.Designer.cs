
namespace DocumentFlow.Controls.Filters
{
    partial class DocumentFilter
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
            comboOrg = new Syncfusion.WinForms.ListView.SfComboBox();
            panel1 = new Panel();
            buttonClearOrg = new ToolButton();
            panel2 = new Panel();
            dateRangeControl1 = new DateRangeControl();
            ((System.ComponentModel.ISupportInitialize)comboOrg).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Dock = DockStyle.Left;
            label1.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(4, 3);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(104, 24);
            label1.TabIndex = 2;
            label1.Text = "Организация";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comboOrg
            // 
            comboOrg.Dock = DockStyle.Left;
            comboOrg.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            comboOrg.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            comboOrg.Location = new Point(108, 3);
            comboOrg.Margin = new Padding(4, 3, 4, 3);
            comboOrg.Name = "comboOrg";
            comboOrg.Padding = new Padding(8, 0, 0, 0);
            comboOrg.Size = new Size(265, 24);
            comboOrg.Style.TokenStyle.CloseButtonBackColor = Color.FromArgb(255, 255, 255);
            comboOrg.TabIndex = 11;
            comboOrg.ThemeName = "Metro";
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(373, 3);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(7, 24);
            panel1.TabIndex = 13;
            // 
            // buttonClearOrg
            // 
            buttonClearOrg.BackClickedColor = Color.FromArgb(204, 232, 255);
            buttonClearOrg.BackColor = Color.White;
            buttonClearOrg.BackHoveredColor = Color.FromArgb(229, 243, 255);
            buttonClearOrg.BorderClickedColor = Color.FromArgb(153, 209, 255);
            buttonClearOrg.BorderColor = Color.FromArgb(217, 217, 217);
            buttonClearOrg.BorderHoveredColor = Color.FromArgb(204, 232, 235);
            buttonClearOrg.Dock = DockStyle.Left;
            buttonClearOrg.Kind = Enums.ToolButtonKind.Delete;
            buttonClearOrg.Location = new Point(380, 3);
            buttonClearOrg.Margin = new Padding(4, 3, 4, 3);
            buttonClearOrg.Name = "buttonClearOrg";
            buttonClearOrg.Size = new Size(28, 24);
            buttonClearOrg.TabIndex = 14;
            buttonClearOrg.TabStop = false;
            buttonClearOrg.Text = "toolButton1";
            buttonClearOrg.Click += ButtonClearOrg_Click;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(408, 3);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(7, 24);
            panel2.TabIndex = 15;
            // 
            // dateRangeControl1
            // 
            dateRangeControl1.Dock = DockStyle.Left;
            dateRangeControl1.DateFrom = new DateTime(2022, 1, 1, 0, 0, 0, 0);
            dateRangeControl1.DateFromEnabled = true;
            dateRangeControl1.Location = new Point(415, 3);
            dateRangeControl1.Name = "dateRangeControl1";
            dateRangeControl1.Size = new Size(413, 24);
            dateRangeControl1.TabIndex = 22;
            dateRangeControl1.DateTo = new DateTime(2022, 12, 31, 23, 59, 59, 999);
            dateRangeControl1.DateToEnabled = true;
            dateRangeControl1.DateFromChanged += DateRangeControl1_DateFromChanged;
            dateRangeControl1.DateToChanged += DateRangeControl1_DateToChanged;
            dateRangeControl1.DateFromEnabledChanged += DateRangeControl1_DateFromEnabledChanged;
            dateRangeControl1.DateToEnabledChanged += DateRangeControl1_DateToEnabledChanged;
            // 
            // DocumentFilter
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            Controls.Add(dateRangeControl1);
            Controls.Add(panel2);
            Controls.Add(buttonClearOrg);
            Controls.Add(panel1);
            Controls.Add(comboOrg);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4, 3, 4, 3);
            Name = "DocumentFilter";
            Padding = new Padding(4, 3, 4, 3);
            Size = new Size(1087, 30);
            ((System.ComponentModel.ISupportInitialize)comboOrg).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Syncfusion.WinForms.ListView.SfComboBox comboOrg;
        private Panel panel1;
        private ToolButton buttonClearOrg;
        private Panel panel2;
        private DateRangeControl dateRangeControl1;
    }
}
