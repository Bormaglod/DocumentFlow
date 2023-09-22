//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Models;

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

    /// <summary>
    /// Функция возвращает информацию о цене указанного продукта в договоре, если он там есть.
    /// </summary>
    /// <param name="contract">Договор, который содержит сведения об изделии.</param>
    /// <param name="product">Изделие, информация о котором будет возвращать функция.</param>
    /// <returns>Информация об изделии (цена, ед. измерения) или null, если изделие не содержится в договоре. Изделие 
    /// ищется в приложениях к данному договору, которые актуальны на текущий момент.</returns>
    PriceApproval? GetPriceProduct(Contract contract, Product product);
}
