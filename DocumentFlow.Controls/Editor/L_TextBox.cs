//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.12.2019
// Time: 23:15
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using DocumentFlow.DataSchema;

    public partial class L_TextBox : UserControl, ILabeled, ISized
    {
        public L_TextBox()
        {
            InitializeComponent();
        }

        new public event EventHandler TextChanged;

        string ILabeled.Text { get => label1.Text; set => label1.Text = value; }

        int ILabeled.Width { get => label1.Width; set => label1.Width = value; }

        int ILabeled.EditWidth { get => textBoxExt.Width; set => textBoxExt.Width = value; }

        bool ILabeled.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabeled.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

        bool ILabeled.Visible
        {
            get => label1.Visible;
            set
            {
                label1.Visible = value;
                textBoxExt.Dock = value ? DockStyle.Left : DockStyle.Top;
            }
        }

        void ISized.SetFullSize() => textBoxExt.Dock = DockStyle.Fill;

        public bool Multiline { get => textBoxExt.Multiline; set => textBoxExt.Multiline = value; }

        public override string Text { get => textBoxExt.Text; set => textBoxExt.Text = value; }

        private void TextBoxExt_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
                TextChanged.Invoke(this, e);
        }
    }
}
