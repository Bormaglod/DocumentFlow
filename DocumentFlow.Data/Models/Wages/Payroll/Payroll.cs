//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;


[EntityName("Платёжная ведомость")]
public class Payroll : AccountingDocument, IBilling
{
    public Payroll() => BillingRange = new(this);
    public int BillingYear { get; } = DateTime.Now.Year;
    public short BillingMonth { get; } = Convert.ToInt16(DateTime.Now.Month);

    /// <summary>
    /// Возвращает сумму заработной платы по платёжной ведомости.
    /// </summary>
    public decimal Wage { get; protected set; }
    public BillingRange BillingRange { get; }
    public string[]? EmployeeNames { get; protected set; }
    public string EmployeeNamesText => EmployeeNames != null ? string.Join(',', EmployeeNames.Distinct()) : string.Empty;
    
    [WritableCollection]
    public IList<PayrollEmployee> Employees { get; protected set; } = null!;
}
