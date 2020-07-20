namespace DocumentFlow
{
    partial class DocumentFlowForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentFlowForm));
            Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionImage captionImage2 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionImage captionImage3 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionImage captionImage4 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionImage captionImage5 = new Syncfusion.Windows.Forms.CaptionImage();
            this.imageMenu = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeSidebar = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
            this.tabControls = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.contextTabsMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuItemCloseCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCloseAllExcept = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSystemMenu = new Syncfusion.Windows.Forms.Tools.ContextMenuStripEx();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeSidebar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControls)).BeginInit();
            this.contextTabsMenu.SuspendLayout();
            this.contextSystemMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageMenu
            // 
            this.imageMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageMenu.ImageStream")));
            this.imageMenu.TransparentColor = System.Drawing.Color.Transparent;
            this.imageMenu.Images.SetKeyName(0, "icons8-logout-16.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeSidebar);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControls);
            this.splitContainer1.Size = new System.Drawing.Size(1102, 502);
            this.splitContainer1.SplitterDistance = 259;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeSidebar
            // 
            this.treeSidebar.BackColor = System.Drawing.Color.White;
            treeNodeAdvStyleInfo1.CheckBoxTickThickness = 1;
            treeNodeAdvStyleInfo1.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.EnsureDefaultOptionedChild = true;
            treeNodeAdvStyleInfo1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            treeNodeAdvStyleInfo1.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            treeNodeAdvStyleInfo1.TextColor = System.Drawing.Color.Black;
            this.treeSidebar.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1)});
            this.treeSidebar.BeforeTouchSize = new System.Drawing.Size(259, 502);
            this.treeSidebar.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.treeSidebar.BorderColor = System.Drawing.Color.White;
            this.treeSidebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeSidebar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeSidebar.FullRowSelect = true;
            // 
            // 
            // 
            this.treeSidebar.HelpTextControl.BaseThemeName = null;
            this.treeSidebar.HelpTextControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeSidebar.HelpTextControl.Location = new System.Drawing.Point(0, 0);
            this.treeSidebar.HelpTextControl.Name = "";
            this.treeSidebar.HelpTextControl.Size = new System.Drawing.Size(15, 15);
            this.treeSidebar.HelpTextControl.TabIndex = 0;
            this.treeSidebar.HelpTextControl.Visible = true;
            this.treeSidebar.HideSelection = false;
            this.treeSidebar.InactiveSelectedNodeForeColor = System.Drawing.SystemColors.ControlText;
            this.treeSidebar.ItemHeight = 24;
            this.treeSidebar.LeftImageList = this.imageMenu;
            this.treeSidebar.Location = new System.Drawing.Point(0, 0);
            this.treeSidebar.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.treeSidebar.Name = "treeSidebar";
            this.treeSidebar.SelectedNodeBackground = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220))))));
            this.treeSidebar.SelectedNodeForeColor = System.Drawing.SystemColors.HighlightText;
            this.treeSidebar.ShowFocusRect = false;
            this.treeSidebar.ShowLines = false;
            this.treeSidebar.Size = new System.Drawing.Size(259, 502);
            this.treeSidebar.Style = Syncfusion.Windows.Forms.Tools.TreeStyle.Metro;
            this.treeSidebar.TabIndex = 0;
            this.treeSidebar.ThemeName = "Metro";
            this.treeSidebar.ThemeStyle.TreeNodeAdvStyle.CheckBoxTickThickness = 0;
            this.treeSidebar.ThemeStyle.TreeNodeAdvStyle.EnsureDefaultOptionedChild = true;
            // 
            // 
            // 
            this.treeSidebar.ToolTipControl.BackColor = System.Drawing.SystemColors.Info;
            this.treeSidebar.ToolTipControl.BaseThemeName = null;
            this.treeSidebar.ToolTipControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeSidebar.ToolTipControl.Location = new System.Drawing.Point(0, 0);
            this.treeSidebar.ToolTipControl.Name = "";
            this.treeSidebar.ToolTipControl.Size = new System.Drawing.Size(15, 15);
            this.treeSidebar.ToolTipControl.TabIndex = 0;
            this.treeSidebar.ToolTipControl.Visible = true;
            this.treeSidebar.TransparentControls = true;
            this.treeSidebar.NodeMouseDoubleClick += new Syncfusion.Windows.Forms.Tools.TreeNodeAdvMouseClickArgs(this.treeSidebar_NodeMouseDoubleClick);
            // 
            // tabControls
            // 
            this.tabControls.ActiveTabFont = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tabControls.BeforeTouchSize = new System.Drawing.Size(839, 502);
            this.tabControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControls.FocusOnTabClick = false;
            this.tabControls.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControls.HotTrack = true;
            this.tabControls.InactiveCloseButtonForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.tabControls.ItemSize = new System.Drawing.Size(99, 24);
            this.tabControls.Location = new System.Drawing.Point(0, 0);
            this.tabControls.Name = "tabControls";
            this.tabControls.ShowCloseButtonForActiveTabOnly = true;
            this.tabControls.ShowTabCloseButton = true;
            this.tabControls.Size = new System.Drawing.Size(839, 502);
            this.tabControls.TabIndex = 1;
            this.tabControls.TabPanelBackColor = System.Drawing.SystemColors.ControlLight;
            this.tabControls.TabPrimitivesHost.TabPrimitives.Add(new Syncfusion.Windows.Forms.Tools.TabPrimitive(Syncfusion.Windows.Forms.Tools.TabPrimitiveType.PreviousTab, null, System.Drawing.Color.Empty, true, 1, "TabPrimitive0", ""));
            this.tabControls.TabPrimitivesHost.TabPrimitives.Add(new Syncfusion.Windows.Forms.Tools.TabPrimitive(Syncfusion.Windows.Forms.Tools.TabPrimitiveType.NextTab, null, System.Drawing.Color.Empty, true, 1, "TabPrimitive1", ""));
            this.tabControls.TabPrimitivesHost.TabPrimitives.Add(new Syncfusion.Windows.Forms.Tools.TabPrimitive(Syncfusion.Windows.Forms.Tools.TabPrimitiveType.DropDown, null, System.Drawing.Color.Empty, true, 1, "TabPrimitive2", ""));
            this.tabControls.TabPrimitivesHost.Visible = true;
            this.tabControls.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererDockingWhidbey);
            this.tabControls.ThemeName = "TabRendererDockingWhidbey";
            this.tabControls.ThemesEnabled = true;
            this.tabControls.ThemeStyle.TabStyle.ActiveFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControls.ThemeStyle.TabStyle.InactiveFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControls.UserMoveTabs = true;
            this.tabControls.SelectedIndexChanging += new Syncfusion.Windows.Forms.Tools.SelectedIndexChangingEventHandler(this.tabControls_SelectedIndexChanging);
            this.tabControls.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControls_MouseClick);
            // 
            // contextTabsMenu
            // 
            this.contextTabsMenu.DropShadowEnabled = false;
            this.contextTabsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCloseCurrent,
            this.menuItemCloseAll,
            this.menuItemCloseAllExcept});
            this.contextTabsMenu.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextTabsMenu.Name = "contextTabsMenu";
            this.contextTabsMenu.Size = new System.Drawing.Size(211, 70);
            this.contextTabsMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextTabsMenu.ThemeName = "Metro";
            // 
            // menuItemCloseCurrent
            // 
            this.menuItemCloseCurrent.Name = "menuItemCloseCurrent";
            this.menuItemCloseCurrent.Size = new System.Drawing.Size(210, 22);
            this.menuItemCloseCurrent.Text = "Закрыть";
            this.menuItemCloseCurrent.Click += new System.EventHandler(this.menuItemCloseCurrent_Click);
            // 
            // menuItemCloseAll
            // 
            this.menuItemCloseAll.Name = "menuItemCloseAll";
            this.menuItemCloseAll.Size = new System.Drawing.Size(210, 22);
            this.menuItemCloseAll.Text = "Закрыть все документы";
            this.menuItemCloseAll.Click += new System.EventHandler(this.menuItemCloseAll_Click);
            // 
            // menuItemCloseAllExcept
            // 
            this.menuItemCloseAllExcept.Name = "menuItemCloseAllExcept";
            this.menuItemCloseAllExcept.Size = new System.Drawing.Size(210, 22);
            this.menuItemCloseAllExcept.Text = "Закрыть все, кроме этой";
            this.menuItemCloseAllExcept.Click += new System.EventHandler(this.menuItemCloseAllExcept_Click);
            // 
            // contextSystemMenu
            // 
            this.contextSystemMenu.DropShadowEnabled = false;
            this.contextSystemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAbout});
            this.contextSystemMenu.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            this.contextSystemMenu.Name = "contextSystemMenu";
            this.contextSystemMenu.Size = new System.Drawing.Size(150, 26);
            this.contextSystemMenu.Style = Syncfusion.Windows.Forms.Tools.ContextMenuStripEx.ContextMenuStyle.Metro;
            this.contextSystemMenu.ThemeName = "Metro";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.Size = new System.Drawing.Size(149, 22);
            this.menuItemAbout.Text = "О программе";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // DocumentFlowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.CaptionBarHeight = 32;
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CaptionForeColor = System.Drawing.Color.White;
            captionImage1.BackColor = System.Drawing.Color.Transparent;
            captionImage1.Image = global::DocumentFlow.Properties.Resources.system_close;
            captionImage1.Location = new System.Drawing.Point(1085, 4);
            captionImage1.Name = "close";
            captionImage2.BackColor = System.Drawing.Color.Transparent;
            captionImage2.Image = global::DocumentFlow.Properties.Resources.system_max;
            captionImage2.Location = new System.Drawing.Point(1059, 4);
            captionImage2.Name = "max";
            captionImage3.BackColor = System.Drawing.Color.Transparent;
            captionImage3.Image = global::DocumentFlow.Properties.Resources.system_min;
            captionImage3.Location = new System.Drawing.Point(1033, 4);
            captionImage3.Name = "min";
            captionImage4.BackColor = System.Drawing.Color.Transparent;
            captionImage4.Image = global::DocumentFlow.Properties.Resources.system_menu;
            captionImage4.Location = new System.Drawing.Point(1007, 4);
            captionImage4.Name = "system";
            captionImage5.BackColor = System.Drawing.Color.Transparent;
            captionImage5.Image = global::DocumentFlow.Properties.Resources.icons8_manufacture_16;
            captionImage5.Location = new System.Drawing.Point(4, 4);
            captionImage5.Name = "icon";
            this.CaptionImages.Add(captionImage1);
            this.CaptionImages.Add(captionImage2);
            this.CaptionImages.Add(captionImage3);
            this.CaptionImages.Add(captionImage4);
            this.CaptionImages.Add(captionImage5);
            this.ClientSize = new System.Drawing.Size(1102, 502);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocumentFlowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DocumentFlow (ООО \"Автоком\")";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocumentFlowForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DocumentFlowForm_FormClosed);
            this.Load += new System.EventHandler(this.DocumentFlowForm_Load);
            this.SizeChanged += new System.EventHandler(this.DocumentFlowForm_SizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeSidebar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControls)).EndInit();
            this.contextTabsMenu.ResumeLayout(false);
            this.contextSystemMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageMenu;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeSidebar;
        private Syncfusion.Windows.Forms.Tools.TabControlAdv tabControls;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextTabsMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemCloseCurrent;
        private System.Windows.Forms.ToolStripMenuItem menuItemCloseAll;
        private System.Windows.Forms.ToolStripMenuItem menuItemCloseAllExcept;
        private Syncfusion.Windows.Forms.Tools.ContextMenuStripEx contextSystemMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
    }
}

