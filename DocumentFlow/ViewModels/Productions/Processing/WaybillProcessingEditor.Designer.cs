namespace DocumentFlow.ViewModels
{
    partial class WaybillProcessingEditor
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
            panel2 = new Panel();
            panelWaybill = new Panel();
            dateWaybill = new Controls.Editors.DfDateTimePicker();
            textWaybillNumber = new Controls.Editors.DfTextBox();
            label1 = new Label();
            gridContent = new Controls.Editors.DfDataGrid();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panelWaybill.SuspendLayout();
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
            panel1.Size = new Size(1073, 32);
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
            lineSplitter1.Size = new Size(1073, 12);
            lineSplitter1.TabIndex = 6;
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
            selectOrder.Size = new Size(1073, 32);
            selectOrder.TabIndex = 13;
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
            selectContract.Size = new Size(1073, 32);
            selectContract.TabIndex = 12;
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
            selectContractor.Size = new Size(1073, 32);
            selectContractor.TabIndex = 11;
            selectContractor.DeleteButtonClick += SelectContractor_DeleteButtonClick;
            selectContractor.OpenButtonClick += SelectContractor_OpenButtonClick;
            selectContractor.UserDocumentModified += SelectContractor_UserDocumentModified;
            // 
            // panel2
            // 
            panel2.Controls.Add(panelWaybill);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 527);
            panel2.Name = "panel2";
            panel2.Size = new Size(1073, 67);
            panel2.TabIndex = 14;
            // 
            // panelWaybill
            // 
            panelWaybill.Controls.Add(dateWaybill);
            panelWaybill.Controls.Add(textWaybillNumber);
            panelWaybill.Dock = DockStyle.Top;
            panelWaybill.Location = new Point(0, 28);
            panelWaybill.Name = "panelWaybill";
            panelWaybill.Padding = new Padding(0, 7, 0, 0);
            panelWaybill.Size = new Size(1073, 39);
            panelWaybill.TabIndex = 2;
            // 
            // dateWaybill
            // 
            dateWaybill.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateWaybill.Dock = DockStyle.Left;
            dateWaybill.EditorFitToSize = false;
            dateWaybill.EditorWidth = 95;
            dateWaybill.EnabledEditor = true;
            dateWaybill.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateWaybill.Format = DateTimePickerFormat.Short;
            dateWaybill.Header = "от";
            dateWaybill.HeaderAutoSize = false;
            dateWaybill.HeaderDock = DockStyle.Left;
            dateWaybill.HeaderTextAlign = ContentAlignment.TopLeft;
            dateWaybill.HeaderVisible = true;
            dateWaybill.HeaderWidth = 25;
            dateWaybill.Location = new Point(220, 7);
            dateWaybill.Margin = new Padding(3, 5, 3, 5);
            dateWaybill.Name = "dateWaybill";
            dateWaybill.Padding = new Padding(0, 0, 0, 7);
            dateWaybill.Required = true;
            dateWaybill.Size = new Size(120, 32);
            dateWaybill.TabIndex = 1;
            // 
            // textWaybillNumber
            // 
            textWaybillNumber.Dock = DockStyle.Left;
            textWaybillNumber.EditorFitToSize = false;
            textWaybillNumber.EditorWidth = 120;
            textWaybillNumber.EnabledEditor = true;
            textWaybillNumber.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textWaybillNumber.Header = "Накладная №";
            textWaybillNumber.HeaderAutoSize = false;
            textWaybillNumber.HeaderDock = DockStyle.Left;
            textWaybillNumber.HeaderTextAlign = ContentAlignment.TopLeft;
            textWaybillNumber.HeaderVisible = true;
            textWaybillNumber.HeaderWidth = 95;
            textWaybillNumber.Location = new Point(0, 7);
            textWaybillNumber.Margin = new Padding(3, 4, 3, 4);
            textWaybillNumber.Multiline = false;
            textWaybillNumber.Name = "textWaybillNumber";
            textWaybillNumber.Padding = new Padding(0, 0, 0, 7);
            textWaybillNumber.Size = new Size(220, 32);
            textWaybillNumber.TabIndex = 0;
            textWaybillNumber.TextValue = "";
            // 
            // label1
            // 
            label1.BackColor = SystemColors.InactiveCaption;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(1073, 28);
            label1.TabIndex = 1;
            label1.Text = "Докуметы 1С";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gridContent
            // 
            gridContent.Dock = DockStyle.Fill;
            gridContent.EditorFitToSize = true;
            gridContent.EditorWidth = 1073;
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
            gridContent.Size = new Size(1073, 387);
            gridContent.TabIndex = 15;
            gridContent.DialogParameters += GridContent_DialogParameters;
            gridContent.ConfirmGeneratingColumn += GridContent_ConfirmGeneratingColumn;
            // 
            // WaybillProcessingEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridContent);
            Controls.Add(panel2);
            Controls.Add(selectOrder);
            Controls.Add(selectContract);
            Controls.Add(selectContractor);
            Controls.Add(lineSplitter1);
            Controls.Add(panel1);
            Name = "WaybillProcessingEditor";
            Size = new Size(1073, 594);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panelWaybill.ResumeLayout(false);
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
        private Panel panel2;
        private Controls.Editors.DfDataGrid gridContent;
        private Label label1;
        private Panel panelWaybill;
        private Controls.Editors.DfDateTimePicker dateWaybill;
        private Controls.Editors.DfTextBox textWaybillNumber;
    }
}
