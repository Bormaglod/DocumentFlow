//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.06.2019
// Time: 22:42
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core
{
    using System;
    using System.Configuration;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate;
    using NHibernate.Transform;
    using Npgsql;
    using DocumentFlow.Data.Mappings;

    public static class Db
    {
        static ISessionFactory sessionFactory;
        static string default_connection = "default";
        static string default_user = "guest";
        static string default_password = "guest";
        static string connectionString = string.Empty;

        /// <summary>
        /// Слово состоящее из букв латинского алфавита и симола рлдчеркивания перед которым стоит один (только один) символ двоеточия
        /// </summary>
        static string parameterPattern = "(?<!:):([a-zA-Z_]+)";

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    sessionFactory = CreateSessionFactory(default_connection, default_user, default_password);
                }

                return sessionFactory;
            }
        }

        public static string ConnectionString => connectionString;

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static ISession OpenSession(string connection, string userName, string password)
        {
            sessionFactory = CreateSessionFactory(connection, userName, password);
            return SessionFactory.OpenSession();
        }

        public static IList<IDictionary> ExecuteSelect(ISession session, string sql, IDictionary<string, Type> types, params (string, object)[] values)
        {
            Dictionary<string, object> row = new Dictionary<string, object>();
            foreach ((string Field, object Value) item in values)
            {
                row.Add(item.Field, item.Value);
            }

            return ExecuteSelect(session, sql, types, (x) => row.ContainsKey(x) ? row[x] : null);
        }

        public static IList<IDictionary> ExecuteSelect(ISession session, string sql, IDictionary<string, Type> types, Func<string, object> dataValues)
        {
            IQuery query = session.CreateSQLQuery(sql)
                .SetResultTransformer(Transformers.AliasToEntityMap);
            CreateQueryParameters(query, types, dataValues);
            return query.List<IDictionary>();
        }

        public static IList<T> ExecuteSelect<T>(ISession session, string sql, IDictionary<string, Type> types, Func<string, object> dataValues)
        {
            IQuery query = session.CreateSQLQuery(sql)
                .SetResultTransformer(Transformers.AliasToBean<T>());
            CreateQueryParameters(query, types, dataValues);
            return query.List<T>();
        }

        public static void ExecuteUpdate(ISession session, string sql, IDictionary<string, Type> types, Func<string, object> dataValues)
        {
            IQuery query = session.CreateSQLQuery(sql);
            CreateQueryParameters(query, types, dataValues);
            query.ExecuteUpdate();
        }

        public static void CreateQueryParameters(IQuery query, IDictionary<string, Type> types, params (string, object)[] values)
        {
            Dictionary<string, object> row = new Dictionary<string, object>();
            foreach ((string Field, object Value) item in values)
            {
                row.Add(item.Field, item.Value);
            }

            CreateQueryParameters(query, types, (x) => row.ContainsKey(x) ? row[x] : null);
        }

        public static void SetQueryParameter(IQuery query, string parameterName, object parameter, Type parameterType)
        {
            if (parameterType == null)
                return;

            if (parameter != null)
            {
                if (parameter is Guid guid && guid == Guid.Empty)
                {
                    parameter = null;
                }

                if (parameter == null)
                    parameterType = typeof(Guid?);
            }

            if (parameter == null)
            {
                Type type = parameterType;

                if (type.GenericTypeArguments.Any())
                {
                    type = type.GenericTypeArguments[0];
                }

                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Int32:
                        query.SetParameter(parameterName, (int?)null);
                        break;
                    case TypeCode.Int64:
                        query.SetParameter(parameterName, (long?)null);
                        break;
                    case TypeCode.Decimal:
                        query.SetParameter(parameterName, (decimal?)null);
                        break;
                    case TypeCode.String:
                        query.SetString(parameterName, null);
                        break;
                    case TypeCode.DateTime:
                        query.SetParameter(parameterName, (DateTime?)null);
                        break;
                    default:
                        if (parameterType == typeof(Guid?))
                            query.SetParameter(parameterName, (Guid?)null);
                        break;
                }
            }
            else
            {
                switch (parameter)
                {
                    case int value:
                        query.SetInt32(parameterName, value);
                        break;
                    case long value:
                        query.SetInt64(parameterName, value);
                        break;
                    case Guid value:
                        query.SetGuid(parameterName, value);
                        break;
                    case string value:
                        query.SetString(parameterName, value);
                        break;
                    case DateTime value:
                        query.SetDateTime(parameterName, value);
                        break;
                    default:
                        query.SetParameter(parameterName, parameter);
                        break;
                }
            }
        }

        public static void CreateQueryParameters(IQuery query, IDictionary<string, Type> types, Func<string, object> dataValues)
        {
            if (dataValues != null)
            {
                foreach (Match match in Regex.Matches(query.QueryString, parameterPattern))
                {
                    string prop = match.Groups[1].Value;
                    Type type;
                    if (types != null)
                        type = types.ContainsKey(prop) ? types[prop] : dataValues(prop).GetType();
                    else
                        type = dataValues(prop).GetType();
                    SetQueryParameter(query, prop, dataValues(prop), type);
                }
            }
        }

        private static ISessionFactory CreateSessionFactory(string connection, string user, string password)
        {
            string newConnectionString = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(newConnectionString);
            builder.Username = user;
            builder.Password = password;

            connectionString = builder.ConnectionString;

            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82.Driver<NpgsqlDriverExtended>()
                    .ConnectionString(ConnectionString)
                    .ShowSql())
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Entity>(new AutomappingConfiguration())
                    .Conventions.AddFromAssemblyOf<Entity>()))
                .ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()))
                .BuildSessionFactory();
            return sessionFactory;
        }
    }
}
