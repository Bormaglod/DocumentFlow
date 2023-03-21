//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Data;

public class ShipmentDocument : AccountingDocument
{
    public Guid? ContractorId { get; set; }
    public string? ContractorName { get; protected set; }
    public Guid? ContractId { get; set; }
    public string? ContractName { get; protected set; }
}