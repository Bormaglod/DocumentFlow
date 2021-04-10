//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2020
// Time: 10:07
//-----------------------------------------------------------------------

namespace DocumentFlow.Code
{
    public interface IStackedColumn
    {
        IStackedColumn Add(string dataField);
        IStackedColumn Add(IColumn column);
        void Header(string header);
    }
}
