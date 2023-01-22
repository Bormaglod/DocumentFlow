//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//
// Версия 2023.1.15
//  - добавлен метод GetWithReturnMaterial
// Версия 2023.1.21
//  - добавлен метод GetOnlyGivingMaterials
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Data;

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

    /// <summary>
    /// Метод возвращает список всех материалов, которые потребуются для изготовления заказа order и
    /// при этом это давальческий материал
    /// </summary>
    /// <typeparam name="T">Тип описывающий материал (должен нсаледоваться от <see cref="ProductPrice"/></typeparam>
    /// <param name="order">Заказ для которого осуществляется поиск всех давальческих материалов.</param>
    /// <returns>Список давальческих материалов, которые потребуются для изготовления заказа order</returns>
    IReadOnlyList<T> GetOnlyGivingMaterials<T>(ProductionOrder order) where T : ProductPrice;
}
