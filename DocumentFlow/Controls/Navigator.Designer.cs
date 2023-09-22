namespace DocumentFlow.Controls
{
    partial class Navigator
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            Syncfusion.Windows.Forms.Tools.TreeNavigator.HeaderCollection headerCollection2 = new Syncfusion.Windows.Forms.Tools.TreeNavigator.HeaderCollection();
            treeNavigator1 = new Syncfusion.Windows.Forms.Tools.TreeNavigator();
            this.treeMenuDocument = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            this.treeMenuDictionary = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            this.treeMenuReport = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            this.treeMenuSystem = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            this.treeMenuAbout = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            this.treeMenuLogout = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            SuspendLayout();
            // 
            // treeNavigator1
            // 
            treeNavigator1.BorderColor = Color.FromArgb(197, 197, 197);
            treeNavigator1.Dock = DockStyle.Fill;
            treeNavigator1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            treeNavigator1.ForeColor = Color.FromArgb(68, 68, 68);
            headerCollection2.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point);
            headerCollection2.HeaderBackColor = Color.FromArgb(255, 255, 255);
            headerCollection2.HeaderForeColor = Color.FromArgb(68, 68, 68);
            treeNavigator1.Header = headerCollection2;
            treeNavigator1.ItemBackColor = Color.FromArgb(255, 255, 255);
            this.treeNavigator1.Items.Add(this.treeMenuDocument);
            this.treeNavigator1.Items.Add(this.treeMenuDictionary);
            this.treeNavigator1.Items.Add(this.treeMenuReport);
            this.treeNavigator1.Items.Add(this.treeMenuSystem);
            this.treeNavigator1.Items.Add(this.treeMenuAbout);
            this.treeNavigator1.Items.Add(this.treeMenuLogout);
            treeNavigator1.Location = new Point(0, 0);
            treeNavigator1.MinimumSize = new Size(150, 150);
            treeNavigator1.Name = "treeNavigator1";
            treeNavigator1.ShowHeader = false;
            treeNavigator1.Size = new Size(299, 584);
            treeNavigator1.Style = Syncfusion.Windows.Forms.Tools.TreeNavigatorStyle.Office2016Colorful;
            treeNavigator1.TabIndex = 2;
            treeNavigator1.Text = "treeNavigator1";
            treeNavigator1.ThemeName = "Office2016Colorful";
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
            // Navigator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(treeNavigator1);
            Name = "Navigator";
            Size = new Size(299, 584);
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TreeNavigator treeNavigator1;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuDocument;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuDictionary;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuReport;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuSystem;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuAbout;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuLogout;
    }
}
