//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.06.2019
// Time: 22:42
//-----------------------------------------------------------------------

using System.Configuration;
using System.Data;
using Npgsql;

namespace DocumentFlow.Data
{
    public static class Db
    {
        private static readonly string default_user = "guest";
        private static readonly string default_password = "guest";

        public static string ConnectionString { get; private set; } = string.Empty;
        public static string ConnectionName { get; private set; }

        public static IDbConnection OpenGuestConnection(string connectionName) => OpenConnection(connectionName, default_user, default_password);

        public static IDbConnection OpenConnection(string connectionName, string userName, string password)
        {
            ConnectionName = connectionName;

            string newConnectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            var builder = new NpgsqlConnectionStringBuilder(newConnectionString)
            {
                Username = userName,
                Password = password
            };

            ConnectionString = builder.ConnectionString;

            return OpenConnection();
        }

        public static IDbConnection OpenConnection()
        {
            var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();

            return conn;
        }
    }
}
