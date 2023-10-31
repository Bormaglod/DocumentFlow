namespace DocumentFlow.Dialogs
{
    partial class OperationsPerformedDialog
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
            dateTime = new Controls.Editors.DfDateTimePicker();
            selectOperation = new Controls.Editors.DfDirectorySelectBox();
            textSpecMaterial = new Controls.Editors.DfTextBox();
            selectMaterial = new Controls.Editors.DfDirectorySelectBox();
            selectEmp = new Controls.Editors.DfDirectorySelectBox();
            textQuantity = new Controls.Editors.DfIntegerTextBox();
            checkDoubleRate = new Controls.Editors.DfCheckBox();
            checkSkipMaterial = new Controls.Editors.DfCheckBox();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            buttonCancel.AccessibleName = "Button";
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancel.Location = new Point(474, 275);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(112, 28);
            buttonCancel.TabIndex = 110;
            buttonCancel.Text = "Отменить";
            // 
            // buttonOk
            // 
            buttonOk.AccessibleName = "Button";
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonOk.Location = new Point(355, 275);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(112, 28);
            buttonOk.TabIndex = 109;
            buttonOk.Text = "Сохранить";
            buttonOk.UseVisualStyleBackColor = false;
            buttonOk.Click += ButtonOk_Click;
            // 
            // dateTime
            // 
            dateTime.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateTime.Dock = DockStyle.Top;
            dateTime.EditorFitToSize = true;
            dateTime.EditorWidth = 372;
            dateTime.EnabledEditor = true;
            dateTime.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateTime.Format = DateTimePickerFormat.Custom;
            dateTime.Header = "Дата/время";
            dateTime.HeaderAutoSize = false;
            dateTime.HeaderDock = DockStyle.Left;
            dateTime.HeaderTextAlign = ContentAlignment.TopLeft;
            dateTime.HeaderVisible = true;
            dateTime.HeaderWidth = 210;
            dateTime.Location = new Point(16, 16);
            dateTime.Margin = new Padding(3, 5, 3, 5);
            dateTime.Name = "dateTime";
            dateTime.Padding = new Padding(0, 0, 0, 7);
            dateTime.Required = true;
            dateTime.Size = new Size(582, 32);
            dateTime.TabIndex = 102;
            // 
            // selectOperation
            // 
            selectOperation.CanSelectFolder = false;
            selectOperation.Dock = DockStyle.Top;
            selectOperation.EditorFitToSize = true;
            selectOperation.EditorWidth = 372;
            selectOperation.EnabledEditor = true;
            selectOperation.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectOperation.Header = "Операция";
            selectOperation.HeaderAutoSize = false;
            selectOperation.HeaderDock = DockStyle.Left;
            selectOperation.HeaderTextAlign = ContentAlignment.TopLeft;
            selectOperation.HeaderVisible = true;
            selectOperation.HeaderWidth = 210;
            selectOperation.Location = new Point(16, 48);
            selectOperation.Margin = new Padding(3, 4, 3, 4);
            selectOperation.Name = "selectOperation";
            selectOperation.Padding = new Padding(0, 0, 0, 7);
            selectOperation.RemoveEmptyFolders = true;
            selectOperation.ShowDeleteButton = true;
            selectOperation.ShowOpenButton = false;
            selectOperation.Size = new Size(582, 32);
            selectOperation.TabIndex = 103;
            selectOperation.SelectedItemChanged += SelectOperation_SelectedItemChanged;
            // 
            // textSpecMaterial
            // 
            textSpecMaterial.Dock = DockStyle.Top;
            textSpecMaterial.EditorFitToSize = true;
            textSpecMaterial.EditorWidth = 372;
            textSpecMaterial.EnabledEditor = false;
            textSpecMaterial.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textSpecMaterial.Header = "Материал (по спецификации)";
            textSpecMaterial.HeaderAutoSize = false;
            textSpecMaterial.HeaderDock = DockStyle.Left;
            textSpecMaterial.HeaderTextAlign = ContentAlignment.TopLeft;
            textSpecMaterial.HeaderVisible = true;
            textSpecMaterial.HeaderWidth = 210;
            textSpecMaterial.Location = new Point(16, 80);
            textSpecMaterial.Margin = new Padding(3, 4, 3, 4);
            textSpecMaterial.Multiline = false;
            textSpecMaterial.Name = "textSpecMaterial";
            textSpecMaterial.Padding = new Padding(0, 0, 0, 7);
            textSpecMaterial.Size = new Size(582, 32);
            textSpecMaterial.TabIndex = 104;
            textSpecMaterial.TextValue = "";
            // 
            // selectMaterial
            // 
            selectMaterial.CanSelectFolder = false;
            selectMaterial.Dock = DockStyle.Top;
            selectMaterial.EditorFitToSize = true;
            selectMaterial.EditorWidth = 372;
            selectMaterial.EnabledEditor = true;
            selectMaterial.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectMaterial.Header = "Использованный материал";
            selectMaterial.HeaderAutoSize = false;
            selectMaterial.HeaderDock = DockStyle.Left;
            selectMaterial.HeaderTextAlign = ContentAlignment.TopLeft;
            selectMaterial.HeaderVisible = true;
            selectMaterial.HeaderWidth = 210;
            selectMaterial.Location = new Point(16, 112);
            selectMaterial.Margin = new Padding(3, 4, 3, 4);
            selectMaterial.Name = "selectMaterial";
            selectMaterial.Padding = new Padding(0, 0, 0, 7);
            selectMaterial.RemoveEmptyFolders = false;
            selectMaterial.ShowDeleteButton = true;
            selectMaterial.ShowOpenButton = false;
            selectMaterial.Size = new Size(582, 32);
            selectMaterial.TabIndex = 105;
            // 
            // selectEmp
            // 
            selectEmp.CanSelectFolder = false;
            selectEmp.Dock = DockStyle.Top;
            selectEmp.EditorFitToSize = true;
            selectEmp.EditorWidth = 372;
            selectEmp.EnabledEditor = true;
            selectEmp.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectEmp.Header = "Исполнитель";
            selectEmp.HeaderAutoSize = false;
            selectEmp.HeaderDock = DockStyle.Left;
            selectEmp.HeaderTextAlign = ContentAlignment.TopLeft;
            selectEmp.HeaderVisible = true;
            selectEmp.HeaderWidth = 210;
            selectEmp.Location = new Point(16, 176);
            selectEmp.Margin = new Padding(3, 4, 3, 4);
            selectEmp.Name = "selectEmp";
            selectEmp.Padding = new Padding(0, 0, 0, 7);
            selectEmp.RemoveEmptyFolders = false;
            selectEmp.ShowDeleteButton = true;
            selectEmp.ShowOpenButton = false;
            selectEmp.Size = new Size(582, 32);
            selectEmp.TabIndex = 106;
            // 
            // textQuantity
            // 
            textQuantity.Dock = DockStyle.Top;
            textQuantity.EditorFitToSize = true;
            textQuantity.EditorWidth = 372;
            textQuantity.EnabledEditor = true;
            textQuantity.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textQuantity.Header = "Количество";
            textQuantity.HeaderAutoSize = false;
            textQuantity.HeaderDock = DockStyle.Left;
            textQuantity.HeaderTextAlign = ContentAlignment.TopLeft;
            textQuantity.HeaderVisible = true;
            textQuantity.HeaderWidth = 210;
            textQuantity.IntegerValue = 0L;
            textQuantity.Location = new Point(16, 208);
            textQuantity.Margin = new Padding(3, 4, 3, 4);
            textQuantity.Name = "textQuantity";
            textQuantity.NumberGroupSeparator = " ";
            textQuantity.NumberGroupSizes = new int[] { 3 };
            textQuantity.Padding = new Padding(0, 0, 0, 7);
            textQuantity.ShowSuffix = false;
            textQuantity.Size = new Size(582, 32);
            textQuantity.Suffix = "суффикс";
            textQuantity.TabIndex = 107;
            // 
            // checkDoubleRate
            // 
            checkDoubleRate.AllowThreeState = true;
            checkDoubleRate.CheckValue = null;
            checkDoubleRate.Dock = DockStyle.Top;
            checkDoubleRate.EditorFitToSize = false;
            checkDoubleRate.EditorWidth = 20;
            checkDoubleRate.EnabledEditor = true;
            checkDoubleRate.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            checkDoubleRate.Header = "Двойная оплата";
            checkDoubleRate.HeaderAutoSize = false;
            checkDoubleRate.HeaderDock = DockStyle.Left;
            checkDoubleRate.HeaderTextAlign = ContentAlignment.TopLeft;
            checkDoubleRate.HeaderVisible = true;
            checkDoubleRate.HeaderWidth = 210;
            checkDoubleRate.Location = new Point(16, 240);
            checkDoubleRate.Margin = new Padding(4, 3, 4, 3);
            checkDoubleRate.Name = "checkDoubleRate";
            checkDoubleRate.Padding = new Padding(0, 0, 0, 9);
            checkDoubleRate.Size = new Size(582, 32);
            checkDoubleRate.TabIndex = 108;
            // 
            // checkSkipMaterial
            // 
            checkSkipMaterial.AllowThreeState = false;
            checkSkipMaterial.CheckValue = null;
            checkSkipMaterial.Dock = DockStyle.Top;
            checkSkipMaterial.EditorFitToSize = false;
            checkSkipMaterial.EditorWidth = 20;
            checkSkipMaterial.EnabledEditor = true;
            checkSkipMaterial.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            checkSkipMaterial.Header = "Материал не использовать";
            checkSkipMaterial.HeaderAutoSize = false;
            checkSkipMaterial.HeaderDock = DockStyle.Left;
            checkSkipMaterial.HeaderTextAlign = ContentAlignment.TopLeft;
            checkSkipMaterial.HeaderVisible = true;
            checkSkipMaterial.HeaderWidth = 210;
            checkSkipMaterial.Location = new Point(16, 144);
            checkSkipMaterial.Margin = new Padding(4, 3, 4, 3);
            checkSkipMaterial.Name = "checkSkipMaterial";
            checkSkipMaterial.Padding = new Padding(0, 0, 0, 9);
            checkSkipMaterial.Size = new Size(582, 32);
            checkSkipMaterial.TabIndex = 111;
            checkSkipMaterial.CheckValueChanged += CheckSkipMaterial_CheckValueChanged;
            // 
            // OperationsPerformedDialog
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            CancelButton = buttonCancel;
            ClientSize = new Size(614, 315);
            Controls.Add(checkDoubleRate);
            Controls.Add(textQuantity);
            Controls.Add(selectEmp);
            Controls.Add(checkSkipMaterial);
            Controls.Add(selectMaterial);
            Controls.Add(textSpecMaterial);
            Controls.Add(selectOperation);
            Controls.Add(dateTime);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(490, 354);
            Name = "OperationsPerformedDialog";
            Padding = new Padding(16, 16, 16, 0);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Выполненные работы";
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Controls.Editors.DfDateTimePicker dateTime;
        private Controls.Editors.DfDirectorySelectBox selectOperation;
        private Controls.Editors.DfTextBox textSpecMaterial;
        private Controls.Editors.DfDirectorySelectBox selectMaterial;
        private Controls.Editors.DfDirectorySelectBox selectEmp;
        private Controls.Editors.DfIntegerTextBox textQuantity;
        private Controls.Editors.DfCheckBox checkDoubleRate;
        private Controls.Editors.DfCheckBox checkSkipMaterial;
    }
}