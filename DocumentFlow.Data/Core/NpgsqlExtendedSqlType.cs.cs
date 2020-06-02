//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.03.2019
// Time: 18:36
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core
{
    using System.Data;
    using NHibernate.SqlTypes;
    using NpgsqlTypes;

    public class NpgsqlExtendedSqlType : SqlType
    {
        public NpgsqlExtendedSqlType(DbType dbType, NpgsqlDbType npgDbType) : base(dbType)
        {
            NpgDbType = npgDbType;
        }

        public NpgsqlExtendedSqlType(DbType dbType, NpgsqlDbType npgDbType, int length) : base(dbType, length)
        {
            NpgDbType = npgDbType;
        }

        public NpgsqlExtendedSqlType(DbType dbType, NpgsqlDbType npgDbType, byte precision, byte scale) : base(dbType, precision, scale)
        {
            NpgDbType = npgDbType;
        }

        public NpgsqlDbType NpgDbType { get; }
    }
}
