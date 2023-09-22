//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.03.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Interfaces;

public interface IContextMenu
{
    IContextMenu Add(string text);
    IContextMenuItem CreateItem(string text, EventHandler action);
    IContextMenuItem CreateItem(string text, Image image, EventHandler action);
    IContextMenu Add(string text, EventHandler action);
    IContextMenu Add(string text, Image image, EventHandler action);
    IContextMenu AddSeparator();
    void AddItems(IContextMenuItem[] menuItems);
}
