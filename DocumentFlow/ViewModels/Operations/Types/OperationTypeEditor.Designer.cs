namespace DocumentFlow.ViewModels
{
    partial class OperationTypeEditor
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
            textName = new Controls.Editors.DfTextBox();
            textSalary = new Controls.Editors.DfCurrencyTextBox();
            SuspendLayout();
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
            textName.HeaderWidth = 130;
            textName.Location = new Point(0, 0);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(725, 32);
            textName.TabIndex = 0;
            textName.TextValue = "";
            // 
            // textSalary
            // 
            textSalary.DecimalValue = new decimal(new int[] { 0, 0, 0, 0 });
            textSalary.Dock = DockStyle.Top;
            textSalary.EditorFitToSize = false;
            textSalary.EditorWidth = 100;
            textSalary.EnabledEditor = true;
            textSalary.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textSalary.Header = "Расценка, руб./час";
            textSalary.HeaderAutoSize = false;
            textSalary.HeaderDock = DockStyle.Left;
            textSalary.HeaderTextAlign = ContentAlignment.TopLeft;
            textSalary.HeaderVisible = true;
            textSalary.HeaderWidth = 130;
            textSalary.Location = new Point(0, 32);
            textSalary.Margin = new Padding(3, 4, 3, 4);
            textSalary.Name = "textSalary";
            textSalary.Padding = new Padding(0, 0, 0, 7);
            textSalary.Size = new Size(725, 32);
            textSalary.TabIndex = 1;
            // 
            // OperationTypeEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textSalary);
            Controls.Add(textName);
            Name = "OperationTypeEditor";
            Size = new Size(725, 238);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfCurrencyTextBox textSalary;
    }
}
