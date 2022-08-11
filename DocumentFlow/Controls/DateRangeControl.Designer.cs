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
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonSelectDateRange = new DocumentFlow.Controls.ToolButton();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerFrom.Calendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerTo.Calendar)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 24);
            this.label2.TabIndex = 17;
            this.label2.Text = "Период с:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimePickerFrom.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.dateTimePickerFrom.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.dateTimePickerFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dateTimePickerFrom.Calendar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimePickerFrom.Calendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimePickerFrom.Calendar.BottomHeight = 6;
            this.dateTimePickerFrom.Calendar.Culture = new System.Globalization.CultureInfo("ru-RU");
            this.dateTimePickerFrom.Calendar.DayNamesColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.dateTimePickerFrom.Calendar.DayNamesFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerFrom.Calendar.DaysColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerFrom.Calendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerFrom.Calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerFrom.Calendar.GridBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimePickerFrom.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None;
            this.dateTimePickerFrom.Calendar.HeaderFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerFrom.Calendar.HeaderHeight = 34;
            this.dateTimePickerFrom.Calendar.HeaderStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dateTimePickerFrom.Calendar.HeadForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerFrom.Calendar.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.dateTimePickerFrom.Calendar.InactiveMonthColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerFrom.Calendar.Iso8601CalenderFormat = false;
            this.dateTimePickerFrom.Calendar.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerFrom.Calendar.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerFrom.Calendar.Name = "monthCalendar";
            // 
            // 
            // 
            this.dateTimePickerFrom.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimePickerFrom.Calendar.NoneButton.AutoSize = true;
            this.dateTimePickerFrom.Calendar.NoneButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerFrom.Calendar.NoneButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerFrom.Calendar.NoneButton.KeepFocusRectangle = false;
            this.dateTimePickerFrom.Calendar.NoneButton.Location = new System.Drawing.Point(57, 0);
            this.dateTimePickerFrom.Calendar.NoneButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimePickerFrom.Calendar.NoneButton.Size = new System.Drawing.Size(72, 6);
            this.dateTimePickerFrom.Calendar.NoneButton.ThemeName = "Metro";
            this.dateTimePickerFrom.Calendar.NoneButton.UseVisualStyle = true;
            this.dateTimePickerFrom.Calendar.ScrollButtonSize = new System.Drawing.Size(24, 24);
            this.dateTimePickerFrom.Calendar.Size = new System.Drawing.Size(129, 130);
            this.dateTimePickerFrom.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.dateTimePickerFrom.Calendar.TabIndex = 0;
            // 
            // 
            // 
            this.dateTimePickerFrom.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimePickerFrom.Calendar.TodayButton.AutoSize = true;
            this.dateTimePickerFrom.Calendar.TodayButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerFrom.Calendar.TodayButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerFrom.Calendar.TodayButton.KeepFocusRectangle = false;
            this.dateTimePickerFrom.Calendar.TodayButton.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerFrom.Calendar.TodayButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimePickerFrom.Calendar.TodayButton.Size = new System.Drawing.Size(57, 6);
            this.dateTimePickerFrom.Calendar.TodayButton.ThemeName = "Metro";
            this.dateTimePickerFrom.Calendar.TodayButton.UseVisualStyle = true;
            this.dateTimePickerFrom.Calendar.WeekFont = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerFrom.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimePickerFrom.CalendarSize = new System.Drawing.Size(189, 176);
            this.dateTimePickerFrom.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dateTimePickerFrom.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerFrom.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerFrom.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFrom.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateTimePickerFrom.DropDownImage = null;
            this.dateTimePickerFrom.DropDownNormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimePickerFrom.DropDownPressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerFrom.DropDownSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(191)))), ((int)(((byte)(237)))));
            this.dateTimePickerFrom.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(61, 0);
            this.dateTimePickerFrom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dateTimePickerFrom.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerFrom.MinValue = new System.DateTime(((long)(0)));
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(140, 24);
            this.dateTimePickerFrom.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.dateTimePickerFrom.TabIndex = 18;
            this.dateTimePickerFrom.Value = new System.DateTime(2019, 6, 9, 16, 1, 37, 456);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(201, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 24);
            this.label3.TabIndex = 19;
            this.label3.Text = "по:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimePickerTo.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.dateTimePickerTo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.dateTimePickerTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dateTimePickerTo.Calendar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.dateTimePickerTo.Calendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimePickerTo.Calendar.BottomHeight = 6;
            this.dateTimePickerTo.Calendar.Culture = new System.Globalization.CultureInfo("ru-RU");
            this.dateTimePickerTo.Calendar.DayNamesColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.dateTimePickerTo.Calendar.DayNamesFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerTo.Calendar.DaysColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerTo.Calendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerTo.Calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerTo.Calendar.GridBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimePickerTo.Calendar.GridLines = Syncfusion.Windows.Forms.Grid.GridBorderStyle.None;
            this.dateTimePickerTo.Calendar.HeaderFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerTo.Calendar.HeaderHeight = 34;
            this.dateTimePickerTo.Calendar.HeaderStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dateTimePickerTo.Calendar.HeadForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerTo.Calendar.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.dateTimePickerTo.Calendar.InactiveMonthColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerTo.Calendar.Iso8601CalenderFormat = false;
            this.dateTimePickerTo.Calendar.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerTo.Calendar.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerTo.Calendar.Name = "monthCalendar";
            // 
            // 
            // 
            this.dateTimePickerTo.Calendar.NoneButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimePickerTo.Calendar.NoneButton.AutoSize = true;
            this.dateTimePickerTo.Calendar.NoneButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerTo.Calendar.NoneButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerTo.Calendar.NoneButton.KeepFocusRectangle = false;
            this.dateTimePickerTo.Calendar.NoneButton.Location = new System.Drawing.Point(57, 0);
            this.dateTimePickerTo.Calendar.NoneButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimePickerTo.Calendar.NoneButton.Size = new System.Drawing.Size(72, 6);
            this.dateTimePickerTo.Calendar.NoneButton.ThemeName = "Metro";
            this.dateTimePickerTo.Calendar.NoneButton.UseVisualStyle = true;
            this.dateTimePickerTo.Calendar.ScrollButtonSize = new System.Drawing.Size(24, 24);
            this.dateTimePickerTo.Calendar.Size = new System.Drawing.Size(129, 130);
            this.dateTimePickerTo.Calendar.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.dateTimePickerTo.Calendar.TabIndex = 0;
            // 
            // 
            // 
            this.dateTimePickerTo.Calendar.TodayButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.dateTimePickerTo.Calendar.TodayButton.AutoSize = true;
            this.dateTimePickerTo.Calendar.TodayButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerTo.Calendar.TodayButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerTo.Calendar.TodayButton.KeepFocusRectangle = false;
            this.dateTimePickerTo.Calendar.TodayButton.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerTo.Calendar.TodayButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimePickerTo.Calendar.TodayButton.Size = new System.Drawing.Size(57, 6);
            this.dateTimePickerTo.Calendar.TodayButton.ThemeName = "Metro";
            this.dateTimePickerTo.Calendar.TodayButton.UseVisualStyle = true;
            this.dateTimePickerTo.Calendar.WeekFont = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerTo.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimePickerTo.CalendarSize = new System.Drawing.Size(189, 176);
            this.dateTimePickerTo.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dateTimePickerTo.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerTo.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerTo.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerTo.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateTimePickerTo.DropDownImage = null;
            this.dateTimePickerTo.DropDownNormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateTimePickerTo.DropDownPressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerTo.DropDownSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(191)))), ((int)(((byte)(237)))));
            this.dateTimePickerTo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTo.Location = new System.Drawing.Point(242, 0);
            this.dateTimePickerTo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dateTimePickerTo.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dateTimePickerTo.MinValue = new System.DateTime(((long)(0)));
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(140, 24);
            this.dateTimePickerTo.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.dateTimePickerTo.TabIndex = 20;
            this.dateTimePickerTo.Value = new System.DateTime(2019, 6, 9, 16, 1, 37, 456);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(382, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(7, 24);
            this.panel3.TabIndex = 21;
            // 
            // buttonSelectDateRange
            // 
            this.buttonSelectDateRange.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.buttonSelectDateRange.BackColor = System.Drawing.Color.White;
            this.buttonSelectDateRange.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.buttonSelectDateRange.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.buttonSelectDateRange.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.buttonSelectDateRange.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.buttonSelectDateRange.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonSelectDateRange.Kind = DocumentFlow.Controls.ToolButtonKind.Select;
            this.buttonSelectDateRange.Location = new System.Drawing.Point(389, 0);
            this.buttonSelectDateRange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSelectDateRange.Name = "buttonSelectDateRange";
            this.buttonSelectDateRange.Size = new System.Drawing.Size(24, 24);
            this.buttonSelectDateRange.TabIndex = 22;
            this.buttonSelectDateRange.Text = "toolButton2";
            this.buttonSelectDateRange.Click += new System.EventHandler(this.ButtonSelectDateRange_Click);
            // 
            // DateRangeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSelectDateRange);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.label2);
            this.Name = "DateRangeControl";
            this.Size = new System.Drawing.Size(413, 24);
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerFrom.Calendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerTo.Calendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePickerTo)).EndInit();
            this.ResumeLayout(false);

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
