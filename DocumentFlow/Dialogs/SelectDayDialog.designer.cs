namespace DocumentFlow.Dialogs
{
    partial class SelectDayDialog
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
            calendar = new Syncfusion.WinForms.Input.SfCalendar();
            SuspendLayout();
            // 
            // calendar
            // 
            calendar.Dock = DockStyle.Fill;
            calendar.FirstDayOfWeek = DayOfWeek.Monday;
            calendar.HighlightTodayCell = true;
            calendar.Location = new Point(10, 10);
            calendar.MinimumSize = new Size(196, 196);
            calendar.Name = "calendar";
            calendar.Size = new Size(322, 228);
            calendar.TabIndex = 0;
            calendar.Text = "sfCalendar1";
            calendar.CellClick += Calendar_CellClick;
            // 
            // SelectDayValueForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(345, 248);
            Controls.Add(calendar);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectDayValueForm";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Выюор дня";
            KeyDown += SelectDayValueWindow_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.WinForms.Input.SfCalendar calendar;
    }
}
