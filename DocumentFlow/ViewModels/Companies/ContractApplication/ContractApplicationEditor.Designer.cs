namespace DocumentFlow.ViewModels
{
    partial class ContractApplicationEditor
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
            textContract = new Controls.Editors.DfTextBox();
            textNumber = new Controls.Editors.DfTextBox();
            textName = new Controls.Editors.DfTextBox();
            dateDocument = new Controls.Editors.DfDateTimePicker();
            dateStart = new Controls.Editors.DfDateTimePicker();
            dateEnd = new Controls.Editors.DfDateTimePicker();
            gridProducts = new Controls.Editors.DfDataGrid();
            textNote = new Controls.Editors.DfTextBox();
            SuspendLayout();
            // 
            // textContract
            // 
            textContract.Dock = DockStyle.Top;
            textContract.EditorFitToSize = false;
            textContract.EditorWidth = 500;
            textContract.EnabledEditor = false;
            textContract.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textContract.Header = "Договор";
            textContract.HeaderAutoSize = false;
            textContract.HeaderDock = DockStyle.Left;
            textContract.HeaderTextAlign = ContentAlignment.TopLeft;
            textContract.HeaderVisible = true;
            textContract.HeaderWidth = 150;
            textContract.Location = new Point(0, 0);
            textContract.Margin = new Padding(3, 4, 3, 4);
            textContract.Multiline = false;
            textContract.Name = "textContract";
            textContract.Padding = new Padding(0, 0, 0, 7);
            textContract.Size = new Size(780, 32);
            textContract.TabIndex = 0;
            textContract.TextValue = "";
            // 
            // textNumber
            // 
            textNumber.Dock = DockStyle.Top;
            textNumber.EditorFitToSize = false;
            textNumber.EditorWidth = 200;
            textNumber.EnabledEditor = true;
            textNumber.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textNumber.Header = "Номер приложения";
            textNumber.HeaderAutoSize = false;
            textNumber.HeaderDock = DockStyle.Left;
            textNumber.HeaderTextAlign = ContentAlignment.TopLeft;
            textNumber.HeaderVisible = true;
            textNumber.HeaderWidth = 150;
            textNumber.Location = new Point(0, 32);
            textNumber.Margin = new Padding(3, 4, 3, 4);
            textNumber.Multiline = false;
            textNumber.Name = "textNumber";
            textNumber.Padding = new Padding(0, 0, 0, 7);
            textNumber.Size = new Size(780, 32);
            textNumber.TabIndex = 1;
            textNumber.TextValue = "";
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
            textName.HeaderWidth = 150;
            textName.Location = new Point(0, 64);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(780, 32);
            textName.TabIndex = 2;
            textName.TextValue = "";
            // 
            // dateDocument
            // 
            dateDocument.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateDocument.Dock = DockStyle.Top;
            dateDocument.EditorFitToSize = false;
            dateDocument.EditorWidth = 150;
            dateDocument.EnabledEditor = true;
            dateDocument.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateDocument.Format = DateTimePickerFormat.Short;
            dateDocument.Header = "Дата подписания";
            dateDocument.HeaderAutoSize = false;
            dateDocument.HeaderDock = DockStyle.Left;
            dateDocument.HeaderTextAlign = ContentAlignment.TopLeft;
            dateDocument.HeaderVisible = true;
            dateDocument.HeaderWidth = 150;
            dateDocument.Location = new Point(0, 96);
            dateDocument.Margin = new Padding(3, 5, 3, 5);
            dateDocument.Name = "dateDocument";
            dateDocument.Padding = new Padding(0, 0, 0, 7);
            dateDocument.Required = true;
            dateDocument.Size = new Size(780, 32);
            dateDocument.TabIndex = 3;
            // 
            // dateStart
            // 
            dateStart.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateStart.Dock = DockStyle.Top;
            dateStart.EditorFitToSize = false;
            dateStart.EditorWidth = 150;
            dateStart.EnabledEditor = true;
            dateStart.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateStart.Format = DateTimePickerFormat.Short;
            dateStart.Header = "Начало действия";
            dateStart.HeaderAutoSize = false;
            dateStart.HeaderDock = DockStyle.Left;
            dateStart.HeaderTextAlign = ContentAlignment.TopLeft;
            dateStart.HeaderVisible = true;
            dateStart.HeaderWidth = 150;
            dateStart.Location = new Point(0, 128);
            dateStart.Margin = new Padding(3, 5, 3, 5);
            dateStart.Name = "dateStart";
            dateStart.Padding = new Padding(0, 0, 0, 7);
            dateStart.Required = true;
            dateStart.Size = new Size(780, 32);
            dateStart.TabIndex = 4;
            // 
            // dateEnd
            // 
            dateEnd.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateEnd.Dock = DockStyle.Top;
            dateEnd.EditorFitToSize = false;
            dateEnd.EditorWidth = 150;
            dateEnd.EnabledEditor = true;
            dateEnd.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateEnd.Format = DateTimePickerFormat.Short;
            dateEnd.Header = "Окончание действия";
            dateEnd.HeaderAutoSize = false;
            dateEnd.HeaderDock = DockStyle.Left;
            dateEnd.HeaderTextAlign = ContentAlignment.TopLeft;
            dateEnd.HeaderVisible = true;
            dateEnd.HeaderWidth = 150;
            dateEnd.Location = new Point(0, 160);
            dateEnd.Margin = new Padding(3, 5, 3, 5);
            dateEnd.Name = "dateEnd";
            dateEnd.Padding = new Padding(0, 0, 0, 7);
            dateEnd.Required = false;
            dateEnd.Size = new Size(780, 32);
            dateEnd.TabIndex = 5;
            // 
            // gridProducts
            // 
            gridProducts.Dock = DockStyle.Fill;
            gridProducts.EditorFitToSize = true;
            gridProducts.EditorWidth = 780;
            gridProducts.EnabledEditor = true;
            gridProducts.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            gridProducts.Header = "gridProducts";
            gridProducts.HeaderAutoSize = false;
            gridProducts.HeaderDock = DockStyle.Top;
            gridProducts.HeaderTextAlign = ContentAlignment.TopLeft;
            gridProducts.HeaderVisible = false;
            gridProducts.HeaderWidth = 100;
            gridProducts.Location = new Point(0, 192);
            gridProducts.Name = "gridProducts";
            gridProducts.Padding = new Padding(0, 0, 0, 7);
            gridProducts.Size = new Size(780, 301);
            gridProducts.TabIndex = 6;
            // 
            // textNote
            // 
            textNote.Dock = DockStyle.Bottom;
            textNote.EditorFitToSize = true;
            textNote.EditorWidth = 630;
            textNote.EnabledEditor = true;
            textNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textNote.Header = "Примечание";
            textNote.HeaderAutoSize = false;
            textNote.HeaderDock = DockStyle.Left;
            textNote.HeaderTextAlign = ContentAlignment.TopLeft;
            textNote.HeaderVisible = true;
            textNote.HeaderWidth = 150;
            textNote.Location = new Point(0, 493);
            textNote.Margin = new Padding(3, 4, 3, 4);
            textNote.Multiline = true;
            textNote.Name = "textNote";
            textNote.Padding = new Padding(0, 0, 0, 7);
            textNote.Size = new Size(780, 70);
            textNote.TabIndex = 7;
            textNote.TextValue = "";
            // 
            // ContractApplicationEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gridProducts);
            Controls.Add(textNote);
            Controls.Add(dateEnd);
            Controls.Add(dateStart);
            Controls.Add(dateDocument);
            Controls.Add(textName);
            Controls.Add(textNumber);
            Controls.Add(textContract);
            Name = "ContractApplicationEditor";
            Size = new Size(780, 563);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textContract;
        private Controls.Editors.DfTextBox textNumber;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfDateTimePicker dateDocument;
        private Controls.Editors.DfDateTimePicker dateStart;
        private Controls.Editors.DfDateTimePicker dateEnd;
        private Controls.Editors.DfDataGrid gridProducts;
        private Controls.Editors.DfTextBox textNote;
    }
}
