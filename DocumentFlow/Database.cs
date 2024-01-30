//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.06.2019
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Settings;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NLog.Extensions.Logging;

using Npgsql;

using System.Data;

namespace DocumentFlow.Data;

public class Database : IDatabase
{
    private NpgsqlDataSource? dataSource;

    private readonly string default_user = "guest";
    private readonly string default_password = "guest";

    private string connectionName = string.Empty;
    private string currentUser = string.Empty;

    private readonly AppSettings settings;

    public Database(IOptions<AppSettings> settings)
    {
        this.settings = settings.Value;
    }

    public string ConnectionString { get; private set; } = string.Empty;

    public string ConnectionName 
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
        ConnectionString = CreateConnectionString(userName, password);
        dataSource = CreateDataSource(ConnectionString);

        return OpenConnection();
    }

    public IDbConnection OpenConnection()
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            ConnectionString = CreateConnectionString(default_user, default_password);
        }

        dataSource ??= CreateDataSource(ConnectionString);

        while (true)
        {
            try
            {
                return dataSource.OpenConnection();
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

    public bool HasPrivilege(string tableName, params Privilege[] privilege)
    {
        using var conn = OpenConnection();
        if (privilege.Length == 0)
        {
            return false;
        }

        return conn.ExecuteScalar<bool>("select has_table_privilege(:user, :table, :privilege)", new
        {
            user = CurrentUser,
            table = tableName,
            privilege = string.Join(',', privilege)
        });
    }

    private string CreateConnectionString(string userName, string password)
    {
        currentUser = userName;

        var newConnectionString = settings.Connections.First(x => x.Name == ConnectionName).ToString();
        var builder = new NpgsqlConnectionStringBuilder(newConnectionString)
        {
            Username = userName,
            Password = password
        };

        return builder.ConnectionString;
    }

    private static NpgsqlDataSource CreateDataSource(string connectionString)
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddNLog());
        NpgsqlLoggingConfiguration.InitializeLogging(loggerFactory, parameterLoggingEnabled: true);

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder
            .UseLoggerFactory(loggerFactory)
            .EnableParameterLogging();
        return dataSourceBuilder.Build();
    }
}
