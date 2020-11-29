//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.09.2020
// Time: 19:46
//-----------------------------------------------------------------------

using DocumentFlow.Code.System;

namespace DocumentFlow.Code
{
    public interface INumericColumn
    {
        NumberFormatMode FormatMode { get; }
        int DecimalDigits { get; }
    }
}
