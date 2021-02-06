//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

namespace DocumentFlow.Reports
{
    public interface IDataStorage
    {
        int Count(string source);

        object Get(string source, string field, int rowindex);

        object Get(string source, string field);
    }
}
