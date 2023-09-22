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
            toggleButton1 = new Syncfusion.Windows.Forms.Tools.ToggleButton();
            ((System.ComponentModel.ISupportInitialize)toggleButton1).BeginInit();
            SuspendLayout();
            // 
            // toggleButton1
            // 
            activeStateCollection1.BackColor = Color.FromArgb(1, 115, 199);
            activeStateCollection1.BorderColor = Color.FromArgb(1, 115, 199);
            activeStateCollection1.ForeColor = Color.FromArgb(255, 255, 255);
            activeStateCollection1.HoverColor = Color.FromArgb(0, 103, 176);
            toggleButton1.ActiveState = activeStateCollection1;
            toggleButton1.Dock = DockStyle.Left;
            toggleButton1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            toggleButton1.ForeColor = Color.Black;
            inactiveStateCollection1.BackColor = Color.White;
            inactiveStateCollection1.BorderColor = Color.FromArgb(150, 150, 150);
            inactiveStateCollection1.ForeColor = Color.FromArgb(80, 80, 80);
            inactiveStateCollection1.HoverColor = Color.FromArgb(255, 255, 255);
            toggleButton1.InactiveState = inactiveStateCollection1;
            toggleButton1.Location = new Point(100, 0);
            toggleButton1.MinimumSize = new Size(52, 23);
            toggleButton1.Name = "toggleButton1";
            toggleButton1.Size = new Size(90, 25);
            sliderCollection1.BackColor = Color.FromArgb(255, 255, 255);
            sliderCollection1.BorderColor = Color.FromArgb(171, 171, 171);
            sliderCollection1.HoverColor = Color.FromArgb(255, 255, 255);
            sliderCollection1.InactiveBackColor = Color.FromArgb(150, 150, 150);
            sliderCollection1.InactiveHoverColor = Color.FromArgb(94, 94, 94);
            toggleButton1.Slider = sliderCollection1;
            toggleButton1.TabIndex = 3;
            toggleButton1.Text = "toggleButton1";
            toggleButton1.ThemeName = "Office2016Colorful";
            toggleButton1.VisualStyle = Syncfusion.Windows.Forms.Tools.ToggleButtonStyle.Office2016Colorful;
            toggleButton1.ToggleStateChanged += ToggleButton1_ToggleStateChanged;
            // 
            // DfToggleButton
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(toggleButton1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "DfToggleButton";
            Size = new Size(200, 32);
            Controls.SetChildIndex(toggleButton1, 0);
            ((System.ComponentModel.ISupportInitialize)toggleButton1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.ToggleButton toggleButton1;
    }
}
