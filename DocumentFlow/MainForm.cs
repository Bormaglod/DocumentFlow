//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.12.2021
//
// Версия 2022.8.28
//  - добавлена реакция на нажатие клавиши F5
// Версия 2022.12.31
//  - добавлен метод ShowStartPage (заглушка)
// Версия 2023.1.9
//  - добавлен пункт меню "О программе"
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.2.12
//  - исправлены мелкие ошибки
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Npgsql;

using Syncfusion.Windows.Forms.Tools;

using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocumentFlow;

public partial class MainForm : Form, IHostApp, ITabPages
{
    private class PageData
    {
        private readonly MenuDestination destination;
        private readonly string name;
        private readonly int order;
        private readonly string? parent;

        public PageData(MenuAttribute menu, Type type) => (destination, name, order, parent, PageType) = (menu.Destination, menu.Name, menu.Order, menu.Parent, type);
        public PageData(MenuDestination destination, string name, int order, string? parent = null) => (this.destination, this.name, this.order, this.parent) = (destination, name, order, parent);

        public MenuDestination Destination => destination;
        public string Name => name;
        public int Order => order;
        public string? Parent => parent;
        public Type? PageType { get; }
    }

#if USE_LISTENER
    private Task? listenerTask;
    private readonly ConcurrentQueue<NotifyMessage> notifies = new();
#endif

    public MainForm()
    {
        InitializeComponent();

        tabControlAdv1.TabPages.Clear();

        CreateMenu();

#if USE_LISTENER
        if (Properties.Settings.Default.UseDataNotification)
        {
            timerDatabaseListen.Start();

            listenerTask = CreateListener();
        }
#endif
    }

    public event EventHandler<NotifyEventArgs>? OnAppNotify;

    #region ITabPages implemented

    TabPageAdv ITabPages.Selected { get => tabControlAdv1.SelectedTab; set => tabControlAdv1.SelectedTab = value; }

    void ITabPages.Add(TabPageAdv page) => tabControlAdv1.TabPages.Add(page);

    void ITabPages.Remove(TabPageAdv page) => tabControlAdv1.TabPages.Remove(page);

    #endregion

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

    public static void ShowStartPage() => Services.Provider.GetService<IPageManager>()?.ShowStartPage();

    private void CreateMenu()
    {
        Type viewerType = typeof(IBrowser<>);

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(p => p.IsInterface)
            .Where(p => p.GetInterfaces().Any(i => i.IsGenericType && viewerType == i.GetGenericTypeDefinition()));

        List<PageData> pageTypes = new();
        foreach (var type in types)
        {
            var attr = type.GetCustomAttribute<MenuAttribute>();
            if (attr != null)
            {
                pageTypes.Add(new(attr, type));
            }
        }

        pageTypes.Add(new(MenuDestination.Document, "Склад", 20));
        pageTypes.Add(new(MenuDestination.Document, "Производство", 30));
        pageTypes.Add(new(MenuDestination.Document, "Расчёты с контрагентами", 30));
        pageTypes.Add(new(MenuDestination.Document, "Зар. плата", 70));

        pageTypes.Add(new(MenuDestination.Directory, "Номенклатура", 90));
        pageTypes.Add(new(MenuDestination.Directory, "Производственные операции", 110));


        foreach (var item in pageTypes.OrderBy(m => m.Order).Where(m => m.Parent == null))
        {
            TreeMenuItem treeMenu = item.Destination switch
            {
                MenuDestination.Directory => treeMenuDictionary,
                MenuDestination.Document => treeMenuDocument,
                _ => throw new NotImplementedException()
            };

            var added = CreateMenu(treeMenu, item, pageTypes);
        }
    }

    private TreeMenuItem CreateMenu(TreeMenuItem root, PageData pageData, IEnumerable<PageData> pages)
    {
        TreeMenuItem menu = CreateTreeMenuItem(root, pageData);
        root.Items.Add(menu);

        foreach (var item in pages.Where(m => m.Parent == pageData.Name).OrderBy(m => m.Order))
        {
            CreateMenu(menu, item, pages);
        }

        return menu;
    }

