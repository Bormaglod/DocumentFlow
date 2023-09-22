//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Models;

public interface IContractorRepository : IDirectoryRepository<Contractor>
{
    /// <summary>
    /// Функция возвращает список поставщиков.
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<Contractor> GetSuppliers();

    /// <summary>
    /// Функция возвращает список покупателей (клиентов).
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<Contractor> GetCustomers();
}
