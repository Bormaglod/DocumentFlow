//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.03.2020
// Time: 13:00
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NTwain;
using NTwain.Data;
using WeifenLuo.WinFormsUI.Docking;
using DocumentFlow.Authorization;
using DocumentFlow.Core;
using DocumentFlow.Data;
using DocumentFlow.Interfaces;
using DocumentFlow.Properties;

namespace DocumentFlow
{
    public partial class MainForm : Form, IContainerPage
    {
        private readonly LoginForm loginForm;
        private readonly ICommandFactory commandFactory;
        private Twain twain;

        public MainForm()
        {
            InitializeComponent();

            commandFactory = new CommandFactory(this);
            var catalogWindow = new CatalogDockControl(commandFactory);
            catalogWindow.ShowPanel(dockPanel1, DockState.DockLeft);

            _ = CreateCompilerTask();

            Text += $" - {Db.ConnectionName}";
        }

        public MainForm(LoginForm loginForm) : this() => this.loginForm = loginForm;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            twain = new(Handle);
        }

        private async Task CreateCompilerTask()
        {
            foreach (var command in commandFactory.Commands)
            {
                try
                {
                    statusBarAdv1.Panels[0].Text = $"Compile: {command.name}";
                    await Task.Run(() => command.Compile());
                }
                catch (Exception)
                {
                }
            }

            statusBarAdv1.Panels[0].Text = string.Empty;
        }

        #region IContainerPage implementation

        ITwain IContainerPage.Twain => twain;

        IPage IContainerPage.Selected
        {
            get => dockPanel1.ActiveContent as IPage;
            set
            {
                if (value is DockContent content)
                {
                    content.ShowPanel(dockPanel1, DockState.Document);
                }
            }
        }

        void IContainerPage.Add(IPage page)
        {
            if (page is DockContent content)
            {
                content.ShowPanel(dockPanel1, DockState.Document);
            }
        }

        IPage IContainerPage.Get(string code)
        {
            return dockPanel1.Contents
                .OfType<IPage>()
                .FirstOrDefault(page => page.Code == code);
        }

        IEnumerable<IContentPage> IContainerPage.GetContentPages()
        {
            return dockPanel1.Contents
                .OfType<IContentPage>();
        }

        void IContainerPage.Logout()
        {
            Hide();
            loginForm.Show();
        }

        void IContainerPage.About() => AboutForm.ShowWindow();

        #endregion

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (loginForm != null)
            {
                loginForm.Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (twain != null)
            {
                if (e.CloseReason == CloseReason.UserClosing && twain.State > 4)
                {
                    e.Cancel = true;
                }
                else
                {
                    twain.Cleanup();
                }
            }

            /*var catalog = dockingManager1.ControlsArray.OfType<CatalogDockControl>().FirstOrDefault();
            if (catalog != null)
            {
                Settings.Default.PanelMenuWidth = catalog.Width;
            }*/

            Settings.Default.WindowState = WindowState;
            Settings.Default.Location = Location;
            Settings.Default.Size = Size;
            Settings.Default.Save();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.Size.Width == 0 || Settings.Default.Size.Height == 0)
                Settings.Default.Upgrade();

            if (Settings.Default.Size.Width == 0 || Settings.Default.Size.Height == 0)
            {
                StartPosition = FormStartPosition.WindowsDefaultLocation;
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = Settings.Default.WindowState;
                if (Settings.Default.WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Normal;
                }

                if (Settings.Default.WindowState == FormWindowState.Normal)
                {
                    Location = Settings.Default.Location;
                    Size = Settings.Default.Size;
                }
            }

            /*var catalog = dockingManager1.ControlsArray.OfType<CatalogDockControl>().FirstOrDefault();
            if (catalog != null)
            {
                catalog.Width = Settings.Default.PanelMenuWidth;
            }*/
        }
    }
}
