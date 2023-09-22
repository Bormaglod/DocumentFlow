//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Models;

public interface IBalanceContractorRepository : IOwnedRepository<Guid, BalanceContractor>
{
    /// <summary>
    /// Возвращает список покупателей с текущим сальдо. Список отсортирован по величине сальдо от большего значения к меньшему.
    /// </summary>
    /// <param name="limit">Значение, определяющее количество пулучаемых записей или 0, если необходимо получить список всех покупателей.</param>
    /// <returns>Список покупателей с текущим сальдо.</returns>
    IReadOnlyList<CurrentBalanceContractor> GetCustomersDebt(int limit = 0);

    /// <summary>
    /// Возвращает список поставщиков с текущим сальдо. Список отсортирован по величине сальдо от большего значения к меньшему.
    /// </summary>
    /// <param name="limit">Значение, определяющее количество пулучаемых записей или 0, если необходимо получить список всех поставщиков.</param>
    /// <returns>Список поставщиков с текущим сальдо.</returns>
    IReadOnlyList<CurrentBalanceContractor> GetSuppliersDebt(int limit = 0);
}
