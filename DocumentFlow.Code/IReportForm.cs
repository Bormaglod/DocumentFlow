//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.01.2021
// Time: 14:27
//-----------------------------------------------------------------------

namespace DocumentFlow.Code
{
    public interface IReportForm
    {
        string Name { get; }
        bool IsDefault { get; }
        IReportForm Parameter(string name, object value);
        object GetParameter(string name);
    }
}
