namespace DocumentFlow.ViewModels
{
    partial class OkpdtrEditor
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
            textSignatory = new Controls.Editors.DfTextBox();
            SuspendLayout();
            // 
            // textCode
            // 
            textCode.Dock = DockStyle.Top;
            textCode.EditorFitToSize = false;
            textCode.EditorWidth = 100;
            textCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textCode.Header = "Код";
            textCode.HeaderAutoSize = false;
            textCode.HeaderTextAlign = ContentAlignment.TopLeft;
            textCode.HeaderVisible = true;
            textCode.HeaderWidth = 200;
            textCode.Location = new Point(0, 0);
            textCode.Margin = new Padding(3, 4, 3, 4);
            textCode.Multiline = false;
            textCode.Name = "textCode";
            textCode.Padding = new Padding(0, 0, 0, 7);
            textCode.Size = new Size(647, 32);
            textCode.TabIndex = 0;
            textCode.TextValue = "";
            // 
            // textName
            // 
            textName.Dock = DockStyle.Top;
            textName.EditorFitToSize = false;
            textName.EditorWidth = 400;
            textName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textName.Header = "Наименование";
            textName.HeaderAutoSize = false;
            textName.HeaderTextAlign = ContentAlignment.TopLeft;
            textName.HeaderVisible = true;
            textName.HeaderWidth = 200;
            textName.Location = new Point(0, 32);
            textName.Margin = new Padding(3, 4, 3, 4);
            textName.Multiline = false;
            textName.Name = "textName";
            textName.Padding = new Padding(0, 0, 0, 7);
            textName.Size = new Size(647, 32);
            textName.TabIndex = 1;
            textName.TextValue = "";
            // 
            // textSignatory
            // 
            textSignatory.Dock = DockStyle.Top;
            textSignatory.EditorFitToSize = false;
            textSignatory.EditorWidth = 400;
            textSignatory.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textSignatory.Header = "Наименование для договоров";
            textSignatory.HeaderAutoSize = false;
            textSignatory.HeaderTextAlign = ContentAlignment.TopLeft;
            textSignatory.HeaderVisible = true;
            textSignatory.HeaderWidth = 200;
            textSignatory.Location = new Point(0, 64);
            textSignatory.Margin = new Padding(3, 4, 3, 4);
            textSignatory.Multiline = false;
            textSignatory.Name = "textSignatory";
            textSignatory.Padding = new Padding(0, 0, 0, 7);
            textSignatory.Size = new Size(647, 32);
            textSignatory.TabIndex = 2;
            textSignatory.TextValue = "";
            // 
            // OkpdtrEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textSignatory);
            Controls.Add(textName);
            Controls.Add(textCode);
            Name = "OkpdtrEditor";
            Size = new Size(647, 316);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Editors.DfTextBox textCode;
        private Controls.Editors.DfTextBox textName;
        private Controls.Editors.DfTextBox textSignatory;
    }
}
