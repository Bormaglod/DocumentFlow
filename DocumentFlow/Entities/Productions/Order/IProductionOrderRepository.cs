//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//
// Версия 2023.1.15
//  - добавлен метод GetWithReturnMaterial
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Companies;

namespace DocumentFlow.Entities.Productions.Order;

public interface IProductionOrderRepository : IDocumentRepository<ProductionOrder>
{
    /// <summary>
    /// Возвращает список записей из табличной части заказа.
    /// </summary>
    /// <param name="order">Заказ на изготовление.</param>
    /// <returns>Список записей из табличной части заказа.</returns>
    IReadOnlyList<ProductionOrderPrice> GetList(ProductionOrder order);

    /// <summary>
    /// Возвращает список заказов по которым есть остатки давальческого сырья полученного в соответствии с договором contract.
    /// </summary>
    /// <param name="contract">Договор с контрагентом-заказчиком на передачу материалов для изготовления продукции.</param>
    /// <returns>Список заказов по которым есть остатки давальческого сырья полученного в соответствии с договором contract.</returns>
    IReadOnlyList<ProductionOrder> GetWithReturnMaterial(Contract contract);
}
