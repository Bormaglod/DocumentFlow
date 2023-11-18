namespace DocumentFlow.ViewModels
{
    partial class OperationsPerformedEditor
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
            textDocNumber = new Controls.Editors.DfIntegerTextBox();
            dateDocument = new Controls.Editors.DfDateTimePicker();
            selectLot = new Controls.Editors.DfDirectorySelectBox();
            textProduct = new Controls.Editors.DfTextBox();
            textCalc = new Controls.Editors.DfTextBox();
            selectOper = new Controls.Editors.DfDirectorySelectBox();
            textMaterialSpec = new Controls.Editors.DfTextBox();
            selectMaterial = new Controls.Editors.DfDirectorySelectBox();
            textQuantity = new Controls.Editors.DfIntegerTextBox();
            selectEmp = new Controls.Editors.DfDirectorySelectBox();
            textSalary = new Controls.Editors.DfCurrencyTextBox();
            checkDoubleRate = new Controls.Editors.DfCheckBox();
            checkSkipMaterial = new Controls.Editors.DfCheckBox();
            SuspendLayout();
            // 
            // textDocNumber
            // 
            textDocNumber.Dock = DockStyle.Top;
            textDocNumber.EditorFitToSize = false;
            textDocNumber.EditorWidth = 170;
            textDocNumber.EnabledEditor = true;
            textDocNumber.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textDocNumber.Header = "Номер";
            textDocNumber.HeaderAutoSize = false;
            textDocNumber.HeaderDock = DockStyle.Left;
            textDocNumber.HeaderTextAlign = ContentAlignment.TopLeft;
            textDocNumber.HeaderVisible = true;
            textDocNumber.HeaderWidth = 210;
            textDocNumber.IntegerValue = 0L;
            textDocNumber.Location = new Point(0, 0);
            textDocNumber.Margin = new Padding(3, 4, 3, 4);
            textDocNumber.Name = "textDocNumber";
            textDocNumber.NumberGroupSeparator = " ";
            textDocNumber.NumberGroupSizes = new int[] { 0 };
            textDocNumber.Padding = new Padding(0, 0, 0, 7);
            textDocNumber.ShowSuffix = false;
            textDocNumber.Size = new Size(844, 32);
            textDocNumber.Suffix = "суффикс";
            textDocNumber.TabIndex = 0;
            // 
            // dateDocument
            // 
            dateDocument.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateDocument.Dock = DockStyle.Top;
            dateDocument.EditorFitToSize = false;
            dateDocument.EditorWidth = 170;
            dateDocument.EnabledEditor = true;
            dateDocument.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateDocument.Format = DateTimePickerFormat.Custom;
            dateDocument.Header = "Дата/время";
            dateDocument.HeaderAutoSize = false;
            dateDocument.HeaderDock = DockStyle.Left;
            dateDocument.HeaderTextAlign = ContentAlignment.TopLeft;
            dateDocument.HeaderVisible = true;
            dateDocument.HeaderWidth = 210;
            dateDocument.Location = new Point(0, 32);
            dateDocument.Margin = new Padding(3, 5, 3, 5);
            dateDocument.Name = "dateDocument";
            dateDocument.Padding = new Padding(0, 0, 0, 7);
            dateDocument.Required = true;
            dateDocument.Size = new Size(844, 32);
            dateDocument.TabIndex = 1;
            // 
            // selectLot
            // 
            selectLot.CanSelectFolder = false;
            selectLot.Dock = DockStyle.Top;
            selectLot.EditorFitToSize = false;
            selectLot.EditorWidth = 350;
            selectLot.EnabledEditor = true;
            selectLot.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectLot.Header = "Производственная партия";
            selectLot.HeaderAutoSize = false;
            selectLot.HeaderDock = DockStyle.Left;
            selectLot.HeaderTextAlign = ContentAlignment.TopLeft;
            selectLot.HeaderVisible = true;
            selectLot.HeaderWidth = 210;
            selectLot.Location = new Point(0, 64);
            selectLot.Margin = new Padding(3, 4, 3, 4);
            selectLot.Name = "selectLot";
            selectLot.Padding = new Padding(0, 0, 0, 7);
            selectLot.RemoveEmptyFolders = false;
            selectLot.ShowDeleteButton = true;
            selectLot.ShowOpenButton = true;
            selectLot.Size = new Size(844, 32);
            selectLot.TabIndex = 2;
            selectLot.DeleteButtonClick += SelectLot_DeleteButtonClick;
            selectLot.OpenButtonClick += SelectLot_OpenButtonClick;
            selectLot.SelectedItemChanged += SelectLot_SelectedItemChanged;
            selectLot.UserDocumentModified += SelectLot_DocumentSelectedChanged;
            // 
            // textProduct
            // 
            textProduct.Dock = DockStyle.Top;
            textProduct.EditorFitToSize = false;
            textProduct.EditorWidth = 600;
            textProduct.EnabledEditor = false;
            textProduct.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textProduct.Header = "Изделие";
            textProduct.HeaderAutoSize = false;
            textProduct.HeaderDock = DockStyle.Left;
            textProduct.HeaderTextAlign = ContentAlignment.TopLeft;
            textProduct.HeaderVisible = true;
            textProduct.HeaderWidth = 210;
            textProduct.Location = new Point(0, 96);
            textProduct.Margin = new Padding(3, 4, 3, 4);
            textProduct.Multiline = false;
            textProduct.Name = "textProduct";
            textProduct.Padding = new Padding(0, 0, 0, 7);
            textProduct.Size = new Size(844, 32);
            textProduct.TabIndex = 3;
            textProduct.TextValue = "";
            // 
            // textCalc
            // 
            textCalc.Dock = DockStyle.Top;
            textCalc.EditorFitToSize = false;
            textCalc.EditorWidth = 600;
            textCalc.EnabledEditor = false;
            textCalc.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textCalc.Header = "Калькуляция";
            textCalc.HeaderAutoSize = false;
            textCalc.HeaderDock = DockStyle.Left;
            textCalc.HeaderTextAlign = ContentAlignment.TopLeft;
            textCalc.HeaderVisible = true;
            textCalc.HeaderWidth = 210;
            textCalc.Location = new Point(0, 128);
            textCalc.Margin = new Padding(3, 4, 3, 4);
            textCalc.Multiline = false;
            textCalc.Name = "textCalc";
            textCalc.Padding = new Padding(0, 0, 0, 7);
            textCalc.Size = new Size(844, 32);
            textCalc.TabIndex = 4;
            textCalc.TextValue = "";
            // 
            // selectOper
            // 
            selectOper.CanSelectFolder = false;
            selectOper.Dock = DockStyle.Top;
            selectOper.EditorFitToSize = false;
            selectOper.EditorWidth = 350;
            selectOper.EnabledEditor = true;
            selectOper.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectOper.Header = "Операция";
            selectOper.HeaderAutoSize = false;
            selectOper.HeaderDock = DockStyle.Left;
            selectOper.HeaderTextAlign = ContentAlignment.TopLeft;
            selectOper.HeaderVisible = true;
            selectOper.HeaderWidth = 210;
            selectOper.Location = new Point(0, 160);
            selectOper.Margin = new Padding(3, 4, 3, 4);
            selectOper.Name = "selectOper";
            selectOper.Padding = new Padding(0, 0, 0, 7);
            selectOper.RemoveEmptyFolders = false;
            selectOper.ShowDeleteButton = true;
            selectOper.ShowOpenButton = true;
            selectOper.Size = new Size(844, 32);
            selectOper.TabIndex = 5;
            selectOper.OpenButtonClick += SelectOper_OpenButtonClick;
            selectOper.SelectedItemChanged += SelectOper_SelectedItemChanged;
            // 
            // textMaterialSpec
            // 
            textMaterialSpec.Dock = DockStyle.Top;
            textMaterialSpec.EditorFitToSize = false;
            textMaterialSpec.EditorWidth = 350;
            textMaterialSpec.EnabledEditor = false;
            textMaterialSpec.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textMaterialSpec.Header = "Материал (по спецификации)";
            textMaterialSpec.HeaderAutoSize = false;
            textMaterialSpec.HeaderDock = DockStyle.Left;
            textMaterialSpec.HeaderTextAlign = ContentAlignment.TopLeft;
            textMaterialSpec.HeaderVisible = true;
            textMaterialSpec.HeaderWidth = 210;
            textMaterialSpec.Location = new Point(0, 192);
            textMaterialSpec.Margin = new Padding(3, 4, 3, 4);
            textMaterialSpec.Multiline = false;
            textMaterialSpec.Name = "textMaterialSpec";
            textMaterialSpec.Padding = new Padding(0, 0, 0, 7);
            textMaterialSpec.Size = new Size(844, 32);
            textMaterialSpec.TabIndex = 6;
            textMaterialSpec.TextValue = "";
            // 
            // selectMaterial
            // 
            selectMaterial.CanSelectFolder = false;
            selectMaterial.Dock = DockStyle.Top;
            selectMaterial.EditorFitToSize = false;
            selectMaterial.EditorWidth = 350;
            selectMaterial.EnabledEditor = true;
            selectMaterial.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectMaterial.Header = "Использованный материал";
            selectMaterial.HeaderAutoSize = false;
            selectMaterial.HeaderDock = DockStyle.Left;
            selectMaterial.HeaderTextAlign = ContentAlignment.TopLeft;
            selectMaterial.HeaderVisible = true;
            selectMaterial.HeaderWidth = 210;
            selectMaterial.Location = new Point(0, 224);
            selectMaterial.Margin = new Padding(3, 4, 3, 4);
            selectMaterial.Name = "selectMaterial";
            selectMaterial.Padding = new Padding(0, 0, 0, 7);
            selectMaterial.RemoveEmptyFolders = false;
            selectMaterial.ShowDeleteButton = true;
            selectMaterial.ShowOpenButton = true;
            selectMaterial.Size = new Size(844, 32);
            selectMaterial.TabIndex = 7;
            selectMaterial.OpenButtonClick += SelectMaterial_OpenButtonClick;
            // 
            // textQuantity
            // 
            textQuantity.Dock = DockStyle.Top;
            textQuantity.EditorFitToSize = false;
            textQuantity.EditorWidth = 150;
            textQuantity.EnabledEditor = true;
            textQuantity.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textQuantity.Header = "Количество";
            textQuantity.HeaderAutoSize = false;
            textQuantity.HeaderDock = DockStyle.Left;
            textQuantity.HeaderTextAlign = ContentAlignment.TopLeft;
            textQuantity.HeaderVisible = true;
            textQuantity.HeaderWidth = 210;
            textQuantity.IntegerValue = 0L;
            textQuantity.Location = new Point(0, 288);
            textQuantity.Margin = new Padding(3, 4, 3, 4);
            textQuantity.Name = "textQuantity";
            textQuantity.NumberGroupSeparator = " ";
            textQuantity.NumberGroupSizes = new int[] { 3 };
            textQuantity.Padding = new Padding(0, 0, 0, 7);
            textQuantity.ShowSuffix = false;
            textQuantity.Size = new Size(844, 32);
            textQuantity.Suffix = "суффикс";
            textQuantity.TabIndex = 8;
            // 
            // selectEmp
            // 
            selectEmp.CanSelectFolder = false;
            selectEmp.Dock = DockStyle.Top;
            selectEmp.EditorFitToSize = false;
            selectEmp.EditorWidth = 350;
            selectEmp.EnabledEditor = true;
            selectEmp.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectEmp.Header = "Исполнитель";
            selectEmp.HeaderAutoSize = false;
            selectEmp.HeaderDock = DockStyle.Left;
            selectEmp.HeaderTextAlign = ContentAlignment.TopLeft;
            selectEmp.HeaderVisible = true;
            selectEmp.HeaderWidth = 210;
            selectEmp.Location = new Point(0, 320);
            selectEmp.Margin = new Padding(3, 4, 3, 4);
            selectEmp.Name = "selectEmp";
            selectEmp.Padding = new Padding(0, 0, 0, 7);
            selectEmp.RemoveEmptyFolders = false;
            selectEmp.ShowDeleteButton = true;
            selectEmp.ShowOpenButton = true;
            selectEmp.Size = new Size(844, 32);
            selectEmp.TabIndex = 9;
            selectEmp.OpenButtonClick += SelectEmp_OpenButtonClick;
            // 
            // textSalary
            // 
            textSalary.CurrencyDecimalDigits = 2;
            textSalary.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textSalary.Dock = DockStyle.Top;
            textSalary.EditorFitToSize = false;
            textSalary.EditorWidth = 150;
            textSalary.EnabledEditor = true;
            textSalary.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textSalary.Header = "Зарплата";
            textSalary.HeaderAutoSize = false;
            textSalary.HeaderDock = DockStyle.Left;
            textSalary.HeaderTextAlign = ContentAlignment.TopLeft;
            textSalary.HeaderVisible = true;
            textSalary.HeaderWidth = 210;
            textSalary.Location = new Point(0, 352);
            textSalary.Margin = new Padding(3, 4, 3, 4);
            textSalary.Name = "textSalary";
            textSalary.Padding = new Padding(0, 0, 0, 7);
            textSalary.Size = new Size(844, 32);
            textSalary.TabIndex = 10;
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
            checkDoubleRate.Header = "Двойная плата";
            checkDoubleRate.HeaderAutoSize = false;
            checkDoubleRate.HeaderDock = DockStyle.Left;
            checkDoubleRate.HeaderTextAlign = ContentAlignment.TopLeft;
            checkDoubleRate.HeaderVisible = true;
            checkDoubleRate.HeaderWidth = 210;
            checkDoubleRate.Location = new Point(0, 384);
            checkDoubleRate.Margin = new Padding(4, 3, 4, 3);
            checkDoubleRate.Name = "checkDoubleRate";
            checkDoubleRate.Padding = new Padding(0, 0, 0, 9);
            checkDoubleRate.Size = new Size(844, 32);
            checkDoubleRate.TabIndex = 11;
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
            checkSkipMaterial.Location = new Point(0, 256);
            checkSkipMaterial.Margin = new Padding(4, 3, 4, 3);
            checkSkipMaterial.Name = "checkSkipMaterial";
            checkSkipMaterial.Padding = new Padding(0, 0, 0, 9);
            checkSkipMaterial.Size = new Size(844, 32);
            checkSkipMaterial.TabIndex = 12;
            checkSkipMaterial.CheckValueChanged += CheckSkipMaterial_CheckValueChanged;
            // 
            // OperationsPerformedEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(checkDoubleRate);
            Controls.Add(textSalary);
            Controls.Add(selectEmp);
            Controls.Add(textQuantity);
            Controls.Add(checkSkipMaterial);
            Controls.Add(selectMaterial);
            Controls.Add(textMaterialSpec);
            Controls.Add(selectOper);
            Controls.Add(textCalc);
            Controls.Add(textProduct);
            Controls.Add(selectLot);
            Controls.Add(dateDocument);
            Controls.Add(textDocNumber);
            Name = "OperationsPerformedEditor";
            Size = new Size(844, 470);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfIntegerTextBox textDocNumber;
        private Controls.Editors.DfDateTimePicker dateDocument;
        private Controls.Editors.DfDirectorySelectBox selectLot;
        private Controls.Editors.DfTextBox textProduct;
        private Controls.Editors.DfTextBox textCalc;
        private Controls.Editors.DfDirectorySelectBox selectOper;
        private Controls.Editors.DfTextBox textMaterialSpec;
        private Controls.Editors.DfDirectorySelectBox selectMaterial;
        private Controls.Editors.DfIntegerTextBox textQuantity;
        private Controls.Editors.DfDirectorySelectBox selectEmp;
        private Controls.Editors.DfCurrencyTextBox textSalary;
        private Controls.Editors.DfCheckBox checkDoubleRate;
        private Controls.Editors.DfCheckBox checkSkipMaterial;
    }
}
