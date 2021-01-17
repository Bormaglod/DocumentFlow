//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.09.2020
// Time: 19:02
//-----------------------------------------------------------------------

using DocumentFlow.Code.Core;

namespace DocumentFlow.Code
{
    public interface ISummary
    {
        ISummary AddColumn(string fieldName, RowSummaryType type, string format);
    }
}
