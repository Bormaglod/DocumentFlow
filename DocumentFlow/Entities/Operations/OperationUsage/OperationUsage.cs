//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;

namespace DocumentFlow.Entities.Operations;

public class OperationUsage : DocumentInfo
{
    public string CalculationName { get; protected set; } = string.Empty;
    public string CalculationCode { get; protected set; } = string.Empty;
    public Guid GoodsId { get; protected set; }
    public string GoodsCode { get; protected set; } = string.Empty;
    public string GoodsName { get; protected set; } = string.Empty;
    public decimal Amount { get; protected set; }
}
