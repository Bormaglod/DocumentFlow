//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.03.2020
// Time: 21:33
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core
{
    using System;

    public abstract class EntityUID : Entity
    {
        public virtual Guid Id { get; protected set; }

        public virtual Guid GetReferenceId(Type type)
        {
            return GetReference(type) is EntityUID e ? e.Id : Guid.Empty;
        }

        public virtual Guid GetReferenceId<T>() where T : EntityUID => GetReferenceId(typeof(T));
    }
}
