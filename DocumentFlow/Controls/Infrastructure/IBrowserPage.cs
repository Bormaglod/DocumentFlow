//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//
// Версия 2022.8.28
//  - метод RefreshPage перенес в IPage
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Infrastructure;

public interface IBrowserPage : IPage
{
    bool ReadOnly { get; set; }
    void Refresh(Guid? owner = null);
}
