namespace DocumentFlow.Dialogs
{
    partial class ProductPriceDialog
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
            textAmount = new Controls.Editors.DfNumericTextBox();
            textPrice = new Controls.Editors.DfCurrencyTextBox();
            textSumma = new Controls.Editors.DfCurrencyTextBox();
            choiceVat = new Controls.Editors.DfChoice();
            textVat = new Controls.Editors.DfCurrencyTextBox();
            textFullSumma = new Controls.Editors.DfCurrencyTextBox();
            selectCalc = new Controls.Editors.DfDirectorySelectBox();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            buttonCancel.AccessibleName = "Button";
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancel.Location = new Point(328, 278);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(96, 32);
            buttonCancel.TabIndex = 1010;
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
            buttonOk.Location = new Point(226, 278);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(96, 32);
            buttonOk.Style.BackColor = Color.FromArgb(242, 242, 242);
            buttonOk.TabIndex = 1009;
            buttonOk.Text = "Сохранить";
            buttonOk.UseVisualStyleBackColor = false;
            buttonOk.Click += ButtonOk_Click;
            // 
            // selectProduct
            // 
            selectProduct.CanSelectFolder = false;
            selectProduct.Dock = DockStyle.Top;
            selectProduct.EditorFitToSize = true;
            selectProduct.EditorWidth = 294;
            selectProduct.EnabledEditor = true;
            selectProduct.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectProduct.Header = "Товар/материал";
            selectProduct.HeaderAutoSize = false;
            selectProduct.HeaderDock = DockStyle.Left;
            selectProduct.HeaderTextAlign = ContentAlignment.TopLeft;
            selectProduct.HeaderVisible = true;
            selectProduct.HeaderWidth = 120;
            selectProduct.Location = new Point(10, 10);
            selectProduct.Margin = new Padding(3, 4, 3, 4);
            selectProduct.Name = "selectProduct";
            selectProduct.Padding = new Padding(0, 0, 0, 7);
            selectProduct.RemoveEmptyFolders = true;
            selectProduct.ShowDeleteButton = false;
            selectProduct.ShowOpenButton = false;
            selectProduct.Size = new Size(414, 32);
            selectProduct.TabIndex = 1001;
            selectProduct.SelectedItemChanged += SelectProduct_SelectedItemChanged;
            selectProduct.UserDocumentModified += SelectProduct_DocumentSelectedChanged;
            // 
            // textAmount
            // 
            textAmount.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textAmount.Dock = DockStyle.Top;
            textAmount.EditorFitToSize = true;
            textAmount.EditorWidth = 294;
            textAmount.EnabledEditor = true;
            textAmount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textAmount.Header = "Количество";
            textAmount.HeaderAutoSize = false;
            textAmount.HeaderDock = DockStyle.Left;
            textAmount.HeaderTextAlign = ContentAlignment.TopLeft;
            textAmount.HeaderVisible = true;
            textAmount.HeaderWidth = 120;
            textAmount.Location = new Point(10, 74);
            textAmount.Margin = new Padding(3, 4, 3, 4);
            textAmount.Name = "textAmount";
            textAmount.NumberDecimalDigits = 3;
            textAmount.Padding = new Padding(0, 0, 0, 7);
            textAmount.ShowSuffix = false;
            textAmount.Size = new Size(414, 32);
            textAmount.Suffix = "суффикс";
            textAmount.TabIndex = 1003;
            textAmount.DecimalValueChanged += AmountOrPriceChanged;
            // 
            // textPrice
            // 
            textPrice.CurrencyDecimalDigits = 2;
            textPrice.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textPrice.Dock = DockStyle.Top;
            textPrice.EditorFitToSize = true;
            textPrice.EditorWidth = 294;
            textPrice.EnabledEditor = true;
            textPrice.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPrice.Header = "Цена";
            textPrice.HeaderAutoSize = false;
            textPrice.HeaderDock = DockStyle.Left;
            textPrice.HeaderTextAlign = ContentAlignment.TopLeft;
            textPrice.HeaderVisible = true;
            textPrice.HeaderWidth = 120;
            textPrice.Location = new Point(10, 106);
            textPrice.Margin = new Padding(3, 4, 3, 4);
            textPrice.Name = "textPrice";
            textPrice.Padding = new Padding(0, 0, 0, 7);
            textPrice.Size = new Size(414, 32);
            textPrice.TabIndex = 1004;
            textPrice.DecimalValueChanged += AmountOrPriceChanged;
            // 
            // textSumma
            // 
            textSumma.CurrencyDecimalDigits = 2;
            textSumma.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textSumma.Dock = DockStyle.Top;
            textSumma.EditorFitToSize = true;
            textSumma.EditorWidth = 294;
            textSumma.EnabledEditor = true;
            textSumma.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textSumma.Header = "Сумма";
            textSumma.HeaderAutoSize = false;
            textSumma.HeaderDock = DockStyle.Left;
            textSumma.HeaderTextAlign = ContentAlignment.TopLeft;
            textSumma.HeaderVisible = true;
            textSumma.HeaderWidth = 120;
            textSumma.Location = new Point(10, 138);
            textSumma.Margin = new Padding(3, 4, 3, 4);
            textSumma.Name = "textSumma";
            textSumma.Padding = new Padding(0, 0, 0, 7);
            textSumma.Size = new Size(414, 32);
            textSumma.TabIndex = 1005;
            textSumma.DecimalValueChanged += CostOrTaxChanged;
            // 
            // choiceVat
            // 
            choiceVat.Dock = DockStyle.Top;
            choiceVat.EditorFitToSize = true;
            choiceVat.EditorWidth = 294;
            choiceVat.EnabledEditor = true;
            choiceVat.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            choiceVat.Header = "НДС%";
            choiceVat.HeaderAutoSize = false;
            choiceVat.HeaderDock = DockStyle.Left;
            choiceVat.HeaderTextAlign = ContentAlignment.TopLeft;
            choiceVat.HeaderVisible = true;
            choiceVat.HeaderWidth = 120;
            choiceVat.Location = new Point(10, 170);
            choiceVat.Margin = new Padding(3, 4, 3, 4);
            choiceVat.Name = "choiceVat";
            choiceVat.Padding = new Padding(0, 0, 0, 7);
            choiceVat.ShowDeleteButton = true;
            choiceVat.Size = new Size(414, 32);
            choiceVat.TabIndex = 1006;
            choiceVat.ChoiceValueChanged += ChoiceVat_ChoiceValueChanged;
            // 
            // textVat
            // 
            textVat.CurrencyDecimalDigits = 2;
            textVat.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textVat.Dock = DockStyle.Top;
            textVat.EditorFitToSize = true;
            textVat.EditorWidth = 294;
            textVat.EnabledEditor = true;
            textVat.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textVat.Header = "НДС";
            textVat.HeaderAutoSize = false;
            textVat.HeaderDock = DockStyle.Left;
            textVat.HeaderTextAlign = ContentAlignment.TopLeft;
            textVat.HeaderVisible = true;
            textVat.HeaderWidth = 120;
            textVat.Location = new Point(10, 202);
            textVat.Margin = new Padding(3, 4, 3, 4);
            textVat.Name = "textVat";
            textVat.Padding = new Padding(0, 0, 0, 7);
            textVat.Size = new Size(414, 32);
            textVat.TabIndex = 1007;
            textVat.DecimalValueChanged += CostOrTaxChanged;
            // 
            // textFullSumma
            // 
            textFullSumma.CurrencyDecimalDigits = 2;
            textFullSumma.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textFullSumma.Dock = DockStyle.Top;
            textFullSumma.EditorFitToSize = true;
            textFullSumma.EditorWidth = 294;
            textFullSumma.EnabledEditor = true;
            textFullSumma.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textFullSumma.Header = "Всего с НДС";
            textFullSumma.HeaderAutoSize = false;
            textFullSumma.HeaderDock = DockStyle.Left;
            textFullSumma.HeaderTextAlign = ContentAlignment.TopLeft;
            textFullSumma.HeaderVisible = true;
            textFullSumma.HeaderWidth = 120;
            textFullSumma.Location = new Point(10, 234);
            textFullSumma.Margin = new Padding(3, 4, 3, 4);
            textFullSumma.Name = "textFullSumma";
            textFullSumma.Padding = new Padding(0, 0, 0, 7);
            textFullSumma.Size = new Size(414, 32);
            textFullSumma.TabIndex = 1008;
            // 
            // selectCalc
            // 
            selectCalc.CanSelectFolder = false;
            selectCalc.Dock = DockStyle.Top;
            selectCalc.EditorFitToSize = true;
            selectCalc.EditorWidth = 294;
            selectCalc.EnabledEditor = true;
            selectCalc.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectCalc.Header = "Калькуляция";
            selectCalc.HeaderAutoSize = false;
            selectCalc.HeaderDock = DockStyle.Left;
            selectCalc.HeaderTextAlign = ContentAlignment.TopLeft;
            selectCalc.HeaderVisible = true;
            selectCalc.HeaderWidth = 120;
            selectCalc.Location = new Point(10, 42);
            selectCalc.Margin = new Padding(3, 4, 3, 4);
            selectCalc.Name = "selectCalc";
            selectCalc.Padding = new Padding(0, 0, 0, 7);
            selectCalc.RemoveEmptyFolders = false;
            selectCalc.ShowDeleteButton = false;
            selectCalc.ShowOpenButton = false;
            selectCalc.Size = new Size(414, 32);
            selectCalc.TabIndex = 1002;
            // 
            // ProductPriceDialog
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            CancelButton = buttonCancel;
            ClientSize = new Size(434, 323);
            Controls.Add(textFullSumma);
            Controls.Add(textVat);
            Controls.Add(choiceVat);
            Controls.Add(textSumma);
            Controls.Add(textPrice);
            Controls.Add(textAmount);
            Controls.Add(selectCalc);
            Controls.Add(selectProduct);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProductPriceDialog";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Изделие";
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Controls.Editors.DfDirectorySelectBox selectProduct;
        private Controls.Editors.DfNumericTextBox textAmount;
        private Controls.Editors.DfCurrencyTextBox textPrice;
        private Controls.Editors.DfCurrencyTextBox textSumma;
        private Controls.Editors.DfChoice choiceVat;
        private Controls.Editors.DfCurrencyTextBox textVat;
        private Controls.Editors.DfCurrencyTextBox textFullSumma;
        private Controls.Editors.DfDirectorySelectBox selectCalc;
    }
}