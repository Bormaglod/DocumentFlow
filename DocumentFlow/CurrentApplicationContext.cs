//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.07.2022
//
// Версия 2022.8.17
//  - рефакторинг
// Версия 2022.12.31
//  - в ShowMainForm добавлен параметр showStartPage с помощью
//    которого вызывается метод ShowStartPage из MainForm
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

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
        Services.ConfigureServices();

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

    public void ShowMainForm(bool showStartPage)
    {
        loginForm.Hide();

        if (showStartPage)
        {
            DocumentFlow.MainForm.ShowStartPage();
        }

        mainForm.Show();
    }

    private void FormClosed(object? sender, FormClosedEventArgs e) => ExitThread();
}
