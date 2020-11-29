//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.03.2020
// Time: 21:33
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Core
{
    public abstract class EntityUID : Entity
    {
        public Guid Id { get; protected set; }

        public Guid GetReferenceId(Type type)
        {
            return GetReference(type) is EntityUID e ? e.Id : Guid.Empty;
        }

        public Guid GetReferenceId<T>() where T : EntityUID => GetReferenceId(typeof(T));
    }
}
