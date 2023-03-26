//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data;

namespace DocumentFlow.Entities.Waybills;

public class Waybill : ShipmentDocument
{
    public string? WaybillNumber { get; set; }
    public DateTime? WaybillDate { get; set; }
    public string? InvoiceNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public bool Upd { get; set; }
    public decimal ProductCost { get; protected set; }
    public bool TaxPayer { get; protected set; }
    public int Tax { get; protected set; }
    public decimal TaxValue { get; protected set; }
    public decimal FullCost { get; protected set; }
    public decimal Paid { get; protected set; }
}
