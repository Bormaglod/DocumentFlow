//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Tools;
using DocumentFlow.Interfaces;

using Syncfusion.Windows.Forms.Tools;

using System.Reflection;

namespace DocumentFlow.Controls;

public partial class Navigator : UserControl
{
    private class PageData
    {
        private readonly MenuDestination destination;
        private readonly string name;
        private readonly int order;
        private readonly string? parent;

        public PageData(MenuItemAttribute menu, Type type) => (destination, name, order, parent, PageType) = (menu.Destination, menu.Name, menu.Order, menu.Parent, type);
        public PageData(MenuDestination destination, string name, int order, string? parent = null) => (this.destination, this.name, this.order, this.parent) = (destination, name, order, parent);

        public MenuDestination Destination => destination;
        public string Name => name;
        public int Order => order;
        public string? Parent => parent;
        public Type? PageType { get; }
    }

    private readonly IPageManager pageManager;

    public Navigator(IPageManager pageManager)
    {
        InitializeComponent();

        this.pageManager = pageManager;

        Type viewerType = typeof(IBrowser<>);

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(p => p.IsInterface)
            .Where(p => p.GetInterfaces().Any(i => i.IsGenericType && viewerType == i.GetGenericTypeDefinition()));

        List<PageData> pageTypes = new();
        foreach (var type in types)
        {
            var attr = type.GetCustomAttribute<MenuItemAttribute>();
            if (attr != null)
            {
                pageTypes.Add(new(attr, type));
            }
        }

        pageTypes.Add(new(MenuDestination.Document, "Склад", 20));
        pageTypes.Add(new(MenuDestination.Document, "Производство", 30));
        pageTypes.Add(new(MenuDestination.Document, "Расчёты с контрагентами", 30));
        pageTypes.Add(new(MenuDestination.Document, "Зар. плата", 70));

        pageTypes.Add(new(MenuDestination.Directory, "Номенклатура", 90));
        pageTypes.Add(new(MenuDestination.Directory, "Производственные операции", 110));

        foreach (var item in pageTypes.OrderBy(m => m.Order).Where(m => m.Parent == null))
        {
            TreeMenuItem treeMenu = item.Destination switch
            {
                MenuDestination.Directory => treeMenuDictionary,
                MenuDestination.Document => treeMenuDocument,
                _ => throw new NotImplementedException()
            };

            var added = CreateMenu(treeMenu, item, pageTypes);
        }
    }

    private TreeMenuItem CreateMenu(TreeMenuItem root, PageData pageData, IEnumerable<PageData> pages)
    {
        TreeMenuItem menu = CreateTreeMenuItem(root, pageData);
        root.Items.Add(menu);

        foreach (var item in pages.Where(m => m.Parent == pageData.Name).OrderBy(m => m.Order))
        {
            CreateMenu(menu, item, pages);
        }

        return menu;
    }

    private TreeMenuItem CreateTreeMenuItem(TreeMenuItem parent, PageData data)
    {
        TreeMenuItem item = new()
        {
            Text = data.Name,
            Tag = data.PageType,
            BackColor = parent.BackColor,
            ForeColor = parent.ForeColor,
            ItemBackColor = parent.ItemBackColor,
            ItemHoverColor = parent.ItemHoverColor,
            SelectedColor = parent.SelectedColor,
            SelectedItemForeColor = parent.SelectedItemForeColor
        };

        if (data.PageType != null)
        {
            item.Click += AddonItem_Click;
        }

        return item;
    }

    private void AddonItem_Click(object? sender, EventArgs e)
    {
        if (sender is TreeMenuItem menu && menu.Tag is Type type)
        {
            pageManager.ShowBrowser(type);
        }
    }

    private void TreeMenuAbout_Click(object sender, EventArgs e) => pageManager.ShowAbout();

    private void TreeMenuEmail_Click(object sender, EventArgs e) => pageManager.ShowEmailPage();
}
