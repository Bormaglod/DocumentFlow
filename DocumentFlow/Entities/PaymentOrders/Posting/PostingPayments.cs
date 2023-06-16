//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2022
//
// Версия 2022.08.17
//  - удалено свойство UseGetId
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.6.16
//  - Атрибут Exclude заменен на AllowOperation(DataOperation.None)
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.PaymentOrders.Posting;

[Description("Платёж")]
public class PostingPayments : AccountingDocument, IDiscriminator
{
    string IDiscriminator.TableName { get => TableName; set => TableName = value; }

    public decimal TransactionAmount { get; set; }
    public Guid DocumentId { get; set; }
    public string? DocumentName { get; protected set; }
    public string? ContractorName { get; protected set; }

    [AllowOperation(DataOperation.None)]
    public string TableName { get; set; } = string.Empty;
}
