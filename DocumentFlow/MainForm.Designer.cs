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
            components = new System.ComponentModel.Container();
            timerDatabaseListen = new System.Windows.Forms.Timer(components);
            dockingManager = new Syncfusion.Windows.Forms.Tools.DockingManager(components);
            timerCheckListener = new System.Windows.Forms.Timer(components);
            statusStripEx1 = new Syncfusion.Windows.Forms.Tools.StatusStripEx();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripProgressBar1 = new ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)dockingManager).BeginInit();
            statusStripEx1.SuspendLayout();
            SuspendLayout();
            // 
            // timerDatabaseListen
            // 
            timerDatabaseListen.Tick += TimerDatabaseListen_Tick;
            // 
            // dockingManager
            // 
            dockingManager.AnimateAutoHiddenWindow = true;
            dockingManager.AutoHideTabForeColor = Color.Empty;
            dockingManager.CloseTabOnMiddleClick = false;
            dockingManager.DockBehavior = Syncfusion.Windows.Forms.Tools.DockBehavior.VS2010;
            dockingManager.DragProviderStyle = Syncfusion.Windows.Forms.Tools.DragProviderStyle.Office2016Colorful;
            dockingManager.EnableDocumentMode = true;
            dockingManager.HostControl = this;
            dockingManager.MetroButtonColor = Color.FromArgb(255, 255, 255);
            dockingManager.MetroColor = Color.FromArgb(17, 158, 218);
            dockingManager.MetroSplitterBackColor = Color.FromArgb(155, 159, 183);
            dockingManager.ReduceFlickeringInRtl = false;
            dockingManager.ThemeName = "Office2016Colorful";
            dockingManager.VisualStyle = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            dockingManager.DockVisibilityChanged += DockingManager_DockVisibilityChanged;
            // 
            // timerCheckListener
            // 
            timerCheckListener.Interval = 1000;
            // 
            // statusStripEx1
            // 
            statusStripEx1.BackColor = Color.FromArgb(241, 241, 241);
            statusStripEx1.BeforeTouchSize = new Size(800, 22);
            statusStripEx1.Dock = Syncfusion.Windows.Forms.Tools.DockStyleEx.Bottom;
            statusStripEx1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripProgressBar1 });
            statusStripEx1.Location = new Point(0, 428);
            statusStripEx1.MetroColor = Color.FromArgb(135, 206, 255);
            statusStripEx1.Name = "statusStripEx1";
            statusStripEx1.ShowSeparator = false;
            statusStripEx1.Size = new Size(800, 22);
            statusStripEx1.TabIndex = 1;
            statusStripEx1.Text = "statusStripEx1";
            statusStripEx1.ThemeName = "Office2016Colorful";
            statusStripEx1.VisualStyle = Syncfusion.Windows.Forms.Tools.StatusStripExStyle.Office2016Colorful;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(125, 15);
            toolStripStatusLabel1.Text = "Загрузка обновления";
            toolStripStatusLabel1.Visible = false;
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(100, 15);
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar1.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(statusStripEx1);
            IsMdiContainer = true;
            Name = "MainForm";
            Text = "Form1";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dockingManager).EndInit();
            statusStripEx1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer timerDatabaseListen;
        private Syncfusion.Windows.Forms.Tools.DockingManager dockingManager;
        private System.Windows.Forms.Timer timerCheckListener;
        private Syncfusion.Windows.Forms.Tools.StatusStripEx statusStripEx1;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripStatusLabel toolStripStatusLabel1;
    }
}