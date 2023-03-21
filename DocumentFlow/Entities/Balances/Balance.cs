//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data;

namespace DocumentFlow.Entities.Balances;

public class Balance : BaseDocument
{
    public Guid reference_id { get; set; }
    public decimal operation_summa { get; set; }
    public decimal amount { get; set; }
    public Guid document_type_id { get; set; }
    public string? document_type_code { get; protected set; }
    public string? document_type_name { get; protected set; }
}
