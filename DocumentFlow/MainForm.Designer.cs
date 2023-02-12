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
            this.components = new System.ComponentModel.Container();
            Syncfusion.Windows.Forms.Tools.TreeNavigator.HeaderCollection headerCollection1 = new Syncfusion.Windows.Forms.Tools.TreeNavigator.HeaderCollection();
            this.treeNavigator1 = new Syncfusion.Windows.Forms.Tools.TreeNavigator();
            this.treeMenuDocument = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            this.treeMenuDictionary = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            this.treeMenuReport = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            this.treeMenuSystem = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            this.treeMenuAbout = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            this.treeMenuLogout = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlAdv1 = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.tabPageAdv1 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.timerDatabaseListen = new System.Windows.Forms.Timer(this.components);
            this.timerCheckListener = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeNavigator1
            // 
            this.treeNavigator1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.treeNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeNavigator1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeNavigator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            headerCollection1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            headerCollection1.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            headerCollection1.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeNavigator1.Header = headerCollection1;
            this.treeNavigator1.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.treeNavigator1.Items.Add(this.treeMenuDocument);
            this.treeNavigator1.Items.Add(this.treeMenuDictionary);
            this.treeNavigator1.Items.Add(this.treeMenuReport);
            this.treeNavigator1.Items.Add(this.treeMenuSystem);
            this.treeNavigator1.Items.Add(this.treeMenuAbout);
            this.treeNavigator1.Items.Add(this.treeMenuLogout);
            this.treeNavigator1.Location = new System.Drawing.Point(0, 0);
            this.treeNavigator1.MinimumSize = new System.Drawing.Size(150, 150);
            this.treeNavigator1.Name = "treeNavigator1";
            this.treeNavigator1.ShowHeader = false;
            this.treeNavigator1.Size = new System.Drawing.Size(198, 582);
            this.treeNavigator1.Style = Syncfusion.Windows.Forms.Tools.TreeNavigatorStyle.Office2016Colorful;
            this.treeNavigator1.TabIndex = 1;
            this.treeNavigator1.Text = "treeNavigator1";
            this.treeNavigator1.ThemeName = "Office2016Colorful";
            // 
            // treeMenuDocument
            // 
            this.treeMenuDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.treeMenuDocument.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeMenuDocument.ItemBackColor = System.Drawing.SystemColors.Control;
            this.treeMenuDocument.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            this.treeMenuDocument.Location = new System.Drawing.Point(0, 0);
            this.treeMenuDocument.Name = "treeMenuDocument";
            this.treeMenuDocument.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.treeMenuDocument.SelectedItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeMenuDocument.Size = new System.Drawing.Size(196, 50);
            this.treeMenuDocument.TabIndex = 0;
            this.treeMenuDocument.Text = "Документы";
            // 
            // treeMenuDictionary
            // 
            this.treeMenuDictionary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.treeMenuDictionary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeMenuDictionary.ItemBackColor = System.Drawing.SystemColors.Control;
            this.treeMenuDictionary.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            this.treeMenuDictionary.Location = new System.Drawing.Point(0, 52);
            this.treeMenuDictionary.Name = "treeMenuDictionary";
            this.treeMenuDictionary.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.treeMenuDictionary.SelectedItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeMenuDictionary.Size = new System.Drawing.Size(196, 50);
            this.treeMenuDictionary.TabIndex = 0;
            this.treeMenuDictionary.Text = "Справочники";
            // 
            // treeMenuReport
            // 
            this.treeMenuReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.treeMenuReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeMenuReport.ItemBackColor = System.Drawing.SystemColors.Control;
            this.treeMenuReport.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            this.treeMenuReport.Location = new System.Drawing.Point(0, 104);
            this.treeMenuReport.Name = "treeMenuReport";
            this.treeMenuReport.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.treeMenuReport.SelectedItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeMenuReport.Size = new System.Drawing.Size(196, 50);
            this.treeMenuReport.TabIndex = 0;
            this.treeMenuReport.Text = "Отчёты";
            // 
            // treeMenuSystem
            // 
            this.treeMenuSystem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.treeMenuSystem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeMenuSystem.ItemBackColor = System.Drawing.SystemColors.Control;
            this.treeMenuSystem.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            this.treeMenuSystem.Location = new System.Drawing.Point(0, 156);
            this.treeMenuSystem.Name = "treeMenuSystem";
            this.treeMenuSystem.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.treeMenuSystem.SelectedItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeMenuSystem.Size = new System.Drawing.Size(196, 50);
            this.treeMenuSystem.TabIndex = 0;
            this.treeMenuSystem.Text = "Система";
            // 
            // treeMenuAbout
            // 
            this.treeMenuAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.treeMenuAbout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeMenuAbout.ItemBackColor = System.Drawing.SystemColors.Control;
            this.treeMenuAbout.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            this.treeMenuAbout.Location = new System.Drawing.Point(0, 208);
            this.treeMenuAbout.Name = "treeMenuAbout";
            this.treeMenuAbout.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.treeMenuAbout.SelectedItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeMenuAbout.Size = new System.Drawing.Size(196, 50);
            this.treeMenuAbout.TabIndex = 0;
            this.treeMenuAbout.Text = "О программе";
            this.treeMenuAbout.Click += new System.EventHandler(this.TreeMenuAbout_Click);
            // 
            // treeMenuLogout
            // 
            this.treeMenuLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.treeMenuLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeMenuLogout.ItemBackColor = System.Drawing.SystemColors.Control;
            this.treeMenuLogout.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            this.treeMenuLogout.Location = new System.Drawing.Point(0, 260);
            this.treeMenuLogout.Name = "treeMenuLogout";
            this.treeMenuLogout.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.treeMenuLogout.SelectedItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.treeMenuLogout.Size = new System.Drawing.Size(196, 50);
            this.treeMenuLogout.TabIndex = 0;
            this.treeMenuLogout.Text = "Заблокировать";
            this.treeMenuLogout.Click += new System.EventHandler(this.TreeMenuLogout_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeNavigator1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlAdv1);
            this.splitContainer1.Size = new System.Drawing.Size(1106, 582);
            this.splitContainer1.SplitterDistance = 198;
            this.splitContainer1.TabIndex = 2;
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.ActiveTabFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(904, 582);
            this.tabControlAdv1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabControlAdv1.Controls.Add(this.tabPageAdv1);
            this.tabControlAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAdv1.FocusOnTabClick = false;
            this.tabControlAdv1.HotTrack = true;
            this.tabControlAdv1.Location = new System.Drawing.Point(0, 0);
            this.tabControlAdv1.Multiline = true;
            this.tabControlAdv1.Name = "tabControlAdv1";
            this.tabControlAdv1.Office2010ColorTheme = Syncfusion.Windows.Forms.Office2010Theme.Silver;
            this.tabControlAdv1.ShowCloseButtonForActiveTabOnly = true;
            this.tabControlAdv1.ShowTabCloseButton = true;
            this.tabControlAdv1.Size = new System.Drawing.Size(904, 582);
            this.tabControlAdv1.TabIndex = 1;
            this.tabControlAdv1.TabPanelBackColor = System.Drawing.SystemColors.ControlLight;
            this.tabControlAdv1.TabPrimitivesHost.TabPrimitives.Add(new Syncfusion.Windows.Forms.Tools.TabPrimitive(Syncfusion.Windows.Forms.Tools.TabPrimitiveType.DropDown, null, System.Drawing.Color.Empty, true, 1, "TabPrimitive0", ""));
            this.tabControlAdv1.TabPrimitivesHost.Visible = true;
            this.tabControlAdv1.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererDockingWhidbey);
            this.tabControlAdv1.ThemeName = "TabRendererDockingWhidbey";
            this.tabControlAdv1.UserMoveTabs = true;
            this.tabControlAdv1.VSLikeScrollButton = true;
            this.tabControlAdv1.SelectedIndexChanging += new Syncfusion.Windows.Forms.Tools.SelectedIndexChangingEventHandler(this.TabControlAdv1_SelectedIndexChanging);
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.Image = null;
            this.tabPageAdv1.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPageAdv1.Location = new System.Drawing.Point(0, 27);
            this.tabPageAdv1.Name = "tabPageAdv1";
            this.tabPageAdv1.ShowCloseButton = true;
            this.tabPageAdv1.Size = new System.Drawing.Size(904, 555);
            this.tabPageAdv1.TabIndex = 1;
            this.tabPageAdv1.Text = "tabPageAdv1";
            this.tabPageAdv1.ThemesEnabled = false;
            // 
            // timerDatabaseListen
            // 
            this.timerDatabaseListen.Tick += new System.EventHandler(this.TimerDatabaseListen_Tick);
            // 
            // timerCheckListener
            // 
            this.timerCheckListener.Interval = 1000;
            this.timerCheckListener.Tick += new System.EventHandler(this.TimerCheckListener_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 582);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "DocumentFlow (ООО \"Автоком\")";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.TreeNavigator treeNavigator1;
        private SplitContainer splitContainer1;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuDocument;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuDictionary;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuReport;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuSystem;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuAbout;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuLogout;
        private System.Windows.Forms.Timer timerDatabaseListen;
        private System.Windows.Forms.Timer timerCheckListener;
        private Syncfusion.Windows.Forms.Tools.TabControlAdv tabControlAdv1;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageAdv1;
    }
}