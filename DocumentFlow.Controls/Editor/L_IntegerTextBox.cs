//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.12.2019
// Time: 19:07
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;

using DocumentFlow.Code;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_IntegerTextBox : UserControl, ILabelControl, IEditControl
    {
        public L_IntegerTextBox()
        {
            InitializeComponent();
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
                integerTextBox1.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        int IEditControl.Width { get => integerTextBox1.Width; set => integerTextBox1.Width = value; }

        object IEditControl.Value
        {
            get => integerTextBox1.IntegerValue;
            set => integerTextBox1.IntegerValue = value == null ? default : Convert.ToInt32(value);
        }

        bool IEditControl.FitToSize
        {
            get => integerTextBox1.Dock == DockStyle.Fill;
            set => integerTextBox1.Dock = DockStyle.Fill;
        }

        public long MinValue { get => integerTextBox1.MinValue; set => integerTextBox1.MinValue = value; }

        public long MaxValue { get => integerTextBox1.MaxValue; set => integerTextBox1.MaxValue = value; }

        public bool AllowLeadingZeros { get => integerTextBox1.AllowLeadingZeros; set => integerTextBox1.AllowLeadingZeros = value; }

        public string NumberGroupSeparator { get => integerTextBox1.NumberGroupSeparator; set => integerTextBox1.NumberGroupSeparator = value; }

        public int[] NumberGroupSizes { get => integerTextBox1.NumberGroupSizes; set => integerTextBox1.NumberGroupSizes = value; }

        public long IntegerValue { get => integerTextBox1.IntegerValue; set => integerTextBox1.IntegerValue = value; }

        private void integerTextBox1_IntegerValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
