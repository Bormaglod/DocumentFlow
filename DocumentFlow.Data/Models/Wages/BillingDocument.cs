//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Data.Models;

public class BillingDocument : AccountingDocument, IBilling
{
    public BillingDocument() => BillingRange = new(this);

    public int BillingYear { get; set; } = DateTime.Now.Year;
    public short BillingMonth { get; set; } = Convert.ToInt16(DateTime.Now.Month);
    public BillingRange BillingRange { get; }
    public string[]? EmployeeNames { get; protected set; }
    public string EmployeeNamesText => EmployeeNames != null ? string.Join(',', EmployeeNames.Distinct()) : string.Empty;
}
