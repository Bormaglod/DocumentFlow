namespace DocumentFlow.ViewModels
{
    partial class Wage1cEditor
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
            panel1 = new Panel();
            comboOrg = new Controls.Editors.DfComboBox();
            dateDocument = new Controls.Editors.DfDateTimePicker();
            textDocNumber = new Controls.Editors.DfIntegerTextBox();
            lineSplitter1 = new Controls.LineSplitter();
            panel2 = new Panel();
            textYear = new Controls.Editors.DfIntegerTextBox();
            choiceMonth = new Controls.Editors.DfChoice();
            gridEmps = new Controls.Editors.DfDataGrid();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(comboOrg);
            panel1.Controls.Add(dateDocument);
            panel1.Controls.Add(textDocNumber);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1027, 32);
            panel1.TabIndex = 2;
            // 
            // comboOrg
            // 
            comboOrg.DisplayMember = "ItemName";
            comboOrg.Dock = DockStyle.Left;
            comboOrg.EditorFitToSize = false;
            comboOrg.EditorWidth = 200;
            comboOrg.EnabledEditor = true;
            comboOrg.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboOrg.Header = "Организация";
            comboOrg.HeaderAutoSize = false;
            comboOrg.HeaderDock = DockStyle.Left;
            comboOrg.HeaderTextAlign = ContentAlignment.TopLeft;
            comboOrg.HeaderVisible = true;
            comboOrg.HeaderWidth = 100;
            comboOrg.Location = new Point(365, 0);
            comboOrg.Margin = new Padding(3, 4, 3, 4);
            comboOrg.Name = "comboOrg";
            comboOrg.Padding = new Padding(0, 0, 0, 7);
            comboOrg.ShowDeleteButton = true;
            comboOrg.ShowOpenButton = false;
            comboOrg.Size = new Size(305, 32);
            comboOrg.TabIndex = 2;
            // 
            // dateDocument
            // 
            dateDocument.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateDocument.Dock = DockStyle.Left;
            dateDocument.EditorFitToSize = false;
            dateDocument.EditorWidth = 170;
            dateDocument.EnabledEditor = true;
            dateDocument.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateDocument.Format = DateTimePickerFormat.Custom;
            dateDocument.Header = "от";
            dateDocument.HeaderAutoSize = false;
            dateDocument.HeaderDock = DockStyle.Left;
            dateDocument.HeaderTextAlign = ContentAlignment.TopLeft;
            dateDocument.HeaderVisible = true;
            dateDocument.HeaderWidth = 25;
            dateDocument.Location = new Point(165, 0);
            dateDocument.Margin = new Padding(3, 5, 3, 5);
            dateDocument.Name = "dateDocument";
            dateDocument.Padding = new Padding(0, 0, 0, 7);
            dateDocument.Required = true;
            dateDocument.Size = new Size(200, 32);
            dateDocument.TabIndex = 1;
            // 
            // textDocNumber
            // 
            textDocNumber.Dock = DockStyle.Left;
            textDocNumber.EditorFitToSize = false;
            textDocNumber.EditorWidth = 100;
            textDocNumber.EnabledEditor = true;
            textDocNumber.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textDocNumber.Header = "Номер";
            textDocNumber.HeaderAutoSize = false;
            textDocNumber.HeaderDock = DockStyle.Left;
            textDocNumber.HeaderTextAlign = ContentAlignment.TopLeft;
            textDocNumber.HeaderVisible = true;
            textDocNumber.HeaderWidth = 60;
            textDocNumber.IntegerValue = 0L;
            textDocNumber.Location = new Point(0, 0);
            textDocNumber.Margin = new Padding(3, 4, 3, 4);
            textDocNumber.Name = "textDocNumber";
            textDocNumber.NumberGroupSeparator = " ";
            textDocNumber.NumberGroupSizes = new int[] { 0 };
            textDocNumber.Padding = new Padding(0, 0, 0, 7);
            textDocNumber.ShowSuffix = false;
            textDocNumber.Size = new Size(165, 32);
            textDocNumber.Suffix = "суффикс";
            textDocNumber.TabIndex = 0;
            // 
            // lineSplitter1
            // 
            lineSplitter1.Dock = DockStyle.Top;
            lineSplitter1.LineColor = Color.FromArgb(22, 165, 220);
            lineSplitter1.LinePlacement = DocumentFlow.Controls.LinePlacement.HorizontalTop;
            lineSplitter1.LineSize = 3;
            lineSplitter1.Location = new Point(0, 32);
            lineSplitter1.Name = "lineSplitter1";
            lineSplitter1.Size = new Size(1027, 12);
            lineSplitter1.TabIndex = 6;
            lineSplitter1.Text = "lineSplitter1";
            // 
            // panel2
            // 
            panel2.Controls.Add(textYear);
            panel2.Controls.Add(choiceMonth);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 44);
            panel2.Name = "panel2";
            panel2.Size = new Size(1027, 32);
            panel2.TabIndex = 7;
            // 
            // textYear
            // 
            textYear.Dock = DockStyle.Left;
            textYear.EditorFitToSize = false;
            textYear.EditorWidth = 100;
            textYear.EnabledEditor = true;
            textYear.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textYear.Header = "год";
            textYear.HeaderAutoSize = false;
            textYear.HeaderDock = DockStyle.Left;
            textYear.HeaderTextAlign = ContentAlignment.TopLeft;
            textYear.HeaderVisible = true;
            textYear.HeaderWidth = 35;
            textYear.IntegerValue = 0L;
            textYear.Location = new Point(365, 0);
            textYear.Margin = new Padding(3, 4, 3, 4);
            textYear.Name = "textYear";
            textYear.NumberGroupSeparator = " ";
            textYear.NumberGroupSizes = new int[] { 0 };
            textYear.Padding = new Padding(0, 0, 0, 7);
            textYear.ShowSuffix = false;
            textYear.Size = new Size(140, 32);
            textYear.Suffix = "суффикс";
            textYear.TabIndex = 1;
            // 
            // choiceMonth
            // 
            choiceMonth.Dock = DockStyle.Left;
            choiceMonth.EditorFitToSize = false;
            choiceMonth.EditorWidth = 200;
            choiceMonth.EnabledEditor = true;
            choiceMonth.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            choiceMonth.Header = "Расчётный период: месяц";
            choiceMonth.HeaderAutoSize = false;
            choiceMonth.HeaderDock = DockStyle.Left;
            choiceMonth.HeaderTextAlign = ContentAlignment.TopLeft;
            choiceMonth.HeaderVisible = true;
            choiceMonth.HeaderWidth = 165;
            choiceMonth.Location = new Point(0, 0);
            choiceMonth.Margin = new Padding(3, 4, 3, 4);
            choiceMonth.Name = "choiceMonth";
            choiceMonth.Padding = new Padding(0, 0, 0, 7);
            choiceMonth.ShowDeleteButton = true;
            choiceMonth.Size = new Size(365, 32);
            choiceMonth.TabIndex = 0;
            // 
            // gridEmps
            // 
            gridEmps.Dock = DockStyle.Fill;
            gridEmps.EditorFitToSize = true;
            gridEmps.EditorWidth = 1027;
            gridEmps.EnabledEditor = true;
            gridEmps.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            gridEmps.Header = "Сотрудники";
            gridEmps.HeaderAutoSize = false;
            gridEmps.HeaderDock = DockStyle.Top;
            gridEmps.HeaderTextAlign = ContentAlignment.TopLeft;
            gridEmps.HeaderVisible = true;
            gridEmps.HeaderWidth = 100;
            gridEmps.Location = new Point(0, 76);
            gridEmps.Name = "gridEmps";
            gridEmps.Padding = new Padding(0, 0, 0, 7);
            gridEmps.Size = new Size(1027, 402);
            gridEmps.TabIndex = 8;
            gridEmps.CreateRow += GridEmps_CreateRow;
            gridEmps.EditRow += GridEmps_EditRow;
            // 
            // GrossPayrollEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridEmps);
            Controls.Add(panel2);
            Controls.Add(lineSplitter1);
            Controls.Add(panel1);
            Name = "GrossPayrollEditor";
            Size = new Size(1027, 478);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Controls.Editors.DfComboBox comboOrg;
        private Controls.Editors.DfDateTimePicker dateDocument;
        private Controls.Editors.DfIntegerTextBox textDocNumber;
        private Controls.LineSplitter lineSplitter1;
        private Panel panel2;
        private Controls.Editors.DfChoice choiceMonth;
        private Controls.Editors.DfIntegerTextBox textYear;
        private Controls.Editors.DfDataGrid gridEmps;
    }
}
