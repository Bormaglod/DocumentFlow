//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.06.2018
// Time: 22:51
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DocumentFlow.Controls
{
    public enum HistoryOperation { Add, Remove }

    public partial class Breadcrumb : UserControl
    {
        private readonly Stack<(Guid id, SplitButton button)> dirs = new();

        public Breadcrumb()
        {
            InitializeComponent();
        }

        public event EventHandler<CrumbClickEventArgs> CrumbClick;

        [Browsable(false)]
        public int Count => dirs.Count;

        [Category("Внешний вид")]
        [DefaultValue(true)]
        public bool ShowButtonRefresh
        {
            get => buttonRefresh.Visible;
            set => buttonRefresh.Visible = value;
        }

        public void Push(Guid id, string name)
        {
            SplitButton crumb = new()
            {
                Text = name,
                BorderColor = Color.White,
                Dock = DockStyle.Left,
                Identifier = id
            };

            panelCrumbs.Controls.Add(crumb);
            crumb.Click += Crumb_Click;
            crumb.BringToFront();

            dirs.Push((id, crumb));

            UpdateButtons();
        }

        public Guid Peek()
        {
            if (Count == 0)
                return Guid.Empty;
            return dirs.Peek().id;
        }

        public Guid Pop()
        {
            Guid directory = dirs.Pop().id;
            SplitButton button = panelCrumbs.Controls.Cast<SplitButton>().FirstOrDefault(c => c.Identifier == directory);
            if (button != null)
            {
                panelCrumbs.Controls.Remove(button);
            }

            UpdateButtons();
            return directory;
        }

        public void Clear()
        {
            while (dirs.Count > 0)
            {
                SplitButton button = dirs.Pop().button;
                panelCrumbs.Controls.Remove(button);
            }

            UpdateButtons();
        }

        private void UpdateButtons() => buttonUp.Enabled = Count > 0;

        private void Crumb_Click(object sender, EventArgs e)
        {
            Guid id = ((SplitButton)sender).Identifier;
            Guid d;
            do
            {
                d = Peek();
                if (d != Guid.Empty)
                {
                    if (d == id)
                        break;

                    Pop();
                }
            } while (d != Guid.Empty);

            if (CrumbClick != null)
            {
                CrumbClick.Invoke(this, new CrumbClickEventArgs(ToolButtonKind.Refresh));
                UpdateButtons();
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (CrumbClick != null)
            {
                CrumbClick.Invoke(this, new CrumbClickEventArgs(ToolButtonKind.Up));
                UpdateButtons();
            }
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            if (CrumbClick != null)
            {
                CrumbClick.Invoke(this, new CrumbClickEventArgs(ToolButtonKind.Home));
                UpdateButtons();
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            if (CrumbClick != null)
            {
                CrumbClick.Invoke(this, new CrumbClickEventArgs(ToolButtonKind.Refresh));
                UpdateButtons();
            }
        }
    }
}
