//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.03.2020
// Time: 13:00
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using WinForms = System.Windows.Forms;
    using Syncfusion.Windows.Forms;
    using Syncfusion.Windows.Forms.Tools;
    using DocumentFlow.Authorization;
    using DocumentFlow.Properties;
    using DocumentFlow.Data.Core;
    using DocumentFlow.Data.Entities;

    public partial class DocumentFlowForm : MetroForm, IContainerPage
    {
        private LoginForm loginForm;
        private ICommandFactory commands;

        public DocumentFlowForm()
        {
            InitializeComponent();
            commands = new CommandFactory(this);
        }

        IPage IContainerPage.Selected
        {
            get => GetPage(tabControls.SelectedTab);
            set
            {
                IPage page = ((IContainerPage)this).Get(value.Id);
                if (page != null)
                    tabControls.SelectedTab = (TabPageAdv)((WinForms.Control)page).Parent;
            }
        }

        void IContainerPage.Add(WinForms.Control control)
        {
            control.Dock = WinForms.DockStyle.Fill;
            
            TabPageAdv page = new TabPageAdv(control.Text);
            page.Controls.Add(control);
            
            tabControls.TabPages.Add(page);
            tabControls.SelectedTab = page;
        }

        bool IContainerPage.Contains(Guid id) => ((IContainerPage)this).Get(id) != null;

        IPage IContainerPage.Get(Guid id)
        {
            foreach (TabPageAdv p in tabControls.TabPages)
            {
                IPage page = GetPage(p);
                if ((page != null) && (page.Id == id))
                {
                    return page;
                }
            }

            return null;
        }

        public DocumentFlowForm(LoginForm loginForm) : this()
        {
            this.loginForm = loginForm;

            using (var session = Db.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    Picture pic_default = session.QueryOver<Picture>().Where(x => x.Code == "file").SingleOrDefault();
                    if (pic_default != null)
                    {
                        imageMenu.Images.Add("default-icon", pic_default.GetImageSmall());
                    }

                    RefreshSidebar(session.QueryOver<Menu>().List());

                    treeSidebar.Nodes.Add(
                        new TreeNodeAdv("Заблокировать")
                        {
                            LeftImageIndices = new int[] { 0 },
                            Tag = "logout"
                        });
                    treeSidebar.ExpandAll();
                }
            }
        }

        private IPage GetPage(TabPageAdv page)
        {
            if (page.Controls.Count == 0)
                return null;

            return page.Controls[0] as IPage;
        }

        private void RefreshSidebar(IList<Menu> menu, TreeNodeAdv parentNode = null, Guid? parent = null)
        {
            foreach (Menu item in menu.Where(x => x.ParentId == parent).OrderBy(x => x.OrderIndex).ThenBy(x => x.Name))
            {
                var node = new TreeNodeAdv(item.Name);
                node.Tag = item;

                if (item.Command?.Picture != null)
                {
                    Image image = imageMenu.Images[item.Code];
                    if (image == null)
                    {
                        imageMenu.Images.Add(item.Code, item.Command.Picture.GetImageSmall());
                    }

                    node.LeftImageIndices = new int[] { imageMenu.Images.IndexOfKey(item.Code) };
                }
                else
                    node.LeftImageIndices = new int[] { imageMenu.Images.IndexOfKey("default-icon") };

                if (parentNode == null)
                    treeSidebar.Nodes.Add(node);
                else
                    parentNode.Nodes.Add(node);

                RefreshSidebar(menu, node, item.Id);
            }
        }

        private void DocumentFlowForm_FormClosed(object sender, WinForms.FormClosedEventArgs e)
        {
            if (loginForm != null)
            {
                loginForm.Close();
            }
        }

        private void DocumentFlowForm_FormClosing(object sender, WinForms.FormClosingEventArgs e)
        {
            Settings.Default.WindowState = WindowState;
            Settings.Default.Save();
        }

        private void treeSidebar_NodeMouseDoubleClick(object sender, TreeViewAdvMouseClickEventArgs e)
        {
            switch (e.Node.Tag)
            {
                case Menu menu:
                    commands.Execute(menu.Command);
                    break;
                case string command:
                    switch (command)
                    {
                        case "logout":
                            Hide();
                            loginForm.Show();
                            break;
                        default:
                            break;
                    }

                    break;
            }
        }

        private void tabControls_ControlRemoved(object sender, WinForms.ControlEventArgs e)
        {
            ISettings viewer = e.Control.Controls.OfType<ISettings>().SingleOrDefault();
            if (viewer != null)
            {
                viewer.SaveSettings();
            }

            IPage page = e.Control.Controls.OfType<IPage>().SingleOrDefault();
            if (page != null)
            {
                page.OnClosed();
            }
        }
    }
}
