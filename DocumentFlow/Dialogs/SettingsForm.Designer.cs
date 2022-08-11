namespace DocumentFlow.Dialogs
{
    partial class SettingsForm
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
            Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOk = new Syncfusion.WinForms.Controls.SfButton();
            this.buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeSettings = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBoxExt1 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.labelHeader = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeSettings)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOk);
            this.panelButtons.Controls.Add(this.buttonCancel);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 404);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(800, 46);
            this.panelButtons.TabIndex = 2;
            // 
            // buttonOk
            // 
            this.buttonOk.AccessibleName = "Button";
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonOk.Location = new System.Drawing.Point(590, 6);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(96, 28);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ок";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleName = "Button";
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancel.Location = new System.Drawing.Point(692, 6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 28);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeSettings);
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(8);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelHeader);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(8);
            this.splitContainer1.Size = new System.Drawing.Size(800, 404);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 3;
            // 
            // treeSettings
            // 
            this.treeSettings.BackColor = System.Drawing.Color.White;
            treeNodeAdvStyleInfo1.CheckBoxTickThickness = 1;
            treeNodeAdvStyleInfo1.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.EnsureDefaultOptionedChild = true;
            treeNodeAdvStyleInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            treeNodeAdvStyleInfo1.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            treeNodeAdvStyleInfo1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.treeSettings.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1)});
            this.treeSettings.BeforeTouchSize = new System.Drawing.Size(250, 360);
            this.treeSettings.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.treeSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            // 
            // 
            // 
            this.treeSettings.HelpTextControl.BaseThemeName = null;
            this.treeSettings.HelpTextControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeSettings.HelpTextControl.Location = new System.Drawing.Point(0, 0);
            this.treeSettings.HelpTextControl.Name = "";
            this.treeSettings.HelpTextControl.Size = new System.Drawing.Size(15, 17);
            this.treeSettings.HelpTextControl.TabIndex = 0;
            this.treeSettings.HelpTextControl.Visible = true;
            this.treeSettings.InactiveSelectedNodeForeColor = System.Drawing.SystemColors.ControlText;
            this.treeSettings.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.treeSettings.Location = new System.Drawing.Point(8, 36);
            this.treeSettings.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.treeSettings.Name = "treeSettings";
            this.treeSettings.SelectedNodeForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.treeSettings.ShowFocusRect = false;
            this.treeSettings.ShowLines = false;
            this.treeSettings.Size = new System.Drawing.Size(250, 360);
            this.treeSettings.Style = Syncfusion.Windows.Forms.Tools.TreeStyle.Office2016Colorful;
            this.treeSettings.TabIndex = 13;
            this.treeSettings.Text = "treeViewAdv1";
            this.treeSettings.ThemeName = "Office2016Colorful";
            this.treeSettings.ThemeStyle.TreeNodeAdvStyle.CheckBoxTickThickness = 0;
            this.treeSettings.ThemeStyle.TreeNodeAdvStyle.EnsureDefaultOptionedChild = true;
            // 
            // 
            // 
            this.treeSettings.ToolTipControl.BackColor = System.Drawing.SystemColors.Info;
            this.treeSettings.ToolTipControl.BaseThemeName = null;
            this.treeSettings.ToolTipControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeSettings.ToolTipControl.Location = new System.Drawing.Point(0, 0);
            this.treeSettings.ToolTipControl.Name = "";
            this.treeSettings.ToolTipControl.Size = new System.Drawing.Size(15, 17);
            this.treeSettings.ToolTipControl.TabIndex = 0;
            this.treeSettings.ToolTipControl.Visible = true;
            this.treeSettings.TransparentControls = true;
            this.treeSettings.AfterSelect += new System.EventHandler(this.TreeSettings_AfterSelect);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBoxExt1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(8, 8);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(250, 28);
            this.panel4.TabIndex = 12;
            // 
            // textBoxExt1
            // 
            this.textBoxExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxExt1.BeforeTouchSize = new System.Drawing.Size(250, 23);
            this.textBoxExt1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textBoxExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxExt1.FarImage = global::DocumentFlow.Properties.Resources.icons8_find_filled_16;
            this.textBoxExt1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textBoxExt1.Location = new System.Drawing.Point(0, 0);
            this.textBoxExt1.Name = "textBoxExt1";
            this.textBoxExt1.Size = new System.Drawing.Size(250, 23);
            this.textBoxExt1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textBoxExt1.TabIndex = 0;
            this.textBoxExt1.ThemeName = "Office2016Colorful";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.panel6);
            this.panelHeader.Controls.Add(this.labelHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(8, 8);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(8);
            this.panelHeader.Size = new System.Drawing.Size(514, 40);
            this.panelHeader.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(8, 30);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(498, 2);
            this.panel6.TabIndex = 1;
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelHeader.Location = new System.Drawing.Point(8, 8);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(51, 19);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "Empty";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelButtons);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.panelButtons.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeSettings)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelButtons;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private SplitContainer splitContainer1;
        private Panel panel4;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt1;
        private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeSettings;
        private Panel panelHeader;
        private Panel panel6;
        private Label labelHeader;
    }
}