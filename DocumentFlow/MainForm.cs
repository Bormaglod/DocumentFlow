//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.06.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.AppInstallData;
using DocumentFlow.Controls;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;
using DocumentFlow.Interfaces;
using DocumentFlow.Messages;
using DocumentFlow.Settings;
using DocumentFlow.Tools;
using DocumentFlow.Tools.Minio;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Minio;

using Npgsql;

using Syncfusion.Windows.Forms.Tools;

using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocumentFlow;

public partial class MainForm : Form, IDockingManager, IRecipient<EditorPageHeaderChangedMessage>
{
    private readonly IServiceProvider services;
    private readonly IDatabase database;
    private readonly AppSettings appSettings;
    private readonly LocalSettings localSettings;
    private readonly Navigator navigator;

    private readonly ConcurrentQueue<NotifyMessage> notifies = new();
    private readonly CancellationTokenSource cancelTokenSource;

    public MainForm(IServiceProvider services, IDatabase database, IOptions<AppSettings> appSettings, IOptionsSnapshot<LocalSettings> localSettings)
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register(this);

        this.services = services;
        this.database = database;
        this.appSettings = appSettings.Value;
        this.localSettings = localSettings.Value;

        navigator = services.GetRequiredService<Navigator>();

        cancelTokenSource = new CancellationTokenSource();
    }

    public void Receive(EditorPageHeaderChangedMessage message)
    {
        if (message.Page is Control control)
        {
            dockingManager.SetDockLabel(control, message.Header);
        }
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

            CancellationToken token = cancelTokenSource.Token;

            Task.Run(() => CreateListener(token), token);
        }

        Text = $"DocumentFlow {Assembly.GetExecutingAssembly().GetName().Version} - <{database.ConnectionName}>";
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

    private async Task CreateListener(CancellationToken token)
    {
        await using var conn = new NpgsqlConnection(database.ConnectionString);
        await conn.OpenAsync(token);
        
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

        await using (var cmd = new NpgsqlCommand("LISTEN db_notification", conn))
        {
            cmd.ExecuteNonQuery();
        }

        while (!token.IsCancellationRequested)
        {
            await conn.WaitAsync(token);
        }

        await Task.CompletedTask;
    }

    private void ExecuteUpdateInstaller(string installerFileName)
    {
        Invoke((MethodInvoker)delegate
        {
            toolStripStatusLabel1.Visible = false;
            toolStripProgressBar1.Visible = false;
        });

        if (MessageBox.Show("Обновление загружено. Установить?", "Установка обновления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        WorkOperations.OpenFile(installerFileName);

        Invoke((MethodInvoker)delegate
        {
            Close();
        });
    }

    private void TimerDatabaseListen_Tick(object sender, EventArgs e)
    {
        if (appSettings.UseDataNotification && notifies.TryDequeue(out NotifyMessage? notify))
        {
            if (string.IsNullOrEmpty(notify.EntityName))
            {
                return;
            }

            EntityActionMessage? message = null;
            switch (notify.Destination)
            {
                case MessageDestination.Object:
                    message = new(notify.EntityName, notify.ObjectId, notify.Action);
                    break;
                case MessageDestination.List:
                    if (notify.ObjectId == Guid.Empty)
                    {
                        message = new(notify.EntityName);
                    }
                    else
                    {
                        message = new(notify.EntityName, notify.ObjectId);
                    }

                    break;
                default:
                    break;
            }

            if (message != null)
            {
                WeakReferenceMessenger.Default.Send(message);
            }
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

        var minio = services.GetRequiredService<IMinioClient>();
        GetObjectStream.Run(minio, "app-install", "app.install.json", (stream) => stream.CopyTo(memoryStream)).Wait();

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

                    toolStripStatusLabel1.Visible = true;
                    toolStripProgressBar1.Visible = true;

                    var file = Path.Combine(path, appInstall.App.FileName);
                    await GetObject.Run(minio, "app-install", appInstall.App.FileName, file)
                        .ContinueWith(task => ExecuteUpdateInstaller(file));
                }
            }
        }
    }
}