    private TreeMenuItem CreateTreeMenuItem(TreeMenuItem parent, PageData data)
    {
        TreeMenuItem item = new()
        {
            Text = data.Name,
            Tag = data.PageType,
            BackColor = parent.BackColor,
            ForeColor = parent.ForeColor,
            ItemBackColor = parent.ItemBackColor,
            ItemHoverColor = parent.ItemHoverColor,
            SelectedColor = parent.SelectedColor,
            SelectedItemForeColor = parent.SelectedItemForeColor
        };

        if (data.PageType != null)
        {
            item.Click += AddonItem_Click;
        }

        return item;
    }

#if USE_LISTENER
    private void Listener()
    {

        using var conn = new NpgsqlConnection(Database.ConnectionString);
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
            await Task.Run(() => Listener());
        }
        catch (Exception)
        {
        }
    }
#endif

    private void AddonItem_Click(object? sender, EventArgs e)
    {
        var pages = Services.Provider.GetService<IPageManager>();
        if (sender is TreeMenuItem menu && menu.Tag is Type type && pages != null)
        {
            pages.ShowBrowser(type);
        }
    }

    private void TreeMenuLogout_Click(object sender, EventArgs e)
    {
        CurrentApplicationContext.Context.ShowLoginForm();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        var settings = Properties.Settings.Default;

        settings.WindowState = WindowState;
        settings.Location = Location;
        settings.Size = Size;
        settings.PanelMenuWidth = splitContainer1.SplitterDistance;
        settings.Save();

        Services.Provider.GetService<IPageManager>()?.OnPageClosing();

#if USE_LISTENER
        if (settings.UseDataNotification)
        {
            timerDatabaseListen.Stop();
            timerCheckListener.Stop();
        }
#endif
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        var settings = Properties.Settings.Default;

        if (settings.Size.Width == 0 || settings.Size.Height == 0)
            settings.Upgrade();

        if (settings.Size.Width == 0 || settings.Size.Height == 0)
        {
            StartPosition = FormStartPosition.WindowsDefaultLocation;
            WindowState = FormWindowState.Normal;
        }
        else
        {
            WindowState = settings.WindowState;
            if (settings.WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }

            if (settings.WindowState == FormWindowState.Normal)
            {
                Location = settings.Location;
                Size = settings.Size;
            }
        }

        splitContainer1.SplitterDistance = settings.PanelMenuWidth;

        Text = $"DocumentFlow {Assembly.GetExecutingAssembly().GetName().Version} - <{Database.ConnectionName}>";

#if USE_LISTENER
        if (settings.UseDataNotification)
        {
            timerCheckListener.Start();
            timerDatabaseListen.Start();
        }
#endif
    }

    private void TimerDatabaseListen_Tick(object sender, EventArgs e)
    {
#if USE_LISTENER
        if (Properties.Settings.Default.UseDataNotification && notifies.TryDequeue(out NotifyMessage? message))
        {
            SendNotify(message.Destination, message.EntityName, message.ObjectId, message.Action);
        }
#endif
    }

    private void TimerCheckListener_Tick(object sender, EventArgs e)
    {
#if USE_LISTENER
        if (listenerTask != null && listenerTask.Status != TaskStatus.WaitingForActivation)
        {
            listenerTask = CreateListener();
        }
#endif
    }

    private void TabControlAdv1_SelectedIndexChanging(object sender, SelectedIndexChangingEventArgs args)
    {
        if (args.NewSelectedIndex != -1)
        {
            Services.Provider.GetService<IPageManager>()?.AddToHistory(tabControlAdv1.TabPages[args.NewSelectedIndex]);
        }
    }

    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F5 && tabControlAdv1.SelectedTab != null)
        {
            var manager = Services.Provider.GetService<IPageManager>();
            if (manager != null)
            {
                var page = manager.Get(tabControlAdv1.SelectedTab);
                page?.RefreshPage();
            }
        }
    }

    private void TreeMenuAbout_Click(object sender, EventArgs e)
    {
        AboutForm.ShowWindow();
    }
}