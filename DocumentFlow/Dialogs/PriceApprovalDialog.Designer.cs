namespace DocumentFlow.Dialogs
{
    partial class PriceApprovalDialog
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
            selectProduct = new Controls.Editors.DfDirectorySelectBox();
            textPrice = new Controls.Editors.DfCurrencyTextBox();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            buttonCancel.AccessibleName = "Button";
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancel.Location = new Point(333, 78);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(96, 32);
            buttonCancel.TabIndex = 1001;
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
            buttonOk.Location = new Point(231, 78);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(96, 32);
            buttonOk.Style.BackColor = Color.FromArgb(242, 242, 242);
            buttonOk.TabIndex = 1000;
            buttonOk.Text = "Сохранить";
            buttonOk.UseVisualStyleBackColor = false;
            buttonOk.Click += ButtonOk_Click;
            // 
            // selectProduct
            // 
            selectProduct.CanSelectFolder = false;
            selectProduct.Dock = DockStyle.Top;
            selectProduct.EditorFitToSize = true;
            selectProduct.EditorWidth = 282;
            selectProduct.EnabledEditor = true;
            selectProduct.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectProduct.Header = "Материал / Изделие";
            selectProduct.HeaderAutoSize = false;
            selectProduct.HeaderDock = DockStyle.Left;
            selectProduct.HeaderTextAlign = ContentAlignment.TopLeft;
            selectProduct.HeaderVisible = true;
            selectProduct.HeaderWidth = 140;
            selectProduct.Location = new Point(10, 10);
            selectProduct.Margin = new Padding(3, 4, 3, 4);
            selectProduct.Name = "selectProduct";
            selectProduct.Padding = new Padding(0, 0, 0, 7);
            selectProduct.RemoveEmptyFolders = false;
            selectProduct.ShowDeleteButton = true;
            selectProduct.ShowOpenButton = false;
            selectProduct.Size = new Size(422, 32);
            selectProduct.TabIndex = 1002;
            // 
            // textPrice
            // 
            textPrice.CurrencyDecimalDigits = 2;
            textPrice.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textPrice.Dock = DockStyle.Top;
            textPrice.EditorFitToSize = true;
            textPrice.EditorWidth = 282;
            textPrice.EnabledEditor = true;
            textPrice.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPrice.Header = "Цена";
            textPrice.HeaderAutoSize = false;
            textPrice.HeaderDock = DockStyle.Left;
            textPrice.HeaderTextAlign = ContentAlignment.TopLeft;
            textPrice.HeaderVisible = true;
            textPrice.HeaderWidth = 140;
            textPrice.Location = new Point(10, 42);
            textPrice.Margin = new Padding(3, 4, 3, 4);
            textPrice.Name = "textPrice";
            textPrice.Padding = new Padding(0, 0, 0, 7);
            textPrice.Size = new Size(422, 32);
            textPrice.TabIndex = 1003;
            // 
            // PriceApprovalDialog
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            CancelButton = buttonCancel;
            ClientSize = new Size(442, 123);
            Controls.Add(textPrice);
            Controls.Add(selectProduct);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PriceApprovalDialog";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Материал / Изделие";
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Controls.Editors.DfDirectorySelectBox selectProduct;
        private Controls.Editors.DfCurrencyTextBox textPrice;
    }
}