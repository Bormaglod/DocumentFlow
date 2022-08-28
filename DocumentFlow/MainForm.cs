//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.12.2021
//
// Версия 2022.8.28
//  - добавлена реакция на нажатие клавиши F5
//
//-----------------------------------------------------------------------

using DocumentFlow.Core;
using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Npgsql;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Windows.Forms.Tools;

using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocumentFlow
{
    public partial class MainForm : Form, IHostApp, ITabPages
    {
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

        private void CreateMenu()
        {
            Type browserType = typeof(IBrowser<>);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(p => p.IsInterface)
                .Where(p => p.GetInterfaces().Any(i => i.IsGenericType && browserType == i.GetGenericTypeDefinition()));

            Dictionary<MenuAttribute, Type> browsers = new();
            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<MenuAttribute>();
                if (attr != null)
                {
                    browsers.Add(attr, type);
                }
            }

            Dictionary<string, TreeMenuItem> menus = new();
            foreach (var item in browsers.Keys.OfType<MenuAttribute>().OrderBy(m => m.Order))
            {
                TreeMenuItem treeMenu = item.Destination switch
                {
                    MenuDestination.Directory => treeMenuDictionary,
                    MenuDestination.Document => treeMenuDocument,
                    _ => throw new NotImplementedException()
                };

                if (!string.IsNullOrEmpty(item.Path))
                {
                    if (menus.ContainsKey(item.Path))
                    {
                        treeMenu = menus[item.Path];
                    }
                    else
                    {
                        treeMenu = AddMenu(treeMenu, item.Path);
                        menus.Add(item.Path, treeMenu);
                    }
                }

                AddMenu(treeMenu, item.Name, browsers[item]);
            }
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

        private TreeMenuItem AddMenu(TreeMenuItem parent, string text, Type? browserType = null)
        {
            TreeMenuItem item = new()
            {
                Text = text,
                Tag = browserType,
                BackColor = parent.BackColor,
                ForeColor = parent.ForeColor,
                ItemBackColor = parent.ItemBackColor,
                ItemHoverColor = parent.ItemHoverColor,
                SelectedColor = parent.SelectedColor,
                SelectedItemForeColor = parent.SelectedItemForeColor
            };

            if (browserType != null)
            {
                item.Click += AddonItem_Click;
            }

            parent.Items.Add(item);

            return item;
        }

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
    }
}