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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentFlowForm));
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015BlueTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            this.statusBarAdv1 = new Syncfusion.Windows.Forms.Tools.StatusBarAdv();
            this.statusBarAdvPanel1 = new Syncfusion.Windows.Forms.Tools.StatusBarAdvPanel();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdv1)).BeginInit();
            this.statusBarAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdvPanel1)).BeginInit();
            this.SuspendLayout();
            // 
            // dockPanel1
            // 
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Padding = new System.Windows.Forms.Padding(6);
            this.dockPanel1.ShowAutoHideContentOnHover = false;
            this.dockPanel1.Size = new System.Drawing.Size(800, 424);
            this.dockPanel1.TabIndex = 0;
            this.dockPanel1.Theme = this.vS2015BlueTheme1;
            // 
            // statusBarAdv1
            // 
            this.statusBarAdv1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.statusBarAdv1.BeforeTouchSize = new System.Drawing.Size(800, 26);
            this.statusBarAdv1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.statusBarAdv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBarAdv1.Controls.Add(this.statusBarAdvPanel1);
            this.statusBarAdv1.CustomLayoutBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.statusBarAdv1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBarAdv1.ForeColor = System.Drawing.Color.White;
            this.statusBarAdv1.Location = new System.Drawing.Point(0, 424);
            this.statusBarAdv1.Name = "statusBarAdv1";
            this.statusBarAdv1.Padding = new System.Windows.Forms.Padding(3);
            this.statusBarAdv1.Size = new System.Drawing.Size(800, 26);
            this.statusBarAdv1.SizingGrip = false;
            this.statusBarAdv1.Spacing = new System.Drawing.Size(2, 2);
            this.statusBarAdv1.Style = Syncfusion.Windows.Forms.Tools.StatusbarStyle.Metro;
            this.statusBarAdv1.TabIndex = 3;
            this.statusBarAdv1.ThemeName = "Metro";
            this.statusBarAdv1.Click += new System.EventHandler(this.statusBarAdv1_Click);
            // 
            // statusBarAdvPanel1
            // 
            this.statusBarAdvPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.statusBarAdvPanel1.BeforeTouchSize = new System.Drawing.Size(112, 20);
            this.statusBarAdvPanel1.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.statusBarAdvPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.statusBarAdvPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBarAdvPanel1.ForeColor = System.Drawing.Color.White;
            this.statusBarAdvPanel1.Location = new System.Drawing.Point(0, 2);
            this.statusBarAdvPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.statusBarAdvPanel1.Name = "statusBarAdvPanel1";
            this.statusBarAdvPanel1.Size = new System.Drawing.Size(112, 20);
            this.statusBarAdvPanel1.SizeToContent = true;
            this.statusBarAdvPanel1.TabIndex = 0;
            this.statusBarAdvPanel1.Text = "statusBarAdvPanel1";
            // 
            // DocumentFlowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.statusBarAdv1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "DocumentFlowForm";
            this.Text = "DocumentFlow (ООО \"Автоком\")";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocumentFlowForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DocumentFlowForm_FormClosed);
            this.Load += new System.EventHandler(this.DocumentFlowForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdv1)).EndInit();
            this.statusBarAdv1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdvPanel1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private Syncfusion.Windows.Forms.Tools.StatusBarAdv statusBarAdv1;
        private Syncfusion.Windows.Forms.Tools.StatusBarAdvPanel statusBarAdvPanel1;
        private WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme vS2015BlueTheme1;
    }
}

