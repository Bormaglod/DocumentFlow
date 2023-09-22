namespace DocumentFlow.ViewModels
{
    partial class MeasurementEditor
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
            textAbbreviation = new Controls.Editors.DfTextBox();
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
            textCode.Size = new Size(588, 32);
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
            textName.Size = new Size(588, 32);
            textName.TabIndex = 1;
            textName.TextValue = "";
            // 
            // textAbbreviation
            // 
            textAbbreviation.Dock = DockStyle.Top;
            textAbbreviation.EditorFitToSize = false;
            textAbbreviation.EditorWidth = 400;
            textAbbreviation.EnabledEditor = true;
            textAbbreviation.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textAbbreviation.Header = "Сокр. наименование";
            textAbbreviation.HeaderAutoSize = false;
            textAbbreviation.HeaderDock = DockStyle.Left;
            textAbbreviation.HeaderTextAlign = ContentAlignment.TopLeft;
            textAbbreviation.HeaderVisible = true;
            textAbbreviation.HeaderWidth = 140;
            textAbbreviation.Location = new Point(0, 64);
            textAbbreviation.Margin = new Padding(3, 4, 3, 4);
            textAbbreviation.Multiline = false;
            textAbbreviation.Name = "textAbbreviation";
            textAbbreviation.Padding = new Padding(0, 0, 0, 7);
            textAbbreviation.Size = new Size(588, 32);
            textAbbreviation.TabIndex = 2;
            textAbbreviation.TextValue = "";
            // 
            // MeasurementEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textAbbreviation);
            Controls.Add(textName);
            Controls.Add(textCode);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "MeasurementEditor";
            Size = new Size(588, 295);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textCode;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfTextBox textAbbreviation;
    }
}
