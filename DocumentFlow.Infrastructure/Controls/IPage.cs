//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.01.2022
//
// Версия 2022.8.28
//  - добавлен метод RefreshPage()
// Версия 2022.12.30
//  - метод RegisterReport перенесен в IBrowserPage и IEditorPage
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Controls.Infrastructure в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Controls;

public interface IPage
{
    string Text { get; }
    void OnPageClosing();
    void RefreshPage();
}
