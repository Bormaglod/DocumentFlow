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
    public Guid ReferenceId { get; set; }
    public decimal OperationSumma { get; set; }
    public decimal Amount { get; set; }
    public Guid DocumentTypeId { get; set; }
    public string? DocumentTypeCode { get; protected set; }
    public string? DocumentTypeName { get; protected set; }
}
