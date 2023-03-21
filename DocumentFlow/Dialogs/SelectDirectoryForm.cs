//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.04.2019
//
// Версия 2022.8.19
//  - добавлен метод SetColumns
//  - исправлена процедура заполнения AddItem
// Версия 2022.8.21
//  - группы теперь отображаются по имени (без указания кода группы)
//  - добавлен метод GetPropertyValue
// Версия 2022.8.31
//  - добавлено поле nameColumn и работа с ним
//  - добавлен метод SetSelectedNode
//  - удалены вызовы ExpandAll
//  - фильтр теперь осуществляет поиск по столбцам
// Версия 2022.12.2
//  - в методе GetNode поиск элемента осуществлялся по указателю, заменен
//    на поиск по id
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Properties;

using Syncfusion.Windows.Forms.Tools.MultiColumnTreeView;

using System.Reflection;

namespace DocumentFlow.Dialogs;

public partial class SelectDirectoryForm<T> : Form
    where T : class, IDirectory
{
    private readonly bool showOnlyFolder;
    private readonly bool removeEmptyFolders;
    private readonly Guid? root;
    private string nameColumn = string.Empty;

    public SelectDirectoryForm(Guid? root, bool showOnlyFolder, bool removeEmptyFolders)
    {
        InitializeComponent();
        this.showOnlyFolder = showOnlyFolder;
        this.removeEmptyFolders = removeEmptyFolders;
        this.root = root;

        ImageList imageList = new();
        imageList.Images.Add(Resources.icons8_folder_16);
        imageList.Images.Add(Resources.record);

        treeSelect.LeftImageList = imageList;
    }

    public T? SelectedItem
    {
        get => (T?)treeSelect.SelectedNode?.Tag;
        set => SetSelectedNode(GetNode(treeSelect.Nodes, value));
    }

    public Guid? SelectedValue
    {
        get => SelectedItem?.Id;
        set => SetSelectedNode(value == null ? null : GetNode(treeSelect.Nodes, value.Value));
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

    public void SetColumns(string nameColumn, IReadOnlyDictionary<string, string> columns)
    {
        this.nameColumn = nameColumn;

        var type = typeof(T);
        foreach (var item in columns.Keys)
        {
            TreeColumnAdv column = new()
            {
                Text = columns[item],
                Tag = type.GetProperty(item)
            };

            treeSelect.Columns.Add(column);
        }

        treeSelect.AutoSizeMode = Syncfusion.Windows.Forms.Tools.MultiColumnTreeView.AutoSizeMode.LastColumnFill;
    }

    public void AddItems(IEnumerable<T> items)
    {
        T? obj = items.FirstOrDefault();
        if (obj == null)
        {
            return;
        }

        treeSelect.BeginUpdate();
        try
        {
            AddItems(items, null, root);
            if (removeEmptyFolders)
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

    private int FillEmptyFolders(TreeNodeAdvCollection nodes, IList<TreeNodeAdv> folders)
    {
        int count = 0;
        foreach (var item in nodes)
        {
            if (item is TreeNodeAdv node && node.Tag is IDirectory d)
            {
                if (d.is_folder)
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

    private void AddItems(IEnumerable<T> items, TreeNodeAdv? node, Guid? parent)
    {
        foreach (var item in items.Where(x => x.parent_id == parent))
        {
            if (showOnlyFolder && !item.is_folder)
            {
                continue;
            }

            TreeNodeAdv new_node = AddItem(node, item.is_folder, item);
            AddItems(items, new_node, item.Id);
        }
    }

    private static string GetPropertyValue(T data, TreeColumnAdv column)
    {
        if (column.Tag is PropertyInfo prop)
        {
            return prop.Name switch
            {
                "code" => data.code,
                "item_name" => data.item_name ?? string.Empty,
                _ => prop.GetValue(data)?.ToString() ?? string.Empty
            };
        }
        else
        {
            return string.Empty;
        }
    }

    private TreeNodeAdv AddItem(TreeNodeAdv? node, bool isFolder, T data)
    {
        TreeNodeAdv n = new()
        {
            Text = data.ToString(),
            Tag = data,
            LeftImageIndices = new int[] { isFolder ? 0 : 1 }
        };

        if (!data.is_folder && treeSelect.Columns.Count > 0)
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

    private TreeNodeAdv? GetNode(TreeNodeAdvCollection nodes, T? data)
    {
        foreach (TreeNodeAdv node in nodes)
        {
            if (node.Tag is IDirectory nodeDir && data != null && nodeDir.Id == data.Id)
            {
                return node;
            }

            TreeNodeAdv? childNode = GetNode(node.Nodes, data);
            if (childNode != null)
            {
                return childNode;
            }
        }

        return null;
    }

    private TreeNodeAdv? GetNode(TreeNodeAdvCollection nodes, Guid id)
    {
        foreach (TreeNodeAdv node in nodes)
        {
            if (node.Tag is T t && t.Id == id)
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
            var res = nodeAdv.Text.Contains(textBoxExt1.Text, StringComparison.InvariantCultureIgnoreCase);
            if (!res)
            {
                foreach (var item in nodeAdv.SubItems.OfType<TreeNodeAdvSubItem>())
                {
                    res = res || item.Text.Contains(textBoxExt1.Text, StringComparison.InvariantCultureIgnoreCase);
                }
            }

            return res;
        }

        return false;
    }

    private void TreeSelect_NodeMouseDoubleClick(object sender, MultiColumnTreeViewAdvMouseClickEventArgs e)
    {
        if (showOnlyFolder || e.Node.LeftImageIndices[0] == 1)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void TextBoxExt1_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(textBoxExt1.Text))
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

    private void ToolButton1_Click(object sender, EventArgs e) => textBoxExt1.Text = string.Empty;
}
