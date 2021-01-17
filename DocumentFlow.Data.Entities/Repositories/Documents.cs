//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.01.2021
// Time: 18:57
//-----------------------------------------------------------------------

using System;
using System.Data;
using Dapper;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Data.Repositories
{
    public static class Documents
    {
        public static DocumentInfo Get(IDbConnection connection, EntityKind entityKind, Guid id)
        {
            return connection.QuerySingleOrDefault<DocumentInfo>($"select di.status_id, di.date_created, di.date_updated, uc.name as user_created, uu.name as user_updated from {entityKind.code} di join user_alias uc on uc.id  = di.user_created_id join user_alias uu on uu.id  = di.user_updated_id where di.id = :id", new { id });
        }
    }
}
