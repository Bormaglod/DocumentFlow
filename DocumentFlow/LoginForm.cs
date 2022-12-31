//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2018
//
// Версия 2022.12.31
//  - в вызов ShowMainForm добавлен параметр newDatabaseSelecting
//    определяющий необходимость показа стартовой страницы
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Core.Repository;

using Microsoft.Extensions.DependencyInjection;

using Npgsql;

using System.Configuration;
using System.IO;

namespace DocumentFlow;

public partial class LoginForm : Form
{
    private string prevDatabaseName = string.Empty;

    public LoginForm()
    {
        InitializeComponent();

        if (string.IsNullOrEmpty(Properties.Settings.Default.LastConnectedDatabase))
        {
            Properties.Settings.Default.Upgrade();
        }

        var settings = ConfigurationManager.ConnectionStrings;

        if (settings != null)
        {
            foreach (ConnectionStringSettings cs in settings)
            {
                comboDatabase.Items.Add(cs.Name);
            }

            comboDatabase.SelectedItem = Properties.Settings.Default.LastConnectedDatabase;
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

        string userName = comboUsers.SelectedItem is UserAlias c ? (c.pg_name ?? string.Empty) : comboUsers.Text;

        Properties.Settings.Default.PreviousUser = userName;
        Properties.Settings.Default.LastConnectedDatabase = comboDatabase.SelectedItem.ToString();

        if (!string.IsNullOrEmpty(userName))
        {
            try
            {
                var database = Services.Provider.GetService<IDatabase>();
                database!.Login(userName, textPassword.Text);
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

            Properties.Settings.Default.Save();

            bool newDatabaseSelecting = prevDatabaseName != comboDatabase.SelectedItem.ToString();
            prevDatabaseName = comboDatabase.SelectedItem.ToString() ?? string.Empty;

            CurrentApplicationContext.Context.ShowMainForm(newDatabaseSelecting);
        }
    }

    private void LoginWindow_VisibleChanged(object sender, EventArgs e)
    {
        //textPassword.Text = "";
        /**/
    }

    private void ComboDatabase_SelectedValueChanged(object sender, EventArgs e)
    {
        Database.ConnectionName = comboDatabase.SelectedItem?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(Database.ConnectionName))
        {
            return;
        }

        var users = Services.Provider.GetService<IUserAliasRepository>();
        var list = users!.GetUsers();
        comboUsers.DataSource = list;

#if DEBUG
        comboUsers.SelectedItem = list!.FirstOrDefault(u => u.pg_name == "postgres");
        if (File.Exists("password.txt"))
        {
            textPassword.Text = File.ReadLines("password.txt").First();
        }
#else
        comboUsers.SelectedItem = list.FirstOrDefault(u => u.pg_name == Properties.Settings.Default.PreviousUser);
#endif
    }
}
