//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.04.2019
// Time: 18:35
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DocumentFlow.Code;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_FileSelectBox : UserControl, ILabelControl, IEditControl
    {
        private string selectedItem;

        public L_FileSelectBox()
        {
            InitializeComponent();
        }

        public event EventHandler ValueChanged;

        string ILabelControl.Text { get => LabelText; set => LabelText = value; }

        int ILabelControl.Width { get => LabelWidth; set => LabelWidth = value; }

        bool ILabelControl.AutoSize { get => AutoSizeLabel; set => AutoSizeLabel = value; }

        ContentAlignment ILabelControl.TextAlign { get => TextAlign; set => TextAlign = value; }

        bool ILabelControl.Visible { get => ShowLabel; set => ShowLabel = value; }

        int IEditControl.Width { get => EditWidth; set => EditWidth = value; }

        object IEditControl.Value
        {
            get => selectedItem;
            set
            {
                if (value is string file)
                {
                    selectedItem = file;
                    textValue.Text = Path.GetFileName(file);
                    OnValueChanged();
                    return;
                }

                ClearCurrent();
            }
        }

        bool IEditControl.FitToSize
        {
            get => panelEdit.Dock == DockStyle.Fill;
            set => panelEdit.Dock = DockStyle.Fill;
        }

        public string LabelText { get => label1.Text; set => label1.Text = value; }

        public int LabelWidth { get => label1.Width; set => label1.Width = value; }

        public ContentAlignment TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        public bool AutoSizeLabel { get => label1.AutoSize; set => label1.AutoSize = value; }

        public int EditWidth { get => panelEdit.Width; set => panelEdit.Width = value; }

        public bool ShowLabel
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                panelEdit.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        public string FileName 
        { 
            get => selectedItem; 
            set 
            {
                IEditControl edit = this;
                edit.Value = value;
            } 
        }

        public void ClearCurrent()
        {
            textValue.Text = string.Empty;
            selectedItem = string.Empty;

            OnValueChanged();
        }

        private void OnValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            ClearCurrent();
        }

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            IEditControl edit = this;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                edit.Value = openFileDialog1.FileName;
            }
        }
    }
}
