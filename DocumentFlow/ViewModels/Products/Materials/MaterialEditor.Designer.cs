namespace DocumentFlow.ViewModels
{
    partial class MaterialEditor
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
            textDocName = new Controls.Editors.DfTextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            choiceMaterialKind = new Controls.Editors.DfChoice();
            selectGroup = new Controls.Editors.DfDirectorySelectBox();
            tabPage2 = new TabPage();
            textMinOrder = new Controls.Editors.DfNumericTextBox();
            choiceVat = new Controls.Editors.DfChoice();
            textPrice = new Controls.Editors.DfCurrencyTextBox();
            textWeight = new Controls.Editors.DfNumericTextBox();
            comboMeasurement = new Controls.Editors.DfComboBox();
            selectCross = new Controls.Editors.DfDirectorySelectBox();
            textExtArticle = new Controls.Editors.DfTextBox();
            gridParts = new Controls.Editors.DfDataGrid();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // textCode
            // 
            textCode.Dock = DockStyle.Top;
            textCode.EditorFitToSize = false;
            textCode.EditorWidth = 180;
            textCode.EnabledEditor = true;
            textCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textCode.Header = "Код";
            textCode.HeaderAutoSize = false;
            textCode.HeaderDock = DockStyle.Left;
            textCode.HeaderTextAlign = ContentAlignment.TopLeft;
            textCode.HeaderVisible = true;
            textCode.HeaderWidth = 200;
            textCode.Location = new Point(10, 10);
            textCode.Margin = new Padding(3, 4, 3, 4);
            textCode.Multiline = false;
            textCode.Name = "textCode";
            textCode.Padding = new Padding(0, 0, 0, 7);
            textCode.Size = new Size(843, 32);
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
            textName.HeaderWidth = 200;
            textName.Location = new Point(10, 42);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(843, 32);
            textName.TabIndex = 1;
            textName.TextValue = "";
            // 
            // textDocName
            // 
            textDocName.Dock = DockStyle.Top;
            textDocName.EditorFitToSize = false;
            textDocName.EditorWidth = 500;
            textDocName.EnabledEditor = true;
            textDocName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textDocName.Header = "Наименование для документов";
            textDocName.HeaderAutoSize = false;
            textDocName.HeaderDock = DockStyle.Left;
            textDocName.HeaderTextAlign = ContentAlignment.TopLeft;
            textDocName.HeaderVisible = true;
            textDocName.HeaderWidth = 200;
            textDocName.Location = new Point(10, 74);
            textDocName.Margin = new Padding(3, 4, 3, 4);
            textDocName.Multiline = false;
            textDocName.Name = "textDocName";
            textDocName.Padding = new Padding(0, 0, 0, 7);
            textDocName.Size = new Size(843, 32);
            textDocName.TabIndex = 2;
            textDocName.TextValue = "";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Top;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(871, 266);
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(choiceMaterialKind);
            tabPage1.Controls.Add(selectGroup);
            tabPage1.Controls.Add(textDocName);
            tabPage1.Controls.Add(textName);
            tabPage1.Controls.Add(textCode);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(10);
            tabPage1.Size = new Size(863, 238);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Основное";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // choiceMaterialKind
            // 
            choiceMaterialKind.Dock = DockStyle.Top;
            choiceMaterialKind.EditorFitToSize = false;
            choiceMaterialKind.EditorWidth = 200;
            choiceMaterialKind.EnabledEditor = true;
            choiceMaterialKind.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            choiceMaterialKind.Header = "Тип материала";
            choiceMaterialKind.HeaderAutoSize = false;
            choiceMaterialKind.HeaderDock = DockStyle.Left;
            choiceMaterialKind.HeaderTextAlign = ContentAlignment.TopLeft;
            choiceMaterialKind.HeaderVisible = true;
            choiceMaterialKind.HeaderWidth = 200;
            choiceMaterialKind.Location = new Point(10, 138);
            choiceMaterialKind.Margin = new Padding(3, 4, 3, 4);
            choiceMaterialKind.Name = "choiceMaterialKind";
            choiceMaterialKind.Padding = new Padding(0, 0, 0, 7);
            choiceMaterialKind.ShowDeleteButton = false;
            choiceMaterialKind.Size = new Size(843, 32);
            choiceMaterialKind.TabIndex = 4;
            // 
            // selectGroup
            // 
            selectGroup.CanSelectFolder = true;
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
            selectGroup.HeaderWidth = 200;
            selectGroup.Location = new Point(10, 106);
            selectGroup.Margin = new Padding(3, 4, 3, 4);
            selectGroup.Name = "selectGroup";
            selectGroup.Padding = new Padding(0, 0, 0, 7);
            selectGroup.RemoveEmptyFolders = false;
            selectGroup.ShowDeleteButton = true;
            selectGroup.ShowOpenButton = false;
            selectGroup.Size = new Size(843, 32);
            selectGroup.TabIndex = 3;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(textMinOrder);
            tabPage2.Controls.Add(choiceVat);
            tabPage2.Controls.Add(textPrice);
            tabPage2.Controls.Add(textWeight);
            tabPage2.Controls.Add(comboMeasurement);
            tabPage2.Controls.Add(selectCross);
            tabPage2.Controls.Add(textExtArticle);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(10);
            tabPage2.Size = new Size(863, 238);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Дополнительно";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // textMinOrder
            // 
            textMinOrder.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textMinOrder.Dock = DockStyle.Top;
            textMinOrder.EditorFitToSize = false;
            textMinOrder.EditorWidth = 150;
            textMinOrder.EnabledEditor = true;
            textMinOrder.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textMinOrder.Header = "Мин. заказ";
            textMinOrder.HeaderAutoSize = false;
            textMinOrder.HeaderDock = DockStyle.Left;
            textMinOrder.HeaderTextAlign = ContentAlignment.TopLeft;
            textMinOrder.HeaderVisible = true;
            textMinOrder.HeaderWidth = 130;
            textMinOrder.Location = new Point(10, 202);
            textMinOrder.Margin = new Padding(3, 4, 3, 4);
            textMinOrder.Name = "textMinOrder";
            textMinOrder.NumberDecimalDigits = 3;
            textMinOrder.Padding = new Padding(0, 0, 0, 7);
            textMinOrder.ShowSuffix = false;
            textMinOrder.Size = new Size(843, 32);
            textMinOrder.Suffix = "суффикс";
            textMinOrder.TabIndex = 6;
            // 
            // choiceVat
            // 
            choiceVat.Dock = DockStyle.Top;
            choiceVat.EditorFitToSize = false;
            choiceVat.EditorWidth = 150;
            choiceVat.EnabledEditor = true;
            choiceVat.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            choiceVat.Header = "НДС";
            choiceVat.HeaderAutoSize = false;
            choiceVat.HeaderDock = DockStyle.Left;
            choiceVat.HeaderTextAlign = ContentAlignment.TopLeft;
            choiceVat.HeaderVisible = true;
            choiceVat.HeaderWidth = 130;
            choiceVat.Location = new Point(10, 170);
            choiceVat.Margin = new Padding(3, 4, 3, 4);
            choiceVat.Name = "choiceVat";
            choiceVat.Padding = new Padding(0, 0, 0, 7);
            choiceVat.ShowDeleteButton = false;
            choiceVat.Size = new Size(843, 32);
            choiceVat.TabIndex = 5;
            // 
            // textPrice
            // 
            textPrice.CurrencyDecimalDigits = 2;
            textPrice.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textPrice.Dock = DockStyle.Top;
            textPrice.EditorFitToSize = false;
            textPrice.EditorWidth = 150;
            textPrice.EnabledEditor = true;
            textPrice.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textPrice.Header = "Цена без НДС";
            textPrice.HeaderAutoSize = false;
            textPrice.HeaderDock = DockStyle.Left;
            textPrice.HeaderTextAlign = ContentAlignment.TopLeft;
            textPrice.HeaderVisible = true;
            textPrice.HeaderWidth = 130;
            textPrice.Location = new Point(10, 138);
            textPrice.Margin = new Padding(3, 4, 3, 4);
            textPrice.Name = "textPrice";
            textPrice.Padding = new Padding(0, 0, 0, 7);
            textPrice.Size = new Size(843, 32);
            textPrice.TabIndex = 4;
            // 
            // textWeight
            // 
            textWeight.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textWeight.Dock = DockStyle.Top;
            textWeight.EditorFitToSize = false;
            textWeight.EditorWidth = 100;
            textWeight.EnabledEditor = true;
            textWeight.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textWeight.Header = "Вес, г";
            textWeight.HeaderAutoSize = false;
            textWeight.HeaderDock = DockStyle.Left;
            textWeight.HeaderTextAlign = ContentAlignment.TopLeft;
            textWeight.HeaderVisible = true;
            textWeight.HeaderWidth = 130;
            textWeight.Location = new Point(10, 106);
            textWeight.Margin = new Padding(3, 4, 3, 4);
            textWeight.Name = "textWeight";
            textWeight.NumberDecimalDigits = 3;
            textWeight.Padding = new Padding(0, 0, 0, 7);
            textWeight.ShowSuffix = false;
            textWeight.Size = new Size(843, 32);
            textWeight.Suffix = "суффикс";
            textWeight.TabIndex = 3;
            // 
            // comboMeasurement
            // 
            comboMeasurement.DisplayMember = "ItemName";
            comboMeasurement.Dock = DockStyle.Top;
            comboMeasurement.EditorFitToSize = false;
            comboMeasurement.EditorWidth = 250;
            comboMeasurement.EnabledEditor = true;
            comboMeasurement.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboMeasurement.Header = "Единица измерения";
            comboMeasurement.HeaderAutoSize = false;
            comboMeasurement.HeaderDock = DockStyle.Left;
            comboMeasurement.HeaderTextAlign = ContentAlignment.TopLeft;
            comboMeasurement.HeaderVisible = true;
            comboMeasurement.HeaderWidth = 130;
            comboMeasurement.Location = new Point(10, 74);
            comboMeasurement.Margin = new Padding(3, 4, 3, 4);
            comboMeasurement.Name = "comboMeasurement";
            comboMeasurement.Padding = new Padding(0, 0, 0, 7);
            comboMeasurement.ShowDeleteButton = false;
            comboMeasurement.ShowOpenButton = false;
            comboMeasurement.Size = new Size(843, 32);
            comboMeasurement.TabIndex = 2;
            // 
            // selectCross
            // 
            selectCross.CanSelectFolder = false;
            selectCross.Dock = DockStyle.Top;
            selectCross.EditorFitToSize = false;
            selectCross.EditorWidth = 400;
            selectCross.EnabledEditor = true;
            selectCross.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectCross.Header = "Кросс-артикул";
            selectCross.HeaderAutoSize = false;
            selectCross.HeaderDock = DockStyle.Left;
            selectCross.HeaderTextAlign = ContentAlignment.TopLeft;
            selectCross.HeaderVisible = true;
            selectCross.HeaderWidth = 130;
            selectCross.Location = new Point(10, 42);
            selectCross.Margin = new Padding(3, 4, 3, 4);
            selectCross.Name = "selectCross";
            selectCross.Padding = new Padding(0, 0, 0, 7);
            selectCross.RemoveEmptyFolders = false;
            selectCross.ShowDeleteButton = true;
            selectCross.ShowOpenButton = true;
            selectCross.Size = new Size(843, 32);
            selectCross.TabIndex = 1;
            selectCross.OpenButtonClick += SelectCross_OpenButtonClick;
            // 
            // textExtArticle
            // 
            textExtArticle.Dock = DockStyle.Top;
            textExtArticle.EditorFitToSize = false;
            textExtArticle.EditorWidth = 250;
            textExtArticle.EnabledEditor = true;
            textExtArticle.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textExtArticle.Header = "Доп. артикул";
            textExtArticle.HeaderAutoSize = false;
            textExtArticle.HeaderDock = DockStyle.Left;
            textExtArticle.HeaderTextAlign = ContentAlignment.TopLeft;
            textExtArticle.HeaderVisible = true;
            textExtArticle.HeaderWidth = 130;
            textExtArticle.Location = new Point(10, 10);
            textExtArticle.Margin = new Padding(3, 4, 3, 4);
            textExtArticle.Multiline = false;
            textExtArticle.Name = "textExtArticle";
            textExtArticle.Padding = new Padding(0, 0, 0, 7);
            textExtArticle.Size = new Size(843, 32);
            textExtArticle.TabIndex = 0;
            textExtArticle.TextValue = "";
            // 
            // gridParts
            // 
            gridParts.Dock = DockStyle.Fill;
            gridParts.EditorFitToSize = true;
            gridParts.EditorWidth = 871;
            gridParts.EnabledEditor = true;
            gridParts.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            gridParts.Header = "Совместимые детали";
            gridParts.HeaderAutoSize = false;
            gridParts.HeaderDock = DockStyle.Top;
            gridParts.HeaderTextAlign = ContentAlignment.TopLeft;
            gridParts.HeaderVisible = true;
            gridParts.HeaderWidth = 100;
            gridParts.Location = new Point(0, 266);
            gridParts.Name = "gridParts";
            gridParts.Padding = new Padding(0, 0, 0, 7);
            gridParts.Size = new Size(871, 203);
            gridParts.TabIndex = 5;
            gridParts.CreateRow += GridParts_CreateRow;
            gridParts.EditRow += GridParts_EditRow;
            // 
            // MaterialEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridParts);
            Controls.Add(tabControl1);
            Name = "MaterialEditor";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textCode;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfTextBox textDocName;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Controls.Editors.DfDirectorySelectBox selectGroup;
        private Controls.Editors.DfNumericTextBox textWeight;
        private Controls.Editors.DfComboBox comboMeasurement;
        private Controls.Editors.DfDirectorySelectBox selectCross;
        private Controls.Editors.DfTextBox textExtArticle;
        private Controls.Editors.DfNumericTextBox textMinOrder;
        private Controls.Editors.DfChoice choiceVat;
        private Controls.Editors.DfCurrencyTextBox textPrice;
        private Controls.Editors.DfChoice choiceMaterialKind;
        private Controls.Editors.DfDataGrid gridParts;
    }
}
