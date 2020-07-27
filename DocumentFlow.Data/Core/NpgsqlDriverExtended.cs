//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.03.2019
// Time: 18:39
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core
{
    using System.Data.Common;
    using NHibernate;
    using NHibernate.Driver;
    using NHibernate.SqlTypes;
    using Npgsql;

    public class NpgsqlDriverExtended : NpgsqlDriver
    {
        protected override void InitializeParameter(DbParameter dbParam, string name, SqlType sqlType)
        {
            if (sqlType is NpgsqlExtendedSqlType && dbParam is NpgsqlParameter)
                InitializeParameter(dbParam as NpgsqlParameter, name, sqlType as NpgsqlExtendedSqlType);
            else
                base.InitializeParameter(dbParam, name, sqlType);
        }

        protected virtual void InitializeParameter(NpgsqlParameter dbParam, string name, NpgsqlExtendedSqlType sqlType)
        {
            if (sqlType == null)
                throw new QueryException(string.Format("No type assigned to parameter '{0}'", name));

            dbParam.ParameterName = FormatNameForParameter(name);
            dbParam.DbType = sqlType.DbType;
            dbParam.NpgsqlDbType = sqlType.NpgDbType;
        }
    }
}
