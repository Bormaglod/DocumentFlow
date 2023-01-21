//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//
// Версия 2023.1.21
//  - добавлен метод GetPaymentBalance
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.PaymentOrders;

public interface IPaymentOrderRepository : IDocumentRepository<PaymentOrder>
{
    /// <summary>
    /// Возвращает остаток суммы которую необходимо распределить в указанном
    /// платёжном документе
    /// </summary>
    /// <param name="order">Платёжный документ.</param>
    /// <returns>Остаток суммы, подлежащий распределению.</returns>
    decimal GetPaymentBalance(PaymentOrder order);

    /// <summary>
    /// Возвращает остаток суммы которую необходимо распределить в указанном
    /// платёжном документе
    /// </summary>
    /// <param name="order">Идентификатор платёжного документа.</param>
    /// <returns>Остаток суммы, подлежащий распределению.</returns>
    decimal GetPaymentBalance(Guid orderId);
}
