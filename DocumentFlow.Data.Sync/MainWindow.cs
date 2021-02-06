using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using Inflector;
using Syncfusion.Windows.Forms.Tools;
using DocumentFlow.Data;

namespace DocumentFlow.Data.Sync
{
    public partial class MainWindow : Form
    {
        private string user;
        private string password;

        public MainWindow()
        {
            InitializeComponent();

            string line = File.ReadLines("passwords.txt").First();
            string[] words = line.Split(':');

            user = words[0].Trim();
            password = words[1].Trim();
        }

        private int DateTimeCompare(DateTime dt1, DateTime dt2)
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

        private void UpdateCommandDatabase(string connectionName)
        {
            textConsole.AppendLine($"****** {connectionName} : COMMANDS ******");
            using (var conn = Db.OpenConnection(connectionName, user, password))
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string[] files = Directory.GetFiles(@"..\..\Commands");

                        foreach (string file in files)
                        {
                            DateTime dtFile = File.GetLastWriteTime(file);

                            string code = Path.GetFileNameWithoutExtension(file).Underscore();
                            Guid id = conn.QuerySingleOrDefault<Guid>("select id from entity_kind where code = :code", new { code });

                            Command cmd;
                            if (id == default)
                            {
                                code = code.Replace('_', '-');
                                cmd = conn.QuerySingle<Command>("select id, date_updated, script from command where code = :code", new { code });
                            }
                            else
                            {
                                cmd = conn.QuerySingle<Command>("select id, date_updated, script from command where entity_kind_id = :id", new { id });
                            }

                            if (DateTimeCompare(dtFile, cmd.date_updated) == 0)
                                textConsole.AppendLine($"{code,-26} is EQUAL");
                            else
                            {
                                if (DateTimeCompare(dtFile, cmd.date_updated) < 0)
                                {
                                    File.WriteAllText(file, cmd.script);
                                    File.SetLastWriteTime(file, cmd.date_updated);
                                    textConsole.AppendLine($"{code,-26} << to FILE");
                                }
                                else
                                {
                                    cmd.script = File.ReadAllText(file);
                                    cmd.date_updated = dtFile;
                                    int cnt = conn.Execute("update command set script = :script, date_updated = :date_updated where id = :id", cmd, transaction);

                                    string res = cnt == 1 ? "OK" : "Fail";
                                    textConsole.AppendLine($"{code,-26} >> to DATABASE, {res}");
                                }
                            }

                            Application.DoEvents();
                        }

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        textConsole.AppendLine(e.Message);
                        return;
                    }
                }
            }
        }

        private void UpdateReportDatabase(string connectionName)
        {
            textConsole.AppendLine($"****** {connectionName} : REPORTS ******");
            using (var conn = Db.OpenConnection(connectionName, user, password))
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string[] files = Directory.GetFiles(@"..\..\Reports");

                        foreach (string file in files)
                        {
                            DateTime dtFile = File.GetLastWriteTime(file);

                            string code = Path.GetFileNameWithoutExtension(file).Underscore();

                            code = code.Replace('_', '-');
                            PrintedForm pf = conn.QuerySingleOrDefault<PrintedForm>("select id, date_updated, schema_form from printed_form where code = :code", new { code });
                            if (pf == default)
                            {
                                continue;
                            }

                            if (DateTimeCompare(dtFile, pf.date_updated) == 0)
                                textConsole.AppendLine($"{code,-26} is EQUAL");
                            else
                            {
                                if (DateTimeCompare(dtFile, pf.date_updated) < 0)
                                {
                                    File.WriteAllText(file, pf.schema_form);
                                    File.SetLastWriteTime(file, pf.date_updated);
                                    textConsole.AppendLine($"{code,-26} << to FILE");
                                }
                                else
                                {
                                    pf.schema_form = File.ReadAllText(file);
                                    pf.date_updated = dtFile;
                                    int cnt = conn.Execute("update printed_form set schema_form = :schema_form::jsonb, date_updated = :date_updated where id = :id", pf, transaction);

                                    string res = cnt == 1 ? "OK" : "Fail";
                                    textConsole.AppendLine($"{code,-26} >> to DATABASE, {res}");
                                }
                            }

                            Application.DoEvents();
                        }

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        textConsole.AppendLine(e.Message);
                        return;
                    }
                }
            }
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            toggleButtonDev.Enabled = false;
            toggleButtonProd.Enabled = false;
            sfButton1.Enabled = false;
            try
            {
                textConsole.Clear();
                if (toggleButtonDev.ToggleState == ToggleButtonState.Active)
                {
                    UpdateCommandDatabase("Разработка");
                    UpdateReportDatabase("Разработка");
                }

                if (toggleButtonProd.ToggleState == ToggleButtonState.Active)
                {
                    UpdateCommandDatabase("Production");
                    UpdateReportDatabase("Production");
                }
            }
            finally
            {
                toggleButtonDev.Enabled = true;
                toggleButtonProd.Enabled = true;
                sfButton1.Enabled = true;
            }
        }
    }
}
