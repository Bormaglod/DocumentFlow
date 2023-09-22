namespace DocumentFlow.ViewModels
{
    partial class WaybillSaleEditor
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
            gridContent = new Controls.Editors.DfDataGrid();
            selectContract = new Controls.Editors.DfDirectorySelectBox();
            selectContractor = new Controls.Editors.DfDirectorySelectBox();
            panelDoc1C = new Panel();
            panelUpd = new Panel();
            labelDoc = new Label();
            toggleUpd = new Controls.Editors.DfToggleButton();
            panelInvoice = new Panel();
            dateInvoice = new Controls.Editors.DfDateTimePicker();
            textInvoiceNumber = new Controls.Editors.DfTextBox();
            panelWaybill = new Panel();
            dateWaybill = new Controls.Editors.DfDateTimePicker();
            textWaybillNumber = new Controls.Editors.DfTextBox();
            label1 = new Label();
            panel1.SuspendLayout();
            panelDoc1C.SuspendLayout();
            panelUpd.SuspendLayout();
            panelInvoice.SuspendLayout();
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
            panel1.Size = new Size(951, 32);
            panel1.TabIndex = 1;
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
            lineSplitter1.Size = new Size(951, 12);
            lineSplitter1.TabIndex = 5;
            lineSplitter1.Text = "lineSplitter1";
            // 
            // gridContent
            // 
            gridContent.Dock = DockStyle.Fill;
            gridContent.EditorFitToSize = true;
            gridContent.EditorWidth = 951;
            gridContent.EnabledEditor = true;
            gridContent.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            gridContent.Header = "gridContent";
            gridContent.HeaderAutoSize = false;
            gridContent.HeaderDock = DockStyle.Top;
            gridContent.HeaderTextAlign = ContentAlignment.TopLeft;
            gridContent.HeaderVisible = false;
            gridContent.HeaderWidth = 100;
            gridContent.Location = new Point(0, 108);
            gridContent.Name = "gridContent";
            gridContent.Padding = new Padding(0, 0, 0, 7);
            gridContent.Size = new Size(951, 270);
            gridContent.TabIndex = 8;
            gridContent.CreateRow += GridContent_CreateRow;
            gridContent.EditRow += GridContent_EditRow;
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
            selectContract.Size = new Size(951, 32);
            selectContract.TabIndex = 7;
            selectContract.DataSourceOnLoad += SelectContract_DataSourceOnLoad;
            selectContract.OpenButtonClick += SelectContract_OpenButtonClick;
            selectContract.SelectedItemChanged += SelectContract_SelectedItemChanged;
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
            selectContractor.Size = new Size(951, 32);
            selectContractor.TabIndex = 6;
            selectContractor.OpenButtonClick += SelectContractor_OpenButtonClick;
            // 
            // panelDoc1C
            // 
            panelDoc1C.Controls.Add(panelUpd);
            panelDoc1C.Controls.Add(panelInvoice);
            panelDoc1C.Controls.Add(panelWaybill);
            panelDoc1C.Controls.Add(label1);
            panelDoc1C.Dock = DockStyle.Bottom;
            panelDoc1C.Location = new Point(0, 378);
            panelDoc1C.Name = "panelDoc1C";
            panelDoc1C.Size = new Size(951, 131);
            panelDoc1C.TabIndex = 9;
            // 
            // panelUpd
            // 
            panelUpd.Controls.Add(labelDoc);
            panelUpd.Controls.Add(toggleUpd);
            panelUpd.Dock = DockStyle.Top;
            panelUpd.Location = new Point(0, 99);
            panelUpd.Name = "panelUpd";
            panelUpd.Size = new Size(951, 32);
            panelUpd.TabIndex = 4;
            // 
            // labelDoc
            // 
            labelDoc.Dock = DockStyle.Fill;
            labelDoc.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            labelDoc.Location = new Point(135, 0);
            labelDoc.Name = "labelDoc";
            labelDoc.Size = new Size(816, 32);
            labelDoc.TabIndex = 4;
            labelDoc.Text = "Счёт-фактура №0 от 00.00.0000";
            // 
            // toggleUpd
            // 
            toggleUpd.Dock = DockStyle.Left;
            toggleUpd.EditorFitToSize = false;
            toggleUpd.EditorWidth = 90;
            toggleUpd.EnabledEditor = true;
            toggleUpd.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            toggleUpd.Header = "УПД";
            toggleUpd.HeaderAutoSize = false;
            toggleUpd.HeaderDock = DockStyle.Left;
            toggleUpd.HeaderTextAlign = ContentAlignment.TopLeft;
            toggleUpd.HeaderVisible = true;
            toggleUpd.HeaderWidth = 40;
            toggleUpd.Location = new Point(0, 0);
            toggleUpd.Margin = new Padding(4, 3, 4, 3);
            toggleUpd.Name = "toggleUpd";
            toggleUpd.Padding = new Padding(0, 0, 0, 7);
            toggleUpd.Size = new Size(135, 32);
            toggleUpd.TabIndex = 3;
            toggleUpd.ToggleValue = false;
            toggleUpd.ToggleValueChanged += ToggleUpd_ToggleValueChanged;
            // 
            // panelInvoice
            // 
            panelInvoice.Controls.Add(dateInvoice);
            panelInvoice.Controls.Add(textInvoiceNumber);
            panelInvoice.Dock = DockStyle.Top;
            panelInvoice.Location = new Point(0, 67);
            panelInvoice.Name = "panelInvoice";
            panelInvoice.Size = new Size(951, 32);
            panelInvoice.TabIndex = 2;
            // 
            // dateInvoice
            // 
            dateInvoice.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateInvoice.Dock = DockStyle.Left;
            dateInvoice.EditorFitToSize = false;
            dateInvoice.EditorWidth = 95;
            dateInvoice.EnabledEditor = true;
            dateInvoice.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateInvoice.Format = DateTimePickerFormat.Short;
            dateInvoice.Header = "от";
            dateInvoice.HeaderAutoSize = false;
            dateInvoice.HeaderDock = DockStyle.Left;
            dateInvoice.HeaderTextAlign = ContentAlignment.TopLeft;
            dateInvoice.HeaderVisible = true;
            dateInvoice.HeaderWidth = 25;
            dateInvoice.Location = new Point(235, 0);
            dateInvoice.Margin = new Padding(3, 5, 3, 5);
            dateInvoice.Name = "dateInvoice";
            dateInvoice.Padding = new Padding(0, 0, 0, 7);
            dateInvoice.Required = true;
            dateInvoice.Size = new Size(120, 32);
            dateInvoice.TabIndex = 1;
            // 
            // textInvoiceNumber
            // 
            textInvoiceNumber.Dock = DockStyle.Left;
            textInvoiceNumber.EditorFitToSize = false;
            textInvoiceNumber.EditorWidth = 120;
            textInvoiceNumber.EnabledEditor = true;
            textInvoiceNumber.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textInvoiceNumber.Header = "Счёт-фактура №";
            textInvoiceNumber.HeaderAutoSize = false;
            textInvoiceNumber.HeaderDock = DockStyle.Left;
            textInvoiceNumber.HeaderTextAlign = ContentAlignment.TopLeft;
            textInvoiceNumber.HeaderVisible = true;
            textInvoiceNumber.HeaderWidth = 110;
            textInvoiceNumber.Location = new Point(0, 0);
            textInvoiceNumber.Margin = new Padding(3, 4, 3, 4);
            textInvoiceNumber.Multiline = false;
            textInvoiceNumber.Name = "textInvoiceNumber";
            textInvoiceNumber.Padding = new Padding(0, 0, 0, 7);
            textInvoiceNumber.Size = new Size(235, 32);
            textInvoiceNumber.TabIndex = 0;
            textInvoiceNumber.TextValue = "";
            // 
            // panelWaybill
            // 
            panelWaybill.Controls.Add(dateWaybill);
            panelWaybill.Controls.Add(textWaybillNumber);
            panelWaybill.Dock = DockStyle.Top;
            panelWaybill.Location = new Point(0, 28);
            panelWaybill.Name = "panelWaybill";
            panelWaybill.Padding = new Padding(0, 7, 0, 0);
            panelWaybill.Size = new Size(951, 39);
            panelWaybill.TabIndex = 1;
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
            dateWaybill.Location = new Point(235, 7);
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
            textWaybillNumber.HeaderWidth = 110;
            textWaybillNumber.Location = new Point(0, 7);
            textWaybillNumber.Margin = new Padding(3, 4, 3, 4);
            textWaybillNumber.Multiline = false;
            textWaybillNumber.Name = "textWaybillNumber";
            textWaybillNumber.Padding = new Padding(0, 0, 0, 7);
            textWaybillNumber.Size = new Size(235, 32);
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
            label1.Size = new Size(951, 28);
            label1.TabIndex = 0;
            label1.Text = "Докуметы 1С";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // WaybillSaleEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridContent);
            Controls.Add(panelDoc1C);
            Controls.Add(selectContract);
            Controls.Add(selectContractor);
            Controls.Add(lineSplitter1);
            Controls.Add(panel1);
            Name = "WaybillSaleEditor";
            Size = new Size(951, 509);
            panel1.ResumeLayout(false);
            panelDoc1C.ResumeLayout(false);
            panelUpd.ResumeLayout(false);
            panelInvoice.ResumeLayout(false);
            panelWaybill.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Controls.Editors.DfComboBox comboOrg;
        private Controls.Editors.DfDateTimePicker dateDocument;
        private Controls.Editors.DfIntegerTextBox textDocNumber;
        private Controls.LineSplitter lineSplitter1;
        private Controls.Editors.DfDataGrid gridContent;
        private Controls.Editors.DfDirectorySelectBox selectContract;
        private Controls.Editors.DfDirectorySelectBox selectContractor;
        private Panel panelDoc1C;
        private Label label1;
        private Panel panelInvoice;
        private Controls.Editors.DfDateTimePicker dateInvoice;
        private Controls.Editors.DfTextBox textInvoiceNumber;
        private Panel panelWaybill;
        private Controls.Editors.DfDateTimePicker dateWaybill;
        private Controls.Editors.DfTextBox textWaybillNumber;
        private Controls.Editors.DfToggleButton toggleUpd;
        private Panel panelUpd;
        private Label labelDoc;
    }
}
