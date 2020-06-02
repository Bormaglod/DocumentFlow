//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
// Time: 22:38
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using DocumentFlow.DataSchema;

    public partial class L_CurrencyTextBox : UserControl, ILabeled, ISized
    {
        public L_CurrencyTextBox()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs> DecimalValueChanged;

        string ILabeled.Text { get => label1.Text; set => label1.Text = value; }

        int ILabeled.Width { get => label1.Width; set => label1.Width = value; }

        int ILabeled.EditWidth { get => currencyTextBox1.Width; set => currencyTextBox1.Width = value; }

        bool ILabeled.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabeled.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        bool ILabeled.Visible
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                currencyTextBox1.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        void ISized.SetFullSize() => currencyTextBox1.Dock = DockStyle.Fill;

        public decimal DecimalValue { get => currencyTextBox1.DecimalValue; set => currencyTextBox1.DecimalValue = value; }

        private void CurrencyTextBox1_DecimalValueChanged(object sender, EventArgs e)
        {
            if (DecimalValueChanged != null)
                DecimalValueChanged.Invoke(this, e);
        }
    }
}
