namespace DocumentFlow.ViewModels
{
    partial class CalculationOperationEditor
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
            textCalcName = new Controls.Editors.DfTextBox();
            textCode = new Controls.Editors.DfTextBox();
            textName = new Controls.Editors.DfTextBox();
            selectOperation = new Controls.Editors.DfDirectorySelectBox();
            textPrice = new Controls.Editors.DfCurrencyTextBox();
            selectEquipment = new Controls.Editors.DfDirectorySelectBox();
            selectTool = new Controls.Editors.DfDirectorySelectBox();
            selectMaterial = new Controls.Editors.DfDirectorySelectBox();
            textAmount = new Controls.Editors.DfNumericTextBox();
            textRepeat = new Controls.Editors.DfIntegerTextBox();
            selectPrevious = new Controls.Editors.DfMultiSelectionComboBox();
            propertyList = new Controls.Editors.DfPropertyList();
            textNote = new Controls.Editors.DfTextBox();
            SuspendLayout();
            // 
            // textCalcName
            // 
            textCalcName.Dock = DockStyle.Top;
            textCalcName.EditorFitToSize = false;
            textCalcName.EditorWidth = 600;
            textCalcName.EnabledEditor = false;
            textCalcName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textCalcName.Header = "Калькуляция";
            textCalcName.HeaderAutoSize = false;
            textCalcName.HeaderDock = DockStyle.Left;
            textCalcName.HeaderTextAlign = ContentAlignment.TopLeft;
            textCalcName.HeaderVisible = true;
            textCalcName.HeaderWidth = 140;
            textCalcName.Location = new Point(0, 0);
            textCalcName.Margin = new Padding(3, 4, 3, 4);
            textCalcName.Multiline = false;
            textCalcName.Name = "textCalcName";
            textCalcName.Padding = new Padding(0, 0, 0, 7);
            textCalcName.Size = new Size(930, 32);
            textCalcName.TabIndex = 0;
            textCalcName.TextValue = "";
            // 
            // textCode
            // 
            textCode.Dock = DockStyle.Top;
            textCode.EditorFitToSize = false;
            textCode.EditorWidth = 150;
            textCode.EnabledEditor = true;
            textCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textCode.Header = "Код";
            textCode.HeaderAutoSize = false;
            textCode.HeaderDock = DockStyle.Left;
            textCode.HeaderTextAlign = ContentAlignment.TopLeft;
            textCode.HeaderVisible = true;
            textCode.HeaderWidth = 140;
            textCode.Location = new Point(0, 32);
            textCode.Margin = new Padding(3, 4, 3, 4);
            textCode.Multiline = false;
            textCode.Name = "textCode";
            textCode.Padding = new Padding(0, 0, 0, 7);
            textCode.Size = new Size(930, 32);
            textCode.TabIndex = 1;
            textCode.TextValue = "";
            // 
            // textName
            // 
            textName.Dock = DockStyle.Top;
            textName.EditorFitToSize = false;
            textName.EditorWidth = 400;
            textName.EnabledEditor = true;
            textName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textName.Header = "Наименование";
            textName.HeaderAutoSize = false;
            textName.HeaderDock = DockStyle.Left;
            textName.HeaderTextAlign = ContentAlignment.TopLeft;
            textName.HeaderVisible = true;
            textName.HeaderWidth = 140;
            textName.Location = new Point(0, 64);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(930, 32);
            textName.TabIndex = 2;
            textName.TextValue = "";
            // 
            // selectOperation
            // 
            selectOperation.CanSelectFolder = false;
            selectOperation.Dock = DockStyle.Top;
            selectOperation.EditorFitToSize = false;
            selectOperation.EditorWidth = 400;
            selectOperation.EnabledEditor = true;
            selectOperation.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectOperation.Header = "Операция";
            selectOperation.HeaderAutoSize = false;
            selectOperation.HeaderDock = DockStyle.Left;
            selectOperation.HeaderTextAlign = ContentAlignment.TopLeft;
            selectOperation.HeaderVisible = true;
            selectOperation.HeaderWidth = 140;
            selectOperation.Location = new Point(0, 96);
            selectOperation.Margin = new Padding(3, 4, 3, 4);
            selectOperation.Name = "selectOperation";
            selectOperation.Padding = new Padding(0, 0, 0, 7);
            selectOperation.RemoveEmptyFolders = false;
            selectOperation.ShowDeleteButton = false;
            selectOperation.ShowOpenButton = true;
            selectOperation.Size = new Size(930, 32);
            selectOperation.TabIndex = 3;
            selectOperation.OpenButtonClick += SelectOperation_OpenButtonClick;
            selectOperation.UserDocumentModified += SelectOperation_DocumentSelectedChanged;
            // 
            // textPrice
            // 
            textPrice.CurrencyDecimalDigits = 4;
            textPrice.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textPrice.Dock = DockStyle.Top;
            textPrice.EditorFitToSize = false;
            textPrice.EditorWidth = 100;
            textPrice.EnabledEditor = false;
            textPrice.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPrice.Header = "Расценка";
            textPrice.HeaderAutoSize = false;
            textPrice.HeaderDock = DockStyle.Left;
            textPrice.HeaderTextAlign = ContentAlignment.TopLeft;
            textPrice.HeaderVisible = true;
            textPrice.HeaderWidth = 140;
            textPrice.Location = new Point(0, 128);
            textPrice.Margin = new Padding(3, 4, 3, 4);
            textPrice.Name = "textPrice";
            textPrice.Padding = new Padding(0, 0, 0, 7);
            textPrice.Size = new Size(930, 32);
            textPrice.TabIndex = 4;
            // 
            // selectEquipment
            // 
            selectEquipment.CanSelectFolder = false;
            selectEquipment.Dock = DockStyle.Top;
            selectEquipment.EditorFitToSize = false;
            selectEquipment.EditorWidth = 400;
            selectEquipment.EnabledEditor = true;
            selectEquipment.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectEquipment.Header = "Оборудование";
            selectEquipment.HeaderAutoSize = false;
            selectEquipment.HeaderDock = DockStyle.Left;
            selectEquipment.HeaderTextAlign = ContentAlignment.TopLeft;
            selectEquipment.HeaderVisible = true;
            selectEquipment.HeaderWidth = 140;
            selectEquipment.Location = new Point(0, 160);
            selectEquipment.Margin = new Padding(3, 4, 3, 4);
            selectEquipment.Name = "selectEquipment";
            selectEquipment.Padding = new Padding(0, 0, 0, 7);
            selectEquipment.RemoveEmptyFolders = false;
            selectEquipment.ShowDeleteButton = true;
            selectEquipment.ShowOpenButton = true;
            selectEquipment.Size = new Size(930, 32);
            selectEquipment.TabIndex = 5;
            selectEquipment.OpenButtonClick += SelectEquipment_OpenButtonClick;
            // 
            // selectTool
            // 
            selectTool.CanSelectFolder = false;
            selectTool.Dock = DockStyle.Top;
            selectTool.EditorFitToSize = false;
            selectTool.EditorWidth = 400;
            selectTool.EnabledEditor = true;
            selectTool.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectTool.Header = "Инструмент";
            selectTool.HeaderAutoSize = false;
            selectTool.HeaderDock = DockStyle.Left;
            selectTool.HeaderTextAlign = ContentAlignment.TopLeft;
            selectTool.HeaderVisible = true;
            selectTool.HeaderWidth = 140;
            selectTool.Location = new Point(0, 192);
            selectTool.Margin = new Padding(3, 4, 3, 4);
            selectTool.Name = "selectTool";
            selectTool.Padding = new Padding(0, 0, 0, 7);
            selectTool.RemoveEmptyFolders = false;
            selectTool.ShowDeleteButton = true;
            selectTool.ShowOpenButton = true;
            selectTool.Size = new Size(930, 32);
            selectTool.TabIndex = 6;
            selectTool.OpenButtonClick += SelectTool_OpenButtonClick;
            // 
            // selectMaterial
            // 
            selectMaterial.CanSelectFolder = false;
            selectMaterial.Dock = DockStyle.Top;
            selectMaterial.EditorFitToSize = false;
            selectMaterial.EditorWidth = 400;
            selectMaterial.EnabledEditor = true;
            selectMaterial.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectMaterial.Header = "Материал";
            selectMaterial.HeaderAutoSize = false;
            selectMaterial.HeaderDock = DockStyle.Left;
            selectMaterial.HeaderTextAlign = ContentAlignment.TopLeft;
            selectMaterial.HeaderVisible = true;
            selectMaterial.HeaderWidth = 140;
            selectMaterial.Location = new Point(0, 224);
            selectMaterial.Margin = new Padding(3, 4, 3, 4);
            selectMaterial.Name = "selectMaterial";
            selectMaterial.Padding = new Padding(0, 0, 0, 7);
            selectMaterial.RemoveEmptyFolders = false;
            selectMaterial.ShowDeleteButton = true;
            selectMaterial.ShowOpenButton = true;
            selectMaterial.Size = new Size(930, 32);
            selectMaterial.TabIndex = 7;
            selectMaterial.OpenButtonClick += SelectMaterial_OpenButtonClick;
            // 
            // textAmount
            // 
            textAmount.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textAmount.Dock = DockStyle.Top;
            textAmount.EditorFitToSize = false;
            textAmount.EditorWidth = 100;
            textAmount.EnabledEditor = true;
            textAmount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textAmount.Header = "Количество";
            textAmount.HeaderAutoSize = false;
            textAmount.HeaderDock = DockStyle.Left;
            textAmount.HeaderTextAlign = ContentAlignment.TopLeft;
            textAmount.HeaderVisible = true;
            textAmount.HeaderWidth = 140;
            textAmount.Location = new Point(0, 256);
            textAmount.Margin = new Padding(3, 4, 3, 4);
            textAmount.Name = "textAmount";
            textAmount.NumberDecimalDigits = 3;
            textAmount.Padding = new Padding(0, 0, 0, 7);
            textAmount.ShowSuffix = false;
            textAmount.Size = new Size(930, 32);
            textAmount.Suffix = "суффикс";
            textAmount.TabIndex = 8;
            // 
            // textRepeat
            // 
            textRepeat.Dock = DockStyle.Top;
            textRepeat.EditorFitToSize = false;
            textRepeat.EditorWidth = 100;
            textRepeat.EnabledEditor = true;
            textRepeat.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textRepeat.Header = "Кол-во повторов";
            textRepeat.HeaderAutoSize = false;
            textRepeat.HeaderDock = DockStyle.Left;
            textRepeat.HeaderTextAlign = ContentAlignment.TopLeft;
            textRepeat.HeaderVisible = true;
            textRepeat.HeaderWidth = 140;
            textRepeat.IntegerValue = 0L;
            textRepeat.Location = new Point(0, 288);
            textRepeat.Margin = new Padding(3, 4, 3, 4);
            textRepeat.Name = "textRepeat";
            textRepeat.Padding = new Padding(0, 0, 0, 7);
            textRepeat.ShowSuffix = false;
            textRepeat.Size = new Size(930, 32);
            textRepeat.Suffix = "суффикс";
            textRepeat.TabIndex = 9;
            // 
            // selectPrevious
            // 
            selectPrevious.Dock = DockStyle.Top;
            selectPrevious.EditorFitToSize = false;
            selectPrevious.EditorWidth = 500;
            selectPrevious.EnabledEditor = true;
            selectPrevious.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectPrevious.Header = "Пред. операции";
            selectPrevious.HeaderAutoSize = false;
            selectPrevious.HeaderDock = DockStyle.Left;
            selectPrevious.HeaderTextAlign = ContentAlignment.TopLeft;
            selectPrevious.HeaderVisible = true;
            selectPrevious.HeaderWidth = 140;
            selectPrevious.Location = new Point(0, 320);
            selectPrevious.Name = "selectPrevious";
            selectPrevious.Padding = new Padding(0, 0, 0, 7);
            selectPrevious.Size = new Size(930, 32);
            selectPrevious.TabIndex = 10;
            // 
            // propertyList
            // 
            propertyList.Dock = DockStyle.Top;
            propertyList.EditorFitToSize = false;
            propertyList.EditorWidth = 500;
            propertyList.EnabledEditor = true;
            propertyList.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            propertyList.Header = "Доп. характеристики";
            propertyList.HeaderAutoSize = false;
            propertyList.HeaderDock = DockStyle.Left;
            propertyList.HeaderTextAlign = ContentAlignment.TopLeft;
            propertyList.HeaderVisible = true;
            propertyList.HeaderWidth = 140;
            propertyList.Location = new Point(0, 352);
            propertyList.Name = "propertyList";
            propertyList.Padding = new Padding(0, 0, 0, 8);
            propertyList.Size = new Size(930, 32);
            propertyList.TabIndex = 11;
            // 
            // textNote
            // 
            textNote.Dock = DockStyle.Top;
            textNote.EditorFitToSize = false;
            textNote.EditorWidth = 500;
            textNote.EnabledEditor = true;
            textNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textNote.Header = "Комментарий";
            textNote.HeaderAutoSize = false;
            textNote.HeaderDock = DockStyle.Left;
            textNote.HeaderTextAlign = ContentAlignment.TopLeft;
            textNote.HeaderVisible = true;
            textNote.HeaderWidth = 140;
            textNote.Location = new Point(0, 384);
            textNote.Margin = new Padding(3, 4, 3, 4);
            textNote.Multiline = true;
            textNote.Name = "textNote";
            textNote.Padding = new Padding(0, 0, 0, 7);
            textNote.Size = new Size(930, 75);
            textNote.TabIndex = 12;
            textNote.TextValue = "";
            // 
            // CalculationOperationEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textNote);
            Controls.Add(propertyList);
            Controls.Add(selectPrevious);
            Controls.Add(textRepeat);
            Controls.Add(textAmount);
            Controls.Add(selectMaterial);
            Controls.Add(selectTool);
            Controls.Add(selectEquipment);
            Controls.Add(textPrice);
            Controls.Add(selectOperation);
            Controls.Add(textName);
            Controls.Add(textCode);
            Controls.Add(textCalcName);
            Name = "CalculationOperationEditor";
            Size = new Size(930, 509);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textCalcName;
        private Controls.Editors.DfTextBox textCode;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfDirectorySelectBox selectOperation;
        private Controls.Editors.DfCurrencyTextBox textPrice;
        private Controls.Editors.DfDirectorySelectBox selectEquipment;
        private Controls.Editors.DfDirectorySelectBox selectTool;
        private Controls.Editors.DfDirectorySelectBox selectMaterial;
        private Controls.Editors.DfNumericTextBox textAmount;
        private Controls.Editors.DfIntegerTextBox textRepeat;
        private Controls.Editors.DfMultiSelectionComboBox selectPrevious;
        private Controls.Editors.DfPropertyList propertyList;
        private Controls.Editors.DfTextBox textNote;
    }
}
