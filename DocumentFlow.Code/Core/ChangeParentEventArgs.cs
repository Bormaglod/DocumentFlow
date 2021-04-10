//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.10.2020
// Time: 13:43
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code.Core
{
    public class ChangeParentEventArgs : EventArgs
    {
        public ChangeParentEventArgs(Guid oldParen, Guid newParent)
        {
            OldParent = oldParen;
            NewParent = newParent;
        }

        public Guid OldParent { get; }
        public Guid NewParent { get; }
    }
}
