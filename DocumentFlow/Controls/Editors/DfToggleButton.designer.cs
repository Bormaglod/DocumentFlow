namespace DocumentFlow.Controls.Editors
{
    partial class DfToggleButton
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
            Syncfusion.Windows.Forms.Tools.ActiveStateCollection activeStateCollection1 = new Syncfusion.Windows.Forms.Tools.ActiveStateCollection();
            Syncfusion.Windows.Forms.Tools.InactiveStateCollection inactiveStateCollection1 = new Syncfusion.Windows.Forms.Tools.InactiveStateCollection();
            Syncfusion.Windows.Forms.Tools.SliderCollection sliderCollection1 = new Syncfusion.Windows.Forms.Tools.SliderCollection();
            this.label1 = new System.Windows.Forms.Label();
            this.toggleButton1 = new Syncfusion.Windows.Forms.Tools.ToggleButton();
            ((System.ComponentModel.ISupportInitialize)(this.toggleButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Давальческий материал";
            // 
            // toggleButton1
            // 
            activeStateCollection1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            activeStateCollection1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            activeStateCollection1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            activeStateCollection1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(176)))));
            this.toggleButton1.ActiveState = activeStateCollection1;
            this.toggleButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toggleButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toggleButton1.ForeColor = System.Drawing.Color.Black;
            inactiveStateCollection1.BackColor = System.Drawing.Color.White;
            inactiveStateCollection1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            inactiveStateCollection1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            inactiveStateCollection1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toggleButton1.InactiveState = inactiveStateCollection1;
            this.toggleButton1.Location = new System.Drawing.Point(117, 0);
            this.toggleButton1.MinimumSize = new System.Drawing.Size(52, 23);
            this.toggleButton1.Name = "toggleButton1";
            this.toggleButton1.Size = new System.Drawing.Size(90, 24);
            sliderCollection1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sliderCollection1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            sliderCollection1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sliderCollection1.InactiveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            sliderCollection1.InactiveHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.toggleButton1.Slider = sliderCollection1;
            this.toggleButton1.TabIndex = 3;
            this.toggleButton1.Text = "toggleButton1";
            this.toggleButton1.ThemeName = "Office2016Colorful";
            this.toggleButton1.VisualStyle = Syncfusion.Windows.Forms.Tools.ToggleButtonStyle.Office2016Colorful;
            this.toggleButton1.ToggleStateChanged += new Syncfusion.Windows.Forms.Tools.ToggleStateChangedEventHandler(this.ToggleButton1_ToggleStateChanged);
            // 
            // L_ToggleButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toggleButton1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "L_ToggleButton";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.Size = new System.Drawing.Size(666, 32);
            ((System.ComponentModel.ISupportInitialize)(this.toggleButton1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.ToggleButton toggleButton1;
    }
}
