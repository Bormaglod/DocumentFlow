//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.07.2022
//-----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow;

public class CurrentApplicationContext : ApplicationContext
{
    private readonly IServiceProvider services;
    private readonly LoginForm loginForm;
    private readonly MainForm mainForm;

    private static CurrentApplicationContext? Instance;

    public CurrentApplicationContext(IServiceProvider services)
    {
        Instance = this;

        this.services = services;

        loginForm = services.GetRequiredService<LoginForm>();
        loginForm.FormClosed += FormClosed;

        mainForm = services.GetRequiredService<MainForm>();
        mainForm.FormClosed += FormClosed;

        loginForm.Show();
    }

    public static IServiceProvider GetServices()
    {
        if (Instance == null)
        {
            throw new Exception("Для CurrentApplicationContext необходимо вызвать конструктор.");
        }

        return Instance.services;
    }

    public void ShowMainForm()
    {
        loginForm.Hide();
        mainForm.Show();
    }

    private void FormClosed(object? sender, FormClosedEventArgs e) => ExitThread();
}
