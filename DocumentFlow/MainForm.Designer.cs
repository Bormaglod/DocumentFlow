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
            ((System.ComponentModel.ISupportInitialize)dockingManager).BeginInit();
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            IsMdiContainer = true;
            Name = "MainForm";
            Text = "Form1";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dockingManager).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer timerDatabaseListen;
        private Syncfusion.Windows.Forms.Tools.DockingManager dockingManager;
        private System.Windows.Forms.Timer timerCheckListener;
    }
}