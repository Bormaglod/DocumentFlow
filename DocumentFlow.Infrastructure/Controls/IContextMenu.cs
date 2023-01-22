//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
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
//  - параметр Action заменен на Action<object?>
// Версия 2022.11.13
//  - добавлен метод Add(string, Action, bool)
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Controls.Infrastructure в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Controls;

public interface IContextMenu
{
    ToolStripMenuItem Add(string text, Image image, Action<object?> action, bool addSeparator = true);
    ToolStripMenuItem Add(string text, Image image, Action<object?> action, ToolStripMenuItem parent);
    ToolStripMenuItem Add(string text, Action<object?> action, ToolStripMenuItem parent);
    ToolStripMenuItem Add(string text, Action<object?> action, bool addSeparator = true);
    ToolStripMenuItem Add(string text, Image image, bool addSeparator = true);
    ToolStripMenuItem Add(string text, bool addSeparator = true);
    void AddSeparator();
}
