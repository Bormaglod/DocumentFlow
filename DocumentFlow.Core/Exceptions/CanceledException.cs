//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2020
// Time: 24:59
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Core.Exceptions
{
    public class CanceledException : Exception
    {
        public CanceledException() : this(false) { }

        public CanceledException(bool needRemoveReference) : base() => NeedRemoveReference = needRemoveReference;

        public bool NeedRemoveReference { get; }
    }
}
