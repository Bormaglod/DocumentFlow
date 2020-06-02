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
            this.imageMenu = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeSidebar = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
            this.tabControls = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeSidebar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControls)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1101, 506);
            this.splitContainer1.SplitterDistance = 260;
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
            this.treeSidebar.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1)});
            this.treeSidebar.BeforeTouchSize = new System.Drawing.Size(260, 506);
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
            this.treeSidebar.Size = new System.Drawing.Size(260, 506);
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
            this.tabControls.BeforeTouchSize = new System.Drawing.Size(837, 506);
            this.tabControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControls.FocusOnTabClick = false;
            this.tabControls.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControls.HotTrack = true;
            this.tabControls.InactiveCloseButtonForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.tabControls.ItemSize = new System.Drawing.Size(99, 24);
            this.tabControls.Location = new System.Drawing.Point(0, 0);
            this.tabControls.Name = "tabControls";
            this.tabControls.ShowCloseButtonForActiveTabOnly = true;
            this.tabControls.ShowTabCloseButton = true;
            this.tabControls.Size = new System.Drawing.Size(837, 506);
            this.tabControls.TabIndex = 1;
            this.tabControls.TabPrimitivesHost.TabPrimitives.Add(new Syncfusion.Windows.Forms.Tools.TabPrimitive(Syncfusion.Windows.Forms.Tools.TabPrimitiveType.PreviousTab, null, System.Drawing.Color.Empty, true, 1, "TabPrimitive0", ""));
            this.tabControls.TabPrimitivesHost.TabPrimitives.Add(new Syncfusion.Windows.Forms.Tools.TabPrimitive(Syncfusion.Windows.Forms.Tools.TabPrimitiveType.NextTab, null, System.Drawing.Color.Empty, true, 1, "TabPrimitive1", ""));
            this.tabControls.TabPrimitivesHost.TabPrimitives.Add(new Syncfusion.Windows.Forms.Tools.TabPrimitive(Syncfusion.Windows.Forms.Tools.TabPrimitiveType.DropDown, null, System.Drawing.Color.Empty, true, 1, "TabPrimitive2", ""));
            this.tabControls.TabPrimitivesHost.Visible = true;
            this.tabControls.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererMetro);
            this.tabControls.ThemeName = "TabRendererMetro";
            this.tabControls.ThemesEnabled = true;
            this.tabControls.UserMoveTabs = true;
            this.tabControls.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.tabControls_ControlRemoved);
            // 
            // DocumentFlowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 506);
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("WindowState", global::DocumentFlow.Properties.Settings.Default, "WindowState", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocumentFlowForm";
            this.Text = "DocumentFlow (ООО \"Автоком\")";
            this.WindowState = global::DocumentFlow.Properties.Settings.Default.WindowState;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocumentFlowForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DocumentFlowForm_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeSidebar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageMenu;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeSidebar;
        private Syncfusion.Windows.Forms.Tools.TabControlAdv tabControls;
    }
}

