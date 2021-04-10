//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.01.2021
// Time: 18:37
//-----------------------------------------------------------------------

using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Data.Repositories
{
    public static class PrintedForms
    {
        public static PrintedForm Get(IDbConnection connection, string code)
        {
            string sql = "select * from printed_form pf left join picture p on (p.id = pf.picture_id) where pf.code = :code";
            return connection.Query<PrintedForm, Picture, PrintedForm>(sql, (form, picture) =>
            {
                form.Picture = picture;
                return form;
            }, new { code }).FirstOrDefault();
        }
    }
}
