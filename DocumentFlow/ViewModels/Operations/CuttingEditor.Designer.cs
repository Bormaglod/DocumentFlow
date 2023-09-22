namespace DocumentFlow.ViewModels
{
    partial class CuttingEditor
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
            tabControl1 = new TabControl();
            tabPageBase = new TabPage();
            textSalary = new Controls.Editors.DfCurrencyTextBox();
            dateNorm = new Controls.Editors.DfDateTimePicker();
            textRate = new Controls.Editors.DfIntegerTextBox();
            textProdTime = new Controls.Editors.DfIntegerTextBox();
            textProduced = new Controls.Editors.DfIntegerTextBox();
            selectGroup = new Controls.Editors.DfDirectorySelectBox();
            textName = new Controls.Editors.DfTextBox();
            textCode = new Controls.Editors.DfTextBox();
            tabPageCutting = new TabPage();
            groupBox2 = new GroupBox();
            textRightSweep = new Controls.Editors.DfIntegerTextBox();
            groupBox1 = new GroupBox();
            this.textLeftCleaning = new Controls.Editors.DfNumericTextBox();
            textLength = new Controls.Editors.DfIntegerTextBox();
            choiceProgram = new Controls.Editors.DfChoice();
            textLeftSweep = new Controls.Editors.DfIntegerTextBox();
            textRightCleaning = new Controls.Editors.DfNumericTextBox();
            tabControl1.SuspendLayout();
            tabPageBase.SuspendLayout();
            tabPageCutting.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageBase);
            tabControl1.Controls.Add(tabPageCutting);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(931, 559);
            tabControl1.TabIndex = 0;
            // 
            // tabPageBase
            // 
            tabPageBase.Controls.Add(textSalary);
            tabPageBase.Controls.Add(dateNorm);
            tabPageBase.Controls.Add(textRate);
            tabPageBase.Controls.Add(textProdTime);
            tabPageBase.Controls.Add(textProduced);
            tabPageBase.Controls.Add(selectGroup);
            tabPageBase.Controls.Add(textName);
            tabPageBase.Controls.Add(textCode);
            tabPageBase.Location = new Point(4, 24);
            tabPageBase.Name = "tabPageBase";
            tabPageBase.Padding = new Padding(10);
            tabPageBase.Size = new Size(923, 531);
            tabPageBase.TabIndex = 0;
            tabPageBase.Text = "Основное";
            tabPageBase.UseVisualStyleBackColor = true;
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
            textSalary.HeaderWidth = 120;
            textSalary.Location = new Point(10, 234);
            textSalary.Margin = new Padding(3, 4, 3, 4);
            textSalary.Name = "textSalary";
            textSalary.Padding = new Padding(0, 0, 0, 7);
            textSalary.Size = new Size(903, 32);
            textSalary.TabIndex = 7;
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
            dateNorm.HeaderWidth = 120;
            dateNorm.Location = new Point(10, 202);
            dateNorm.Margin = new Padding(3, 5, 3, 5);
            dateNorm.Name = "dateNorm";
            dateNorm.Padding = new Padding(0, 0, 0, 7);
            dateNorm.Required = false;
            dateNorm.Size = new Size(903, 32);
            dateNorm.TabIndex = 6;
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
            textRate.HeaderWidth = 120;
            textRate.IntegerValue = 0L;
            textRate.Location = new Point(10, 170);
            textRate.Margin = new Padding(3, 4, 3, 4);
            textRate.Name = "textRate";
            textRate.Padding = new Padding(0, 0, 0, 7);
            textRate.ShowSuffix = true;
            textRate.Size = new Size(903, 32);
            textRate.Suffix = "ед./час";
            textRate.TabIndex = 5;
            // 
            // textProdTime
            // 
            textProdTime.Dock = DockStyle.Top;
            textProdTime.EditorFitToSize = false;
            textProdTime.EditorWidth = 100;
            textProdTime.EnabledEditor = false;
            textProdTime.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textProdTime.Header = "Время выработки";
            textProdTime.HeaderAutoSize = false;
            textProdTime.HeaderDock = DockStyle.Left;
            textProdTime.HeaderTextAlign = ContentAlignment.TopLeft;
            textProdTime.HeaderVisible = true;
            textProdTime.HeaderWidth = 120;
            textProdTime.IntegerValue = 0L;
            textProdTime.Location = new Point(10, 138);
            textProdTime.Margin = new Padding(3, 4, 3, 4);
            textProdTime.Name = "textProdTime";
            textProdTime.Padding = new Padding(0, 0, 0, 7);
            textProdTime.ShowSuffix = true;
            textProdTime.Size = new Size(903, 32);
            textProdTime.Suffix = "сек.";
            textProdTime.TabIndex = 4;
            // 
            // textProduced
            // 
            textProduced.Dock = DockStyle.Top;
            textProduced.EditorFitToSize = false;
            textProduced.EditorWidth = 100;
            textProduced.EnabledEditor = false;
            textProduced.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textProduced.Header = "Выработка";
            textProduced.HeaderAutoSize = false;
            textProduced.HeaderDock = DockStyle.Left;
            textProduced.HeaderTextAlign = ContentAlignment.TopLeft;
            textProduced.HeaderVisible = true;
            textProduced.HeaderWidth = 120;
            textProduced.IntegerValue = 0L;
            textProduced.Location = new Point(10, 106);
            textProduced.Margin = new Padding(3, 4, 3, 4);
            textProduced.Name = "textProduced";
            textProduced.Padding = new Padding(0, 0, 0, 7);
            textProduced.ShowSuffix = true;
            textProduced.Size = new Size(903, 32);
            textProduced.Suffix = "шт.";
            textProduced.TabIndex = 3;
            // 
            // selectGroup
            // 
            selectGroup.CanSelectFolder = false;
            selectGroup.Dock = DockStyle.Top;
            selectGroup.EditorFitToSize = false;
            selectGroup.EditorWidth = 250;
            selectGroup.EnabledEditor = true;
            selectGroup.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectGroup.Header = "Группа";
            selectGroup.HeaderAutoSize = false;
            selectGroup.HeaderDock = DockStyle.Left;
            selectGroup.HeaderTextAlign = ContentAlignment.TopLeft;
            selectGroup.HeaderVisible = true;
            selectGroup.HeaderWidth = 120;
            selectGroup.Location = new Point(10, 74);
            selectGroup.Margin = new Padding(3, 4, 3, 4);
            selectGroup.Name = "selectGroup";
            selectGroup.Padding = new Padding(0, 0, 0, 7);
            selectGroup.RemoveEmptyFolders = false;
            selectGroup.ShowDeleteButton = true;
            selectGroup.ShowOpenButton = false;
            selectGroup.Size = new Size(903, 32);
            selectGroup.TabIndex = 2;
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
            textName.HeaderWidth = 120;
            textName.Location = new Point(10, 42);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(903, 32);
            textName.TabIndex = 1;
            textName.TextValue = "";
            // 
            // textCode
            // 
            textCode.Dock = DockStyle.Top;
            textCode.EditorFitToSize = false;
            textCode.EditorWidth = 150;
            textCode.EnabledEditor = false;
            textCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textCode.Header = "Код";
            textCode.HeaderAutoSize = false;
            textCode.HeaderDock = DockStyle.Left;
            textCode.HeaderTextAlign = ContentAlignment.TopLeft;
            textCode.HeaderVisible = true;
            textCode.HeaderWidth = 120;
            textCode.Location = new Point(10, 10);
            textCode.Margin = new Padding(3, 4, 3, 4);
            textCode.Multiline = false;
            textCode.Name = "textCode";
            textCode.Padding = new Padding(0, 0, 0, 7);
            textCode.Size = new Size(903, 32);
            textCode.TabIndex = 0;
            textCode.TextValue = "";
            // 
            // tabPageCutting
            // 
            tabPageCutting.Controls.Add(groupBox2);
            tabPageCutting.Controls.Add(groupBox1);
            tabPageCutting.Controls.Add(textLength);
            tabPageCutting.Controls.Add(choiceProgram);
            tabPageCutting.Location = new Point(4, 24);
            tabPageCutting.Name = "tabPageCutting";
            tabPageCutting.Padding = new Padding(10);
            tabPageCutting.Size = new Size(923, 531);
            tabPageCutting.TabIndex = 1;
            tabPageCutting.Text = "Параметры резки";
            tabPageCutting.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textRightSweep);
            groupBox2.Controls.Add(textRightCleaning);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Location = new Point(10, 164);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(903, 90);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Зачистка справа";
            // 
            // textRightSweep
            // 
            textRightSweep.Dock = DockStyle.Top;
            textRightSweep.EditorFitToSize = false;
            textRightSweep.EditorWidth = 100;
            textRightSweep.EnabledEditor = true;
            textRightSweep.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textRightSweep.Header = "Ширина окна";
            textRightSweep.HeaderAutoSize = false;
            textRightSweep.HeaderDock = DockStyle.Left;
            textRightSweep.HeaderTextAlign = ContentAlignment.TopLeft;
            textRightSweep.HeaderVisible = true;
            textRightSweep.HeaderWidth = 110;
            textRightSweep.IntegerValue = 0L;
            textRightSweep.Location = new Point(3, 51);
            textRightSweep.Margin = new Padding(3, 4, 3, 4);
            textRightSweep.Name = "textRightSweep";
            textRightSweep.Padding = new Padding(0, 0, 0, 7);
            textRightSweep.ShowSuffix = true;
            textRightSweep.Size = new Size(897, 32);
            textRightSweep.Suffix = "мм";
            textRightSweep.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textLeftSweep);
            groupBox1.Controls.Add(this.textLeftCleaning);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(10, 74);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(903, 90);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Зачистка слева";
            // 
            // textLeftCleaning
            // 
            this.textLeftCleaning.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.textLeftCleaning.Dock = DockStyle.Top;
            this.textLeftCleaning.EditorFitToSize = false;
            this.textLeftCleaning.EditorWidth = 100;
            this.textLeftCleaning.EnabledEditor = true;
            this.textLeftCleaning.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.textLeftCleaning.Header = "Длина зачистки";
            this.textLeftCleaning.HeaderAutoSize = false;
            this.textLeftCleaning.HeaderDock = DockStyle.Left;
            this.textLeftCleaning.HeaderTextAlign = ContentAlignment.TopLeft;
            this.textLeftCleaning.HeaderVisible = true;
            this.textLeftCleaning.HeaderWidth = 110;
            this.textLeftCleaning.Location = new Point(3, 19);
            this.textLeftCleaning.Margin = new Padding(3, 4, 3, 4);
            this.textLeftCleaning.Name = "textLeftCleaning";
            this.textLeftCleaning.NumberDecimalDigits = 1;
            this.textLeftCleaning.Padding = new Padding(0, 0, 0, 7);
            this.textLeftCleaning.ShowSuffix = false;
            this.textLeftCleaning.Size = new Size(897, 32);
            this.textLeftCleaning.Suffix = "мм";
            this.textLeftCleaning.TabIndex = 2;
            // 
            // textLength
            // 
            textLength.Dock = DockStyle.Top;
            textLength.EditorFitToSize = false;
            textLength.EditorWidth = 100;
            textLength.EnabledEditor = true;
            textLength.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textLength.Header = "Длина провода";
            textLength.HeaderAutoSize = false;
            textLength.HeaderDock = DockStyle.Left;
            textLength.HeaderTextAlign = ContentAlignment.TopLeft;
            textLength.HeaderVisible = true;
            textLength.HeaderWidth = 112;
            textLength.IntegerValue = 0L;
            textLength.Location = new Point(10, 42);
            textLength.Margin = new Padding(3, 4, 3, 4);
            textLength.Name = "textLength";
            textLength.Padding = new Padding(0, 0, 0, 7);
            textLength.ShowSuffix = true;
            textLength.Size = new Size(903, 32);
            textLength.Suffix = "мм";
            textLength.TabIndex = 1;
            // 
            // choiceProgram
            // 
            choiceProgram.Dock = DockStyle.Top;
            choiceProgram.EditorFitToSize = false;
            choiceProgram.EditorWidth = 130;
            choiceProgram.EnabledEditor = true;
            choiceProgram.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            choiceProgram.Header = "Программа";
            choiceProgram.HeaderAutoSize = false;
            choiceProgram.HeaderDock = DockStyle.Left;
            choiceProgram.HeaderTextAlign = ContentAlignment.TopLeft;
            choiceProgram.HeaderVisible = true;
            choiceProgram.HeaderWidth = 112;
            choiceProgram.Location = new Point(10, 10);
            choiceProgram.Margin = new Padding(3, 4, 3, 4);
            choiceProgram.Name = "choiceProgram";
            choiceProgram.Padding = new Padding(0, 0, 0, 7);
            choiceProgram.ShowDeleteButton = true;
            choiceProgram.Size = new Size(903, 32);
            choiceProgram.TabIndex = 0;
            // 
            // textLeftSweep
            // 
            textLeftSweep.Dock = DockStyle.Top;
            textLeftSweep.EditorFitToSize = false;
            textLeftSweep.EditorWidth = 100;
            textLeftSweep.EnabledEditor = true;
            textLeftSweep.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textLeftSweep.Header = "Ширина окна";
            textLeftSweep.HeaderAutoSize = false;
            textLeftSweep.HeaderDock = DockStyle.Left;
            textLeftSweep.HeaderTextAlign = ContentAlignment.TopLeft;
            textLeftSweep.HeaderVisible = true;
            textLeftSweep.HeaderWidth = 110;
            textLeftSweep.IntegerValue = 0L;
            textLeftSweep.Location = new Point(3, 51);
            textLeftSweep.Margin = new Padding(3, 4, 3, 4);
            textLeftSweep.Name = "textLeftSweep";
            textLeftSweep.Padding = new Padding(0, 0, 0, 7);
            textLeftSweep.ShowSuffix = true;
            textLeftSweep.Size = new Size(897, 32);
            textLeftSweep.Suffix = "мм";
            textLeftSweep.TabIndex = 3;
            // 
            // textRightCleaning
            // 
            textRightCleaning.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textRightCleaning.Dock = DockStyle.Top;
            textRightCleaning.EditorFitToSize = false;
            textRightCleaning.EditorWidth = 100;
            textRightCleaning.EnabledEditor = true;
            textRightCleaning.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textRightCleaning.Header = "Длина зачистки";
            textRightCleaning.HeaderAutoSize = false;
            textRightCleaning.HeaderDock = DockStyle.Left;
            textRightCleaning.HeaderTextAlign = ContentAlignment.TopLeft;
            textRightCleaning.HeaderVisible = true;
            textRightCleaning.HeaderWidth = 110;
            textRightCleaning.Location = new Point(3, 19);
            textRightCleaning.Margin = new Padding(3, 4, 3, 4);
            textRightCleaning.Name = "textRightCleaning";
            textRightCleaning.NumberDecimalDigits = 1;
            textRightCleaning.Padding = new Padding(0, 0, 0, 7);
            textRightCleaning.ShowSuffix = true;
            textRightCleaning.Size = new Size(897, 32);
            textRightCleaning.Suffix = "мм";
            textRightCleaning.TabIndex = 2;
            // 
            // CuttingEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Name = "CuttingEditor";
            Size = new Size(931, 559);
            tabControl1.ResumeLayout(false);
            tabPageBase.ResumeLayout(false);
            tabPageCutting.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPageBase;
        private TabPage tabPageCutting;
        private Controls.Editors.DfCurrencyTextBox textSalary;
        private Controls.Editors.DfDateTimePicker dateNorm;
        private Controls.Editors.DfIntegerTextBox textRate;
        private Controls.Editors.DfIntegerTextBox textProdTime;
        private Controls.Editors.DfIntegerTextBox textProduced;
        private Controls.Editors.DfDirectorySelectBox selectGroup;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfTextBox textCode;
        private Controls.Editors.DfIntegerTextBox textLength;
        private Controls.Editors.DfChoice choiceProgram;
        private GroupBox groupBox2;
        private Controls.Editors.DfIntegerTextBox textRightSweep;
        private GroupBox groupBox1;
        private Controls.Editors.DfNumericTextBox textLeftCleaning;
        private Controls.Editors.DfIntegerTextBox textLeftSweep;
        private Controls.Editors.DfNumericTextBox textRightCleaning;
    }
}
