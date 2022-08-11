//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.09.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Infrastructure;

public enum ButtonIconSize { Small, Large }

public interface IToolBar
{
    ButtonIconSize IconSize { get; set; }

    ToolStripButton Add(string text, Image image16, Image image32, Action action);
    void AddSeparator();
}
