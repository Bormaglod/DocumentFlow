//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.03.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Infrastructure;

public interface IContextMenu
{
    ToolStripMenuItem Add(string text, Image image, Action action);
    void AddSeparator();
}
