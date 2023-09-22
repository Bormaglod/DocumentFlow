//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.04.2019
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Properties;

using Syncfusion.Windows.Forms.Tools.MultiColumnTreeView;

using System.Reflection;

namespace DocumentFlow.Dialogs;

public partial class DirectoryDialog : Form
{
    private string nameColumn = string.Empty;

    public DirectoryDialog()
    {
        InitializeComponent();

        ImageList imageList = new();
        imageList.Images.Add(Resources.icons8_folder_16);
        imageList.Images.Add(Resources.record);

        treeSelect.LeftImageList = imageList;
    }

    public IDirectory? SelectedDirectoryItem
    {
        get => (IDirectory?)treeSelect.SelectedNode?.Tag;
    }

    public Guid? SelectedItem
    {
        get => SelectedDirectoryItem?.Id;
        set => SetSelectedNode(value == null ? null : GetNode(treeSelect.Nodes, value.Value));
    }

    public bool RemoveEmptyFolders { get; set; }

    public bool CanSelectFolder { get; set; }

    public Guid? Root { get; set; }

    public void SetDataSource(IEnumerable<IDirectory> items)
    {
        treeSelect.BeginUpdate();
        try
        {
            AddItems(items, null, Root);
            if (RemoveEmptyFolders)
            {
                List<TreeNodeAdv> folders = new();
                FillEmptyFolders(treeSelect.Nodes, folders);
                foreach (var item in folders)
                {
                    item.Remove();
                }
            }
        }
        finally
        {
            treeSelect.EndUpdate();
        }

        treeSelect.Select();
        treeSelect.VScrollPos = 0;
    }

    private void SetSelectedNode(TreeNodeAdv? node)
    {
        treeSelect.SelectedNode = node;
        if (node != null)
        {
            while (node.Parent != null && !node.Parent.Expanded)
            {
                node = node.Parent;
                node.Expand();
            }

            treeSelect.EnsureVisibleV(treeSelect.SelectedNode, false);
        }
    }

    public void SetColumns(string nameColumn, IReadOnlyDictionary<PropertyInfo, string> columns)
    {
        this.nameColumn = nameColumn;

        foreach (var item in columns.Keys)
        {
            TreeColumnAdv column = new()
            {
                Text = columns[item],
                Tag = item
            };

            treeSelect.Columns.Add(column);
        }

        treeSelect.AutoSizeMode = Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.AutoSizeMode.LastColumnFill;
    }

    private int FillEmptyFolders(TreeNodeAdvCollection nodes, IList<TreeNodeAdv> folders)
    {
        int count = 0;
        foreach (var item in nodes)
        {
            if (item is TreeNodeAdv node && node.Tag is IDirectory d)
            {
                if (d.IsFolder)
                {
                    int n = FillEmptyFolders(node.Nodes, folders);
                    if (n == 0)
                    {
                        folders.Add(node);
                    }
                    else
                    {
                        count += n;
                    }
                }
                else
                {
                    count++;
                }
            }
        }

        return count;
    }

    private void AddItems(IEnumerable<IDirectory> items, TreeNodeAdv? node, Guid? parent)
    {
        foreach (var item in items.Where(x => x.ParentId == parent))
        {
            TreeNodeAdv new_node = AddItem(node, item.IsFolder, item);
            AddItems(items, new_node, item.Id);
        }
    }

    private static string GetPropertyValue(IDirectory data, TreeColumnAdv column)
    {
        if (column.Tag is PropertyInfo prop)
        {
            return prop.Name switch
            {
                nameof(data.Code) => data.Code,
                nameof(data.ItemName) => data.ItemName ?? string.Empty,
                _ => prop.GetValue(data)?.ToString() ?? string.Empty
            };
        }
        else
        {
            return string.Empty;
        }
    }

    private TreeNodeAdv AddItem(TreeNodeAdv? node, bool isFolder, IDirectory data)
    {
        TreeNodeAdv n = new()
        {
            Text = data.ToString(),
            Tag = data,
            LeftImageIndices = new int[] { isFolder ? 0 : 1 }
        };

        if (!data.IsFolder && treeSelect.Columns.Count > 0)
        {
            foreach (TreeColumnAdv item in treeSelect.Columns)
            {
                if (item.Tag is not PropertyInfo prop)
                {
                    continue;
                }

                string text = GetPropertyValue(data, item);

                if (prop.Name == nameColumn)
                {
                    n.Text = text;
                }
                else
                {
                    TreeNodeAdvSubItem subItem = new(text);
                    n.SubItems.Add(subItem);
                }
            }
        }

        if (node == null)
        {
            treeSelect.Nodes.Add(n);
        }
        else
        {
            node.Nodes.Add(n);
        }

        return n;
    }

    private TreeNodeAdv? GetNode(TreeNodeAdvCollection nodes, Guid id)
    {
        foreach (TreeNodeAdv node in nodes)
        {
            if (node.Tag is IDirectory t && t.Id == id)
            {
                return node;
            }

            TreeNodeAdv? childNode = GetNode(node.Nodes, id);
            if (childNode != null)
            {
                return childNode;
            }
        }

        return null;
    }

    private bool FilterNodes(object node)
    {
        if (node is TreeNodeAdv nodeAdv)
        {
            var res = nodeAdv.Text.Contains(textSearch.Text, StringComparison.InvariantCultureIgnoreCase);
            if (!res)
            {
                foreach (var item in nodeAdv.SubItems.OfType<TreeNodeAdvSubItem>())
                {
                    res = res || item.Text.Contains(textSearch.Text, StringComparison.InvariantCultureIgnoreCase);
                }
            }

            return res;
        }

        return false;
    }

    private void TreeSelect_NodeMouseDoubleClick(object sender, MultiColumnTreeViewAdvMouseClickEventArgs e)
    {
        if (CanSelectFolder || e.Node.LeftImageIndices[0] == 1)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void ButtonClearSearch_Click(object sender, EventArgs e) => textSearch.Text = string.Empty;

    private void TextSearch_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(textSearch.Text))
        {
            treeSelect.Filter = null;
            treeSelect.RefreshFilter();
            treeSelect.VScrollPos = 0;
        }
        else
        {
            treeSelect.Filter ??= FilterNodes;
            treeSelect.RefreshFilter();
        }
    }
}
