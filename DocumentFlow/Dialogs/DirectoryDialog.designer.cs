using DocumentFlow.Controls.Interfaces;

namespace DocumentFlow.Dialogs
{
    partial class DirectoryDialog
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
            Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeNodeAdvSubItemStyleInfo treeNodeAdvSubItemStyleInfo2 = new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeNodeAdvSubItemStyleInfo();
            Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo2 = new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeNodeAdvStyleInfo();
            Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeColumnAdvStyleInfo treeColumnAdvStyleInfo2 = new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.TreeColumnAdvStyleInfo();
            panel1 = new Panel();
            textSearch = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            panel4 = new Panel();
            panel3 = new Panel();
            buttonClearSearch = new Controls.ToolButton();
            panel2 = new Panel();
            buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            buttonSelect = new Syncfusion.WinForms.Controls.SfButton();
            treeSelect = new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.MultiColumnTreeView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textSearch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)treeSelect).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Controls.Add(textSearch);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(buttonClearSearch);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(buttonCancel);
            panel1.Controls.Add(buttonSelect);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(6);
            panel1.Size = new Size(844, 37);
            panel1.TabIndex = 2;
            // 
            // textSearch
            // 
            textSearch.BackColor = Color.FromArgb(255, 255, 255);
            textSearch.BeforeTouchSize = new Size(597, 25);
            textSearch.BorderColor = Color.FromArgb(197, 197, 197);
            textSearch.BorderStyle = BorderStyle.FixedSingle;
            textSearch.Dock = DockStyle.Fill;
            textSearch.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textSearch.ForeColor = Color.FromArgb(68, 68, 68);
            textSearch.Location = new Point(108, 6);
            textSearch.Name = "textSearch";
            textSearch.NearImage = Properties.Resources.icons8_find_filled_16;
            textSearch.Size = new Size(597, 25);
            textSearch.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            textSearch.TabIndex = 2;
            textSearch.ThemeName = "Office2016Colorful";
            textSearch.TextChanged += TextSearch_TextChanged;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(102, 6);
            panel4.Name = "panel4";
            panel4.Size = new Size(6, 25);
            panel4.TabIndex = 6;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(705, 6);
            panel3.Name = "panel3";
            panel3.Size = new Size(6, 25);
            panel3.TabIndex = 5;
            // 
            // buttonClearSearch
            // 
            buttonClearSearch.BackClickedColor = Color.FromArgb(204, 232, 255);
            buttonClearSearch.BackColor = Color.White;
            buttonClearSearch.BackHoveredColor = Color.FromArgb(229, 243, 255);
            buttonClearSearch.BorderClickedColor = Color.FromArgb(153, 209, 255);
            buttonClearSearch.BorderColor = Color.FromArgb(217, 217, 217);
            buttonClearSearch.BorderHoveredColor = Color.FromArgb(204, 232, 235);
            buttonClearSearch.Dock = DockStyle.Right;
            buttonClearSearch.Kind = DocumentFlow.Controls.Enums.ToolButtonKind.Delete;
            buttonClearSearch.Location = new Point(711, 6);
            buttonClearSearch.Name = "buttonClearSearch";
            buttonClearSearch.Size = new Size(25, 25);
            buttonClearSearch.TabIndex = 3;
            buttonClearSearch.TabStop = false;
            buttonClearSearch.Text = "toolButton1";
            buttonClearSearch.Click += ButtonClearSearch_Click;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(736, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(6, 25);
            panel2.TabIndex = 4;
            // 
            // buttonCancel
            // 
            buttonCancel.AccessibleName = "Button";
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Dock = DockStyle.Right;
            buttonCancel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancel.Location = new Point(742, 6);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(96, 25);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Отмена";
            // 
            // buttonSelect
            // 
            buttonSelect.AccessibleName = "Button";
            buttonSelect.DialogResult = DialogResult.OK;
            buttonSelect.Dock = DockStyle.Left;
            buttonSelect.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonSelect.Location = new Point(6, 6);
            buttonSelect.Name = "buttonSelect";
            buttonSelect.Size = new Size(96, 25);
            buttonSelect.TabIndex = 0;
            buttonSelect.Text = "Выбор";
            // 
            // treeSelect
            // 
            treeSelect.AutoAdjustMultiLineHeight = true;
            treeSelect.AutoGenerateColumns = true;
            treeSelect.AutoSizeMode = Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.AutoSizeMode.AllCellsWithLastColumnFill;
            treeSelect.BackColor = Color.White;
            treeNodeAdvSubItemStyleInfo2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            treeNodeAdvStyleInfo2.CheckBoxTickThickness = 1;
            treeNodeAdvStyleInfo2.CheckColor = Color.FromArgb(109, 109, 109);
            treeNodeAdvStyleInfo2.DisabledCheckColor = Color.FromArgb(210, 210, 210);
            treeNodeAdvStyleInfo2.DisabledOptionButtonColor = Color.FromArgb(243, 243, 243);
            treeNodeAdvStyleInfo2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            treeNodeAdvStyleInfo2.HoverCheckColor = Color.FromArgb(68, 68, 68);
            treeNodeAdvStyleInfo2.HoverOptionButtonColor = Color.FromArgb(222, 236, 249);
            treeNodeAdvStyleInfo2.IntermediateCheckColor = Color.FromArgb(109, 109, 109);
            treeNodeAdvStyleInfo2.IntermediateDisabledCheckColor = Color.FromArgb(210, 210, 210);
            treeNodeAdvStyleInfo2.IntermediateHoverCheckColor = Color.FromArgb(68, 68, 68);
            treeNodeAdvStyleInfo2.OptionButtonColor = Color.FromArgb(255, 255, 255);
            treeNodeAdvStyleInfo2.SelectedOptionButtonColor = Color.FromArgb(109, 109, 109);
            treeNodeAdvStyleInfo2.ShowPlusMinus = true;
            treeColumnAdvStyleInfo2.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            treeSelect.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.StyleNamePair[] { new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.StyleNamePair("Standard - SubItem", treeNodeAdvSubItemStyleInfo2), new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.StyleNamePair("Standard", treeNodeAdvStyleInfo2), new Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.StyleNamePair("Standard - Column", treeColumnAdvStyleInfo2) });
            treeSelect.BeforeTouchSize = new Size(844, 384);
            treeSelect.Border3DStyle = Border3DStyle.Flat;
            treeSelect.BorderSingle = ButtonBorderStyle.None;
            treeSelect.BorderStyle = BorderStyle.FixedSingle;
            treeSelect.Dock = DockStyle.Fill;
            treeSelect.Filter = null;
            treeSelect.FilterLevel = Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.FilterLevel.Extended;
            treeSelect.ForeColor = SystemColors.ControlText;
            treeSelect.FullRowSelect = true;
            treeSelect.HeaderHeight = 0;
            // 
            // 
            // 
            treeSelect.HelpTextControl.BaseThemeName = null;
            treeSelect.HelpTextControl.BorderStyle = BorderStyle.FixedSingle;
            treeSelect.HelpTextControl.Location = new Point(0, 0);
            treeSelect.HelpTextControl.Name = "m_helpText";
            treeSelect.HelpTextControl.Size = new Size(55, 17);
            treeSelect.HelpTextControl.TabIndex = 0;
            treeSelect.HelpTextControl.Text = "help text";
            treeSelect.HideSelection = false;
            treeSelect.ItemHeight = 24;
            treeSelect.KeepDottedSelection = false;
            treeSelect.Location = new Point(0, 37);
            treeSelect.Name = "treeSelect";
            treeSelect.NodeHoverColor = Color.FromArgb(51, 153, 255);
            treeSelect.ShowLines = false;
            treeSelect.Size = new Size(844, 384);
            treeSelect.Style = Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.MultiColumnVisualStyle.Metro;
            treeSelect.TabIndex = 3;
            treeSelect.Text = "multiColumnTreeView1";
            treeSelect.ThemeName = "Metro";
            treeSelect.ThemeStyle.TreeNodeAdvStyle.CheckBoxTickThickness = 0;
            // 
            // 
            // 
            treeSelect.ToolTipControl.BackColor = SystemColors.Info;
            treeSelect.ToolTipControl.BaseThemeName = null;
            treeSelect.ToolTipControl.BorderStyle = BorderStyle.FixedSingle;
            treeSelect.ToolTipControl.Location = new Point(0, 0);
            treeSelect.ToolTipControl.Name = "m_toolTip";
            treeSelect.ToolTipControl.Size = new Size(41, 15);
            treeSelect.ToolTipControl.TabIndex = 1;
            treeSelect.ToolTipControl.Text = "toolTip";
            treeSelect.NodeMouseDoubleClick += TreeSelect_NodeMouseDoubleClick;
            // 
            // SelectDirectoryDialog
            // 
            AcceptButton = buttonSelect;
            AutoScaleMode = AutoScaleMode.None;
            CancelButton = buttonCancel;
            ClientSize = new Size(844, 421);
            Controls.Add(treeSelect);
            Controls.Add(panel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectDirectoryDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Выбор";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)textSearch).EndInit();
            ((System.ComponentModel.ISupportInitialize)treeSelect).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonSelect;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textSearch;
        private Panel panel4;
        private Panel panel3;
        private DocumentFlow.Controls.ToolButton buttonClearSearch;
        private Panel panel2;
        private Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.MultiColumnTreeView treeSelect;
    }
}