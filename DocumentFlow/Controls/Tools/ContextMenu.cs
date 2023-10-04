//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.03.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

namespace DocumentFlow.Controls.Tools;

public class ContextMenu : IContextMenu
{
    private class ContextMenuGroup
    {
        public ContextMenuGroup(ToolStripSeparator? separator, IContextMenuItem[] items)
        {
            Separator = separator;
            Items = items;
        }

        public ToolStripSeparator? Separator { get; set; }

        public IContextMenuItem[] Items { get; set; }

        public void UpdateVisible()
        {
            if (Separator != null)
            {
                bool visible = false;
                for (int i = 0; i < Items.Length; i++)
                {
                    visible = visible || Items[i].Visible;
                }

                Separator.Visible = visible;
            }
        }
    }

    private readonly ToolStripItemCollection items;
    private readonly List<ContextMenuGroup> groups = new();
    
    public ContextMenu(ToolStripItemCollection items) => this.items = items;

    public IContextMenu Add(string text)
    {
        ToolStripMenuItem menuItem = new(text);
        items.Add(menuItem);

        return new ContextMenu(menuItem.DropDownItems);
    }

    public IContextMenuItem CreateItem(string text, EventHandler action)
    {
        return CreateItem(new ToolStripMenuItem(text), action);
    }

    public IContextMenuItem CreateItem(string text, Image image, EventHandler action)
    {
        return CreateItem(new ToolStripMenuItem(text, image), action);
    }

    public IContextMenu Add(string text, EventHandler action)
    {
        CreateMenuItem(text, action);

        return this;
    }

    public IContextMenu Add(string text, Image image, EventHandler action)
    {
        var menuItem = CreateMenuItem(text, action);
        menuItem.Image = image;

        return this;
    }

    public IContextMenu AddSeparator()
    {
        items.Add(new ToolStripSeparator());
        return this;
    }

    public void AddItems(IContextMenuItem[] menuItems)
    {
        ToolStripSeparator? separator = null;
        if (items.Count > 0)
        {
            separator = new ToolStripSeparator();
            items.Add(separator);
        }

        for (int i = 0; i < menuItems.Length; i++)
        {
            items.Add(((ContextMenuItem)menuItems[i]).ToolStripItem);
        }

        groups.Add(new ContextMenuGroup(separator, menuItems));
    }

    private ToolStripMenuItem CreateMenuItem(string text, EventHandler action)
    {
        ToolStripMenuItem menuItem = new(text);
        menuItem.Tag = new ContextMenuItem(menuItem);
        menuItem.Click += (sender, args) =>
        {
            action(menuItem.Tag, EventArgs.Empty);
        };

        items.Add(menuItem);

        return menuItem;
    }

    private IContextMenuItem CreateItem(ToolStripMenuItem menuItem, EventHandler action)
    {
        menuItem.Click += (sender, args) =>
        {
            action(menuItem.Tag, EventArgs.Empty);
        };

        ContextMenuItem contextMenuItem = new(menuItem);
        contextMenuItem.ItemVisibleChanged += UpdateVisibleMenuItems;

        menuItem.Tag = contextMenuItem;

        return contextMenuItem;
    }

    private void UpdateVisibleMenuItems(object? sender, EventArgs e)
    {
        if (sender is IContextMenuItem menuItem)
        {
            foreach (var item in groups)
            {
                if (item.Items.Contains(menuItem))
                {
                    item.UpdateVisible();
                }
            }
        }
    }
}
