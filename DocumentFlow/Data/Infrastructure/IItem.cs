//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//
// Версия 2022.8.21
//  - свойство code теперь не допускает значения null
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Infrastructure;

public interface IItem
{
    string code { get; set; }
    string? item_name { get; set; }
}
