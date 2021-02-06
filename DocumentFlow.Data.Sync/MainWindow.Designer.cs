
namespace DocumentFlow.Data.Sync
{
    partial class MainWindow
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
            Syncfusion.Windows.Forms.Tools.ActiveStateCollection activeStateCollection9 = new Syncfusion.Windows.Forms.Tools.ActiveStateCollection();
            Syncfusion.Windows.Forms.Tools.InactiveStateCollection inactiveStateCollection9 = new Syncfusion.Windows.Forms.Tools.InactiveStateCollection();
            Syncfusion.Windows.Forms.Tools.SliderCollection sliderCollection9 = new Syncfusion.Windows.Forms.Tools.SliderCollection();
            Syncfusion.Windows.Forms.Tools.ActiveStateCollection activeStateCollection10 = new Syncfusion.Windows.Forms.Tools.ActiveStateCollection();
            Syncfusion.Windows.Forms.Tools.InactiveStateCollection inactiveStateCollection10 = new Syncfusion.Windows.Forms.Tools.InactiveStateCollection();
            Syncfusion.Windows.Forms.Tools.SliderCollection sliderCollection10 = new Syncfusion.Windows.Forms.Tools.SliderCollection();
            this.toggleButtonDev = new Syncfusion.Windows.Forms.Tools.ToggleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.toggleButtonProd = new Syncfusion.Windows.Forms.Tools.ToggleButton();
            this.textConsole = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.sfButton1 = new Syncfusion.WinForms.Controls.SfButton();
            ((System.ComponentModel.ISupportInitialize)(this.toggleButtonDev)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleButtonProd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textConsole)).BeginInit();
            this.SuspendLayout();
            // 
            // toggleButtonDev
            // 
            activeStateCollection9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            activeStateCollection9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            activeStateCollection9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            activeStateCollection9.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(176)))));
            this.toggleButtonDev.ActiveState = activeStateCollection9;
            this.toggleButtonDev.Dock = System.Windows.Forms.DockStyle.Right;
            this.toggleButtonDev.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggleButtonDev.ForeColor = System.Drawing.Color.Black;
            inactiveStateCollection9.BackColor = System.Drawing.Color.White;
            inactiveStateCollection9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            inactiveStateCollection9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            inactiveStateCollection9.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toggleButtonDev.InactiveState = inactiveStateCollection9;
            this.toggleButtonDev.Location = new System.Drawing.Point(100, 0);
            this.toggleButtonDev.MinimumSize = new System.Drawing.Size(52, 20);
            this.toggleButtonDev.Name = "toggleButtonDev";
            this.toggleButtonDev.Size = new System.Drawing.Size(100, 32);
            sliderCollection9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sliderCollection9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            sliderCollection9.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sliderCollection9.InactiveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            sliderCollection9.InactiveHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.toggleButtonDev.Slider = sliderCollection9;
            this.toggleButtonDev.TabIndex = 0;
            this.toggleButtonDev.Text = "toggleButton1";
            this.toggleButtonDev.ThemeName = "Office2016Colorful";
            this.toggleButtonDev.ToggleState = Syncfusion.Windows.Forms.Tools.ToggleButtonState.Active;
            this.toggleButtonDev.VisualStyle = Syncfusion.Windows.Forms.Tools.ToggleButtonStyle.Office2016Colorful;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Разработка";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.toggleButtonDev);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 32);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.toggleButtonProd);
            this.panel2.Location = new System.Drawing.Point(241, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 32);
            this.panel2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "Production";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggleButtonProd
            // 
            activeStateCollection10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            activeStateCollection10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            activeStateCollection10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            activeStateCollection10.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(176)))));
            this.toggleButtonProd.ActiveState = activeStateCollection10;
            this.toggleButtonProd.Dock = System.Windows.Forms.DockStyle.Right;
            this.toggleButtonProd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toggleButtonProd.ForeColor = System.Drawing.Color.Black;
            inactiveStateCollection10.BackColor = System.Drawing.Color.White;
            inactiveStateCollection10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            inactiveStateCollection10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            inactiveStateCollection10.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toggleButtonProd.InactiveState = inactiveStateCollection10;
            this.toggleButtonProd.Location = new System.Drawing.Point(100, 0);
            this.toggleButtonProd.MinimumSize = new System.Drawing.Size(52, 20);
            this.toggleButtonProd.Name = "toggleButtonProd";
            this.toggleButtonProd.Size = new System.Drawing.Size(100, 32);
            sliderCollection10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sliderCollection10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            sliderCollection10.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sliderCollection10.InactiveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            sliderCollection10.InactiveHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.toggleButtonProd.Slider = sliderCollection10;
            this.toggleButtonProd.TabIndex = 0;
            this.toggleButtonProd.Text = "toggleButton3";
            this.toggleButtonProd.ThemeName = "Office2016Colorful";
            this.toggleButtonProd.ToggleState = Syncfusion.Windows.Forms.Tools.ToggleButtonState.Active;
            this.toggleButtonProd.VisualStyle = Syncfusion.Windows.Forms.Tools.ToggleButtonStyle.Office2016Colorful;
            // 
            // textConsole
            // 
            this.textConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textConsole.BeforeTouchSize = new System.Drawing.Size(927, 442);
            this.textConsole.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.textConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textConsole.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textConsole.Location = new System.Drawing.Point(16, 50);
            this.textConsole.Multiline = true;
            this.textConsole.Name = "textConsole";
            this.textConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textConsole.Size = new System.Drawing.Size(927, 442);
            this.textConsole.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.textConsole.TabIndex = 6;
            this.textConsole.ThemeName = "Metro";
            // 
            // sfButton1
            // 
            this.sfButton1.AccessibleName = "Button";
            this.sfButton1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sfButton1.Location = new System.Drawing.Point(482, 12);
            this.sfButton1.Name = "sfButton1";
            this.sfButton1.Size = new System.Drawing.Size(96, 32);
            this.sfButton1.TabIndex = 7;
            this.sfButton1.Text = "Start";
            this.sfButton1.Click += new System.EventHandler(this.sfButton1_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(955, 504);
            this.Controls.Add(this.sfButton1);
            this.Controls.Add(this.textConsole);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "MainWindow";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.toggleButtonDev)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toggleButtonProd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textConsole)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.ToggleButton toggleButtonDev;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private Syncfusion.Windows.Forms.Tools.ToggleButton toggleButtonProd;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textConsole;
        private Syncfusion.WinForms.Controls.SfButton sfButton1;
    }
}

