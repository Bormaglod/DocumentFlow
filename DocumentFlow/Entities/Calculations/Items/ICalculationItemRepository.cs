//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Calculations;

public interface ICalculationItemRepository<T> : IOwnedRepository<Guid, T>
    where T : CalculationItem
{
    decimal GetSumItemCost(Guid owner_id);
    void RecalculatePrices(Guid calculate_id);
}
