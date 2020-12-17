//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.03.2020
// Time: 13:00
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using DocumentFlow.Authorization;
using DocumentFlow.Code.Data;
using DocumentFlow.Data.Entities;
using DocumentFlow.Properties;

namespace DocumentFlow
{
    public partial class DocumentFlowForm : Form, IContainerPage
    {
        private readonly LoginForm loginForm;
        private readonly ICommandFactory commandFactory;

        public DocumentFlowForm()
        {
            InitializeComponent();

            commandFactory = new CommandFactory(this);
            var catalogWindow = new CatalogWindow(commandFactory);
            catalogWindow.ShowWindow(dockPanel1, DockState.DockLeft);

            _ = CreateCompilerTask();
        }

        async private Task CreateCompilerTask()
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

        public DocumentFlowForm(LoginForm loginForm) : this()
        {
            this.loginForm = loginForm;
        }

        #region IContainerPage implemented

        IPage IContainerPage.Selected
        {
            get => dockPanel1.ActiveContent as IPage;
            set
            {
                if (value is ToolWindow win)
                {
                    win.ShowWindow(dockPanel1, DockState.Document);
                }
            }
        }

        void IContainerPage.Add(IPage page)
        {
            if (page is ToolWindow win)
            {
                win.ShowWindow(dockPanel1, DockState.Document);
            }
        }

        bool IContainerPage.Contains<T>(Guid id) => ((IContainerPage)this).Get<T>(id) != null;

        IPage IContainerPage.Get<T>(Guid id)
        {
            return dockPanel1.Contents
                .OfType<IPage>()
                .FirstOrDefault(page => page.Id == id && page is T);
        }

        IEnumerable<IPage> IContainerPage.Get(Guid id)
        {
            return dockPanel1.Contents
                .OfType<IPage>()
                .Where(page => page.Id == id);
        }

        IEnumerable<T> IContainerPage.GetAll<T>() => dockPanel1.Contents.OfType<T>();

        void IContainerPage.Logout()
        {
            Hide();
            loginForm.Show();
        }

        void IContainerPage.About()
        {
            var form = new AboutForm();
            form.ShowDialog();
        }

        #endregion

        private void DocumentFlowForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (loginForm != null)
            {
                loginForm.Close();
            }
        }

        private void DocumentFlowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.WindowState = WindowState;
            Settings.Default.Location = Location;
            Settings.Default.Size = Size;
            //Settings.Default.PanelMenuWidth = panelMenu.Width;
            Settings.Default.Save();
        }

        private void DocumentFlowForm_Load(object sender, EventArgs e)
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
        }
    }
}
