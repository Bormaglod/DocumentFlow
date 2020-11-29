namespace DocumentFlow
{
    partial class SelectDayValueWindow
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
            this.sfCalendar1 = new Syncfusion.WinForms.Input.SfCalendar();
            this.SuspendLayout();
            // 
            // sfCalendar1
            // 
            this.sfCalendar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfCalendar1.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.sfCalendar1.HighlightTodayCell = true;
            this.sfCalendar1.Location = new System.Drawing.Point(10, 10);
            this.sfCalendar1.MinimumSize = new System.Drawing.Size(196, 196);
            this.sfCalendar1.Name = "sfCalendar1";
            this.sfCalendar1.Size = new System.Drawing.Size(322, 228);
            this.sfCalendar1.TabIndex = 0;
            this.sfCalendar1.Text = "sfCalendar1";
            this.sfCalendar1.SelectionChanging += new Syncfusion.WinForms.Input.Events.SelectionChangingEventHandler(this.sfCalendar1_SelectionChanging);
            // 
            // SelectDayValueWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(345, 248);
            this.Controls.Add(this.sfCalendar1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectDayValueWindow";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выюор дня";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectDayValueWindow_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.Input.SfCalendar sfCalendar1;
    }
}
