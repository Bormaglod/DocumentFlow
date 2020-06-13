//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.06.2020
// Time: 21:09
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core
{
    using System;
    using Npgsql;
    using NpgsqlTypes;

    public static class NpgsqlExtension
    {
        public static void CreateParameter(this NpgsqlCommand command, string name, object value, Type type)
        {
            if (value == null && type == null)
                return;

            if (type == null)
                type = value.GetType();

            NpgsqlDbType dbType;

            if (type == typeof(Guid?) || type == typeof(Guid))
                dbType = NpgsqlDbType.Uuid;
            else if (type == typeof(int?) || type == typeof(int))
                dbType = NpgsqlDbType.Integer;
            else if (type == typeof(DateTime?) || type == typeof(DateTime))
                dbType = NpgsqlDbType.TimestampTz;
            else if (type == typeof(string))
                dbType = NpgsqlDbType.Varchar;
            else
                throw new Exception("Тип не обработан.");

            NpgsqlParameter parameter = command.Parameters.Add(name, dbType);
            if (value == null)
                parameter.NpgsqlValue = DBNull.Value;
            else
                parameter.NpgsqlValue = value;
        }
    }
}
