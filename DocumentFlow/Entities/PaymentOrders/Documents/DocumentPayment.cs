//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.02.2022
//
// Версия 2022.12.26
//  - document_id теперь может быть null
//  - если document_id равен null, то этот документ проводился с помощью
//    документа "Корректировка долга"
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.PaymentOrders.Documents;

public class DocumentPayment : PaymentOrder
{
    public decimal PostingTransaction { get; protected set; }
    public Guid? DocumentId { get; protected set; }
    public string DocumentName => DocumentId == null ? "Корректировка долга" : "Платёжный ордер";
}
