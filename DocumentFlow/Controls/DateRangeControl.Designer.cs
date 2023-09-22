namespace DocumentFlow.Controls
{
    partial class DateRangeControl
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
            components = new System.ComponentModel.Container();
            label2 = new Label();
            dateTimePickerFrom = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            label3 = new Label();
            dateTimePickerTo = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            panel3 = new Panel();
            buttonSelectDateRange = new ToolButton();
            ((System.ComponentModel.ISupportInitialize)dateTimePickerFrom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateTimePickerFrom.Calendar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateTimePickerTo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateTimePickerTo.Calendar).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.Dock = DockStyle.Left;
            label2.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(0, 0);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(61, 24);
            label2.TabIndex = 17;
            label2.Text = "Период с:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dateTimePickerFrom
            // 
            dateTimePickerFrom.BackColor = Color.FromArgb(255, 255, 255);
            dateTimePickerFrom.Border3DStyle = Border3DStyle.Flat;
            dateTimePickerFrom.BorderColor = Color.FromArgb(171, 171, 171);
            dateTimePickerFrom.BorderStyle = BorderStyle.FixedSingle;
            // 
            // 
            // 
            dateTimePickerFrom.Calendar.BorderColor = Color.FromArgb(209, 211, 212);
            dateTimePickerFrom.Calendar.BorderStyle = BorderStyle.FixedSingle;
            dateTimePickerFrom.Calendar.BottomHeight = 6;
            dateTimePickerFrom.Calendar.Culture = new System.Globalization.CultureInfo("ru-RU");
            dateTimePickerFrom.Calendar.DayNamesColor = Color.FromArgb(102, 102, 102);
            dateTimePickerFrom.Calendar.DayNamesFont = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimePickerFrom.Calendar.DaysColor = Color.FromArgb(68, 68, 68);
            dateTimePickerFrom.Calendar.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimePickerFrom.Calendar.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerFrom.Calendar.GridBackColor = Color.FromArgb(255, 255, 255);
            dateTimePickerFrom.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None;
            dateTimePickerFrom.Calendar.HeaderFont = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            dateTimePickerFrom.Calendar.HeaderStartColor = Color.FromArgb(240, 240, 240);
            dateTimePickerFrom.Calendar.HeadForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerFrom.Calendar.HighlightColor = Color.FromArgb(205, 230, 247);
            dateTimePickerFrom.Calendar.InactiveMonthColor = Color.FromArgb(68, 68, 68);
            dateTimePickerFrom.Calendar.Iso8601CalenderFormat = false;
            dateTimePickerFrom.Calendar.Location = new Point(0, 0);
            dateTimePickerFrom.Calendar.MetroColor = Color.FromArgb(22, 165, 220);
            dateTimePickerFrom.Calendar.Name = "monthCalendar";
            // 
            // 
            // 
            dateTimePickerFrom.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            dateTimePickerFrom.Calendar.NoneButton.AutoSize = true;
            dateTimePickerFrom.Calendar.NoneButton.BackColor = Color.FromArgb(22, 165, 220);
            dateTimePickerFrom.Calendar.NoneButton.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerFrom.Calendar.NoneButton.KeepFocusRectangle = false;
            dateTimePickerFrom.Calendar.NoneButton.Location = new Point(57, 0);
            dateTimePickerFrom.Calendar.NoneButton.MetroColor = Color.FromArgb(255, 255, 255);
            dateTimePickerFrom.Calendar.NoneButton.Size = new Size(72, 6);
            dateTimePickerFrom.Calendar.NoneButton.ThemeName = "Metro";
            dateTimePickerFrom.Calendar.NoneButton.UseVisualStyle = true;
            dateTimePickerFrom.Calendar.ScrollButtonSize = new Size(24, 24);
            dateTimePickerFrom.Calendar.Size = new Size(129, 130);
            dateTimePickerFrom.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            dateTimePickerFrom.Calendar.TabIndex = 0;
            // 
            // 
            // 
            dateTimePickerFrom.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            dateTimePickerFrom.Calendar.TodayButton.AutoSize = true;
            dateTimePickerFrom.Calendar.TodayButton.BackColor = Color.FromArgb(22, 165, 220);
            dateTimePickerFrom.Calendar.TodayButton.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerFrom.Calendar.TodayButton.KeepFocusRectangle = false;
            dateTimePickerFrom.Calendar.TodayButton.Location = new Point(0, 0);
            dateTimePickerFrom.Calendar.TodayButton.MetroColor = Color.FromArgb(255, 255, 255);
            dateTimePickerFrom.Calendar.TodayButton.Size = new Size(57, 6);
            dateTimePickerFrom.Calendar.TodayButton.ThemeName = "Metro";
            dateTimePickerFrom.Calendar.TodayButton.UseVisualStyle = true;
            dateTimePickerFrom.Calendar.WeekFont = new Font("Verdana", 8F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimePickerFrom.CalendarMonthBackground = Color.FromArgb(255, 255, 255);
            dateTimePickerFrom.CalendarSize = new Size(189, 176);
            dateTimePickerFrom.CalendarTitleBackColor = Color.FromArgb(240, 240, 240);
            dateTimePickerFrom.CalendarTitleForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerFrom.CalendarTrailingForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerFrom.CustomFormat = "dd.MM.yyyy";
            dateTimePickerFrom.Dock = DockStyle.Left;
            dateTimePickerFrom.DropDownImage = null;
            dateTimePickerFrom.DropDownNormalColor = Color.FromArgb(255, 255, 255);
            dateTimePickerFrom.DropDownPressedColor = Color.FromArgb(22, 165, 220);
            dateTimePickerFrom.DropDownSelectedColor = Color.FromArgb(71, 191, 237);
            dateTimePickerFrom.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimePickerFrom.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerFrom.Format = DateTimePickerFormat.Custom;
            dateTimePickerFrom.Location = new Point(61, 0);
            dateTimePickerFrom.Margin = new Padding(4, 3, 4, 3);
            dateTimePickerFrom.MetroColor = Color.FromArgb(22, 165, 220);
            dateTimePickerFrom.MinValue = new DateTime(0L);
            dateTimePickerFrom.Name = "dateTimePickerFrom";
            dateTimePickerFrom.Size = new Size(140, 24);
            dateTimePickerFrom.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            dateTimePickerFrom.TabIndex = 18;
            dateTimePickerFrom.Value = new DateTime(2019, 6, 9, 16, 1, 37, 456);
            dateTimePickerFrom.CheckBoxCheckedChanged += DateTimePickerFrom_CheckBoxCheckedChanged;
            dateTimePickerFrom.ValueChanged += DateTimePickerFrom_ValueChanged;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Left;
            label3.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(201, 0);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(41, 24);
            label3.TabIndex = 19;
            label3.Text = "по:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dateTimePickerTo
            // 
            dateTimePickerTo.BackColor = Color.FromArgb(255, 255, 255);
            dateTimePickerTo.Border3DStyle = Border3DStyle.Flat;
            dateTimePickerTo.BorderColor = Color.FromArgb(171, 171, 171);
            dateTimePickerTo.BorderStyle = BorderStyle.FixedSingle;
            // 
            // 
            // 
            dateTimePickerTo.Calendar.BorderColor = Color.FromArgb(209, 211, 212);
            dateTimePickerTo.Calendar.BorderStyle = BorderStyle.FixedSingle;
            dateTimePickerTo.Calendar.BottomHeight = 6;
            dateTimePickerTo.Calendar.Culture = new System.Globalization.CultureInfo("ru-RU");
            dateTimePickerTo.Calendar.DayNamesColor = Color.FromArgb(102, 102, 102);
            dateTimePickerTo.Calendar.DayNamesFont = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimePickerTo.Calendar.DaysColor = Color.FromArgb(68, 68, 68);
            dateTimePickerTo.Calendar.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimePickerTo.Calendar.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerTo.Calendar.GridBackColor = Color.FromArgb(255, 255, 255);
            dateTimePickerTo.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None;
            dateTimePickerTo.Calendar.HeaderFont = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            dateTimePickerTo.Calendar.HeaderStartColor = Color.FromArgb(240, 240, 240);
            dateTimePickerTo.Calendar.HeadForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerTo.Calendar.HighlightColor = Color.FromArgb(205, 230, 247);
            dateTimePickerTo.Calendar.InactiveMonthColor = Color.FromArgb(68, 68, 68);
            dateTimePickerTo.Calendar.Iso8601CalenderFormat = false;
            dateTimePickerTo.Calendar.Location = new Point(0, 0);
            dateTimePickerTo.Calendar.MetroColor = Color.FromArgb(22, 165, 220);
            dateTimePickerTo.Calendar.Name = "monthCalendar";
            // 
            // 
            // 
            dateTimePickerTo.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            dateTimePickerTo.Calendar.NoneButton.AutoSize = true;
            dateTimePickerTo.Calendar.NoneButton.BackColor = Color.FromArgb(22, 165, 220);
            dateTimePickerTo.Calendar.NoneButton.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerTo.Calendar.NoneButton.KeepFocusRectangle = false;
            dateTimePickerTo.Calendar.NoneButton.Location = new Point(57, 0);
            dateTimePickerTo.Calendar.NoneButton.MetroColor = Color.FromArgb(255, 255, 255);
            dateTimePickerTo.Calendar.NoneButton.Size = new Size(72, 6);
            dateTimePickerTo.Calendar.NoneButton.ThemeName = "Metro";
            dateTimePickerTo.Calendar.NoneButton.UseVisualStyle = true;
            dateTimePickerTo.Calendar.ScrollButtonSize = new Size(24, 24);
            dateTimePickerTo.Calendar.Size = new Size(129, 130);
            dateTimePickerTo.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            dateTimePickerTo.Calendar.TabIndex = 0;
            // 
            // 
            // 
            dateTimePickerTo.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            dateTimePickerTo.Calendar.TodayButton.AutoSize = true;
            dateTimePickerTo.Calendar.TodayButton.BackColor = Color.FromArgb(22, 165, 220);
            dateTimePickerTo.Calendar.TodayButton.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerTo.Calendar.TodayButton.KeepFocusRectangle = false;
            dateTimePickerTo.Calendar.TodayButton.Location = new Point(0, 0);
            dateTimePickerTo.Calendar.TodayButton.MetroColor = Color.FromArgb(255, 255, 255);
            dateTimePickerTo.Calendar.TodayButton.Size = new Size(57, 6);
            dateTimePickerTo.Calendar.TodayButton.ThemeName = "Metro";
            dateTimePickerTo.Calendar.TodayButton.UseVisualStyle = true;
            dateTimePickerTo.Calendar.WeekFont = new Font("Verdana", 8F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimePickerTo.CalendarMonthBackground = Color.FromArgb(255, 255, 255);
            dateTimePickerTo.CalendarSize = new Size(189, 176);
            dateTimePickerTo.CalendarTitleBackColor = Color.FromArgb(240, 240, 240);
            dateTimePickerTo.CalendarTitleForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerTo.CalendarTrailingForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerTo.CustomFormat = "dd.MM.yyyy";
            dateTimePickerTo.Dock = DockStyle.Left;
            dateTimePickerTo.DropDownImage = null;
            dateTimePickerTo.DropDownNormalColor = Color.FromArgb(255, 255, 255);
            dateTimePickerTo.DropDownPressedColor = Color.FromArgb(22, 165, 220);
            dateTimePickerTo.DropDownSelectedColor = Color.FromArgb(71, 191, 237);
            dateTimePickerTo.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimePickerTo.ForeColor = Color.FromArgb(68, 68, 68);
            dateTimePickerTo.Format = DateTimePickerFormat.Custom;
            dateTimePickerTo.Location = new Point(242, 0);
            dateTimePickerTo.Margin = new Padding(4, 3, 4, 3);
            dateTimePickerTo.MetroColor = Color.FromArgb(22, 165, 220);
            dateTimePickerTo.MinValue = new DateTime(0L);
            dateTimePickerTo.Name = "dateTimePickerTo";
            dateTimePickerTo.Size = new Size(140, 24);
            dateTimePickerTo.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            dateTimePickerTo.TabIndex = 20;
            dateTimePickerTo.Value = new DateTime(2019, 6, 9, 16, 1, 37, 456);
            dateTimePickerTo.CheckBoxCheckedChanged += DateTimePickerTo_CheckBoxCheckedChanged;
            dateTimePickerTo.ValueChanged += DateTimePickerTo_ValueChanged;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(382, 0);
            panel3.Margin = new Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(7, 24);
            panel3.TabIndex = 21;
            // 
            // buttonSelectDateRange
            // 
            buttonSelectDateRange.BackClickedColor = Color.FromArgb(204, 232, 255);
            buttonSelectDateRange.BackColor = Color.White;
            buttonSelectDateRange.BackHoveredColor = Color.FromArgb(229, 243, 255);
            buttonSelectDateRange.BorderClickedColor = Color.FromArgb(153, 209, 255);
            buttonSelectDateRange.BorderColor = Color.FromArgb(217, 217, 217);
            buttonSelectDateRange.BorderHoveredColor = Color.FromArgb(204, 232, 235);
            buttonSelectDateRange.Dock = DockStyle.Left;
            buttonSelectDateRange.Kind = Enums.ToolButtonKind.Select;
            buttonSelectDateRange.Location = new Point(389, 0);
            buttonSelectDateRange.Margin = new Padding(4, 3, 4, 3);
            buttonSelectDateRange.Name = "buttonSelectDateRange";
            buttonSelectDateRange.Size = new Size(24, 24);
            buttonSelectDateRange.TabIndex = 22;
            buttonSelectDateRange.TabStop = false;
            buttonSelectDateRange.Text = "toolButton2";
            buttonSelectDateRange.Click += ButtonSelectDateRange_Click;
            // 
            // DateRangeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonSelectDateRange);
            Controls.Add(panel3);
            Controls.Add(dateTimePickerTo);
            Controls.Add(label3);
            Controls.Add(dateTimePickerFrom);
            Controls.Add(label2);
            Name = "DateRangeControl";
            Size = new Size(413, 24);
            ((System.ComponentModel.ISupportInitialize)dateTimePickerFrom.Calendar).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateTimePickerFrom).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateTimePickerTo.Calendar).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateTimePickerTo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label2;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dateTimePickerFrom;
        private Label label3;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dateTimePickerTo;
        private Panel panel3;
        private ToolButton buttonSelectDateRange;
    }
}
