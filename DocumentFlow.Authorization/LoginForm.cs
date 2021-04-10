//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2018
// Time: 14:53
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
#if DEBUG
using System.IO;
#endif
using System.Linq;
using System.Windows.Forms;
using Dapper;
using Npgsql;
using DocumentFlow.Data;
using DocumentFlow.Data.Entities;
using DocumentFlow.Authorization.Properties;

namespace DocumentFlow.Authorization
{
    public partial class LoginForm : Form
    {
        private Form mainWindow;
        private readonly Type windowType;

        public LoginForm(Type type)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Settings.Default.LastConnectedDatabase))
            {
                Settings.Default.Upgrade();
            }

            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    comboDatabase.Items.Add(cs.Name);
                }

                comboDatabase.SelectedItem = Settings.Default.LastConnectedDatabase;
            }

            windowType = type;
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (comboDatabase.SelectedItem == null)
            {
                MessageBox.Show("Вы должны указать выбрать базу данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textPassword.Text))
            {
                MessageBox.Show("Вы должны указать пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string userName = comboUsers.SelectedItem is UserAlias c ? c.pg_name : comboUsers.Text;

            Settings.Default.PreviousUser = userName;
            Settings.Default.LastConnectedDatabase = comboDatabase.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(userName))
            {
                try
                {
                    using var connection = Db.OpenConnection(comboDatabase.SelectedItem.ToString(), userName, textPassword.Text);
                    connection.Execute("login", commandType: CommandType.StoredProcedure);
                }
                catch (PostgresException ex)
                {
                    if (ex.SqlState == "28P01")
                    {
                        MessageBox.Show("Указан не верный пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                        throw;
                }

                Settings.Default.Save();
                Hide();

                if (mainWindow == null)
                {
                    mainWindow = Activator.CreateInstance(windowType, this) as Form;
                }

                mainWindow.Show();
            }
        }

        private void LoginWindow_VisibleChanged(object sender, EventArgs e)
        {
            //textPassword.Text = "";
            /**/
        }

        private void ComboDatabase_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboDatabase.SelectedItem == null)
                return;

            using var connection = Db.OpenGuestConnection(comboDatabase.SelectedItem.ToString());
            List<UserAlias> users;
#if DEBUG
            string sql = "select * from user_alias where not is_system or pg_name = 'postgres' order by name";
#else
            string sql = "select* from user_alias where not is_system order by name";
#endif

            users = connection.Query<UserAlias>(sql).ToList();
            comboUsers.DataSource = users;

#if DEBUG
            comboUsers.SelectedItem = users.FirstOrDefault(u => u.pg_name == "postgres");
            textPassword.Text = File.ReadLines("password.txt").First();
#else
            comboUsers.SelectedItem = users.FirstOrDefault(u => u.pg_name == Settings.Default.PreviousUser);
#endif
        }
    }
}
