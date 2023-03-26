//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.09.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Accounts;

[Description("Расч. счёт")]
public class Account : Directory
{
    public decimal AccountValue { get; set; }
    public Guid? BankId { get; set; }
    public string? BankName { get; protected set; }
    public string? CompanyName { get; protected set; }
}
