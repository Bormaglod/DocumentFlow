//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Waybills;

public class Waybill : ShipmentDocument
{
    public string? waybill_number { get; set; }
    public DateTime? waybill_date { get; set; }
    public string? invoice_number { get; set; }
    public DateTime? invoice_date { get; set; }
    public bool upd { get; set; }
    public decimal product_cost { get; protected set; }
    public bool tax_payer { get; protected set; }
    public int tax { get; protected set; }
    public decimal tax_value { get; protected set; }
    public decimal full_cost { get; protected set; }
    public decimal paid { get; protected set; }
}
