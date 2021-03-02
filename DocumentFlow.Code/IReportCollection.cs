//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.01.2021
// Time: 14:27
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace DocumentFlow.Code
{
    public interface IReportCollection
    {
        IEnumerable<IReportForm> Forms { get; }
        IReportForm Default { get; }
        IReportCollection Add(IReportForm form);
    }
}
