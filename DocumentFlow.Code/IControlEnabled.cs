//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.12.2020
// Time: 17:09
//-----------------------------------------------------------------------

namespace DocumentFlow.Code
{
    public interface IControlEnabled
    {
        bool Ability(object entity, string dataName, IInformation info);
    }
}
