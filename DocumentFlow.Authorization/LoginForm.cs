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
    using System.Configuration;
#if DEBUG
    using System.IO;
    using System.Linq;
#endif
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
        private Type windowType;

        public LoginForm(Type type)
        {
            InitializeComponent();

            GetConnectionStrings();

            windowType = type;
#if DEBUG
            comboDatabase.SelectedIndex = 1;
            comboUsers.Text = "postgres";
            textPassword.Text = File.ReadLines("password.txt").First();
#endif
        }

        private void GetConnectionStrings()
        {
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

        private void buttonLogin_Click(object sender, EventArgs e)
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

            UserAlias c = comboUsers.SelectedItem as UserAlias;
            string userName = c != null ? c.PgName : comboUsers.Text;

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

        private void comboDatabase_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboDatabase.SelectedItem == null)
                return;

            using (var session = Db.OpenSession(comboDatabase.SelectedItem.ToString(), "guest", "guest"))
            {
                using (var transaction = session.BeginTransaction())
                {
                    foreach (UserAlias user in session.QueryOver<UserAlias>()/*.Where(u => !(u.Administrator || u.IsGroup))*/.List())
                    {
                        comboUsers.Items.Add(user);
                    }
                }
            }
        }
    }
}
