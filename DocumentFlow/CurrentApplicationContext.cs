//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow;

public class CurrentApplicationContext : ApplicationContext
{
    private readonly IServiceProvider services;
    private readonly LoginForm loginForm;
    private readonly MainForm mainForm;

    public CurrentApplicationContext(IServiceProvider services)
    {
        this.services = services;

        loginForm = services.GetRequiredService<LoginForm>();
        loginForm.FormClosed += FormClosed;

        mainForm = services.GetRequiredService<MainForm>();
        mainForm.FormClosed += FormClosed;

        loginForm.Show();
    }

    public void ShowLoginForm()
    {
        mainForm.Hide();
        loginForm.Show();
    }

    public void ShowMainForm()
    {
        loginForm.Hide();

        services
            .GetRequiredService<IPageManager>()
            .ShowStartPage();

        mainForm.Show();
    }

    private void FormClosed(object? sender, FormClosedEventArgs e) => ExitThread();
}
