//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Productions.Performed;

[Description("Выполнение работы")]
public class OperationsPerformed : AccountingDocument
{
    private long _quantity;

    public DateOnly date => document_date != null ? DateOnly.FromDateTime(document_date.Value) : DateOnly.FromDateTime(DateTime.Today);
    public Guid employee_id { get; set; }
    public Guid operation_id { get; set; }
    public Guid? replacing_material_id { get; set; }

    public long quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            NotifyPropertyChanged(nameof(quantity));
        }
    }

    public decimal salary { get; set; }
    public string? lot_name { get; protected set; }
    public string? order_name { get; protected set; }
    public string? goods_name { get; protected set; }
    public Guid calculation_id { get; protected set; }
    public string calculation_name { get; protected set; } = string.Empty;
    public string operation_code { get; protected set; } = string.Empty;
    public string? operation_name { get; protected set; }
    public string? employee_name { get; protected set; }
    public string? material_name { get; protected set; }
}