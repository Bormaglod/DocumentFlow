namespace DocumentFlow.Dialogs
{
    partial class GroupColumnsDialog
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
            listViewAll = new Syncfusion.WinForms.ListView.SfListView();
            buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            buttonOk = new Syncfusion.WinForms.Controls.SfButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            listViewSelected = new Syncfusion.WinForms.ListView.SfListView();
            flowLayoutPanel1 = new FlowLayoutPanel();
            buttonSelect = new Syncfusion.WinForms.Controls.SfButton();
            buttonRemove = new Syncfusion.WinForms.Controls.SfButton();
            flowLayoutPanel2 = new FlowLayoutPanel();
            buttonUp = new Syncfusion.WinForms.Controls.SfButton();
            buttonDown = new Syncfusion.WinForms.Controls.SfButton();
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // listViewAll
            // 
            listViewAll.AccessibleName = "ScrollControl";
            listViewAll.Dock = DockStyle.Fill;
            listViewAll.Location = new Point(3, 28);
            listViewAll.Name = "listViewAll";
            listViewAll.Size = new Size(240, 260);
            listViewAll.Style.BorderColor = Color.FromArgb(204, 204, 204);
            listViewAll.TabIndex = 0;
            listViewAll.Text = "sfListView1";
            listViewAll.DoubleClick += ListViewAll_DoubleClick;
            // 
            // buttonCancel
            // 
            buttonCancel.AccessibleName = "Button";
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancel.Location = new Point(460, 10);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(112, 28);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Отменить";
            // 
            // buttonOk
            // 
            buttonOk.AccessibleName = "Button";
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonOk.Location = new Point(342, 10);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(112, 28);
            buttonOk.TabIndex = 4;
            buttonOk.Text = "Сохранить";
            buttonOk.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 36F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 36F));
            tableLayoutPanel1.Controls.Add(listViewAll, 0, 1);
            tableLayoutPanel1.Controls.Add(listViewSelected, 2, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 3, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(10, 10);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(564, 291);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // listViewSelected
            // 
            listViewSelected.AccessibleName = "ScrollControl";
            listViewSelected.Dock = DockStyle.Fill;
            listViewSelected.Location = new Point(285, 28);
            listViewSelected.Name = "listViewSelected";
            listViewSelected.Size = new Size(240, 260);
            listViewSelected.Style.BorderColor = Color.FromArgb(204, 204, 204);
            listViewSelected.TabIndex = 1;
            listViewSelected.Text = "sfListView2";
            listViewSelected.DoubleClick += ListViewSelected_DoubleClick;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(buttonSelect);
            flowLayoutPanel1.Controls.Add(buttonRemove);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(249, 28);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(30, 260);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // buttonSelect
            // 
            buttonSelect.AccessibleName = "Button";
            buttonSelect.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonSelect.ImageSize = new Size(16, 16);
            buttonSelect.Location = new Point(3, 3);
            buttonSelect.Name = "buttonSelect";
            buttonSelect.Size = new Size(24, 24);
            buttonSelect.Style.Image = Properties.Resources.icons8_right_16;
            buttonSelect.TabIndex = 0;
            buttonSelect.Text = "sfButton1";
            buttonSelect.UseVisualStyleBackColor = true;
            buttonSelect.Click += ButtonSelect_Click;
            // 
            // buttonRemove
            // 
            buttonRemove.AccessibleName = "Button";
            buttonRemove.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonRemove.ImageSize = new Size(16, 16);
            buttonRemove.Location = new Point(3, 33);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(24, 24);
            buttonRemove.Style.Image = Properties.Resources.icons8_left_16;
            buttonRemove.TabIndex = 1;
            buttonRemove.Text = "sfButton2";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += ButtonRemove_Click;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(buttonUp);
            flowLayoutPanel2.Controls.Add(buttonDown);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(531, 28);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(30, 260);
            flowLayoutPanel2.TabIndex = 3;
            // 
            // buttonUp
            // 
            buttonUp.AccessibleName = "Button";
            buttonUp.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonUp.ImageSize = new Size(16, 16);
            buttonUp.Location = new Point(3, 3);
            buttonUp.Name = "buttonUp";
            buttonUp.Size = new Size(24, 24);
            buttonUp.Style.Image = Properties.Resources.icons8_up_16;
            buttonUp.TabIndex = 0;
            buttonUp.Text = "sfButton3";
            buttonUp.UseVisualStyleBackColor = true;
            buttonUp.Click += ButtonUp_Click;
            // 
            // buttonDown
            // 
            buttonDown.AccessibleName = "Button";
            buttonDown.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonDown.ImageSize = new Size(16, 16);
            buttonDown.Location = new Point(3, 33);
            buttonDown.Name = "buttonDown";
            buttonDown.Size = new Size(24, 24);
            buttonDown.Style.Image = Properties.Resources.icons8_down_16;
            buttonDown.TabIndex = 1;
            buttonDown.Text = "sfButton4";
            buttonDown.UseVisualStyleBackColor = true;
            buttonDown.Click += ButtonDown_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(143, 15);
            label1.TabIndex = 4;
            label1.Text = "Доступные группировки";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(285, 0);
            label2.Name = "label2";
            label2.Size = new Size(140, 15);
            label2.TabIndex = 5;
            label2.Text = "Выбранные групировки";
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonCancel);
            panel1.Controls.Add(buttonOk);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 311);
            panel1.Name = "panel1";
            panel1.Size = new Size(584, 50);
            panel1.TabIndex = 7;
            // 
            // panel2
            // 
            panel2.Controls.Add(tableLayoutPanel1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(10);
            panel2.Size = new Size(584, 311);
            panel2.TabIndex = 8;
            // 
            // GroupColumnsDialog
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            CancelButton = buttonCancel;
            ClientSize = new Size(584, 361);
            Controls.Add(panel2);
            Controls.Add(panel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GroupColumnsDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Группировка";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.WinForms.ListView.SfListView listViewAll;
        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Panel panel2;
        private Syncfusion.WinForms.ListView.SfListView listViewSelected;
        private FlowLayoutPanel flowLayoutPanel1;
        private Syncfusion.WinForms.Controls.SfButton buttonSelect;
        private Syncfusion.WinForms.Controls.SfButton buttonRemove;
        private FlowLayoutPanel flowLayoutPanel2;
        private Syncfusion.WinForms.Controls.SfButton buttonUp;
        private Syncfusion.WinForms.Controls.SfButton buttonDown;
        private Label label1;
        private Label label2;
    }
}