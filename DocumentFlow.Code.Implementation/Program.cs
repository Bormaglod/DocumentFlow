using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using Inflector;
using Dapper;
using Npgsql;

namespace DocumentFlow.Code.Implementation
{
    class Cmd
    {
        public Guid id { get; set; }
        public DateTime date_updated { get; set; }
        public string script { get; set; }
    }

    class Program
    {
        static void Main(string[] _)
        {
            string line = File.ReadLines("passwords.txt").First();
            string[] words = line.Split(':');
            string user = words[0].Trim();
            string password = words[1].Trim();

            UpdateDatabase("Разработка", user, password);
            UpdateDatabase("Production", user, password);

            Console.ReadLine();
        }

        private static int DatetimeCompare(DateTime dt1, DateTime dt2)
        {
            if (dt1.Date == dt2.Date)
            {
                TimeSpan ts1 = new TimeSpan(0, dt1.Hour, dt1.Minute, dt1.Second, dt1.Millisecond);
                TimeSpan ts2 = new TimeSpan(0, dt2.Hour, dt2.Minute, dt2.Second, dt2.Millisecond);
                return TimeSpan.Compare(ts1, ts2);
            }
            else
            {
                return DateTime.Compare(dt1.Date, dt2.Date);
            }
        }

        private static void UpdateDatabase(string connectionName, string userName, string password)
        {
            Console.WriteLine($"****** {connectionName} ******");
            using (var conn = OpenConnection(connectionName, userName, password))
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string[] files = Directory.GetFiles("..\\..\\Entities");
                        foreach (string file in files)
                        {
                            DateTime dtFile = File.GetLastWriteTime(file);

                            string code = Path.GetFileNameWithoutExtension(file).Underscore();
                            Guid id = conn.QuerySingleOrDefault<Guid>("select id from entity_kind where code = :code", new { code });

                            Cmd cmd;
                            if (id == default)
                            {
                                code = code.Replace('_', '-');
                                cmd = conn.QuerySingle<Cmd>("select id, date_updated, script from command where code = :code", new { code });
                            }
                            else
                            {
                                cmd = conn.QuerySingle<Cmd>("select id, date_updated, script from command where entity_kind_id = :id", new { id });
                            }
                            
                            if (DatetimeCompare(dtFile, cmd.date_updated) == 0)
                                Console.WriteLine($"{code,-26} is EQUAL");
                            else
                            {
                                if (DatetimeCompare(dtFile, cmd.date_updated) < 0)
                                {
                                    File.WriteAllText(file, cmd.script);
                                    File.SetLastWriteTime(file, cmd.date_updated);
                                    Console.WriteLine($"{code,-26} << to FILE");
                                }
                                else
                                {
                                    cmd.script = File.ReadAllText(file);
                                    cmd.date_updated = dtFile;
                                    int cnt = conn.Execute("update command set script = :script, date_updated = :date_updated where id = :id", cmd, transaction);

                                    string res = cnt == 1 ? "OK" : "Fail";
                                    Console.WriteLine($"{code,-26} >> to DATABASE, {res}");
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine(e.Message);
                        return;
                    }
                }
            }
        }

        private static IDbConnection OpenConnection(string connectionName, string userName, string password)
        {
            string newConnectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(newConnectionString)
            {
                Username = userName,
                Password = password
            };

            NpgsqlConnection conn = new NpgsqlConnection(builder.ConnectionString);
            conn.Open();

            return conn;
        }
    }
}
