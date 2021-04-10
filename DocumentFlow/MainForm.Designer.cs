
namespace DocumentFlow
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusBarAdv1 = new Syncfusion.Windows.Forms.Tools.StatusBarAdv();
            this.statusBarAdvPanel1 = new Syncfusion.Windows.Forms.Tools.StatusBarAdvPanel();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015BlueTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdv1)).BeginInit();
            this.statusBarAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdvPanel1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBarAdv1
            // 
            this.statusBarAdv1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.statusBarAdv1.BeforeTouchSize = new System.Drawing.Size(800, 26);
            this.statusBarAdv1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.statusBarAdv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBarAdv1.Controls.Add(this.statusBarAdvPanel1);
            this.statusBarAdv1.CustomLayoutBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.statusBarAdv1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBarAdv1.ForeColor = System.Drawing.Color.White;
            this.statusBarAdv1.Location = new System.Drawing.Point(0, 424);
            this.statusBarAdv1.Name = "statusBarAdv1";
            this.statusBarAdv1.Padding = new System.Windows.Forms.Padding(3);
            this.statusBarAdv1.Size = new System.Drawing.Size(800, 26);
            this.statusBarAdv1.SizingGrip = false;
            this.statusBarAdv1.Spacing = new System.Drawing.Size(2, 2);
            this.statusBarAdv1.Style = Syncfusion.Windows.Forms.Tools.StatusbarStyle.Metro;
            this.statusBarAdv1.TabIndex = 0;
            this.statusBarAdv1.ThemeName = "Metro";
            // 
            // statusBarAdvPanel1
            // 
            this.statusBarAdvPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.statusBarAdvPanel1.BeforeTouchSize = new System.Drawing.Size(117, 20);
            this.statusBarAdvPanel1.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.statusBarAdvPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.statusBarAdvPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBarAdvPanel1.ForeColor = System.Drawing.Color.White;
            this.statusBarAdvPanel1.Location = new System.Drawing.Point(0, 2);
            this.statusBarAdvPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.statusBarAdvPanel1.Name = "statusBarAdvPanel1";
            this.statusBarAdvPanel1.Size = new System.Drawing.Size(117, 20);
            this.statusBarAdvPanel1.SizeToContent = true;
            this.statusBarAdvPanel1.TabIndex = 0;
            this.statusBarAdvPanel1.Text = "statusBarAdvPanel1";
            // 
            // dockPanel1
            // 
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingMdi;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(800, 424);
            this.dockPanel1.TabIndex = 1;
            this.dockPanel1.Theme = this.vS2015BlueTheme1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.statusBarAdv1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "DocumentFlow (ООО \"Автоком\")";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdv1)).EndInit();
            this.statusBarAdv1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdvPanel1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.StatusBarAdv statusBarAdv1;
        private Syncfusion.Windows.Forms.Tools.StatusBarAdvPanel statusBarAdvPanel1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme vS2015BlueTheme1;
    }
}

