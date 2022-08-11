//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.02.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.PaymentOrders.Documents;

public class DocumentPayment : PaymentOrder
{
    public decimal posting_transaction { get; protected set; }
    public Guid document_id { get; protected set; }
    public string document_name => "Платёжный ордер";
}
