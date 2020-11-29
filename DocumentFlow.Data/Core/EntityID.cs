//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.03.2020
// Time: 21:33
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core
{
    public abstract class EntityID : Entity
    {
        public long Id { get; protected set; }
    }
}
