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
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocumentFlow;

public partial class MainForm : 
    Form, 
    IRecipient<EditorPageHeaderChangedMessage>, 
    IRecipient<EntityBrowserOpenMessage>,
    IRecipient<EntityEditorOpenMessage>,
    IRecipient<PageOpenMessage>,
    IRecipient<PageCloseMessage>
{
    private readonly IServiceProvider services;
    private readonly IDatabase database;
    private readonly AppSettings appSettings;
    private readonly LocalSettings localSettings;
    private readonly Navigator navigator;

    private readonly ConcurrentQueue<NotifyMessage> notifies = new();
    private readonly CancellationTokenSource cancelTokenSource;

    private readonly List<IPage> pages = new();

    public MainForm(IServiceProvider services, IDatabase database, IOptions<AppSettings> appSettings, IOptionsSnapshot<LocalSettings> localSettings)
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.RegisterAll(this);
        WeakReferenceMessenger.Default.Register<MainForm, PageVisibilityMessage>(this, (form, message) =>
        {
            if (message.Page is Control control)
            {
                message.Reply(dockingManager.GetDockVisibility(control));
            }
        });

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

    public void Receive(EntityBrowserOpenMessage message)
    {
        var page = pages.FirstOrDefault(p => p.GetType().GetInterface(message.BrowserType.Name) != null);
        if (page == null)
        {
            try
            {
                var browser = services.GetRequiredService(message.BrowserType);
                if (browser is IBrowserPage browserPage)
                {
                    browserPage.UpdatePage(message.Text);
                    pages.Add(browserPage);
                    Activate(browserPage);
                }
            }
            catch (PostgresException e)
            {
                if (e.SqlState == "42501")
                {
                    MessageBox.Show("Доступ запрещен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw;
                }
            }
        }
        else
        {
            ActivatePage(page);
        }
    }

    public void Receive(EntityEditorOpenMessage message)
    {
        IEditorPage? page = null;
        if (message.ObjectId != null)
        {
            page = pages
                .OfType<IEditorPage>()
                .Where(p => p.Editor.GetType().GetInterface(message.EditorType.Name) != null)
                .FirstOrDefault(p => p.Editor.DocumentInfo.Id == message.ObjectId);
        }

        if (page == null)
        {
            try
            {
                page = services.GetRequiredService<IEditorPage>();

                var editor = services.GetRequiredService(message.EditorType);

                if (editor is IDocumentEditor documentEditor && message.Owner != null)
                {
                    documentEditor.SetOwner(message.Owner);
                }

                if (editor is IDirectoryEditor directoryEditor && message.ParentId != null)
                {
                    directoryEditor.ParentId = message.ParentId;
                }

                pages.Add(page);
                Activate(page);

                if (editor is IEditor e)
                {
                    e.EnabledEditor = !message.ReadOnly;
                    page.Editor = e;
                    if (message.ObjectId != null)
                    {
                        page.RefreshPage(message.ObjectId.Value);
                    }
                    else
                    {
                        page.CreatePage();
                    }
                }
            }
            catch (PostgresException e)
            {
                if (e.SqlState == "42501")
                {
                    MessageBox.Show("Доступ запрещен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw;
                }
            }
        }
        else
        {
            ActivatePage(page);
        }
    }

    public void Receive(PageOpenMessage message) => ShowPage(message.PageType);

    public void Receive(PageCloseMessage message)
    {
        if (message.Page is Control control)
        {
            dockingManager.SetDockVisibility(control, false);
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

        ShowPage(typeof(IStartPage));
    }

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

    private void Activate(IPage page)
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

    private void ActivatePage(IPage page)
    {
        bool refreshRequired = !dockingManager.GetDockVisibility((Control)page);
        Activate(page);

        if (refreshRequired)
        {
            page.RefreshPage();
        }
    }

    private void ShowPage(Type pageType)
    {
        var page = pages.FirstOrDefault(p => p.GetType().GetInterface(pageType.Name) != null);
        if (page == null)
        {
            if (services.GetRequiredService(pageType) is IPage newPage)
            {
                pages.Add(newPage);

                ActivatePage(newPage);
            }
        }
        else
        {
            ActivatePage(page);
        }
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

        foreach (var item in pages)
        {
            item.NotifyPageClosing();
        }

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