namespace DocumentFlow.Dialogs
{
    partial class CodeGeneratorDialog
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
            buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            buttonOk = new Syncfusion.WinForms.Controls.SfButton();
            labelCode = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            comboType = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            comboBrand = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            comboModel = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            label5 = new Label();
            label6 = new Label();
            panelType = new Panel();
            panelBrand = new Panel();
            panelModel = new Panel();
            panelEngineFrom = new Panel();
            comboEngineFrom = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            label7 = new Label();
            panelEngineTo = new Panel();
            comboEngineTo = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            label8 = new Label();
            panelNumber = new Panel();
            numericNumber = new Syncfusion.Windows.Forms.Tools.NumericUpDownExt();
            panelMod = new Panel();
            numericMod = new Syncfusion.Windows.Forms.Tools.NumericUpDownExt();
            ((System.ComponentModel.ISupportInitialize)comboType).BeginInit();
            ((System.ComponentModel.ISupportInitialize)comboBrand).BeginInit();
            ((System.ComponentModel.ISupportInitialize)comboModel).BeginInit();
            panelType.SuspendLayout();
            panelBrand.SuspendLayout();
            panelModel.SuspendLayout();
            panelEngineFrom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)comboEngineFrom).BeginInit();
            panelEngineTo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)comboEngineTo).BeginInit();
            panelNumber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericNumber).BeginInit();
            panelMod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericMod).BeginInit();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            buttonCancel.AccessibleName = "Button";
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancel.Location = new Point(211, 263);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(112, 28);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Отменить";
            // 
            // buttonOk
            // 
            buttonOk.AccessibleName = "Button";
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonOk.Location = new Point(93, 263);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(112, 28);
            buttonOk.TabIndex = 4;
            buttonOk.Text = "Сохранить";
            buttonOk.UseVisualStyleBackColor = false;
            // 
            // labelCode
            // 
            labelCode.Dock = DockStyle.Top;
            labelCode.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelCode.Location = new Point(8, 8);
            labelCode.Name = "labelCode";
            labelCode.Size = new Size(318, 46);
            labelCode.TabIndex = 6;
            labelCode.Text = "0.000.3724000.00";
            labelCode.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Left;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 21);
            label2.TabIndex = 7;
            label2.Text = "Тип изделия";
            // 
            // label3
            // 
            label3.Dock = DockStyle.Left;
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 21);
            label3.TabIndex = 8;
            label3.Text = "Модель";
            // 
            // label4
            // 
            label4.Dock = DockStyle.Left;
            label4.Location = new Point(0, 0);
            label4.Name = "label4";
            label4.Size = new Size(100, 21);
            label4.TabIndex = 9;
            label4.Text = "Бренд";
            // 
            // comboType
            // 
            comboType.BackColor = Color.FromArgb(255, 255, 255);
            comboType.BeforeTouchSize = new Size(218, 21);
            comboType.Dock = DockStyle.Fill;
            comboType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboType.ForeColor = Color.FromArgb(68, 68, 68);
            comboType.Location = new Point(100, 0);
            comboType.Name = "comboType";
            comboType.Size = new Size(218, 21);
            comboType.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            comboType.TabIndex = 11;
            comboType.ThemeName = "Office2016Colorful";
            // 
            // comboBrand
            // 
            comboBrand.BackColor = Color.FromArgb(255, 255, 255);
            comboBrand.BeforeTouchSize = new Size(218, 21);
            comboBrand.Dock = DockStyle.Fill;
            comboBrand.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBrand.ForeColor = Color.FromArgb(68, 68, 68);
            comboBrand.Location = new Point(100, 0);
            comboBrand.Name = "comboBrand";
            comboBrand.Size = new Size(218, 21);
            comboBrand.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            comboBrand.TabIndex = 12;
            comboBrand.ThemeName = "Office2016Colorful";
            // 
            // comboModel
            // 
            comboModel.BackColor = Color.FromArgb(255, 255, 255);
            comboModel.BeforeTouchSize = new Size(218, 21);
            comboModel.Dock = DockStyle.Fill;
            comboModel.DropDownStyle = ComboBoxStyle.DropDownList;
            comboModel.ForeColor = Color.FromArgb(68, 68, 68);
            comboModel.Location = new Point(100, 0);
            comboModel.Name = "comboModel";
            comboModel.Size = new Size(218, 21);
            comboModel.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            comboModel.TabIndex = 13;
            comboModel.ThemeName = "Office2016Colorful";
            // 
            // label5
            // 
            label5.Dock = DockStyle.Left;
            label5.Location = new Point(0, 0);
            label5.Name = "label5";
            label5.Size = new Size(100, 21);
            label5.TabIndex = 16;
            label5.Text = "Номер изделия";
            // 
            // label6
            // 
            label6.Dock = DockStyle.Left;
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(100, 21);
            label6.TabIndex = 17;
            label6.Text = "Модификация";
            // 
            // panelType
            // 
            panelType.Controls.Add(comboType);
            panelType.Controls.Add(label2);
            panelType.Dock = DockStyle.Top;
            panelType.Location = new Point(8, 54);
            panelType.Name = "panelType";
            panelType.Padding = new Padding(0, 0, 0, 8);
            panelType.Size = new Size(318, 29);
            panelType.TabIndex = 18;
            // 
            // panelBrand
            // 
            panelBrand.Controls.Add(comboBrand);
            panelBrand.Controls.Add(label4);
            panelBrand.Dock = DockStyle.Top;
            panelBrand.Location = new Point(8, 83);
            panelBrand.Name = "panelBrand";
            panelBrand.Padding = new Padding(0, 0, 0, 8);
            panelBrand.Size = new Size(318, 29);
            panelBrand.TabIndex = 19;
            // 
            // panelModel
            // 
            panelModel.Controls.Add(comboModel);
            panelModel.Controls.Add(label3);
            panelModel.Dock = DockStyle.Top;
            panelModel.Location = new Point(8, 112);
            panelModel.Name = "panelModel";
            panelModel.Padding = new Padding(0, 0, 0, 8);
            panelModel.Size = new Size(318, 29);
            panelModel.TabIndex = 20;
            // 
            // panelEngineFrom
            // 
            panelEngineFrom.Controls.Add(comboEngineFrom);
            panelEngineFrom.Controls.Add(label7);
            panelEngineFrom.Dock = DockStyle.Top;
            panelEngineFrom.Location = new Point(8, 141);
            panelEngineFrom.Name = "panelEngineFrom";
            panelEngineFrom.Padding = new Padding(0, 0, 0, 8);
            panelEngineFrom.Size = new Size(318, 29);
            panelEngineFrom.TabIndex = 21;
            // 
            // comboEngineFrom
            // 
            comboEngineFrom.BackColor = Color.FromArgb(255, 255, 255);
            comboEngineFrom.BeforeTouchSize = new Size(218, 21);
            comboEngineFrom.Dock = DockStyle.Fill;
            comboEngineFrom.DropDownStyle = ComboBoxStyle.DropDownList;
            comboEngineFrom.ForeColor = Color.FromArgb(68, 68, 68);
            comboEngineFrom.Location = new Point(100, 0);
            comboEngineFrom.Name = "comboEngineFrom";
            comboEngineFrom.Size = new Size(218, 21);
            comboEngineFrom.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            comboEngineFrom.TabIndex = 1;
            comboEngineFrom.ThemeName = "Office2016Colorful";
            // 
            // label7
            // 
            label7.Dock = DockStyle.Left;
            label7.Location = new Point(0, 0);
            label7.Name = "label7";
            label7.Size = new Size(100, 21);
            label7.TabIndex = 0;
            label7.Text = "Переделать из...";
            // 
            // panelEngineTo
            // 
            panelEngineTo.Controls.Add(comboEngineTo);
            panelEngineTo.Controls.Add(label8);
            panelEngineTo.Dock = DockStyle.Top;
            panelEngineTo.Location = new Point(8, 170);
            panelEngineTo.Name = "panelEngineTo";
            panelEngineTo.Padding = new Padding(0, 0, 0, 8);
            panelEngineTo.Size = new Size(318, 29);
            panelEngineTo.TabIndex = 22;
            // 
            // comboEngineTo
            // 
            comboEngineTo.BackColor = Color.FromArgb(255, 255, 255);
            comboEngineTo.BeforeTouchSize = new Size(218, 21);
            comboEngineTo.Dock = DockStyle.Fill;
            comboEngineTo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboEngineTo.ForeColor = Color.FromArgb(68, 68, 68);
            comboEngineTo.Location = new Point(100, 0);
            comboEngineTo.Name = "comboEngineTo";
            comboEngineTo.Size = new Size(218, 21);
            comboEngineTo.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            comboEngineTo.TabIndex = 1;
            comboEngineTo.ThemeName = "Office2016Colorful";
            // 
            // label8
            // 
            label8.Dock = DockStyle.Left;
            label8.Location = new Point(0, 0);
            label8.Name = "label8";
            label8.Size = new Size(100, 21);
            label8.TabIndex = 0;
            label8.Text = "Переделать в...";
            // 
            // panelNumber
            // 
            panelNumber.Controls.Add(numericNumber);
            panelNumber.Controls.Add(label5);
            panelNumber.Dock = DockStyle.Top;
            panelNumber.Location = new Point(8, 199);
            panelNumber.Name = "panelNumber";
            panelNumber.Padding = new Padding(0, 0, 0, 8);
            panelNumber.Size = new Size(318, 29);
            panelNumber.TabIndex = 23;
            // 
            // numericNumber
            // 
            numericNumber.BackColor = Color.White;
            numericNumber.BeforeTouchSize = new Size(100, 23);
            numericNumber.Border3DStyle = Border3DStyle.Flat;
            numericNumber.BorderColor = Color.FromArgb(197, 197, 197);
            numericNumber.BorderStyle = BorderStyle.FixedSingle;
            numericNumber.Dock = DockStyle.Left;
            numericNumber.ForeColor = Color.FromArgb(68, 68, 68);
            numericNumber.Location = new Point(100, 0);
            numericNumber.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            numericNumber.Name = "numericNumber";
            numericNumber.Size = new Size(100, 23);
            numericNumber.TabIndex = 17;
            numericNumber.ThemeName = "Office2016Colorful";
            numericNumber.VisualStyle = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            // 
            // panelMod
            // 
            panelMod.Controls.Add(numericMod);
            panelMod.Controls.Add(label6);
            panelMod.Dock = DockStyle.Top;
            panelMod.Location = new Point(8, 228);
            panelMod.Name = "panelMod";
            panelMod.Padding = new Padding(0, 0, 0, 8);
            panelMod.Size = new Size(318, 29);
            panelMod.TabIndex = 24;
            // 
            // numericMod
            // 
            numericMod.BackColor = Color.White;
            numericMod.BeforeTouchSize = new Size(100, 23);
            numericMod.BorderColor = Color.FromArgb(197, 197, 197);
            numericMod.BorderStyle = BorderStyle.FixedSingle;
            numericMod.Dock = DockStyle.Left;
            numericMod.ForeColor = Color.FromArgb(68, 68, 68);
            numericMod.Location = new Point(100, 0);
            numericMod.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            numericMod.Name = "numericMod";
            numericMod.Size = new Size(100, 23);
            numericMod.TabIndex = 18;
            numericMod.ThemeName = "Office2016Colorful";
            numericMod.VisualStyle = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            // 
            // CodeGeneratorDialog
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            CancelButton = buttonCancel;
            ClientSize = new Size(334, 303);
            Controls.Add(panelMod);
            Controls.Add(panelNumber);
            Controls.Add(panelEngineTo);
            Controls.Add(panelEngineFrom);
            Controls.Add(panelModel);
            Controls.Add(panelBrand);
            Controls.Add(panelType);
            Controls.Add(labelCode);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CodeGeneratorDialog";
            Padding = new Padding(8, 8, 8, 0);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Генератор кода изделия";
            ((System.ComponentModel.ISupportInitialize)comboType).EndInit();
            ((System.ComponentModel.ISupportInitialize)comboBrand).EndInit();
            ((System.ComponentModel.ISupportInitialize)comboModel).EndInit();
            panelType.ResumeLayout(false);
            panelBrand.ResumeLayout(false);
            panelModel.ResumeLayout(false);
            panelEngineFrom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)comboEngineFrom).EndInit();
            panelEngineTo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)comboEngineTo).EndInit();
            panelNumber.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericNumber).EndInit();
            panelMod.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericMod).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Label labelCode;
        private Label label2;
        private Label label3;
        private Label label4;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboType;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboBrand;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboModel;
        private Label label5;
        private Label label6;
        private Panel panelType;
        private Panel panelBrand;
        private Panel panelModel;
        private Panel panelEngineFrom;
        private Panel panelEngineTo;
        private Panel panelNumber;
        private Panel panelMod;
        private Label label7;
        private Label label8;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboEngineFrom;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboEngineTo;
        private Syncfusion.Windows.Forms.Tools.NumericUpDownExt numericNumber;
        private Syncfusion.Windows.Forms.Tools.NumericUpDownExt numericMod;
    }
}