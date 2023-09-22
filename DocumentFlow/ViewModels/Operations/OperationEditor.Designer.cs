namespace DocumentFlow.ViewModels
{
    partial class OperationEditor
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
            textCode = new Controls.Editors.DfTextBox();
            textName = new Controls.Editors.DfTextBox();
            comboType = new Controls.Editors.DfComboBox();
            selectGroup = new Controls.Editors.DfDirectorySelectBox();
            textProduced = new Controls.Editors.DfIntegerTextBox();
            textProdTime = new Controls.Editors.DfIntegerTextBox();
            textRate = new Controls.Editors.DfIntegerTextBox();
            dateNorm = new Controls.Editors.DfDateTimePicker();
            textSalary = new Controls.Editors.DfCurrencyTextBox();
            gridGoods = new Controls.Editors.DfDataGrid();
            SuspendLayout();
            // 
            // textCode
            // 
            textCode.Dock = DockStyle.Top;
            textCode.EditorFitToSize = false;
            textCode.EditorWidth = 100;
            textCode.EnabledEditor = true;
            textCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textCode.Header = "Код";
            textCode.HeaderAutoSize = false;
            textCode.HeaderDock = DockStyle.Left;
            textCode.HeaderTextAlign = ContentAlignment.TopLeft;
            textCode.HeaderVisible = true;
            textCode.HeaderWidth = 140;
            textCode.Location = new Point(0, 0);
            textCode.Margin = new Padding(3, 4, 3, 4);
            textCode.Multiline = false;
            textCode.Name = "textCode";
            textCode.Padding = new Padding(0, 0, 0, 7);
            textCode.Size = new Size(1008, 32);
            textCode.TabIndex = 0;
            textCode.TextValue = "";
            // 
            // textName
            // 
            textName.Dock = DockStyle.Top;
            textName.EditorFitToSize = false;
            textName.EditorWidth = 500;
            textName.EnabledEditor = true;
            textName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textName.Header = "Наименование";
            textName.HeaderAutoSize = false;
            textName.HeaderDock = DockStyle.Left;
            textName.HeaderTextAlign = ContentAlignment.TopLeft;
            textName.HeaderVisible = true;
            textName.HeaderWidth = 140;
            textName.Location = new Point(0, 32);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(1008, 32);
            textName.TabIndex = 1;
            textName.TextValue = "";
            // 
            // comboType
            // 
            comboType.Dock = DockStyle.Top;
            comboType.EditorFitToSize = false;
            comboType.EditorWidth = 250;
            comboType.EnabledEditor = true;
            comboType.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboType.Header = "Тип операции";
            comboType.HeaderAutoSize = false;
            comboType.HeaderDock = DockStyle.Left;
            comboType.HeaderTextAlign = ContentAlignment.TopLeft;
            comboType.HeaderVisible = true;
            comboType.HeaderWidth = 140;
            comboType.Location = new Point(0, 64);
            comboType.Margin = new Padding(3, 4, 3, 4);
            comboType.Name = "comboType";
            comboType.Padding = new Padding(0, 0, 0, 7);
            comboType.ShowDeleteButton = false;
            comboType.ShowOpenButton = true;
            comboType.Size = new Size(1008, 32);
            comboType.TabIndex = 2;
            comboType.OpenButtonClick += ComboType_OpenButtonClick;
            // 
            // selectGroup
            // 
            selectGroup.CanSelectFolder = false;
            selectGroup.Dock = DockStyle.Top;
            selectGroup.EditorFitToSize = false;
            selectGroup.EditorWidth = 400;
            selectGroup.EnabledEditor = true;
            selectGroup.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectGroup.Header = "Группа";
            selectGroup.HeaderAutoSize = false;
            selectGroup.HeaderDock = DockStyle.Left;
            selectGroup.HeaderTextAlign = ContentAlignment.TopLeft;
            selectGroup.HeaderVisible = true;
            selectGroup.HeaderWidth = 140;
            selectGroup.Location = new Point(0, 96);
            selectGroup.Margin = new Padding(3, 4, 3, 4);
            selectGroup.Name = "selectGroup";
            selectGroup.Padding = new Padding(0, 0, 0, 7);
            selectGroup.RemoveEmptyFolders = false;
            selectGroup.ShowDeleteButton = true;
            selectGroup.ShowOpenButton = false;
            selectGroup.Size = new Size(1008, 32);
            selectGroup.TabIndex = 3;
            // 
            // textProduced
            // 
            textProduced.Dock = DockStyle.Top;
            textProduced.EditorFitToSize = false;
            textProduced.EditorWidth = 100;
            textProduced.EnabledEditor = true;
            textProduced.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textProduced.Header = "Выработка";
            textProduced.HeaderAutoSize = false;
            textProduced.HeaderDock = DockStyle.Left;
            textProduced.HeaderTextAlign = ContentAlignment.TopLeft;
            textProduced.HeaderVisible = true;
            textProduced.HeaderWidth = 140;
            textProduced.IntegerValue = 0L;
            textProduced.Location = new Point(0, 128);
            textProduced.Margin = new Padding(3, 4, 3, 4);
            textProduced.Name = "textProduced";
            textProduced.Padding = new Padding(0, 0, 0, 7);
            textProduced.ShowSuffix = true;
            textProduced.Size = new Size(1008, 32);
            textProduced.Suffix = "шт.";
            textProduced.TabIndex = 4;
            // 
            // textProdTime
            // 
            textProdTime.Dock = DockStyle.Top;
            textProdTime.EditorFitToSize = false;
            textProdTime.EditorWidth = 100;
            textProdTime.EnabledEditor = true;
            textProdTime.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textProdTime.Header = "Время выработки";
            textProdTime.HeaderAutoSize = false;
            textProdTime.HeaderDock = DockStyle.Left;
            textProdTime.HeaderTextAlign = ContentAlignment.TopLeft;
            textProdTime.HeaderVisible = true;
            textProdTime.HeaderWidth = 140;
            textProdTime.IntegerValue = 0L;
            textProdTime.Location = new Point(0, 160);
            textProdTime.Margin = new Padding(3, 4, 3, 4);
            textProdTime.Name = "textProdTime";
            textProdTime.Padding = new Padding(0, 0, 0, 7);
            textProdTime.ShowSuffix = true;
            textProdTime.Size = new Size(1008, 32);
            textProdTime.Suffix = "сек.";
            textProdTime.TabIndex = 5;
            // 
            // textRate
            // 
            textRate.Dock = DockStyle.Top;
            textRate.EditorFitToSize = false;
            textRate.EditorWidth = 100;
            textRate.EnabledEditor = false;
            textRate.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textRate.Header = "Норма выработка";
            textRate.HeaderAutoSize = false;
            textRate.HeaderDock = DockStyle.Left;
            textRate.HeaderTextAlign = ContentAlignment.TopLeft;
            textRate.HeaderVisible = true;
            textRate.HeaderWidth = 140;
            textRate.IntegerValue = 0L;
            textRate.Location = new Point(0, 192);
            textRate.Margin = new Padding(3, 4, 3, 4);
            textRate.Name = "textRate";
            textRate.Padding = new Padding(0, 0, 0, 7);
            textRate.ShowSuffix = true;
            textRate.Size = new Size(1008, 32);
            textRate.Suffix = "ед./час";
            textRate.TabIndex = 6;
            // 
            // dateNorm
            // 
            dateNorm.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateNorm.Dock = DockStyle.Top;
            dateNorm.EditorFitToSize = false;
            dateNorm.EditorWidth = 150;
            dateNorm.EnabledEditor = true;
            dateNorm.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateNorm.Format = DateTimePickerFormat.Long;
            dateNorm.Header = "Дата нормирования";
            dateNorm.HeaderAutoSize = false;
            dateNorm.HeaderDock = DockStyle.Left;
            dateNorm.HeaderTextAlign = ContentAlignment.TopLeft;
            dateNorm.HeaderVisible = true;
            dateNorm.HeaderWidth = 140;
            dateNorm.Location = new Point(0, 224);
            dateNorm.Margin = new Padding(3, 5, 3, 5);
            dateNorm.Name = "dateNorm";
            dateNorm.Padding = new Padding(0, 0, 0, 7);
            dateNorm.Required = false;
            dateNorm.Size = new Size(1008, 32);
            dateNorm.TabIndex = 7;
            // 
            // textSalary
            // 
            textSalary.CurrencyDecimalDigits = 4;
            textSalary.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textSalary.Dock = DockStyle.Top;
            textSalary.EditorFitToSize = false;
            textSalary.EditorWidth = 100;
            textSalary.EnabledEditor = false;
            textSalary.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textSalary.Header = "Зарплата";
            textSalary.HeaderAutoSize = false;
            textSalary.HeaderDock = DockStyle.Left;
            textSalary.HeaderTextAlign = ContentAlignment.TopLeft;
            textSalary.HeaderVisible = true;
            textSalary.HeaderWidth = 140;
            textSalary.Location = new Point(0, 256);
            textSalary.Margin = new Padding(3, 4, 3, 4);
            textSalary.Name = "textSalary";
            textSalary.Padding = new Padding(0, 0, 0, 7);
            textSalary.Size = new Size(1008, 32);
            textSalary.TabIndex = 8;
            // 
            // gridGoods
            // 
            gridGoods.Dock = DockStyle.Fill;
            gridGoods.EditorFitToSize = true;
            gridGoods.EditorWidth = 1008;
            gridGoods.EnabledEditor = true;
            gridGoods.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            gridGoods.Header = "Операция будет использоваться только при производстве этих изделий";
            gridGoods.HeaderAutoSize = false;
            gridGoods.HeaderDock = DockStyle.Top;
            gridGoods.HeaderTextAlign = ContentAlignment.TopLeft;
            gridGoods.HeaderVisible = true;
            gridGoods.HeaderWidth = 100;
            gridGoods.Location = new Point(0, 288);
            gridGoods.Name = "gridGoods";
            gridGoods.Padding = new Padding(0, 0, 0, 7);
            gridGoods.Size = new Size(1008, 181);
            gridGoods.TabIndex = 9;
            gridGoods.CreateRow += GridGoods_CreateRow;
            gridGoods.EditRow += GridGoods_EditRow;
            // 
            // OperationEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridGoods);
            Controls.Add(textSalary);
            Controls.Add(dateNorm);
            Controls.Add(textRate);
            Controls.Add(textProdTime);
            Controls.Add(textProduced);
            Controls.Add(selectGroup);
            Controls.Add(comboType);
            Controls.Add(textName);
            Controls.Add(textCode);
            Name = "OperationEditor";
            Size = new Size(1008, 469);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textCode;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfComboBox comboType;
        private Controls.Editors.DfDirectorySelectBox selectGroup;
        private Controls.Editors.DfIntegerTextBox textProduced;
        private Controls.Editors.DfIntegerTextBox textProdTime;
        private Controls.Editors.DfIntegerTextBox textRate;
        private Controls.Editors.DfDateTimePicker dateNorm;
        private Controls.Editors.DfCurrencyTextBox textSalary;
        private Controls.Editors.DfDataGrid gridGoods;
    }
}
