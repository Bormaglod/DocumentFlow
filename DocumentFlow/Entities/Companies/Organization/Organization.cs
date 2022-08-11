//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Companies;

[Description("Организация")]
public class Organization : Company
{
    public string? address { get; set; }
    public string? phone { get; set; }
    public string? email { get; set; }
    public bool default_org { get; set; }
}
