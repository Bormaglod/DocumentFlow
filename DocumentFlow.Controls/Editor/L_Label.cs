//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2021
// Time: 17:23
//-----------------------------------------------------------------------

using System.Windows.Forms;
using DocumentFlow.Code;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_Label : UserControl, IValuable
    {
        public L_Label()
        {
            InitializeComponent();
        }

        object IValuable.Value
        {
            get => label1.Text;
            set => label1.Text = value.ToString();
        }
    }
}
