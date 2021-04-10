namespace DocumentFlow
{
    partial class CatalogDockControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatalogDockControl));
            Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
            this.imageMenu = new System.Windows.Forms.ImageList(this.components);
            this.treeSidebar = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
            ((System.ComponentModel.ISupportInitialize)(this.treeSidebar)).BeginInit();
            this.SuspendLayout();
            // 
            // imageMenu
            // 
            this.imageMenu.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageMenu.ImageStream")));
            this.imageMenu.TransparentColor = System.Drawing.Color.Transparent;
            this.imageMenu.Images.SetKeyName(0, "icons8-logout-16.png");
            this.imageMenu.Images.SetKeyName(1, "icons8-о-нас-16.png");
            // 
            // treeSidebar
            // 
            this.treeSidebar.BackColor = System.Drawing.Color.White;
            treeNodeAdvStyleInfo1.CheckBoxTickThickness = 1;
            treeNodeAdvStyleInfo1.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.EnsureDefaultOptionedChild = true;
            treeNodeAdvStyleInfo1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            treeNodeAdvStyleInfo1.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.treeSidebar.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1)});
            this.treeSidebar.BeforeTouchSize = new System.Drawing.Size(315, 480);
            this.treeSidebar.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.treeSidebar.BorderColor = System.Drawing.Color.White;
            this.treeSidebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeSidebar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeSidebar.FullRowSelect = true;
            // 
            // 
            // 
            this.treeSidebar.HelpTextControl.BaseThemeName = null;
            this.treeSidebar.HelpTextControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeSidebar.HelpTextControl.Location = new System.Drawing.Point(0, 0);
            this.treeSidebar.HelpTextControl.Name = "";
            this.treeSidebar.HelpTextControl.Size = new System.Drawing.Size(15, 17);
            this.treeSidebar.HelpTextControl.TabIndex = 0;
            this.treeSidebar.HelpTextControl.Visible = true;
            this.treeSidebar.HideSelection = false;
            this.treeSidebar.InactiveSelectedNodeForeColor = System.Drawing.SystemColors.ControlText;
            this.treeSidebar.ItemHeight = 24;
            this.treeSidebar.LeftImageList = this.imageMenu;
            this.treeSidebar.Location = new System.Drawing.Point(0, 0);
            this.treeSidebar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.treeSidebar.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.treeSidebar.Name = "treeSidebar";
            this.treeSidebar.SelectedNodeForeColor = System.Drawing.SystemColors.HighlightText;
            this.treeSidebar.ShowFocusRect = false;
            this.treeSidebar.ShowLines = false;
            this.treeSidebar.Size = new System.Drawing.Size(315, 480);
            this.treeSidebar.Style = Syncfusion.Windows.Forms.Tools.TreeStyle.Metro;
            this.treeSidebar.TabIndex = 1;
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
            this.treeSidebar.NodeMouseDoubleClick += new Syncfusion.Windows.Forms.Tools.TreeNodeAdvMouseClickArgs(this.treeSidebar_NodeMouseDoubleClick);
            // 
            // CatalogDockControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 480);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.treeSidebar);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "CatalogDockControl";
            this.TabText = "Каталог";
            ((System.ComponentModel.ISupportInitialize)(this.treeSidebar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageMenu;
        private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeSidebar;
    }
}