//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2014
// Time: 21:19
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core
{
    using NHibernate;
    using NHibernate.SqlCommand;
    using DocumentFlow.Core;

    public class SqlStatementInterceptor : EmptyInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            LogHelper.Logger.Debug(sql.ToString());
            return sql;
        }
    }
}
