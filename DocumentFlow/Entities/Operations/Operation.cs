//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Operations;

[Description("Операция")]
public class Operation : Directory
{
    public int produced { get; set; }
    public int prod_time { get; set; }
    public int production_rate { get; set; }
    public Guid type_id { get; set; }
    public string? type_name { get; protected set; }
    public decimal salary { get; set; }
    public bool operation_using { get; protected set; }
}
