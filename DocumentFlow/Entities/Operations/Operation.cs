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

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Operations;

[Description("Операция")]
public class Operation : Directory
{
#pragma warning disable IDE1006 // Стили именования
    public int produced { get; set; }
    public int prod_time { get; set; }
    public int production_rate { get; set; }
    public Guid type_id { get; set; }
    public string? type_name { get; protected set; }
    public decimal salary { get; set; }
    public bool operation_using { get; protected set; }
    public DateTime? date_norm { get; set; }
#pragma warning restore IDE1006 // Стили именования
}
