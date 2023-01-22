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
    /*private readonly SplashForm splash;
    private readonly bool complete = false;*/

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
        //var start = DateTime.Now;

        //using var tokenSource = new CancellationTokenSource();
        //_ = CreateSplashWindow(tokenSource.Token);
        ////Task.Run(() => RunShowSplashWindow(cts.Token));*/
        ///*new Thread(run).Start(cts.Token);*/
        ///*SplashForm splash = new();
        //splash.Show();*/

        ///*complete = false;

        //var timer = new System.Windows.Forms.Timer() 
        //{ 
        //    Interval = 1000 
        //};

        //timer.Tick += Timer_Tick;
        //timer.Start();*/

        //try
        //{
            Services.ConfigureServices();

            context = this;

            loginForm = new LoginForm();
            loginForm.FormClosed += FormClosed;

            mainForm = new MainForm();
            mainForm.FormClosed += FormClosed;
        //}
        //finally
        //{
        //    tokenSource.Cancel();
        //    //cts.Cancel();
        //    /*var end = DateTime.Now;
            
        //    TimeSpan time = end - start;
        //    int delay = 10000 - Convert.ToInt32(time.TotalMilliseconds);
        //    if (delay > 0)
        //    {
        //        Task.Delay(time);
        //    }

        //    splash.Close();*/
        //    /*complete = true;
        //    //while (splash.Visible) {}
        //    timer.Stop();*/
        //}

        loginForm.Show();
    }

    /*private void Timer_Tick(object? sender, EventArgs e)
    {
        if (complete) 
        {
            splash.Close();
        }
    }*/

    public ITabPages TabPages => mainForm;

    public IHostApp App => mainForm;

    //private void ShowSplashWindow(CancellationToken token)
    //{
    //    var f = new SplashForm();
    //    Application.Run(f);
    //    //var f = new SplashForm();
    //    //f.Show();

    //    while (!token.IsCancellationRequested)
    //    {
    //        Task.Delay(5000, token);
    //    }

    //    f.Close();
    //    /*do
    //    {
    //        ShowSplashWindow();

    //        await Task.Delay(60000, ct);
    //    } while (!ct.IsCancellationRequested);*/
    //}

    //private async Task CreateSplashWindow(CancellationToken token)
    //{
    //    if (token.IsCancellationRequested)
    //        return;

    //    try
    //    {
    //        await Task.Run(() => ShowSplashWindow(token), token).ConfigureAwait(false);
    //    }
    //    catch (TaskCanceledException)
    //    {
    //    }
    //}

    /*public void ShowSplashWindow()
    {
        Application.Run(new SplashForm());
    }*/

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
