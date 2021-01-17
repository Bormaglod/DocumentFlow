//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.01.2021
// Time: 18:37
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Data.Repositories
{
    public static class Reports
    {
        public static IEnumerable<PrintKindForm> Get(IDbConnection connection, Command command)
        {
            if (command?.entity_kind_id != null && command.entity_kind_id.HasValue)
            {
                return Get(connection, command.entity_kind_id.Value);
            }

            return new PrintKindForm[] { };
        }

        public static IEnumerable<PrintKindForm> Get(IDbConnection connection, Guid kind_id)
        {
            string sql = "select * from print_kind_form pkf join print_form pf on (pf.id = pkf.print_form_id) where entity_kind_id = :id";
            return connection.Query<PrintKindForm, PrintForm, PrintKindForm>(sql, (kind, form) =>
            {
                kind.PrintForm = form;
                return kind;
            }, new { id = kind_id });
        }
    }
}
