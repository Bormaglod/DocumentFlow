//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.09.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Interfaces;

public interface IToolBar
{
    IToolBar DoNotSeparateUserButtons();
    IToolBar LargeIcons();
    IToolBar SmallIcons();
    IToolBar Add(string text, Image image16, Image image32, Action action);
    IToolBar AddSeparator();
}
