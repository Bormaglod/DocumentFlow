//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
// Time: 09:00
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using DocumentFlow.Code;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_NumericTextBox : UserControl, ILabelControl, IEditControl
    {
        public L_NumericTextBox()
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
                decimalText.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        int IEditControl.Width { get => decimalText.Width; set => decimalText.Width = value; }

        object IValuable.Value
        {
            get
            {
                if (Nullable && decimalText.DecimalValue == default)
                    return null;

                return decimalText.DecimalValue;
            }

            set => decimalText.DecimalValue = value == null ? default : Convert.ToDecimal(value);
        }

        bool IEditControl.FitToSize
        {
            get => decimalText.Dock == DockStyle.Fill;
            set => decimalText.Dock = DockStyle.Fill;
        }

        public bool Nullable { get; set; }

        public int NumberDecimalDigits { get => decimalText.NumberDecimalDigits; set => decimalText.NumberDecimalDigits = value; }

        private void OnValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void DecimalText_DecimalValueChanged(object sender, EventArgs e)
        {
            OnValueChanged();
        }
    }
}
