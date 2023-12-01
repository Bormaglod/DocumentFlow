namespace DocumentFlow.Dialogs
{
    partial class ScannerDialog
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
            groupBox1 = new GroupBox();
            linkItemProps = new LinkLabel();
            linkNativeProps = new LinkLabel();
            buttonBrowse = new Syncfusion.WinForms.Controls.SfButton();
            comboDevices = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            groupBox2 = new GroupBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            toolStrip2 = new ToolStrip();
            buttonDeletePage = new ToolStripButton();
            panel1 = new Panel();
            buttonScan = new Syncfusion.WinForms.Controls.SfButton();
            buttonSave = new Syncfusion.WinForms.Controls.SfButton();
            groupBox3 = new GroupBox();
            pictureBox1 = new PictureBox();
            toolStrip1 = new ToolStrip();
            buttonTurnPage = new ToolStripButton();
            splitContainer1 = new SplitContainer();
            groupBox4 = new GroupBox();
            linkViewItemProps = new LinkLabel();
            buttonProperties = new Syncfusion.WinForms.Controls.SfButton();
            buttonSelectItems = new Syncfusion.WinForms.Controls.SfButton();
            panel2 = new Panel();
            buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)comboDevices).BeginInit();
            groupBox2.SuspendLayout();
            toolStrip2.SuspendLayout();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox4.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(linkItemProps);
            groupBox1.Controls.Add(linkNativeProps);
            groupBox1.Controls.Add(buttonBrowse);
            groupBox1.Controls.Add(comboDevices);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(316, 99);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Устройство";
            // 
            // linkItemProps
            // 
            linkItemProps.AutoSize = true;
            linkItemProps.Location = new Point(6, 73);
            linkItemProps.Name = "linkItemProps";
            linkItemProps.Size = new Size(229, 15);
            linkItemProps.TabIndex = 3;
            linkItemProps.TabStop = true;
            linkItemProps.Text = "Просмотр свойств элемента устройства";
            linkItemProps.LinkClicked += LinkItemProps_LinkClicked;
            // 
            // linkNativeProps
            // 
            linkNativeProps.AutoSize = true;
            linkNativeProps.Location = new Point(6, 52);
            linkNativeProps.Name = "linkNativeProps";
            linkNativeProps.Size = new Size(249, 15);
            linkNativeProps.TabIndex = 2;
            linkNativeProps.TabStop = true;
            linkNativeProps.Text = "Просмотр собственных свойств устройства";
            linkNativeProps.LinkClicked += LinkNativeProps_LinkClicked;
            // 
            // buttonBrowse
            // 
            buttonBrowse.AccessibleName = "Button";
            buttonBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonBrowse.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonBrowse.Location = new Point(237, 22);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new Size(73, 21);
            buttonBrowse.TabIndex = 1;
            buttonBrowse.Text = "Выбор";
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += ButtonBrowse_Click;
            // 
            // comboDevices
            // 
            comboDevices.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboDevices.BackColor = Color.FromArgb(255, 255, 255);
            comboDevices.BeforeTouchSize = new Size(225, 23);
            comboDevices.DropDownStyle = ComboBoxStyle.DropDownList;
            comboDevices.ForeColor = Color.FromArgb(68, 68, 68);
            comboDevices.Location = new Point(6, 22);
            comboDevices.Name = "comboDevices";
            comboDevices.Size = new Size(225, 23);
            comboDevices.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            comboDevices.TabIndex = 0;
            comboDevices.ThemeName = "Office2016Colorful";
            comboDevices.SelectedIndexChanging += ComboDevices_SelectedIndexChanging;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(flowLayoutPanel1);
            groupBox2.Controls.Add(toolStrip2);
            groupBox2.Controls.Add(panel1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 184);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(316, 317);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Изображения";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 44);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(310, 235);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip2
            // 
            toolStrip2.Items.AddRange(new ToolStripItem[] { buttonDeletePage });
            toolStrip2.Location = new Point(3, 19);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new Size(310, 25);
            toolStrip2.TabIndex = 2;
            toolStrip2.Text = "toolStrip2";
            // 
            // buttonDeletePage
            // 
            buttonDeletePage.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonDeletePage.Image = Properties.Resources.icons8_delete_16;
            buttonDeletePage.ImageTransparentColor = Color.Magenta;
            buttonDeletePage.Name = "buttonDeletePage";
            buttonDeletePage.Size = new Size(23, 22);
            buttonDeletePage.Text = "Удалить страницу";
            buttonDeletePage.Click += ButtonDeletePage_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonScan);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(3, 279);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(0, 6, 6, 6);
            panel1.Size = new Size(310, 35);
            panel1.TabIndex = 0;
            // 
            // buttonScan
            // 
            buttonScan.AccessibleName = "Button";
            buttonScan.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonScan.Location = new Point(211, 6);
            buttonScan.Name = "buttonScan";
            buttonScan.Size = new Size(96, 23);
            buttonScan.TabIndex = 0;
            buttonScan.Text = "Сканировать";
            buttonScan.UseVisualStyleBackColor = true;
            buttonScan.Click += ButtonScan_Click;
            // 
            // buttonSave
            // 
            buttonSave.AccessibleName = "Button";
            buttonSave.DialogResult = DialogResult.OK;
            buttonSave.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonSave.Location = new Point(526, 6);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(96, 23);
            buttonSave.TabIndex = 1;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(pictureBox1);
            groupBox3.Controls.Add(toolStrip1);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(6);
            groupBox3.Size = new Size(410, 501);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Сканирование";
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(6, 47);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(398, 448);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { buttonTurnPage });
            toolStrip1.Location = new Point(6, 22);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(398, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // buttonTurnPage
            // 
            buttonTurnPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
            buttonTurnPage.Image = Properties.Resources.icons8_clockwise_rotation_16;
            buttonTurnPage.ImageTransparentColor = Color.Magenta;
            buttonTurnPage.Name = "buttonTurnPage";
            buttonTurnPage.Size = new Size(23, 22);
            buttonTurnPage.Text = "Повернуть страницу";
            buttonTurnPage.Click += ButtonTurnPage_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(6, 6);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox2);
            splitContainer1.Panel1.Controls.Add(groupBox4);
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox3);
            splitContainer1.Size = new Size(730, 501);
            splitContainer1.SplitterDistance = 316;
            splitContainer1.TabIndex = 3;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(linkViewItemProps);
            groupBox4.Controls.Add(buttonProperties);
            groupBox4.Controls.Add(buttonSelectItems);
            groupBox4.Dock = DockStyle.Top;
            groupBox4.Location = new Point(0, 99);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(316, 85);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "Элементы";
            // 
            // linkViewItemProps
            // 
            linkViewItemProps.AutoSize = true;
            linkViewItemProps.Location = new Point(6, 54);
            linkViewItemProps.Name = "linkViewItemProps";
            linkViewItemProps.Size = new Size(165, 15);
            linkViewItemProps.TabIndex = 2;
            linkViewItemProps.TabStop = true;
            linkViewItemProps.Text = "Просмотр свойств элемента";
            linkViewItemProps.LinkClicked += LinkViewItemProps_LinkClicked;
            // 
            // buttonProperties
            // 
            buttonProperties.AccessibleName = "Button";
            buttonProperties.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonProperties.Location = new Point(137, 22);
            buttonProperties.Name = "buttonProperties";
            buttonProperties.Size = new Size(96, 23);
            buttonProperties.TabIndex = 1;
            buttonProperties.Text = "Свойства...";
            buttonProperties.UseVisualStyleBackColor = true;
            buttonProperties.Click += ButtonProperties_Click;
            // 
            // buttonSelectItems
            // 
            buttonSelectItems.AccessibleName = "Button";
            buttonSelectItems.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonSelectItems.Location = new Point(6, 22);
            buttonSelectItems.Name = "buttonSelectItems";
            buttonSelectItems.Size = new Size(125, 23);
            buttonSelectItems.TabIndex = 0;
            buttonSelectItems.Text = "Выбор элементов...";
            buttonSelectItems.UseVisualStyleBackColor = true;
            buttonSelectItems.Click += ButtonSelectItems_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(buttonCancel);
            panel2.Controls.Add(buttonSave);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(6, 507);
            panel2.Name = "panel2";
            panel2.Size = new Size(730, 34);
            panel2.TabIndex = 4;
            // 
            // buttonCancel
            // 
            buttonCancel.AccessibleName = "Button";
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancel.Location = new Point(628, 6);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(96, 23);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Отменить";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // ScannerDialog
            // 
            AcceptButton = buttonSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            CancelButton = buttonCancel;
            ClientSize = new Size(742, 547);
            Controls.Add(splitContainer1);
            Controls.Add(panel2);
            DoubleBuffered = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ScannerDialog";
            Padding = new Padding(6);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Сканирование";
            Load += ScannerDialog_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)comboDevices).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            panel1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Syncfusion.WinForms.Controls.SfButton buttonBrowse;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboDevices;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private LinkLabel linkItemProps;
        private LinkLabel linkNativeProps;
        private Panel panel1;
        private Syncfusion.WinForms.Controls.SfButton buttonScan;
        private PictureBox pictureBox1;
        private SplitContainer splitContainer1;
        private GroupBox groupBox4;
        private LinkLabel linkViewItemProps;
        private Syncfusion.WinForms.Controls.SfButton buttonProperties;
        private Syncfusion.WinForms.Controls.SfButton buttonSelectItems;
        private FlowLayoutPanel flowLayoutPanel1;
        private ToolStrip toolStrip1;
        private ToolStripButton buttonTurnPage;
        private ToolStrip toolStrip2;
        private ToolStripButton buttonDeletePage;
        private Syncfusion.WinForms.Controls.SfButton buttonSave;
        private Panel panel2;
        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
    }
}