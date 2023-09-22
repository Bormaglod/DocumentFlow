namespace DocumentFlow.ViewModels
{
    partial class WireEditor
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
            textWSize = new Controls.Editors.DfNumericTextBox();
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
            textCode.HeaderTextAlign = ContentAlignment.TopLeft;
            textCode.HeaderVisible = true;
            textCode.HeaderWidth = 100;
            textCode.Location = new Point(0, 0);
            textCode.Margin = new Padding(3, 4, 3, 4);
            textCode.Multiline = false;
            textCode.Name = "textCode";
            textCode.Padding = new Padding(0, 0, 0, 7);
            textCode.Size = new Size(818, 32);
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
            textName.HeaderTextAlign = ContentAlignment.TopLeft;
            textName.HeaderVisible = true;
            textName.HeaderWidth = 100;
            textName.Location = new Point(0, 32);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(818, 32);
            textName.TabIndex = 1;
            textName.TextValue = "";
            // 
            // textWSize
            // 
            textWSize.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textWSize.Dock = DockStyle.Top;
            textWSize.EditorFitToSize = false;
            textWSize.EditorWidth = 100;
            textWSize.EnabledEditor = true;
            textWSize.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textWSize.Header = "Сечение";
            textWSize.HeaderAutoSize = false;
            textWSize.HeaderTextAlign = ContentAlignment.TopLeft;
            textWSize.HeaderVisible = true;
            textWSize.HeaderWidth = 100;
            textWSize.Location = new Point(0, 64);
            textWSize.Margin = new Padding(3, 4, 3, 4);
            textWSize.Name = "textWSize";
            textWSize.NumberDecimalDigits = 2;
            textWSize.Padding = new Padding(0, 0, 0, 7);
            textWSize.Size = new Size(818, 32);
            textWSize.TabIndex = 2;
            // 
            // WireEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textWSize);
            Controls.Add(textName);
            Controls.Add(textCode);
            Name = "WireEditor";
            Size = new Size(818, 362);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textCode;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfNumericTextBox textWSize;
    }
}
