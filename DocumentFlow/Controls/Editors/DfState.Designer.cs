
namespace DocumentFlow.Controls.Editors
{
    partial class DfState
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonAction = new Syncfusion.WinForms.Controls.SfButton();
            this.labelStateValue = new System.Windows.Forms.Label();
            this.labelState = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.buttonAction);
            this.panel1.Controls.Add(this.labelStateValue);
            this.panel1.Controls.Add(this.labelState);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 7);
            this.panel1.Size = new System.Drawing.Size(827, 37);
            this.panel1.TabIndex = 1;
            // 
            // buttonAction
            // 
            this.buttonAction.AccessibleName = "Button";
            this.buttonAction.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonAction.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAction.Location = new System.Drawing.Point(223, 5);
            this.buttonAction.Name = "buttonAction";
            this.buttonAction.Size = new System.Drawing.Size(96, 25);
            this.buttonAction.TabIndex = 2;
            this.buttonAction.Text = "Утвердить";
            this.buttonAction.UseVisualStyleBackColor = true;
            this.buttonAction.Click += new System.EventHandler(this.ButtonAction_Click);
            // 
            // labelStateValue
            // 
            this.labelStateValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelStateValue.ForeColor = System.Drawing.Color.Green;
            this.labelStateValue.Location = new System.Drawing.Point(130, 5);
            this.labelStateValue.Name = "labelStateValue";
            this.labelStateValue.Size = new System.Drawing.Size(93, 25);
            this.labelStateValue.TabIndex = 1;
            this.labelStateValue.Text = "Подготовлена";
            this.labelStateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelState
            // 
            this.labelState.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelState.Location = new System.Drawing.Point(0, 5);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(130, 25);
            this.labelState.TabIndex = 0;
            this.labelState.Text = "Состояние";
            this.labelState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LU_State
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "LU_State";
            this.Size = new System.Drawing.Size(827, 41);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Syncfusion.WinForms.Controls.SfButton buttonAction;
        private System.Windows.Forms.Label labelStateValue;
        private System.Windows.Forms.Label labelState;
    }
}
