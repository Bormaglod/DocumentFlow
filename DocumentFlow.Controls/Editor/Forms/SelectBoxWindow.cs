//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.04.2019
// Time: 18:35
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Tools.MultiColumnTreeView;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Controls.Editor.Forms
{
    public partial class SelectBoxWindow : Form
    {
        private readonly bool showOnlyFolder;

        public SelectBoxWindow(bool showOnlyFolder)
        {
            InitializeComponent();
            this.showOnlyFolder = showOnlyFolder;
        }

        public IIdentifier SelectedItem
        {
            get => (IIdentifier)treeSelect.SelectedNode?.Tag;
            set
            {
                treeSelect.SelectedNode = GetNode(treeSelect.Nodes, value);
                
            }
        }

        public void AddItems(IList<IIdentifier> items)
        {
            object obj = items.FirstOrDefault();
            if (obj == null)
            {
                return;
            }

            Dictionary<PropertyInfo, string> columns = new Dictionary<PropertyInfo, string>();
            Type type = obj.GetType();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                ColumnDescriptionAttribute attr = prop.GetCustomAttribute<ColumnDescriptionAttribute>();
                if (attr == null)
                    continue;

                columns.Add(prop, attr.Title);
            }

            if (columns.Count > 1)
            {
                foreach (PropertyInfo prop in columns.Keys)
                {
                    TreeColumnAdv column = new TreeColumnAdv()
                    {
                        Text = columns[prop],
                        Highlighted = false,
                        Background = new BrushInfo(SystemColors.Highlight),
                        Tag = prop
                    };

                    treeSelect.Columns.Add(column);
                }
            }

            treeSelect.ShowColumnsHeader = columns.Count > 1;

            AddItems(items, null, null);
            treeSelect.ExpandAll();
            treeSelect.Select();
        }

        private void AddItems(IList<IIdentifier> items, TreeNodeAdv node, IIdentifier<Guid> parent)
        {
            foreach (var item in items.OfType<IParent>().Where(x => x.parent_id == parent?.id))
            {
                if (showOnlyFolder && !item.is_folder)
                {
                    continue;
                }

                if (item is IIdentifier<Guid> id_item)
                {
                    TreeNodeAdv new_node = AddItem(node, item.is_folder, id_item);
                    AddItems(items, new_node, id_item);
                }
            }
        }

        private TreeNodeAdv AddItem(TreeNodeAdv node, bool isFolder, IIdentifier<Guid> data)
        {
            TreeNodeAdv n = new TreeNodeAdv
            {
                Text = data.ToString(),
                Tag = data,
                LeftImageIndices = new int[] { isFolder ? 0 : 1 }
            };

            if (treeSelect.Columns.Count > 1)
            {
                foreach (TreeColumnAdv item in treeSelect.Columns)
                {
                    PropertyInfo prop = (PropertyInfo)item.Tag;
                    string subItemText = prop.GetValue(data).ToString();

                    TreeNodeAdvSubItem subItem = new TreeNodeAdvSubItem(subItemText);
                    n.SubItems.Add(subItem);
                }
            }

            if (node == null)
                treeSelect.Nodes.Add(n);
            else
                node.Nodes.Add(n);

            return n;
        }

        private TreeNodeAdv GetNode(TreeNodeAdvCollection nodes, object data)
        {
            foreach (TreeNodeAdv node in nodes)
            {
                if (node.Tag == data)
                    return node;

                TreeNodeAdv childNode = GetNode(node.Nodes, data);
                if (childNode != null)
                    return childNode;
            }

            return null;
        }

        private void TreeSelect_NodeMouseDoubleClick(object sender, MultiColumnTreeViewAdvMouseClickEventArgs e)
        {
            if (showOnlyFolder || e.Node.LeftImageIndices[0] == 1)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
