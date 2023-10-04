namespace DocumentFlow.Dialogs
{
    partial class MaterialQuantityDialog
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
            selectMaterial = new Controls.Editors.DfDirectorySelectBox();
            textQuantity = new Controls.Editors.DfNumericTextBox();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            buttonCancel.AccessibleName = "Button";
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancel.Location = new Point(302, 86);
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
            buttonOk.Location = new Point(200, 86);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(96, 32);
            buttonOk.Style.BackColor = Color.FromArgb(242, 242, 242);
            buttonOk.TabIndex = 2;
            buttonOk.Text = "Сохранить";
            buttonOk.UseVisualStyleBackColor = false;
            buttonOk.Click += ButtonOk_Click;
            // 
            // selectMaterial
            // 
            selectMaterial.CanSelectFolder = false;
            selectMaterial.Dock = DockStyle.Top;
            selectMaterial.EditorFitToSize = true;
            selectMaterial.EditorWidth = 288;
            selectMaterial.EnabledEditor = true;
            selectMaterial.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectMaterial.Header = "Материал";
            selectMaterial.HeaderAutoSize = false;
            selectMaterial.HeaderDock = DockStyle.Left;
            selectMaterial.HeaderTextAlign = ContentAlignment.TopLeft;
            selectMaterial.HeaderVisible = true;
            selectMaterial.HeaderWidth = 100;
            selectMaterial.Location = new Point(10, 10);
            selectMaterial.Margin = new Padding(3, 4, 3, 4);
            selectMaterial.Name = "selectMaterial";
            selectMaterial.Padding = new Padding(0, 0, 0, 7);
            selectMaterial.RemoveEmptyFolders = false;
            selectMaterial.ShowDeleteButton = false;
            selectMaterial.ShowOpenButton = false;
            selectMaterial.Size = new Size(388, 32);
            selectMaterial.TabIndex = 4;
            // 
            // textQuantity
            // 
            textQuantity.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textQuantity.Dock = DockStyle.Top;
            textQuantity.EditorFitToSize = true;
            textQuantity.EditorWidth = 288;
            textQuantity.EnabledEditor = true;
            textQuantity.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textQuantity.Header = "Количество";
            textQuantity.HeaderAutoSize = false;
            textQuantity.HeaderDock = DockStyle.Left;
            textQuantity.HeaderTextAlign = ContentAlignment.TopLeft;
            textQuantity.HeaderVisible = true;
            textQuantity.HeaderWidth = 100;
            textQuantity.Location = new Point(10, 42);
            textQuantity.Margin = new Padding(3, 4, 3, 4);
            textQuantity.Name = "textQuantity";
            textQuantity.NumberDecimalDigits = 3;
            textQuantity.Padding = new Padding(0, 0, 0, 7);
            textQuantity.ShowSuffix = false;
            textQuantity.Size = new Size(388, 32);
            textQuantity.Suffix = "суффикс";
            textQuantity.TabIndex = 5;
            // 
            // MaterialQuantityDialog
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            CancelButton = buttonCancel;
            ClientSize = new Size(408, 131);
            Controls.Add(textQuantity);
            Controls.Add(selectMaterial);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "MaterialQuantityDialog";
            Padding = new Padding(10);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Материал";
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Controls.Editors.DfDirectorySelectBox selectMaterial;
        private Controls.Editors.DfNumericTextBox textQuantity;
    }
}