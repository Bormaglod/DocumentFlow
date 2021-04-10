//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.10.2020
// Time: 20:11
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code
{
    public interface IEditControl : IValuable
    {
        event EventHandler ValueChanged;

        int Width { get; set; }
        bool FitToSize { get; set; }
        bool Nullable { get; set; }
    }
}
