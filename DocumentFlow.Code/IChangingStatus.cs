//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2020
// Time: 17:17
//-----------------------------------------------------------------------

namespace DocumentFlow.Code
{
    public interface IChangingStatus
    {
        bool CanChange(IDatabase database, object entity, string status_from, string status_to);
    }
}
