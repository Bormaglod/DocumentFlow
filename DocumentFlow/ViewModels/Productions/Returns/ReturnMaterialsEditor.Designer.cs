namespace DocumentFlow.ViewModels
{
    partial class ReturnMaterialsEditor
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
            selectOrder = new Controls.Editors.DfDocumentSelectBox();
            selectContract = new Controls.Editors.DfDirectorySelectBox();
            selectContractor = new Controls.Editors.DfDirectorySelectBox();
            gridContent = new Controls.Editors.DfDataGrid();
            panel1.SuspendLayout();
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
            panel1.Size = new Size(1013, 32);
            panel1.TabIndex = 3;
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
            lineSplitter1.Size = new Size(1013, 12);
            lineSplitter1.TabIndex = 7;
            lineSplitter1.Text = "lineSplitter1";
            // 
            // selectOrder
            // 
            selectOrder.DisableCurrentItem = false;
            selectOrder.Dock = DockStyle.Top;
            selectOrder.EditorFitToSize = false;
            selectOrder.EditorWidth = 400;
            selectOrder.EnabledEditor = true;
            selectOrder.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectOrder.Header = "Заказ";
            selectOrder.HeaderAutoSize = false;
            selectOrder.HeaderDock = DockStyle.Left;
            selectOrder.HeaderTextAlign = ContentAlignment.TopLeft;
            selectOrder.HeaderVisible = true;
            selectOrder.HeaderWidth = 80;
            selectOrder.Location = new Point(0, 108);
            selectOrder.Margin = new Padding(3, 4, 3, 4);
            selectOrder.Name = "selectOrder";
            selectOrder.Padding = new Padding(0, 0, 0, 7);
            selectOrder.ShowDeleteButton = true;
            selectOrder.ShowOpenButton = true;
            selectOrder.Size = new Size(1013, 32);
            selectOrder.TabIndex = 16;
            selectOrder.DocumentDialogColumns += SelectOrder_DocumentDialogColumns;
            selectOrder.DataSourceOnLoad += SelectOrder_DataSourceOnLoad;
            selectOrder.OpenButtonClick += SelectOrder_OpenButtonClick;
            // 
            // selectContract
            // 
            selectContract.CanSelectFolder = false;
            selectContract.Dock = DockStyle.Top;
            selectContract.EditorFitToSize = false;
            selectContract.EditorWidth = 400;
            selectContract.EnabledEditor = true;
            selectContract.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectContract.Header = "Договор";
            selectContract.HeaderAutoSize = false;
            selectContract.HeaderDock = DockStyle.Left;
            selectContract.HeaderTextAlign = ContentAlignment.TopLeft;
            selectContract.HeaderVisible = true;
            selectContract.HeaderWidth = 80;
            selectContract.Location = new Point(0, 76);
            selectContract.Margin = new Padding(3, 4, 3, 4);
            selectContract.Name = "selectContract";
            selectContract.Padding = new Padding(0, 0, 0, 7);
            selectContract.RemoveEmptyFolders = false;
            selectContract.ShowDeleteButton = true;
            selectContract.ShowOpenButton = true;
            selectContract.Size = new Size(1013, 32);
            selectContract.TabIndex = 15;
            selectContract.DataSourceOnLoad += SelectContract_DataSourceOnLoad;
            selectContract.OpenButtonClick += SelectContract_OpenButtonClick;
            // 
            // selectContractor
            // 
            selectContractor.CanSelectFolder = false;
            selectContractor.Dock = DockStyle.Top;
            selectContractor.EditorFitToSize = false;
            selectContractor.EditorWidth = 400;
            selectContractor.EnabledEditor = true;
            selectContractor.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectContractor.Header = "Контрагент";
            selectContractor.HeaderAutoSize = false;
            selectContractor.HeaderDock = DockStyle.Left;
            selectContractor.HeaderTextAlign = ContentAlignment.TopLeft;
            selectContractor.HeaderVisible = true;
            selectContractor.HeaderWidth = 80;
            selectContractor.Location = new Point(0, 44);
            selectContractor.Margin = new Padding(3, 4, 3, 4);
            selectContractor.Name = "selectContractor";
            selectContractor.Padding = new Padding(0, 0, 0, 7);
            selectContractor.RemoveEmptyFolders = false;
            selectContractor.ShowDeleteButton = true;
            selectContractor.ShowOpenButton = true;
            selectContractor.Size = new Size(1013, 32);
            selectContractor.TabIndex = 14;
            selectContractor.DeleteButtonClick += SelectContractor_DeleteButtonClick;
            selectContractor.OpenButtonClick += SelectContractor_OpenButtonClick;
            selectContractor.UserDocumentModified += SelectContractor_UserDocumentModified;
            // 
            // gridContent
            // 
            gridContent.Dock = DockStyle.Fill;
            gridContent.EditorFitToSize = true;
            gridContent.EditorWidth = 1013;
            gridContent.EnabledEditor = true;
            gridContent.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            gridContent.Header = "Давальческий материал";
            gridContent.HeaderAutoSize = false;
            gridContent.HeaderDock = DockStyle.Top;
            gridContent.HeaderTextAlign = ContentAlignment.TopLeft;
            gridContent.HeaderVisible = true;
            gridContent.HeaderWidth = 100;
            gridContent.Location = new Point(0, 140);
            gridContent.Name = "gridContent";
            gridContent.Padding = new Padding(0, 0, 0, 7);
            gridContent.Size = new Size(1013, 439);
            gridContent.TabIndex = 17;
            gridContent.CreateRow += GridContent_CreateRow;
            gridContent.EditRow += GridContent_EditRow;
            // 
            // ReturnMaterialsEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridContent);
            Controls.Add(selectOrder);
            Controls.Add(selectContract);
            Controls.Add(selectContractor);
            Controls.Add(lineSplitter1);
            Controls.Add(panel1);
            Name = "ReturnMaterialsEditor";
            Size = new Size(1013, 579);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Controls.Editors.DfComboBox comboOrg;
        private Controls.Editors.DfDateTimePicker dateDocument;
        private Controls.Editors.DfIntegerTextBox textDocNumber;
        private Controls.LineSplitter lineSplitter1;
        private Controls.Editors.DfDocumentSelectBox selectOrder;
        private Controls.Editors.DfDirectorySelectBox selectContract;
        private Controls.Editors.DfDirectorySelectBox selectContractor;
        private Controls.Editors.DfDataGrid gridContent;
    }
}
