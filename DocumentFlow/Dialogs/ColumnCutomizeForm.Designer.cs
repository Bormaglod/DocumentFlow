namespace DocumentFlow.Dialogs
{
    partial class ColumnCutomizeForm
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
            Syncfusion.Windows.Forms.Tools.ActiveStateCollection activeStateCollection1 = new Syncfusion.Windows.Forms.Tools.ActiveStateCollection();
            Syncfusion.Windows.Forms.Tools.InactiveStateCollection inactiveStateCollection1 = new Syncfusion.Windows.Forms.Tools.InactiveStateCollection();
            Syncfusion.Windows.Forms.Tools.SliderCollection sliderCollection1 = new Syncfusion.Windows.Forms.Tools.SliderCollection();
            this.buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.buttonOk = new Syncfusion.WinForms.Controls.SfButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.textWidth = new Syncfusion.Windows.Forms.Tools.IntegerTextBox();
            this.comboAutoSize = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonVisible = new Syncfusion.Windows.Forms.Tools.ToggleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboAutoSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonVisible)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleName = "Button";
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancel.Location = new System.Drawing.Point(387, 101);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 28);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отменить";
            // 
            // buttonOk
            // 
            this.buttonOk.AccessibleName = "Button";
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonOk.Location = new System.Drawing.Point(268, 101);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(112, 28);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Сохранить";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Наименование";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Авторазмер";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(323, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ширина";
            // 
            // textName
            // 
            this.textName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textName.BeforeTouchSize = new System.Drawing.Size(118, 23);
            this.textName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textName.Location = new System.Drawing.Point(112, 9);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(387, 23);
            this.textName.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textName.TabIndex = 9;
            this.textName.ThemeName = "Office2016Colorful";
            // 
            // textWidth
            // 
            this.textWidth.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textWidth.BeforeTouchSize = new System.Drawing.Size(118, 23);
            this.textWidth.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.textWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textWidth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textWidth.IntegerValue = ((long)(0));
            this.textWidth.Location = new System.Drawing.Point(381, 38);
            this.textWidth.MinValue = ((long)(0));
            this.textWidth.Name = "textWidth";
            this.textWidth.PositiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textWidth.Size = new System.Drawing.Size(118, 23);
            this.textWidth.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016Colorful;
            this.textWidth.TabIndex = 10;
            this.textWidth.Text = "0";
            this.textWidth.ThemeName = "Office2016Colorful";
            this.textWidth.ZeroColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            // 
            // comboAutoSize
            // 
            this.comboAutoSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboAutoSize.BeforeTouchSize = new System.Drawing.Size(200, 23);
            this.comboAutoSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAutoSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.comboAutoSize.Items.AddRange(new object[] {
            "Нет",
            "По содержимому колонки",
            "По ширине заголовка",
            "Пропорционально"});
            this.comboAutoSize.Location = new System.Drawing.Point(112, 38);
            this.comboAutoSize.Name = "comboAutoSize";
            this.comboAutoSize.Size = new System.Drawing.Size(200, 23);
            this.comboAutoSize.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.comboAutoSize.TabIndex = 11;
            this.comboAutoSize.Text = "Нет";
            this.comboAutoSize.ThemeName = "Office2016Colorful";
            this.comboAutoSize.SelectedIndexChanging += new Syncfusion.Windows.Forms.Tools.SelectedIndexChangingHandler(this.ComboBoxAdv1_SelectedIndexChanging);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Visible";
            // 
            // buttonVisible
            // 
            activeStateCollection1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            activeStateCollection1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            activeStateCollection1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            activeStateCollection1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(176)))));
            this.buttonVisible.ActiveState = activeStateCollection1;
            this.buttonVisible.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonVisible.ForeColor = System.Drawing.Color.Black;
            inactiveStateCollection1.BackColor = System.Drawing.Color.White;
            inactiveStateCollection1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            inactiveStateCollection1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            inactiveStateCollection1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonVisible.InactiveState = inactiveStateCollection1;
            this.buttonVisible.Location = new System.Drawing.Point(112, 67);
            this.buttonVisible.MinimumSize = new System.Drawing.Size(52, 20);
            this.buttonVisible.Name = "buttonVisible";
            this.buttonVisible.Size = new System.Drawing.Size(60, 23);
            sliderCollection1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sliderCollection1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            sliderCollection1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            sliderCollection1.InactiveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            sliderCollection1.InactiveHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.buttonVisible.Slider = sliderCollection1;
            this.buttonVisible.TabIndex = 13;
            this.buttonVisible.ThemeName = "Office2016Colorful";
            this.buttonVisible.VisualStyle = Syncfusion.Windows.Forms.Tools.ToggleButtonStyle.Office2016Colorful;
            // 
            // ColumnCutomizeForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(511, 141);
            this.Controls.Add(this.buttonVisible);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboAutoSize);
            this.Controls.Add(this.textWidth);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColumnCutomizeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Колонка";
            ((System.ComponentModel.ISupportInitialize)(this.textName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboAutoSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonVisible)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Label label1;
        private Label label2;
        private Label label3;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textName;
        private Syncfusion.Windows.Forms.Tools.IntegerTextBox textWidth;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboAutoSize;
        private Label label4;
        private Syncfusion.Windows.Forms.Tools.ToggleButton buttonVisible;
    }
}