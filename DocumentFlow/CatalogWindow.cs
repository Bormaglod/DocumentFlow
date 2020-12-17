//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2020
// Time: 09:26
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Dapper;
using Syncfusion.Windows.Forms.Tools;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;

namespace DocumentFlow
{
    public partial class CatalogWindow : ToolWindow
    {
        private readonly ICommandFactory сommandFactory;

        public CatalogWindow(ICommandFactory сommandFactory)
        {
            InitializeComponent();

            this.сommandFactory = сommandFactory;

            using (var conn = Db.OpenConnection())
            {
                var pic_default = conn.Query<Picture>("select * from picture where code = :code", new { code = "file" }).SingleOrDefault();

                if (pic_default != null)
                {
                    imageMenu.Images.Add("default-icon", pic_default.GetImageSmall());
                }

                var catalog = conn.Query<Catalog>("select * from menu");
                foreach (var catalogItem in catalog)
                {
                    catalogItem.Command = сommandFactory.Commands.FirstOrDefault(x => x.Id == catalogItem.command_id);
                }

                RefreshSidebar(catalog);

                treeSidebar.Nodes.AddRange(new TreeNodeAdv[] {
                    new TreeNodeAdv("О программе")
                    {
                        LeftImageIndices = new int[] { 1 },
                        Tag = "about"
                    },
                    new TreeNodeAdv("Заблокировать")
                    {
                        LeftImageIndices = new int[] { 0 },
                        Tag = "logout"
                    }
                });

                treeSidebar.ExpandAll();
                treeSidebar.SelectedNode = treeSidebar.Nodes[0];
            }
        }

        private void RefreshSidebar(IEnumerable<Catalog> catalog, TreeNodeAdv parentNode = null, Guid? parent = null)
        {
            foreach (var item in catalog.Where(x => x.parent_id == parent).OrderBy(x => x.order_index).ThenBy(x => x.name))
            {
                var node = new TreeNodeAdv(item.name)
                {
                    Tag = item
                };

                if (item.Command?.Picture != null)
                {
                    Image image = imageMenu.Images[item.code];
                    if (image == null)
                    {
                        imageMenu.Images.Add(item.code, item.Command.Picture.GetImageSmall());
                    }

                    node.LeftImageIndices = new int[] { imageMenu.Images.IndexOfKey(item.code) };
                }
                else
                    node.LeftImageIndices = new int[] { imageMenu.Images.IndexOfKey("default-icon") };

                if (parentNode == null)
                    treeSidebar.Nodes.Add(node);
                else
                    parentNode.Nodes.Add(node);

                RefreshSidebar(catalog, node, item.Id);
            }
        }

        private void treeSidebar_NodeMouseDoubleClick(object sender, TreeViewAdvMouseClickEventArgs e)
        {
            switch (e.Node.Tag)
            {
                case Catalog menu:
                    сommandFactory.Execute(menu.Command);
                    break;
                case string command:
                    сommandFactory.Execute(command);
                    break;
            }
        }
    }
}
