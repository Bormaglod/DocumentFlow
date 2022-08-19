namespace DocumentFlow.Dialogs
{
    partial class SelectDirectoryForm<T>
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
            Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeColumnAdvStyleInfo treeColumnAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeColumnAdvStyleInfo();
            Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeNodeAdvStyleInfo();
            Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeNodeAdvSubItemStyleInfo treeNodeAdvSubItemStyleInfo1 = new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeNodeAdvSubItemStyleInfo();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxExt1 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolButton1 = new DocumentFlow.Controls.ToolButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.buttonSelect = new Syncfusion.WinForms.Controls.SfButton();
            this.treeSelect = new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.MultiColumnTreeView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeSelect)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.textBoxExt1);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.toolButton1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(6);
            this.panel1.Size = new System.Drawing.Size(844, 37);
            this.panel1.TabIndex = 2;
            // 
            // textBoxExt1
            // 
            this.textBoxExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxExt1.BeforeTouchSize = new System.Drawing.Size(597, 25);
            this.textBoxExt1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textBoxExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxExt1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxExt1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textBoxExt1.Location = new System.Drawing.Point(108, 6);
            this.textBoxExt1.Name = "textBoxExt1";
            this.textBoxExt1.NearImage = global::DocumentFlow.Properties.Resources.icons8_find_filled_16;
            this.textBoxExt1.Size = new System.Drawing.Size(597, 25);
            this.textBoxExt1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textBoxExt1.TabIndex = 2;
            this.textBoxExt1.ThemeName = "Office2016Colorful";
            this.textBoxExt1.TextChanged += new System.EventHandler(this.TextBoxExt1_TextChanged);
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(102, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(6, 25);
            this.panel4.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(705, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(6, 25);
            this.panel3.TabIndex = 5;
            // 
            // toolButton1
            // 
            this.toolButton1.BackClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.toolButton1.BackColor = System.Drawing.Color.White;
            this.toolButton1.BackHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.toolButton1.BorderClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.toolButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.toolButton1.BorderHoveredColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(235)))));
            this.toolButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolButton1.Kind = DocumentFlow.Controls.ToolButtonKind.Delete;
            this.toolButton1.Location = new System.Drawing.Point(711, 6);
            this.toolButton1.Name = "toolButton1";
            this.toolButton1.Size = new System.Drawing.Size(25, 25);
            this.toolButton1.TabIndex = 3;
            this.toolButton1.TabStop = false;
            this.toolButton1.Text = "toolButton1";
            this.toolButton1.Click += new System.EventHandler(this.ToolButton1_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(736, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(6, 25);
            this.panel2.TabIndex = 4;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleName = "Button";
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancel.Location = new System.Drawing.Point(742, 6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 25);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Отмена";
            // 
            // buttonSelect
            // 
            this.buttonSelect.AccessibleName = "Button";
            this.buttonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonSelect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSelect.Location = new System.Drawing.Point(6, 6);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(96, 25);
            this.buttonSelect.TabIndex = 0;
            this.buttonSelect.Text = "Выбор";
            // 
            // treeSelect
            // 
            this.treeSelect.AutoAdjustMultiLineHeight = true;
            this.treeSelect.AutoGenerateColumns = true;
            this.treeSelect.AutoSizeMode = Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.AutoSizeMode.AllCellsWithLastColumnFill;
            this.treeSelect.BackColor = System.Drawing.Color.White;
            treeColumnAdvStyleInfo1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            treeNodeAdvStyleInfo1.CheckBoxTickThickness = 1;
            treeNodeAdvStyleInfo1.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.DisabledCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            treeNodeAdvStyleInfo1.DisabledOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            treeNodeAdvStyleInfo1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            treeNodeAdvStyleInfo1.HoverCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            treeNodeAdvStyleInfo1.HoverOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(236)))), ((int)(((byte)(249)))));
            treeNodeAdvStyleInfo1.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.IntermediateDisabledCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            treeNodeAdvStyleInfo1.IntermediateHoverCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            treeNodeAdvStyleInfo1.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            treeNodeAdvStyleInfo1.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.ShowPlusMinus = true;
            treeNodeAdvSubItemStyleInfo1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeSelect.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.StyleNamePair("Standard - Column", treeColumnAdvStyleInfo1),
            new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.StyleNamePair("Standard", treeNodeAdvStyleInfo1),
            new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.StyleNamePair("Standard - SubItem", treeNodeAdvSubItemStyleInfo1)});
            this.treeSelect.BeforeTouchSize = new System.Drawing.Size(844, 384);
            this.treeSelect.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.treeSelect.BorderSingle = System.Windows.Forms.ButtonBorderStyle.None;
            this.treeSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeSelect.Filter = null;
            this.treeSelect.FilterLevel = Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.FilterLevel.Extended;
            this.treeSelect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.treeSelect.FullRowSelect = true;
            this.treeSelect.HeaderHeight = 0;
            // 
            // 
            // 
            this.treeSelect.HelpTextControl.BaseThemeName = null;
            this.treeSelect.HelpTextControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeSelect.HelpTextControl.Location = new System.Drawing.Point(0, 0);
            this.treeSelect.HelpTextControl.Name = "m_helpText";
            this.treeSelect.HelpTextControl.Size = new System.Drawing.Size(55, 17);
            this.treeSelect.HelpTextControl.TabIndex = 0;
            this.treeSelect.HelpTextControl.Text = "help text";
            this.treeSelect.HideSelection = false;
            this.treeSelect.ItemHeight = 24;
            this.treeSelect.KeepDottedSelection = false;
            this.treeSelect.Location = new System.Drawing.Point(0, 37);
            this.treeSelect.Name = "treeSelect";
            this.treeSelect.ShowLines = false;
            this.treeSelect.Size = new System.Drawing.Size(844, 384);
            this.treeSelect.Style = Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.MultiColumnVisualStyle.Metro;
            this.treeSelect.TabIndex = 3;
            this.treeSelect.Text = "multiColumnTreeView1";
            this.treeSelect.ThemeName = "Metro";
            this.treeSelect.ThemeStyle.TreeNodeAdvStyle.CheckBoxTickThickness = 0;
            // 
            // 
            // 
            this.treeSelect.ToolTipControl.BackColor = System.Drawing.SystemColors.Info;
            this.treeSelect.ToolTipControl.BaseThemeName = null;
            this.treeSelect.ToolTipControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeSelect.ToolTipControl.Location = new System.Drawing.Point(0, 0);
            this.treeSelect.ToolTipControl.Name = "m_toolTip";
            this.treeSelect.ToolTipControl.Size = new System.Drawing.Size(41, 15);
            this.treeSelect.ToolTipControl.TabIndex = 1;
            this.treeSelect.ToolTipControl.Text = "toolTip";
            this.treeSelect.NodeMouseDoubleClick += new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeNodeAdvMouseClickArgs(this.TreeSelect_NodeMouseDoubleClick);
            // 
            // SelectDirectoryForm
            // 
            this.AcceptButton = this.buttonSelect;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(844, 421);
            this.Controls.Add(this.treeSelect);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectDirectoryForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeSelect)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonSelect;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private DocumentFlow.Controls.ToolButton toolButton1;
        private System.Windows.Forms.Panel panel2;
        private Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.MultiColumnTreeView treeSelect;
    }
}