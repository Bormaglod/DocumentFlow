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
    using DocumentFlow.Core;
    using DocumentFlow.Properties;
    using DocumentFlow.Data.Core;
    using DocumentFlow.Data.Entities;

    public partial class DocumentFlowForm : MetroForm, IContainerPage
    {
        private LoginForm loginForm;
        private ICommandFactory commands;
        private Stack<TabPageAdv> historyPages = new Stack<TabPageAdv>();
        readonly private Dictionary<string, int> buttonPos = new Dictionary<string, int>() { { "close", 28 }, { "max", 54 }, { "min", 80 }, { "system", 106 } };

        public DocumentFlowForm()
        {
            InitializeComponent();
            commands = new CommandFactory(this);

            foreach (CaptionImage image in CaptionImages)
            {
                if (image.Name == "icon")
                {
                    image.ImageMouseDown += new CaptionImage.MouseDown(icon_ImageMouseDown);
                }
                else
                {
                    image.Location = new Point(Width - buttonPos[image.Name], 4);
                    image.ImageMouseEnter += new CaptionImage.MouseEnter(image_ImageMouseEnter);
                    image.ImageMouseLeave += new CaptionImage.MouseLeave(image_ImageMouseLeave);
                    image.ImageMouseUp += new CaptionImage.MouseUp(image_ImageMouseUp);
                }
            }
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
            page.Closed += Page_Closed;
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

        protected override void OnHandleCreated(EventArgs e)
        {
            uint dwWindowProperty;

            User32.SetParent(Handle, IntPtr.Zero);

            dwWindowProperty = User32.GetWindowLong(Handle, User32.GWL.STYLE);
            dwWindowProperty = (dwWindowProperty | (uint)User32.WS.CAPTION | (uint)User32.WS.SYSMENU);
            User32.SetWindowLong(Handle, User32.GWL.STYLE, dwWindowProperty);

            base.OnHandleCreated(e);
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
            Settings.Default.Location = Location;
            Settings.Default.Size = Size;
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

        private void Page_Closed(object sender, EventArgs e)
        {
            if (sender is TabPageAdv page)
            {
                ISettings viewer = page.Controls.OfType<ISettings>().SingleOrDefault();
                if (viewer != null)
                {
                    viewer.SaveSettings();
                }

                IPage p = page.Controls.OfType<IPage>().SingleOrDefault();
                if (p != null)
                {
                    p.OnClosed();
                }
            }

            if (historyPages.Count > 0)
            {
                tabControls.SelectedTab = historyPages.Pop();
            }
        }

        private void tabControls_MouseClick(object sender, WinForms.MouseEventArgs e)
        {
            if (e.Button == WinForms.MouseButtons.Right)
            {
                if (tabControls.HitTestTabs(e.Location) != -1)
                {
                    contextTabsMenu.Show(tabControls, e.Location);
                }
            }
        }

        private void menuItemCloseCurrent_Click(object sender, EventArgs e)
        {
            tabControls.SelectedTab.Close();
        }

        private void menuItemCloseAll_Click(object sender, EventArgs e)
        {
            while (tabControls.TabPages.Count > 0)
            {
                tabControls.TabPages[0].Close();
            }
        }

        private void menuItemCloseAllExcept_Click(object sender, EventArgs e)
        {
            TabPageAdv[] pages = tabControls.TabPages.OfType<TabPageAdv>().Where(x => x != tabControls.SelectedTab).ToArray();
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].Close();
            }
        }

        private void tabControls_SelectedIndexChanging(object sender, SelectedIndexChangingEventArgs args)
        {
            historyPages.Push(tabControls.SelectedTab);
        }

        void image_ImageMouseEnter(object sender, ImageMouseEnterEventArgs e)
        {
            if (sender is CaptionImage image)
            {
                switch (image.Name)
                {
                    case "close":
                        image.BackColor = Color.Red;
                        break;
                    case "max":
                        image.BackColor = Color.LightBlue;
                        break;
                    case "min":
                        image.BackColor = Color.LightBlue;
                        break;
                    case "system":
                        image.BackColor = Color.LightBlue;
                        break;
                    default:
                        break;
                }
            }
        }

        void image_ImageMouseLeave(object sender, ImageMouseLeaveEventArgs e)
        {
            if (sender is CaptionImage image)
            {
                image.BackColor = Color.Transparent;
            }
        }

        void image_ImageMouseUp(object sender, ImageMouseUpEventArgs e)
        {
            if (sender is CaptionImage image)
            {
                switch (image.Name)
                {
                    case "close":
                        Close();
                        break;
                    case "max":
                        if (WindowState == WinForms.FormWindowState.Normal)
                        {
                            WindowState = WinForms.FormWindowState.Maximized;
                            image.Image = Resources.system_restore;
                        }
                        else
                        {
                            WindowState = WinForms.FormWindowState.Normal;
                            image.Image = Resources.system_max;
                        }

                        break;
                    case "min":
                        WindowState = WinForms.FormWindowState.Minimized;
                        break;
                    case "system":
                        Point p = PointToScreen(e.Location);
                        p.Offset(-6, -4);
                        contextSystemMenu.Show(p);
                        break;
                    default:
                        break;
                }
            }
        }

        void icon_ImageMouseDown(object sender, ImageMouseDownEventArgs e)
        {
            IntPtr wMenu = User32.GetSystemMenu(Handle, false);
            if (WindowState == WinForms.FormWindowState.Maximized)
            {
                User32.EnableMenuItem(wMenu, (uint)User32.SC.MAXIMIZE, (uint)User32.MF.GRAYED);
            }
            else
            {
                User32.EnableMenuItem(wMenu, (uint)User32.SC.MAXIMIZE, (uint)User32.MF.ENABLED);
            }

            Point m = ((WinForms.MouseEventArgs)e).Location;
            m.Offset(Location);

            int command = User32.TrackPopupMenuEx(wMenu, (uint)(User32.TPM.LEFTALIGN | User32.TPM.RETURNCMD), m.X, m.Y, Handle, IntPtr.Zero);
            if (command == 0)
                return;

            User32.PostMessage(Handle, (uint)User32.WM.SYSCOMMAND, new IntPtr(command), IntPtr.Zero);
        }

        private void DocumentFlowForm_SizeChanged(object sender, EventArgs e)
        {
            foreach (CaptionImage image in CaptionImages)
            {
                if (!buttonPos.ContainsKey(image.Name))
                    continue;

                if (image.Name == "max")
                {
                    image.Image = WindowState == WinForms.FormWindowState.Maximized ? Resources.system_restore : Resources.system_max;
                }

                image.Location = new Point(Width - buttonPos[image.Name], 4);
            }
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void DocumentFlowForm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.Size.Width == 0 || Settings.Default.Size.Height == 0)
                Settings.Default.Upgrade();
            
            if (Settings.Default.Size.Width == 0 || Settings.Default.Size.Height == 0)
            {
                StartPosition = WinForms.FormStartPosition.WindowsDefaultLocation;
                WindowState = WinForms.FormWindowState.Normal;
            }
            else
            {
                WindowState = Settings.Default.WindowState;
                if (Settings.Default.WindowState == WinForms.FormWindowState.Minimized)
                {
                    WindowState = WinForms.FormWindowState.Normal;
                }

                if (Settings.Default.WindowState == WinForms.FormWindowState.Normal)
                {
                    Location = Settings.Default.Location;
                    Size = Settings.Default.Size;
                }
            }
        }
    }
}
