//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.09.2021
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Controls.Infrastructure в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Controls;

public enum ButtonIconSize { Small, Large }

public interface IToolBar
{
    ButtonIconSize IconSize { get; set; }

    ToolStripButton Add(string text, Image image16, Image image32, Action action);
    void AddSeparator();
}
