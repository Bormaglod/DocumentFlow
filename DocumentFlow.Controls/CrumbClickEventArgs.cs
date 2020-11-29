//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.06.2018
// Time: 19:09
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Controls
{
    public class CrumbClickEventArgs : EventArgs
    {
        private ToolButtonKind kind;

        public CrumbClickEventArgs(ToolButtonKind buttonKind)
        {
            kind = buttonKind;
        }

        public ToolButtonKind Kind { get => kind; set => kind = value; }
    }
}
