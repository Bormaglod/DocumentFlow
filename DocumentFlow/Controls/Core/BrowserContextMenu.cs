//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.03.2022
//
// Версия 2022.8.28
//  - в метод Add добавлен параметр addSeparator
//  - добавлен метод Add(string, image, bool)
//  - добавлен метод Add(string, bool)
//  - добавлен метод Add(string, image, Action, ToolStripMenuItem)
//  - добавлен метод Add(string, Action, ToolStripMenuItem)
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Controls.Core;

internal class BrowserContextMenu : IContextMenu
{
    private readonly ContextMenuStrip menu;

    public BrowserContextMenu(ContextMenuStrip contextMenu) => menu = contextMenu;

    public ToolStripMenuItem Add(string text, Image image, Action<object?> action, bool addSeparator = true) => InternalAdd(text, image, action, null, addSeparator);

    public ToolStripMenuItem Add(string text, Image image, bool addSeparator = true) => InternalAdd(text, image, null, null, addSeparator);

    public ToolStripMenuItem Add(string text, bool addSeparator = true) => InternalAdd(text, null, null, null, addSeparator);

    public ToolStripMenuItem Add(string text, Image image, Action<object?> action, ToolStripMenuItem parent) => InternalAdd(text, image, action, parent, false);
    
    public ToolStripMenuItem Add(string text, Action<object?> action, ToolStripMenuItem parent) => InternalAdd(text, null, action, parent, false);

    private ToolStripMenuItem InternalAdd(string text, Image? image, Action<object?>? action, ToolStripMenuItem? parent, bool addSeparator)
    {
        if (addSeparator)
        {
            AddSeparator();
        }

        ToolStripMenuItem menuItem = new(text, image);
        if (action != null)
        {
            menuItem.Click += (s, e) => 
                action(menuItem.Tag);
        }

        if (parent != null)
        {
            parent.DropDownItems.Add(menuItem);
        }
        else
        {
            menu.Items.Add(menuItem);
        }

        return menuItem;
    }

    public void AddSeparator() => menu.Items.Add(new ToolStripSeparator());
}
