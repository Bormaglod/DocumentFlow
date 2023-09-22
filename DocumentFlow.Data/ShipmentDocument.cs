//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data;

public abstract class ShipmentDocument : AccountingDocument
{
    public Guid? ContractorId { get; set; }
    public string? ContractorName { get; protected set; }
    public Guid? ContractId { get; set; }
    public string? ContractName { get; protected set; }
}