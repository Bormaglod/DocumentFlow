//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.PaymentOrders.Posting;

[Description("Платёж")]
public class PostingPayments : AccountingDocument, IDiscriminator
{
    bool IDiscriminator.UseGetId => false;

    string IDiscriminator.TableName { get => table_name; set => table_name = value; }

    public decimal transaction_amount { get; set; }
    public Guid document_id { get; set; }
    public string? document_name { get; protected set; }
    public string? contractor_name { get; protected set; }

    [Exclude]
    public string table_name { get; set; } = string.Empty;
}
