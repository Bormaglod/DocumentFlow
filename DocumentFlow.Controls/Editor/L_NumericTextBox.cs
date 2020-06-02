//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
// Time: 09:00
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using DocumentFlow.DataSchema;

    public partial class L_NumericTextBox : UserControl, ILabeled, ISized
    {
        public L_NumericTextBox()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs> DecimalValueChanged;

        string ILabeled.Text { get => label1.Text; set => label1.Text = value; }

        int ILabeled.Width { get => label1.Width; set => label1.Width = value; }

        int ILabeled.EditWidth { get => decimalText.Width; set => decimalText.Width = value; }

        bool ILabeled.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabeled.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        bool ILabeled.Visible
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                decimalText.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        void ISized.SetFullSize() => decimalText.Dock = DockStyle.Fill;

        public int NumberDecimalDigits { get => decimalText.NumberDecimalDigits; set => decimalText.NumberDecimalDigits = value; }

        public string NumberDecimalSeparator { get => decimalText.NumberDecimalSeparator; set => decimalText.NumberDecimalSeparator = value; }

        public string NumberGroupSeparator { get => decimalText.NumberGroupSeparator; set => decimalText.NumberGroupSeparator = value; }

        public int[] NumberGroupSizes { get => decimalText.NumberGroupSizes; set => decimalText.NumberGroupSizes = value; }

        public decimal DecimalValue { get => decimalText.DecimalValue; set => decimalText.DecimalValue = value; }

        private void DecimalText_DecimalValueChanged(object sender, EventArgs e)
        {
            if (DecimalValueChanged != null)
            {
                DecimalValueChanged.Invoke(this, new EventArgs());
            }
        }
    }
}
