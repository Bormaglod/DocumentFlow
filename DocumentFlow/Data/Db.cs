//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.06.2019
//
// Версия 2023.1.24
//  - IDatabase перенесён из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Infrastructure.Data;

using Npgsql;

using System.Configuration;
using System.Data;

namespace DocumentFlow.Data;

public class Database : IDatabase
{
    private readonly string default_user = "guest";
    private readonly string default_password = "guest";

    private static string connectionName = string.Empty;
    private static string currentUser = string.Empty;

    public static string ConnectionString { get; private set; } = string.Empty;

    public static string ConnectionName 
    {
        get => connectionName;
        set
        {
            connectionName = value;
            ConnectionString = string.Empty;
        }
    }

    public string CurrentUser => currentUser;

    public IDbConnection OpenConnection(string userName, string password)
    {
        CreateConnectionString(userName, password);
        return OpenConnection();
    }

    public IDbConnection OpenConnection()
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            CreateConnectionString(default_user, default_password);
        }

        while (true)
        {
            var conn = new NpgsqlConnection(ConnectionString);

            try
            {
                conn.Open();
                return conn;
            }
            catch (NpgsqlException e)
            {
                if (e.InnerException is TimeoutException)
                {
                    if (MessageBox.Show("Сервер не отвечает. Повторить?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }

            }
        }
    }

    public void Login(string userName, string password)
    {
        using var conn = OpenConnection(userName, password);
        conn.Execute("login", commandType: CommandType.StoredProcedure);
    }

    private static void CreateConnectionString(string userName, string password)
    {
        currentUser = userName;

        string newConnectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
        var builder = new NpgsqlConnectionStringBuilder(newConnectionString)
        {
            Username = userName,
            Password = password
        };

        ConnectionString = builder.ConnectionString;
    }
}
