//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Models;

public interface IProductionOrderRepository : IDocumentRepository<ProductionOrder>
{
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

    /// <summary>
    /// Метод возвращает список активных заказов, т.е. не помеченных на удаление, проведённых и не закрытых.
    /// </summary>
    /// <returns>Список активных заказов.</returns>
    IReadOnlyList<ProductionOrder> GetActiveOrders();

    /// <summary>
    /// Метод возвращает список изделий из заказа указанного в параметре order.
    /// </summary>
    /// <param name="order">Заказ на изготовление.</param>
    /// <returns>Список изделий из заказа указанного в параметре order.</returns>
    IReadOnlyList<Goods> GetGoods(ProductionOrder order);

    /// <summary>
    /// Метод заменяет указанную калькуляцию в заказе и во всех выполненных работах. Перепроведение документов не производится 
    /// (изменяется только стоимость работы, если изменилась цена работы в калькуляции). Применять только для идентичных, 
    /// по составу работ, калькуляций.
    /// </summary>
    /// <param name="order">Заказ на изготовление.</param>
    /// <param name="currentCalculationId">Идентификатор калькуляции, которая будет замещаяться.</param>
    /// <param name="newCalculationId">Идентификатор калькуляции, которая будет использоваться вместо currentCalculationId.</param>
    void ChangeCalculation(ProductionOrder order, Guid currentCalculationId, Guid newCalculationId);
}
