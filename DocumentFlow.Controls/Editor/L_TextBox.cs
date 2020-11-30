//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.12.2019
// Time: 23:15
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using DocumentFlow.Code;
using DocumentFlow.Core;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_TextBox : UserControl, ILabelControl, IEditControl
    {
        public L_TextBox()
        {
            InitializeComponent();
            Nullable = true;
        }

        public event EventHandler ValueChanged;

        string ILabelControl.Text { get => label1.Text; set => label1.Text = value; }

        int ILabelControl.Width { get => label1.Width; set => label1.Width = value; }

        bool ILabelControl.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabelControl.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        bool ILabelControl.Visible
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                textBoxExt.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        int IEditControl.Width { get => textBoxExt.Width; set => textBoxExt.Width = value; }

        object IEditControl.Value 
        {
            get => Nullable ? textBoxExt.Text.NullIfEmpty() : textBoxExt.Text;
            set => textBoxExt.Text = value == null ? string.Empty : value.ToString();
        }

        bool IEditControl.FitToSize 
        {
            get => textBoxExt.Dock == DockStyle.Fill;
            set => textBoxExt.Dock = DockStyle.Fill; 
        }

        public bool Nullable { get; set; }

        public bool Multiline { get => textBoxExt.Multiline; set => textBoxExt.Multiline = value; }

        private void TextBoxExt_TextChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}
