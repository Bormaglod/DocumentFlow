namespace DocumentFlow.Dialogs
{
    partial class WageEmployeeDialog
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
            selectEmp = new Controls.Editors.DfDirectorySelectBox();
            textSalary = new Controls.Editors.DfCurrencyTextBox();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            buttonCancel.AccessibleName = "Button";
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancel.Location = new Point(298, 76);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(96, 32);
            buttonCancel.TabIndex = 3;
            buttonCancel.Text = "Отменить";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            buttonOk.AccessibleName = "Button";
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.BackColor = Color.FromArgb(242, 242, 242);
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonOk.Location = new Point(196, 76);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(96, 32);
            buttonOk.Style.BackColor = Color.FromArgb(242, 242, 242);
            buttonOk.TabIndex = 2;
            buttonOk.Text = "Сохранить";
            buttonOk.UseVisualStyleBackColor = false;
            buttonOk.Click += ButtonOk_Click;
            // 
            // selectEmp
            // 
            selectEmp.CanSelectFolder = false;
            selectEmp.Dock = DockStyle.Top;
            selectEmp.EditorFitToSize = true;
            selectEmp.EditorWidth = 304;
            selectEmp.EnabledEditor = true;
            selectEmp.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectEmp.Header = "Сотрудник";
            selectEmp.HeaderAutoSize = false;
            selectEmp.HeaderDock = DockStyle.Left;
            selectEmp.HeaderTextAlign = ContentAlignment.TopLeft;
            selectEmp.HeaderVisible = true;
            selectEmp.HeaderWidth = 80;
            selectEmp.Location = new Point(10, 10);
            selectEmp.Margin = new Padding(3, 4, 3, 4);
            selectEmp.Name = "selectEmp";
            selectEmp.Padding = new Padding(0, 0, 0, 7);
            selectEmp.RemoveEmptyFolders = false;
            selectEmp.ShowDeleteButton = false;
            selectEmp.ShowOpenButton = false;
            selectEmp.Size = new Size(384, 32);
            selectEmp.TabIndex = 4;
            // 
            // textSalary
            // 
            textSalary.CurrencyDecimalDigits = 2;
            textSalary.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textSalary.Dock = DockStyle.Top;
            textSalary.EditorFitToSize = true;
            textSalary.EditorWidth = 304;
            textSalary.EnabledEditor = true;
            textSalary.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textSalary.Header = "Зар. плата";
            textSalary.HeaderAutoSize = false;
            textSalary.HeaderDock = DockStyle.Left;
            textSalary.HeaderTextAlign = ContentAlignment.TopLeft;
            textSalary.HeaderVisible = true;
            textSalary.HeaderWidth = 80;
            textSalary.Location = new Point(10, 42);
            textSalary.Margin = new Padding(3, 4, 3, 4);
            textSalary.Name = "textSalary";
            textSalary.Padding = new Padding(0, 0, 0, 7);
            textSalary.Size = new Size(384, 32);
            textSalary.TabIndex = 5;
            // 
            // WageEmployeeDialog
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            CancelButton = buttonCancel;
            ClientSize = new Size(404, 121);
            Controls.Add(textSalary);
            Controls.Add(selectEmp);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(420, 160);
            Name = "WageEmployeeDialog";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Заработная плата сотрудника";
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Controls.Editors.DfDirectorySelectBox selectEmp;
        private Controls.Editors.DfCurrencyTextBox textSalary;
    }
}