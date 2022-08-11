namespace DocumentFlow.Dialogs
{
    partial class SelectDayValueForm
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
            this.calendar = new Syncfusion.WinForms.Input.SfCalendar();
            this.SuspendLayout();
            // 
            // calendar
            // 
            this.calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.calendar.HighlightTodayCell = true;
            this.calendar.Location = new System.Drawing.Point(10, 10);
            this.calendar.MinimumSize = new System.Drawing.Size(196, 196);
            this.calendar.Name = "calendar";
            this.calendar.Size = new System.Drawing.Size(322, 228);
            this.calendar.TabIndex = 0;
            this.calendar.Text = "sfCalendar1";
            this.calendar.CellClick += new System.EventHandler<Syncfusion.WinForms.Input.Events.CalendarCellEventArgs>(this.Calendar_CellClick);
            // 
            // SelectDayValueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(345, 248);
            this.Controls.Add(this.calendar);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectDayValueForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выюор дня";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectDayValueWindow_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.Input.SfCalendar calendar;
    }
}
