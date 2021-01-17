//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.01.2021
// Time: 18:57
//-----------------------------------------------------------------------

using System;
using System.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Data.Repositories
{
    public static class EntityKinds
    {
        public static DocumentInfo Get(this EntityKind entityKind, IDbConnection connection, Guid doc_id)
        {
            return Documents.Get(connection, entityKind, doc_id);
        }
    }
}
