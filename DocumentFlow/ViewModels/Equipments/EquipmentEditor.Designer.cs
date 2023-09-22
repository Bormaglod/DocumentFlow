namespace DocumentFlow.ViewModels
{
    partial class EquipmentEditor
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
            textSerial = new Controls.Editors.DfTextBox();
            dateCommissioning = new Controls.Editors.DfDateTimePicker();
            textStartingHits = new Controls.Editors.DfIntegerTextBox();
            toggleTool = new Controls.Editors.DfToggleButton();
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
            textCode.Size = new Size(564, 32);
            textCode.TabIndex = 0;
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
            textName.Location = new Point(0, 32);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(564, 32);
            textName.TabIndex = 1;
            textName.TextValue = "";
            // 
            // textSerial
            // 
            textSerial.Dock = DockStyle.Top;
            textSerial.EditorFitToSize = false;
            textSerial.EditorWidth = 100;
            textSerial.EnabledEditor = true;
            textSerial.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textSerial.Header = "Серийный номер";
            textSerial.HeaderAutoSize = false;
            textSerial.HeaderDock = DockStyle.Left;
            textSerial.HeaderTextAlign = ContentAlignment.TopLeft;
            textSerial.HeaderVisible = true;
            textSerial.HeaderWidth = 140;
            textSerial.Location = new Point(0, 64);
            textSerial.Margin = new Padding(3, 4, 3, 4);
            textSerial.Multiline = false;
            textSerial.Name = "textSerial";
            textSerial.Padding = new Padding(0, 0, 0, 7);
            textSerial.Size = new Size(564, 32);
            textSerial.TabIndex = 2;
            textSerial.TextValue = "";
            // 
            // dateCommissioning
            // 
            dateCommissioning.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateCommissioning.Dock = DockStyle.Top;
            dateCommissioning.EditorFitToSize = false;
            dateCommissioning.EditorWidth = 150;
            dateCommissioning.EnabledEditor = true;
            dateCommissioning.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateCommissioning.Format = DateTimePickerFormat.Long;
            dateCommissioning.Header = "Ввод в экспл.";
            dateCommissioning.HeaderAutoSize = false;
            dateCommissioning.HeaderDock = DockStyle.Left;
            dateCommissioning.HeaderTextAlign = ContentAlignment.TopLeft;
            dateCommissioning.HeaderVisible = true;
            dateCommissioning.HeaderWidth = 140;
            dateCommissioning.Location = new Point(0, 96);
            dateCommissioning.Margin = new Padding(3, 5, 3, 5);
            dateCommissioning.Name = "dateCommissioning";
            dateCommissioning.Padding = new Padding(0, 0, 0, 7);
            dateCommissioning.Required = false;
            dateCommissioning.Size = new Size(564, 32);
            dateCommissioning.TabIndex = 3;
            // 
            // textStartingHits
            // 
            textStartingHits.Dock = DockStyle.Top;
            textStartingHits.EditorFitToSize = false;
            textStartingHits.EditorWidth = 150;
            textStartingHits.EnabledEditor = true;
            textStartingHits.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textStartingHits.Header = "Кол-во опрессовок";
            textStartingHits.HeaderAutoSize = false;
            textStartingHits.HeaderDock = DockStyle.Left;
            textStartingHits.HeaderTextAlign = ContentAlignment.TopLeft;
            textStartingHits.HeaderVisible = true;
            textStartingHits.HeaderWidth = 140;
            textStartingHits.IntegerValue = 0L;
            textStartingHits.Location = new Point(0, 128);
            textStartingHits.Margin = new Padding(3, 4, 3, 4);
            textStartingHits.Name = "textStartingHits";
            textStartingHits.Padding = new Padding(0, 0, 0, 7);
            textStartingHits.ShowSuffix = false;
            textStartingHits.Size = new Size(564, 32);
            textStartingHits.Suffix = "суффикс";
            textStartingHits.TabIndex = 4;
            // 
            // toggleTool
            // 
            toggleTool.Dock = DockStyle.Top;
            toggleTool.EditorFitToSize = false;
            toggleTool.EditorWidth = 90;
            toggleTool.EnabledEditor = true;
            toggleTool.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            toggleTool.Header = "Это инструмент";
            toggleTool.HeaderAutoSize = false;
            toggleTool.HeaderDock = DockStyle.Left;
            toggleTool.HeaderTextAlign = ContentAlignment.TopLeft;
            toggleTool.HeaderVisible = true;
            toggleTool.HeaderWidth = 140;
            toggleTool.Location = new Point(0, 160);
            toggleTool.Margin = new Padding(4, 3, 4, 3);
            toggleTool.Name = "toggleTool";
            toggleTool.Padding = new Padding(0, 0, 0, 7);
            toggleTool.Size = new Size(564, 32);
            toggleTool.TabIndex = 5;
            toggleTool.ToggleValue = false;
            // 
            // EquipmentEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(toggleTool);
            Controls.Add(textStartingHits);
            Controls.Add(dateCommissioning);
            Controls.Add(textSerial);
            Controls.Add(textName);
            Controls.Add(textCode);
            Name = "EquipmentEditor";
            Size = new Size(564, 211);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textCode;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfTextBox textSerial;
        private Controls.Editors.DfDateTimePicker dateCommissioning;
        private Controls.Editors.DfIntegerTextBox textStartingHits;
        private Controls.Editors.DfToggleButton toggleTool;
    }
}
