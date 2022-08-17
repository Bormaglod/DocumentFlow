//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.07.2022
//
// Версия 2022.8.17
//  - рефакторинг
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Infrastructure;

namespace DocumentFlow;

public class CurrentApplicationContext : ApplicationContext
{
    private static CurrentApplicationContext? context;
    private readonly LoginForm loginForm;
    private readonly MainForm mainForm;

    public static CurrentApplicationContext Context
    {
        get
        {
            context ??= new();
            return context;
        }
    }

    public CurrentApplicationContext()
    {
        context = this;

        loginForm = new LoginForm();
        loginForm.FormClosed += FormClosed;

        mainForm = new MainForm();
        mainForm.FormClosed += FormClosed;

        loginForm.Show();
    }

    public ITabPages TabPages => mainForm;

    public IHostApp App => mainForm;

    public void ShowLoginForm()
    {
        mainForm.Hide();
        loginForm.Show();
    }

    public void ShowMainForm()
    {
        loginForm.Hide();
        mainForm.Show();
    }

    private void FormClosed(object? sender, FormClosedEventArgs e) => ExitThread();
}
