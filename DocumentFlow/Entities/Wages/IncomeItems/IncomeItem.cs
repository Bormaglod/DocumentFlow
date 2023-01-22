//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//
// Версия 2022.8.21
//  - свойство code теперь не допускает значения null
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Wages.IncomeItems;

public class IncomeItem : Identifier<Guid>, IItem
{
    public string code { get; set; } = string.Empty;
    public string? item_name { get; set; }
}
