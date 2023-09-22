//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2018
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Settings;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Npgsql;

using System.IO;

namespace DocumentFlow;

public partial class LoginForm : Form
{
    private readonly IDatabase database;
    private readonly IUserAliasRepository users;
    private readonly IServiceProvider services;
    private readonly AppSettings appSettings;
    private readonly LocalSettings localSettings;

    public LoginForm(IServiceProvider services, IDatabase database, IUserAliasRepository users, IOptions<AppSettings> oprions, IOptionsSnapshot<LocalSettings> settings)
    {
        InitializeComponent();

        this.services = services;
        this.database = database;
        this.users = users;

        appSettings = oprions.Value;
        localSettings = settings.Value;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        
        foreach (var cs in appSettings.Connections)
        {
            comboDatabase.Items.Add(cs.Name);
        }

        comboDatabase.SelectedItem = localSettings.Login.LastConnection;

        textPassword.Select();
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

        string userName = comboUsers.SelectedItem is UserAlias c ? (c.PgName ?? string.Empty) : comboUsers.Text;

        localSettings.Login.PreviousUser = userName;
        localSettings.Login.LastConnection = comboDatabase.SelectedItem.ToString() ?? string.Empty;

        if (!string.IsNullOrEmpty(userName))
        {
            try
            {
                database.Login(userName, textPassword.Text);
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

            localSettings.Save();

            var context = services.GetRequiredService<CurrentApplicationContext>();
            context.ShowMainForm();
        }
    }

    private void LoginWindow_VisibleChanged(object sender, EventArgs e)
    {
        //textPassword.Text = "";
        /**/
    }

    private void ComboDatabase_SelectedValueChanged(object sender, EventArgs e)
    {
        database.ConnectionName = comboDatabase.SelectedItem?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(database.ConnectionName))
        {
            return;
        }

        var list = users.GetUsers();
        comboUsers.DataSource = list;

#if DEBUG
        if (File.Exists("password.txt"))
        {
            var user = File.ReadLines("password.txt").First().Split(':', StringSplitOptions.TrimEntries);
            comboUsers.SelectedItem = list.FirstOrDefault(u => u.PgName == user[0]);
            textPassword.Text = user[1];
        }
#else
        if (localSettings != null) 
        {
            comboUsers.SelectedItem = list.FirstOrDefault(u => u.PgName == localSettings.Login.PreviousUser);
        }
#endif
    }
}
