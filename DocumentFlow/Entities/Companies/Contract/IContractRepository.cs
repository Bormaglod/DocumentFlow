//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Companies;

public interface IContractRepository : IOwnedRepository<Guid, Contract>
{
    /// <summary>
    /// Функция возвращает список договоров поставки материалов с указанным поставщиком.
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<Contract> GetSuppliers(Guid supplier);

    /// <summary>
    /// Функция возвращает список договоров поставки продукции с указанным покупателем.
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<Contract> GetCustomers(Guid customer);

    /// <summary>
    /// Функция возвращает список договоров указанного контрагента.
    /// </summary>
    /// <param name="contractor">Параметр указывает значение контрагента для которого возвращается список договоров.</param>
    /// <returns>Список договоров указанного контрагента.</returns>
    IReadOnlyList<Contract> Get(Contractor contractor);
}
