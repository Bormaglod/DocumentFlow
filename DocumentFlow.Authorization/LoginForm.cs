//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2018
// Time: 14:53
//-----------------------------------------------------------------------

namespace DocumentFlow.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
#if DEBUG
    using System.IO;
#endif
    using System.Linq;
    using System.Windows.Forms;
    using NHibernate;
    using Npgsql;
    using Syncfusion.Windows.Forms;
    using DocumentFlow.Data.Core;
    using DocumentFlow.Data.Entities;
    using DocumentFlow.Authorization.Properties;

    public partial class LoginForm : MetroForm
    {
        private Form mainWindow;
        private readonly Type windowType;

        public LoginForm(Type type)
        {
            InitializeComponent();

            GetConnectionStrings();

            windowType = type;
        }

        private void GetConnectionStrings()
        {
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

            string userName = comboUsers.SelectedItem is UserAlias c ? c.PgName : comboUsers.Text;

            Settings.Default.PreviousUser = userName;
            Settings.Default.LastConnectedDatabase = comboDatabase.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(userName))
            {
                try
                {
                    using (var session = Db.OpenSession(comboDatabase.SelectedItem.ToString(), userName, textPassword.Text))
                    {
                        using (var transaction = session.BeginTransaction())
                        {
                            IQuery query = session.CreateSQLQuery("select login()");
                            query.ExecuteUpdate();
                            transaction.Commit();
                        }
                    }
                }
                catch (PostgresException ex)
                {
                    if (ex.SqlState == "28P01")
                    {
                        MessageBox.Show("Указан не верный пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                        throw ex;
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

            using (var session = Db.OpenSession(comboDatabase.SelectedItem.ToString(), "guest", "guest"))
            {
                using (var transaction = session.BeginTransaction())
                {
#if DEBUG
                    comboUsers.DataSource = session.QueryOver<UserAlias>().Where(u => !u.IsSystem || u.PgName == "postgres").OrderBy(u => u.Name).Asc.List();
#else
                    comboUsers.DataSource = session.QueryOver<UserAlias>().Where(u => !u.IsSystem).OrderBy(u => u.Name).Asc.List();
#endif
                }

#if DEBUG
                comboUsers.SelectedItem = (comboUsers.DataSource as List<UserAlias>).FirstOrDefault(u => u.PgName == "postgres");
                textPassword.Text = File.ReadLines("password.txt").First();
#else
                comboUsers.SelectedItem = (comboUsers.DataSource as List<UserAlias>).FirstOrDefault(u => u.Name == Settings.Default.PreviousUser);
#endif
            }
        }
    }
}
