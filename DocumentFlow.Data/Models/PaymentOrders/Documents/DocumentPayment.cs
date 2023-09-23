//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.02.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Models;

public class DocumentPayment : PaymentOrder
{
    public decimal PostingTransaction { get; protected set; }
    public Guid? DocumentId { get; protected set; }
    public string DocumentName => DocumentId == null ? "Корректировка долга" : "Платёжный ордер";
}
