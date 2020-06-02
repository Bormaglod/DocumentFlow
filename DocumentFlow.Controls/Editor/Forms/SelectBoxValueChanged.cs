//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.06.2018
// Time: 11:14
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Forms
{
    using System;

    public class SelectBoxValueChanged : EventArgs
    {
        public SelectBoxValueChanged(object selected)
        {
            SelectedItem = selected;
        }

        public object SelectedItem { get; }
    }
}
