//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Controls.Infrastructure в DocumentFlow.Infrastructure.Controls
// Версия 2023.1.28
//  - добавлено свойство Size
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Controls;

public interface ICard
{
    int Index { get; }
    string Title { get; }
    Size Size { get; }
    void RefreshCard();
}
