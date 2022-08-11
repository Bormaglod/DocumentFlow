//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.09.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Accounts;

[Description("Расч. счёт")]
public class Account : Directory
{
    public decimal account_value { get; set; }
    public Guid? bank_id { get; set; }
    public string? bank_name { get; protected set; }
    public string? company_name { get; protected set; }
}
