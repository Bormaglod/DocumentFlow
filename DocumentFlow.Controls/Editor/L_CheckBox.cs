//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
// Time: 18:54
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System.Drawing;
    using System.Windows.Forms;
    using DocumentFlow.DataSchema;

    public partial class L_CheckBox : UserControl, ILabeled
    {
        public L_CheckBox()
        {
            InitializeComponent();
        }

        string ILabeled.Text { get => label1.Text; set => label1.Text = value; }

        int ILabeled.Width { get => label1.Width; set => label1.Width = value; }

        int ILabeled.EditWidth { get => checkBoxAdv1.Width; set => checkBoxAdv1.Width = value; }

        bool ILabeled.AutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

        ContentAlignment ILabeled.TextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }
        
        bool ILabeled.Visible { get => label1.Visible; set => label1.Visible = value; }

        public bool BoolValue { get => checkBoxAdv1.BoolValue; set => checkBoxAdv1.BoolValue = value; }
    }
}
