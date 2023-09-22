namespace DocumentFlow.Dialogs
{
    partial class DirectoryItemDialog
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
            buttonCancel = new Syncfusion.WinForms.Controls.SfButton();
            buttonOk = new Syncfusion.WinForms.Controls.SfButton();
            selectBox = new Controls.Editors.DfDirectorySelectBox();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            buttonCancel.AccessibleName = "Button";
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancel.Location = new Point(394, 52);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(96, 32);
            buttonCancel.TabIndex = 1003;
            buttonCancel.Text = "Отменить";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            buttonOk.AccessibleName = "Button";
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.BackColor = Color.FromArgb(242, 242, 242);
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonOk.Location = new Point(292, 52);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(96, 32);
            buttonOk.Style.BackColor = Color.FromArgb(242, 242, 242);
            buttonOk.TabIndex = 1002;
            buttonOk.Text = "Сохранить";
            buttonOk.UseVisualStyleBackColor = false;
            buttonOk.Click += ButtonOk_Click;
            // 
            // selectBox
            // 
            selectBox.CanSelectFolder = false;
            selectBox.Dock = DockStyle.Top;
            selectBox.EditorFitToSize = true;
            selectBox.EditorWidth = 418;
            selectBox.EnabledEditor = true;
            selectBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            selectBox.Header = "selectBox";
            selectBox.HeaderAutoSize = true;
            selectBox.HeaderDock = DockStyle.Left;
            selectBox.HeaderTextAlign = ContentAlignment.TopLeft;
            selectBox.HeaderVisible = true;
            selectBox.HeaderWidth = 100;
            selectBox.Location = new Point(10, 10);
            selectBox.Margin = new Padding(3, 4, 3, 4);
            selectBox.Name = "selectBox";
            selectBox.Padding = new Padding(0, 0, 0, 7);
            selectBox.RemoveEmptyFolders = false;
            selectBox.ShowDeleteButton = true;
            selectBox.ShowOpenButton = false;
            selectBox.Size = new Size(480, 32);
            selectBox.TabIndex = 1004;
            // 
            // DirectorySelectDialog
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            CancelButton = buttonCancel;
            ClientSize = new Size(500, 97);
            Controls.Add(selectBox);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DirectorySelectDialog";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Выбор";
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton buttonCancel;
        private Syncfusion.WinForms.Controls.SfButton buttonOk;
        private Controls.Editors.DfDirectorySelectBox selectBox;
    }
}