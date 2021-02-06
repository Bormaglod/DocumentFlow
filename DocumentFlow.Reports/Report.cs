//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Linq;
using Npgsql;
using Syncfusion.Pdf;
using DocumentFlow.Core;
using DocumentFlow.Data;

namespace DocumentFlow.Reports
{
    public class Report : IDisposable
    {
        private bool _disposed = false;
        private readonly Dictionary<DbType, Type> typeMap;
        private readonly DataStorage storage;
        private PdfDocument document;

        public Report()
        {
            storage = new DataStorage();

            typeMap = new Dictionary<DbType, Type>()
            {
                [DbType.AnsiString] = typeof(string),
                [DbType.Binary] = typeof(byte[]),
                [DbType.Byte] = typeof(byte),
                [DbType.Boolean] = typeof(bool),
                [DbType.Currency] = typeof(decimal),
                [DbType.Date] = typeof(DateTime),
                [DbType.DateTime] = typeof(DateTime),
                [DbType.Decimal] = typeof(decimal),
                [DbType.Double] = typeof(double),
                [DbType.Guid] = typeof(Guid),
                [DbType.Int16] = typeof(short),
                [DbType.Int32] = typeof(int),
                [DbType.Int64] = typeof(long),
                [DbType.SByte] = typeof(sbyte),
                [DbType.Single] = typeof(float),
                [DbType.String] = typeof(string),
                [DbType.Time] = typeof(DateTime),
                [DbType.UInt16] = typeof(ushort),
                [DbType.UInt32] = typeof(uint),
                [DbType.UInt64] = typeof(ulong),
                [DbType.VarNumeric] = typeof(decimal),
                [DbType.AnsiStringFixedLength] = typeof(char),
                [DbType.StringFixedLength] = typeof(char),
                [DbType.Xml] = typeof(string),
                [DbType.DateTimeOffset] = typeof(DateTimeOffset)
            };
        }

        public IDataStorage Storage => storage;

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public ReportDictionary ReportDictionary { get; set; }

        public ReportPage ReportPage { get; set; }

        public void Dispose() => Dispose(true);

        public static Report FromFile(string file)
        {
            string json = File.ReadAllText(file);
            return FromText(json);
        }

        public static Report FromText(string jsonText)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = {
                    new JsonStringEnumConverter()
                },
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return JsonSerializer.Deserialize<Report>(jsonText, options);
        }

        public PdfDocument GeneratePdf(string name)
        {
            if (ReportPage != null)
            {
                Prepare();
                document = new PdfDocument();
                document.DocumentInformation.Author = "ООО \"Автоком\"";
                document.DocumentInformation.Title = TextObject.Parse(ReportPage.Title, (source, field) => storage.Get(source, field).ToString());
                document.DocumentInformation.Subject = name;
                document.PageSettings.Size = ReportPage.PageSize.Size;
                document.PageSettings.SetMargins(
                    Length.FromMillimeter(ReportPage.MarginSize.Left).ToPoint(),
                    Length.FromMillimeter(ReportPage.MarginSize.Top).ToPoint(),
                    Length.FromMillimeter(ReportPage.MarginSize.Right).ToPoint(),
                    Length.FromMillimeter(ReportPage.MarginSize.Bottom).ToPoint()
                    );

                ReportPage.GeneratePdf(document);

                return document;
            }

            throw new ArgumentNullException("Не определен ReportPage");
        }

        public void Prepare()
        {
            foreach (Connection connection in ReportDictionary.Connections)
            {
                string connectionString = string.Empty;
                switch (connection.ConnectionStringType)
                {
                    case ConnectionStringType.Current:
                        connectionString = Db.ConnectionString;
                        break;
                    case ConnectionStringType.Embeded:
                        break;
                    case ConnectionStringType.Specified:
                        connectionString = connection.ConnectionString;
                        break;
                    default:
                        break;
                }

                if (string.IsNullOrEmpty(connectionString))
                {
                    return;
                }

                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString))
                {
                    npgsqlConnection.Open();
                    foreach (TableDataSource table in connection.Sources)
                    {
                        storage.AddSource(table.Name);
                        using (NpgsqlCommand command = new NpgsqlCommand(table.SelectCommand, npgsqlConnection))
                        {
                            foreach (string paramName in table.Parameters)
                            {
                                CommandParameter parameter = ReportDictionary.Parameters.FirstOrDefault(x => x.Name == paramName);
                                if (parameter == null)
                                {
                                    continue;
                                }

                                Enum.TryParse(parameter.DataType, out DbType type);
                                NpgsqlParameter npgsqlParameter = new NpgsqlParameter(parameter.Name, type);

                                if (parameter.Setted)
                                {
                                    npgsqlParameter.Value = parameter.Value;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(parameter.DefaultValue))
                                    {
                                        if (typeMap[type] == typeof(Guid))
                                            npgsqlParameter.Value = new Guid(parameter.DefaultValue);
                                        else
                                            npgsqlParameter.Value = Convert.ChangeType(parameter.DefaultValue, typeMap[type]);
                                    }
                                }

                                command.Parameters.Add(npgsqlParameter);
                            }

                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        Dictionary<string, object> row = new Dictionary<string, object>();
                                        foreach (Column column in table.Columns)
                                        {
                                            row.Add(column.Name, reader[column.Name]);
                                        }

                                        storage.AddRow(table.Name, row);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (document != null)
                {
                    document.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
