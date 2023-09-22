//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Models;

public interface IBalanceProcessingRepository : IOwnedRepository<Guid, BalanceProcessing>
{
    /// <summary>
    /// Возвращает список давальческих материалов и остаток этого материала.
    /// </summary>
    /// <returns>Список давальческих материалов и остаток этого материала.</returns>
    IReadOnlyList<BalanceProcessing> GetRemainders();
}
