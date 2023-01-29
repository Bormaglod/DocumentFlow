﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.1.29
//  - добавлен метод GetRemainders
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Balances;

public interface IBalanceProcessingRepository : IOwnedRepository<Guid, BalanceProcessing>
{
    /// <summary>
    /// Возвращает список давальческих материалов и остаток этого материала.
    /// </summary>
    /// <returns>Список давальческих материалов и остаток этого материала.</returns>
    IReadOnlyList<BalanceProcessing> GetRemainders();
}
