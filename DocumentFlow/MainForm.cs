//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.AppInstallData;
using DocumentFlow.Controls;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;
using DocumentFlow.Interfaces;
using DocumentFlow.Settings;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Npgsql;

using Syncfusion.Windows.Forms.Tools;

using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocumentFlow;

public partial class MainForm : Form, IDockingManager, IHostApp
{
    private readonly IServiceProvider services;
    private readonly IDatabase database;
    private readonly AppSettings appSettings;
    private readonly LocalSettings localSettings;
    private readonly Navigator navigator;

    private readonly ConcurrentQueue<NotifyMessage> notifies = new();

    public event EventHandler<NotifyEventArgs>? OnAppNotify;

    public MainForm(IServiceProvider services, IDatabase database, IOptions<AppSettings> appSettings, IOptionsSnapshot<LocalSettings> localSettings)
    {
        InitializeComponent();

        this.services = services;
        this.database = database;
        this.appSettings = appSettings.Value;
        this.localSettings = localSettings.Value;

        navigator = services.GetRequiredService<Navigator>();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        dockingManager.DocumentWindowSettings.ShowTabList = true;

        WindowState = localSettings.MainForm.WindowState;
        if (localSettings.MainForm.WindowState == FormWindowState.Minimized)
        {
            WindowState = FormWindowState.Normal;
        }

        if (localSettings.MainForm.WindowState == FormWindowState.Normal)
        {
            Location = localSettings.MainForm.Location;
            Size = localSettings.MainForm.Size;
        }

        dockingManager.DockControl(navigator, this, DockingStyle.Left, localSettings.MainForm.NavigatorWidth);
        dockingManager.SetDockLabel(navigator, "Навигатор");

        if (appSettings.UseDataNotification)
        {
            timerCheckListener.Start();
            timerDatabaseListen.Start();

            _ = CreateListener();
        }

        Text = $"DocumentFlow {Assembly.GetExecutingAssembly().GetName().Version} - <{database.ConnectionName}>";
    }

    public void SendNotify(string entityName, IDocumentInfo document, MessageAction action)
    {
        OnAppNotify?.Invoke(this, new NotifyEventArgs(entityName, document, action));
    }

    public void SendNotify(MessageDestination destination, string? entityName, Guid objectId, MessageAction action)
    {
        if (string.IsNullOrEmpty(entityName))
        {
            return;
        }

        NotifyEventArgs? args = null;
        switch (destination)
        {
            case MessageDestination.Object:
                args = new(entityName, objectId, action);
                break;
            case MessageDestination.List:
                if (objectId == Guid.Empty)
                {
                    args = new(entityName);
                }
                else
                {
                    args = new(entityName, objectId);
                }

                break;
            default:
                break;
        }

        if (args != null)
        {
            OnAppNotify?.Invoke(this, args);
        }
    }

    #region IDockingManager interface implemented

    void IDockingManager.Activate(IPage page)
    {
        if (page is Control control)
        {
            if (!dockingManager.GetDockVisibility(control))
            {
                dockingManager.SetDockLabel(control, control.Text);
                dockingManager.DockAsDocument(control);
            }
            else
            {
                dockingManager.ActivateControl(control);
            }
        }
    }

    void IDockingManager.UpdateHeader(IPage page)
    {
        if (page is Control control)
        {
            dockingManager.SetDockLabel(control, control.Text);
        }
    }

    void IDockingManager.Close(IPage page)
    {
        if (page is Control control)
        {
            dockingManager.SetDockVisibility(control, false);
        }
    }

    bool IDockingManager.IsVisibility(IPage page)
    {
        if (page is Control control)
        {
            return dockingManager.GetDockVisibility(control);
        }

        throw new NotImplementedException();
    }

    #endregion

    private void Listener()
    {
        using var conn = new NpgsqlConnection(database.ConnectionString);
        conn.Open();
        conn.Notification += (o, e) =>
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                    {
                        new JsonStringEnumConverter()
                    }
            };
            NotifyMessage? message = JsonSerializer.Deserialize<NotifyMessage>(e.Payload, options);
            if (message != null)
            {
                notifies.Enqueue(message);
            }
        };

        using (var cmd = new NpgsqlCommand("LISTEN db_notification", conn))
        {
            cmd.ExecuteNonQuery();
        }

        while (true)
        {
            conn.Wait();
        }
    }

    private async Task CreateListener()
    {
        try
        {
            await Task.Run(Listener);
        }
        catch (Exception)
        {
        }
    }

    private void ExecuteUpdateInstaller(string installerFileName)
    {
        if (MessageBox.Show("Обновление загружено. Установить?", "Установка обновления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        Process.Start(new ProcessStartInfo(installerFileName) { UseShellExecute = true });

        Invoke((MethodInvoker)delegate
        {
            Close();
        });
    }

    private void TimerDatabaseListen_Tick(object sender, EventArgs e)
    {
        if (appSettings.UseDataNotification && notifies.TryDequeue(out NotifyMessage? message))
        {
            SendNotify(message.Destination, message.EntityName, message.ObjectId, message.Action);
        }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        localSettings.MainForm.WindowState = WindowState;
        localSettings.MainForm.Location = Location;
        localSettings.MainForm.Size = Size;
        localSettings.MainForm.NavigatorWidth = navigator.Width;
        localSettings.Save();

        services.GetRequiredService<IPageManager>().NotifyMainFormClosing();

        if (appSettings.UseDataNotification)
        {
            timerDatabaseListen.Stop();
            timerCheckListener.Stop();
        }
    }

    private void DockingManager_DockVisibilityChanged(object sender, DockVisibilityChangedEventArgs arg)
    {
        if (arg.Control is IPage page)
        {
            page.NotifyPageClosing();
        }
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
        using MemoryStream memoryStream = new();

        using var s3 = services.GetRequiredService<IS3Object>();
        s3.GetObject("app-install", "app.install.json", (stream) => stream.CopyTo(memoryStream)).Wait();

        memoryStream.Position = 0;

        var appInstall = JsonSerializer.Deserialize<AppInstall>(memoryStream);
        if (appInstall != null)
        {
            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            if (currentVersion != null)
            {
                var fileVersion = Version.Parse(appInstall.App.Version);
                if (currentVersion < fileVersion)
                {
                    if (MessageBox.Show($"Текущая версия приложения {currentVersion}. Доступно для скачивания версия {fileVersion}. Скачать и установить?", "Обновление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }

                    var path = FileHelper.GetTempPath("AppInstall");
                    if (!Path.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var file = Path.Combine(path, appInstall.App.FileName);
                    await s3
                        .GetObject("app-install", appInstall.App.FileName, file)
                        .ContinueWith(task => ExecuteUpdateInstaller(file));
                }
            }
        }
    }
}