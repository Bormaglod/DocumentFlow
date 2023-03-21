//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Persons;

[Description("Физ. лицо")]
public class Person : Directory
{
    public string? surname { get; set; }
    public string? first_name { get; set; }
    public string? middle_name { get; set; }
    public string? phone { get; set; }
    public string? email { get; set; }
}
