//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.PurchaseRequestLib;

[Description("Заявка")]
public class PurchaseRequest : ShipmentDocument
{
    public decimal cost_order { get; protected set; }
    public bool tax_payer { get; protected set; }
    public int tax { get; protected set; }
    public decimal tax_value { get; protected set; }
    public decimal full_cost { get; protected set; }
    public decimal paid { get; protected set; }
    public string? note { get; set; }
}
