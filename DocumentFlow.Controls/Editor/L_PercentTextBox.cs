//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.12.2019
// Time: 18:48
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using DocumentFlow.Code;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_PercentTextBox : UserControl, ILabelControl, IEditControl
    {
        public L_PercentTextBox()
        {
            InitializeComponent();
            Nullable = false;
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
                percentTextBox1.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        int IEditControl.Width { get => percentTextBox1.Width; set => percentTextBox1.Width = value; }

        object IValuable.Value
        {
            get
            {
                if (Nullable && percentTextBox1.PercentValue == default)
                    return null;

                return percentTextBox1.PercentValue;
            }

            set => percentTextBox1.PercentValue = value == null ? default : Convert.ToDouble(value);
        }

        bool IEditControl.FitToSize
        {
            get => percentTextBox1.Dock == DockStyle.Fill;
            set => percentTextBox1.Dock = DockStyle.Fill;
        }

        public bool Nullable { get; set; }

        public int PercentDecimalDigits { get => percentTextBox1.PercentDecimalDigits; set => percentTextBox1.PercentDecimalDigits = value; }

        public double MaxValue { get => percentTextBox1.MaxValue; set => percentTextBox1.MaxValue = value; }

        public double MinValue { get => percentTextBox1.MinValue; set => percentTextBox1.MinValue = value; }

        private void percentTextBox1_DoubleValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}
