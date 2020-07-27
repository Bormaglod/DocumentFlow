//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.04.2019
// Time: 18:35
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Forms
{
    using System.Windows.Forms;
    using Syncfusion.Windows.Forms;
    using Syncfusion.Windows.Forms.Tools;

    public partial class SelectBoxWindow : MetroForm
    {
        private readonly bool folderSelect;

        public SelectBoxWindow(bool folderSelect)
        {
            InitializeComponent();
            this.folderSelect = folderSelect;
        }

        public TreeNodeAdv SelectedNode { get => treeMaterials.SelectedNode; set => treeMaterials.SelectedNode = value; }

        public object SelectedItem
        {
            get { return treeMaterials.SelectedNode?.Tag; }
            set { treeMaterials.SelectedNode = GetNode(treeMaterials.Nodes, value); }
        }

        public void Clear() => treeMaterials.Nodes.Clear();

        public void ExpandAll() => treeMaterials.ExpandAll();

        public TreeNodeAdv AddItem(TreeNodeAdv node, bool IsFolder, object data)
        {
            TreeNodeAdv n = new TreeNodeAdv
            {
                Text = data.ToString(),
                Tag = data,
                LeftImageIndices = new int[] { IsFolder ? 0 : 1 }
            };

            if (node == null)
                treeMaterials.Nodes.Add(n);
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

        private void TreeMaterials_NodeMouseDoubleClick(object sender, TreeViewAdvMouseClickEventArgs e)
        {
            if (folderSelect || e.Node.LeftImageIndices[0] == 1)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
