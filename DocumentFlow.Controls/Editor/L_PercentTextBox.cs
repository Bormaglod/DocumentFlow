//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.12.2019
// Time: 18:48
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System.Drawing;
    using System.Windows.Forms;
    using DocumentFlow.DataSchema;

    public partial class L_PercentTextBox : UserControl, ILabeled, ISized
    {
        public L_PercentTextBox()
        {
            InitializeComponent();
        }

        string ILabeled.Text { get => label1.Text; set => label1.Text = value; }

        int ILabeled.Width { get => label1.Width; set => label1.Width = value; }

        int ILabeled.EditWidth { get => percentTextBox1.Width; set => percentTextBox1.Width = value; }

        bool ILabeled.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabeled.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        bool ILabeled.Visible
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                percentTextBox1.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        void ISized.SetFullSize() => percentTextBox1.Dock = DockStyle.Fill;

        public int PercentDecimalDigits { get => percentTextBox1.PercentDecimalDigits; set => percentTextBox1.PercentDecimalDigits = value; }

        public double MaxValue { get => percentTextBox1.MaxValue; set => percentTextBox1.MaxValue = value; }

        public double MinValue { get => percentTextBox1.MinValue; set => percentTextBox1.MinValue = value; }

        public double PercentValue { get => percentTextBox1.PercentValue; set => percentTextBox1.PercentValue = value; }
    }
}
