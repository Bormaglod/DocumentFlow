//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.07.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Dialogs;

public partial class SettingsForm : Form
{
    private ISettingsPage? current;

    public SettingsForm(IEnumerable<ISettingsPage> settingsPages)
    {
        InitializeComponent();

        panelHeader.Visible = false;

        foreach (var page in settingsPages)
        {
            TreeNodeAdv? node = null;
            var nodes = treeSettings.Nodes;

            foreach (var p in page.Path.Split('/', StringSplitOptions.RemoveEmptyEntries))
            {
                node = nodes.OfType<TreeNodeAdv>().FirstOrDefault(x => x.Text == p);
                if (node == null)
                {
                    node = new(p);
                    nodes.Add(node);
                }

                nodes = node.Nodes;
            }

            if (node != null)
            {
                page.Control.Visible = false;
                page.Control.Dock = DockStyle.Fill;

                splitContainer1.Panel2.Controls.Add(page.Control);

                node.Tag = page;
            }
        }

        if (treeSettings.Nodes.Count == 0)
        {
            panelHeader.Visible = false;
        }
        else
        {
            treeSettings.ExpandAll();
            treeSettings.SelectedItem = treeSettings.Nodes[0];
        }
    }

    public ISettingsPage? Current
    {
        get => current;
        set
        {
            if (current != null)
            {
                current.Control.Visible = false;
            }

            current = value;

            if (current != null)
            {
                current.Control.Visible = true;
            }
        }
    }

    private void TreeSettings_AfterSelect(object sender, EventArgs e)
    {
        if (treeSettings.SelectedNode != null)
        {
            labelHeader.Text = treeSettings.SelectedNode.Text;
            panelHeader.Visible = true;

            Current = treeSettings.SelectedNode.Tag != null ? (ISettingsPage)treeSettings.SelectedNode.Tag : null;
        }
        else
        {
            panelHeader.Visible = false;

            Current = null;
        }
    }
}
