//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.12.2019
// Time: 19:07
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System.Drawing;
    using System.Windows.Forms;
    using DocumentFlow.DataSchema;

    public partial class L_IntegerTextBox : UserControl, ILabeled, ISized
    {
        public L_IntegerTextBox()
        {
            InitializeComponent();
        }

        string ILabeled.Text { get => label1.Text; set => label1.Text = value; }

        int ILabeled.Width { get => label1.Width; set => label1.Width = value; }

        int ILabeled.EditWidth { get => integerTextBox1.Width; set => integerTextBox1.Width = value; }

        bool ILabeled.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabeled.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        bool ILabeled.Visible
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                integerTextBox1.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        void ISized.SetFullSize() => integerTextBox1.Dock = DockStyle.Fill;

        public long MinValue { get => integerTextBox1.MinValue; set => integerTextBox1.MinValue = value; }

        public long MaxValue { get => integerTextBox1.MaxValue; set => integerTextBox1.MaxValue = value; }

        public bool AllowLeadingZeros { get => integerTextBox1.AllowLeadingZeros; set => integerTextBox1.AllowLeadingZeros = value; }

        public string NumberGroupSeparator { get => integerTextBox1.NumberGroupSeparator; set => integerTextBox1.NumberGroupSeparator = value; }

        public int[] NumberGroupSizes { get => integerTextBox1.NumberGroupSizes; set => integerTextBox1.NumberGroupSizes = value; }

        public long IntegerValue { get => integerTextBox1.IntegerValue; set => integerTextBox1.IntegerValue = value; }
    }
}
