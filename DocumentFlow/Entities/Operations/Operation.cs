//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//
// Версия 2023.2.4
//  - добавлено свойство date_norm
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Operations;

[Description("Операция")]
public class Operation : Directory
{
    public int Produced { get; set; }
    public int ProdTime { get; set; }
    public int ProductionRate { get; set; }
    public Guid TypeId { get; set; }
    public string? TypeName { get; protected set; }
    public decimal Salary { get; set; }
    public bool OperationUsing { get; protected set; }
    public DateTime? DateNorm { get; set; }
}
