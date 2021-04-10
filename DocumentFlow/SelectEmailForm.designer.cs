namespace DocumentFlow
{
    partial class SelectEmailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.textBoxExt1 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.listFiles = new Syncfusion.WinForms.ListView.SfListView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.buttonDelete = new Syncfusion.WinForms.Controls.SfButton();
            this.buttonAdd = new Syncfusion.WinForms.Controls.SfButton();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textSubject = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.comboTo = new Syncfusion.WinForms.ListView.SfComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboFrom = new Syncfusion.WinForms.ListView.SfComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.buttonSend = new Syncfusion.WinForms.Controls.SfButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textSubject)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboTo)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboFrom)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(8);
            this.panel1.Size = new System.Drawing.Size(640, 369);
            this.panel1.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.textBoxExt1);
            this.panel8.Controls.Add(this.label5);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(8, 210);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(624, 151);
            this.panel8.TabIndex = 15;
            // 
            // textBoxExt1
            // 
            this.textBoxExt1.BeforeTouchSize = new System.Drawing.Size(550, 28);
            this.textBoxExt1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.textBoxExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxExt1.Location = new System.Drawing.Point(74, 0);
            this.textBoxExt1.Multiline = true;
            this.textBoxExt1.Name = "textBoxExt1";
            this.textBoxExt1.Size = new System.Drawing.Size(550, 151);
            this.textBoxExt1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.textBoxExt1.TabIndex = 1;
            this.textBoxExt1.ThemeName = "Metro";
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 151);
            this.label5.TabIndex = 0;
            this.label5.Text = "Текст:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel9);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(8, 110);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(624, 100);
            this.panel6.TabIndex = 14;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.listFiles);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(74, 0);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.panel9.Size = new System.Drawing.Size(442, 100);
            this.panel9.TabIndex = 12;
            // 
            // listFiles
            // 
            this.listFiles.AccessibleName = "ScrollControl";
            this.listFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listFiles.Location = new System.Drawing.Point(0, 0);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(442, 94);
            this.listFiles.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.listFiles.TabIndex = 10;
            this.listFiles.Text = "sfListView1";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.buttonDelete);
            this.panel7.Controls.Add(this.buttonAdd);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(516, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(108, 100);
            this.panel7.TabIndex = 11;
            // 
            // buttonDelete
            // 
            this.buttonDelete.AccessibleName = "Button";
            this.buttonDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonDelete.Location = new System.Drawing.Point(6, 34);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(96, 28);
            this.buttonDelete.TabIndex = 1;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.AccessibleName = "Button";
            this.buttonAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAdd.Location = new System.Drawing.Point(6, 0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(96, 28);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 100);
            this.label4.TabIndex = 6;
            this.label4.Text = "Вложения:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.textSubject);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(8, 76);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.panel5.Size = new System.Drawing.Size(624, 34);
            this.panel5.TabIndex = 13;
            // 
            // textSubject
            // 
            this.textSubject.BeforeTouchSize = new System.Drawing.Size(550, 28);
            this.textSubject.Dock = System.Windows.Forms.DockStyle.Top;
            this.textSubject.Location = new System.Drawing.Point(74, 0);
            this.textSubject.Multiline = true;
            this.textSubject.Name = "textSubject";
            this.textSubject.Size = new System.Drawing.Size(550, 28);
            this.textSubject.TabIndex = 9;
            this.textSubject.SizeChanged += new System.EventHandler(this.ControlSizeChanged);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 28);
            this.label3.TabIndex = 5;
            this.label3.Text = "Тема:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.comboTo);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(8, 42);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.panel4.Size = new System.Drawing.Size(624, 34);
            this.panel4.TabIndex = 12;
            // 
            // comboTo
            // 
            this.comboTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboTo.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.MultiSelection;
            this.comboTo.DelimiterChar = ";";
            this.comboTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboTo.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.comboTo.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboTo.Location = new System.Drawing.Point(74, 0);
            this.comboTo.Name = "comboTo";
            this.comboTo.Size = new System.Drawing.Size(550, 28);
            this.comboTo.Style.EditorStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.comboTo.Style.EditorStyle.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.comboTo.Style.EditorStyle.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.comboTo.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboTo.TabIndex = 7;
            this.comboTo.SizeChanged += new System.EventHandler(this.ControlSizeChanged);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "Кому:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.comboFrom);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(8, 8);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.panel3.Size = new System.Drawing.Size(624, 34);
            this.panel3.TabIndex = 11;
            // 
            // comboFrom
            // 
            this.comboFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboFrom.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.MultiSelection;
            this.comboFrom.DelimiterChar = ";";
            this.comboFrom.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboFrom.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.comboFrom.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboFrom.Location = new System.Drawing.Point(74, 0);
            this.comboFrom.Name = "comboFrom";
            this.comboFrom.Size = new System.Drawing.Size(550, 28);
            this.comboFrom.Style.DropDownButtonStyle.FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.comboFrom.Style.DropDownButtonStyle.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.comboFrom.Style.DropDownStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.comboFrom.Style.EditorStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.comboFrom.Style.EditorStyle.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.comboFrom.Style.EditorStyle.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.comboFrom.Style.TokenStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.comboFrom.Style.TokenStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.comboFrom.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboFrom.TabIndex = 8;
            this.comboFrom.SizeChanged += new System.EventHandler(this.ControlSizeChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "От:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.buttonCancel);
            this.panel2.Controls.Add(this.buttonSend);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 369);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(640, 45);
            this.panel2.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleName = "Button";
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancel.Location = new System.Drawing.Point(532, 7);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 28);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Отменить";
            // 
            // buttonSend
            // 
            this.buttonSend.AccessibleName = "Button";
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSend.Location = new System.Drawing.Point(430, 7);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(96, 28);
            this.buttonSend.TabIndex = 0;
            this.buttonSend.Text = "Отправить";
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Выберите файл";
            // 
            // SelectEmailForm
            // 
            this.AcceptButton = this.buttonSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(640, 414);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectEmailForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Выбор адреса эл. почты";
            this.panel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textSubject)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboTo)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboFrom)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Syncfusion.WinForms.Controls.SfButton buttonSend;
        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private System.Windows.Forms.Panel panel6;
        private Syncfusion.WinForms.ListView.SfListView listFiles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private Syncfusion.WinForms.ListView.SfComboBox comboTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private Syncfusion.WinForms.ListView.SfComboBox comboFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel7;
        private Syncfusion.WinForms.Controls.SfButton buttonDelete;
        private Syncfusion.WinForms.Controls.SfButton buttonAdd;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel8;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel9;
    }
}
