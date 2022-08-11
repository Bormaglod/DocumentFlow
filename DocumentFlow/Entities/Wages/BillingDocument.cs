//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Wages.Core;

namespace DocumentFlow.Entities.Wages;

public class BillingDocument : AccountingDocument
{
    public BillingDocument() => billing_range = new(this);
    public int billing_year { get; set; } = DateTime.Now.Year;
    public short billing_month { get; set; } = Convert.ToInt16(DateTime.Now.Month);
    public BillingRange billing_range { get; }
    public string[]? employee_names { get; protected set; }
    public string employee_names_text => employee_names != null ? string.Join(',', employee_names) : string.Empty;
}
