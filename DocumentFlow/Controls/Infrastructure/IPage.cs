﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.01.2022
//
// Версия 2022.8.28
//  - добавлен метод RefreshPage()
// Версия 2022.12.30
//  - метод RegisterReport перенесен в IBrowserPage и IEditorPage
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Infrastructure;

public interface IPage
{
    string Text { get; }
    void OnPageClosing();
    void RefreshPage();
}
