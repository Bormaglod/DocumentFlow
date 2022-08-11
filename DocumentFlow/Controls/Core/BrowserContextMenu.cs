//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.03.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Controls.Core;

internal class BrowserContextMenu : IContextMenu
{
    private readonly ContextMenuStrip menu;
    private bool isFirst = true;

    public BrowserContextMenu(ContextMenuStrip contextMenu) => menu = contextMenu;

    public ToolStripMenuItem Add(string text, Image image, Action action)
    {
        if (isFirst)
        {
            AddSeparator();
            isFirst = false;
        }

        ToolStripMenuItem menuItem = new(text, image, (s, e) => action());
        menu.Items.Add(menuItem);

        return menuItem;
    }

    public void AddSeparator() => menu.Items.Add(new ToolStripSeparator());
}
