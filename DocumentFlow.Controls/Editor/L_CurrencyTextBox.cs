//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
// Time: 22:38
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using DocumentFlow.Code;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_CurrencyTextBox : UserControl, ILabelControl, IEditControl
    {
        public L_CurrencyTextBox()
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
                currencyTextBox1.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        int IEditControl.Width { get => currencyTextBox1.Width; set => currencyTextBox1.Width = value; }

        object IEditControl.Value
        {
            get => currencyTextBox1.DecimalValue;
            set => currencyTextBox1.DecimalValue = value == null ? default : Convert.ToDecimal(value);
        }

        bool IEditControl.FitToSize
        {
            get => currencyTextBox1.Dock == DockStyle.Fill;
            set => currencyTextBox1.Dock = DockStyle.Fill;
        }

        private void CurrencyTextBox1_DecimalValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}
