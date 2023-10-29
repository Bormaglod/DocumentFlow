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
            Syncfusion.Windows.Forms.Tools.TreeNavigator.HeaderCollection headerCollection1 = new Syncfusion.Windows.Forms.Tools.TreeNavigator.HeaderCollection();
            treeNavigator1 = new Syncfusion.Windows.Forms.Tools.TreeNavigator();
            treeMenuDocument = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            treeMenuDictionary = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            treeMenuEmail = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            treeMenuReport = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            treeMenuSystem = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            treeMenuAbout = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            treeMenuLogout = new Syncfusion.Windows.Forms.Tools.TreeMenuItem();
            SuspendLayout();
            // 
            // treeNavigator1
            // 
            treeNavigator1.BorderColor = Color.FromArgb(197, 197, 197);
            treeNavigator1.Dock = DockStyle.Fill;
            treeNavigator1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            treeNavigator1.ForeColor = Color.FromArgb(68, 68, 68);
            headerCollection1.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point);
            headerCollection1.HeaderBackColor = Color.FromArgb(255, 255, 255);
            headerCollection1.HeaderForeColor = Color.FromArgb(68, 68, 68);
            treeNavigator1.Header = headerCollection1;
            treeNavigator1.ItemBackColor = Color.FromArgb(255, 255, 255);
            treeNavigator1.Items.Add(treeMenuDocument);
            treeNavigator1.Items.Add(treeMenuDictionary);
            treeNavigator1.Items.Add(treeMenuEmail);
            treeNavigator1.Items.Add(treeMenuReport);
            treeNavigator1.Items.Add(treeMenuSystem);
            treeNavigator1.Items.Add(treeMenuAbout);
            treeNavigator1.Items.Add(treeMenuLogout);
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
            treeMenuDocument.BackColor = Color.FromArgb(255, 255, 255);
            treeMenuDocument.ForeColor = Color.FromArgb(68, 68, 68);
            treeMenuDocument.ItemBackColor = SystemColors.Control;
            treeMenuDocument.ItemHoverColor = Color.FromArgb(230, 242, 250);
            treeMenuDocument.Location = new Point(0, 0);
            treeMenuDocument.Name = "treeMenuDocument";
            treeMenuDocument.SelectedColor = Color.FromArgb(197, 197, 197);
            treeMenuDocument.SelectedItemForeColor = Color.FromArgb(68, 68, 68);
            treeMenuDocument.Size = new Size(297, 50);
            treeMenuDocument.TabIndex = 0;
            treeMenuDocument.Text = "Документы";
            // 
            // treeMenuDictionary
            // 
            treeMenuDictionary.BackColor = Color.FromArgb(255, 255, 255);
            treeMenuDictionary.ForeColor = Color.FromArgb(68, 68, 68);
            treeMenuDictionary.ItemBackColor = SystemColors.Control;
            treeMenuDictionary.ItemHoverColor = Color.FromArgb(230, 242, 250);
            treeMenuDictionary.Location = new Point(0, 52);
            treeMenuDictionary.Name = "treeMenuDictionary";
            treeMenuDictionary.SelectedColor = Color.FromArgb(197, 197, 197);
            treeMenuDictionary.SelectedItemForeColor = Color.FromArgb(68, 68, 68);
            treeMenuDictionary.Size = new Size(297, 50);
            treeMenuDictionary.TabIndex = 0;
            treeMenuDictionary.Text = "Справочники";
            // 
            // treeMenuEmail
            // 
            treeMenuEmail.BackColor = Color.FromArgb(255, 255, 255);
            treeMenuEmail.ForeColor = Color.FromArgb(68, 68, 68);
            treeMenuEmail.ItemBackColor = SystemColors.Control;
            treeMenuEmail.ItemHoverColor = Color.FromArgb(230, 242, 250);
            treeMenuEmail.Location = new Point(0, 104);
            treeMenuEmail.Name = "treeMenuEmail";
            treeMenuEmail.SelectedColor = Color.FromArgb(197, 197, 197);
            treeMenuEmail.SelectedItemForeColor = Color.FromArgb(68, 68, 68);
            treeMenuEmail.Size = new Size(297, 50);
            treeMenuEmail.TabIndex = 0;
            treeMenuEmail.Text = "Почта";
            treeMenuEmail.Click += TreeMenuEmail_Click;
            // 
            // treeMenuReport
            // 
            treeMenuReport.BackColor = Color.FromArgb(255, 255, 255);
            treeMenuReport.ForeColor = Color.FromArgb(68, 68, 68);
            treeMenuReport.ItemBackColor = SystemColors.Control;
            treeMenuReport.ItemHoverColor = Color.FromArgb(230, 242, 250);
            treeMenuReport.Location = new Point(0, 156);
            treeMenuReport.Name = "treeMenuReport";
            treeMenuReport.SelectedColor = Color.FromArgb(197, 197, 197);
            treeMenuReport.SelectedItemForeColor = Color.FromArgb(68, 68, 68);
            treeMenuReport.Size = new Size(297, 50);
            treeMenuReport.TabIndex = 0;
            treeMenuReport.Text = "Отчёты";
            // 
            // treeMenuSystem
            // 
            treeMenuSystem.BackColor = Color.FromArgb(255, 255, 255);
            treeMenuSystem.ForeColor = Color.FromArgb(68, 68, 68);
            treeMenuSystem.ItemBackColor = SystemColors.Control;
            treeMenuSystem.ItemHoverColor = Color.FromArgb(230, 242, 250);
            treeMenuSystem.Location = new Point(0, 208);
            treeMenuSystem.Name = "treeMenuSystem";
            treeMenuSystem.SelectedColor = Color.FromArgb(197, 197, 197);
            treeMenuSystem.SelectedItemForeColor = Color.FromArgb(68, 68, 68);
            treeMenuSystem.Size = new Size(297, 50);
            treeMenuSystem.TabIndex = 0;
            treeMenuSystem.Text = "Система";
            // 
            // treeMenuAbout
            // 
            treeMenuAbout.BackColor = Color.FromArgb(255, 255, 255);
            treeMenuAbout.ForeColor = Color.FromArgb(68, 68, 68);
            treeMenuAbout.ItemBackColor = SystemColors.Control;
            treeMenuAbout.ItemHoverColor = Color.FromArgb(230, 242, 250);
            treeMenuAbout.Location = new Point(0, 260);
            treeMenuAbout.Name = "treeMenuAbout";
            treeMenuAbout.SelectedColor = Color.FromArgb(197, 197, 197);
            treeMenuAbout.SelectedItemForeColor = Color.FromArgb(68, 68, 68);
            treeMenuAbout.Size = new Size(297, 50);
            treeMenuAbout.TabIndex = 0;
            treeMenuAbout.Text = "О программе";
            treeMenuAbout.Click += TreeMenuAbout_Click;
            // 
            // treeMenuLogout
            // 
            treeMenuLogout.BackColor = Color.FromArgb(255, 255, 255);
            treeMenuLogout.ForeColor = Color.FromArgb(68, 68, 68);
            treeMenuLogout.ItemBackColor = SystemColors.Control;
            treeMenuLogout.ItemHoverColor = Color.FromArgb(230, 242, 250);
            treeMenuLogout.Location = new Point(0, 312);
            treeMenuLogout.Name = "treeMenuLogout";
            treeMenuLogout.SelectedColor = Color.FromArgb(197, 197, 197);
            treeMenuLogout.SelectedItemForeColor = Color.FromArgb(68, 68, 68);
            treeMenuLogout.Size = new Size(297, 50);
            treeMenuLogout.TabIndex = 0;
            treeMenuLogout.Text = "Заблокировать";
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
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuEmail;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuReport;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuSystem;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuAbout;
        private Syncfusion.Windows.Forms.Tools.TreeMenuItem treeMenuLogout;
    }
}
