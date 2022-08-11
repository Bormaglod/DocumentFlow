//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

public class ShipmentDocument : AccountingDocument
{
    public Guid? contractor_id { get; set; }
    public string? contractor_name { get; protected set; }
    public Guid? contract_id { get; set; }
    public string? contract_name { get; protected set; }
}