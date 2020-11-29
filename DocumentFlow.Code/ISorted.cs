//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.10.2020
// Time: 19:20
//-----------------------------------------------------------------------

using DocumentFlow.Code.System;

namespace DocumentFlow.Code
{
    public interface ISorted
    {
        ISorted Add(string columnName, SortDirection direction = SortDirection.Ascending);
    }
}
