//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//
// Версия 2023.2.11
//  - изменено наследование с BasePayroll на AccountingDocument
//  - добавлена реализация интерфейса IBilling
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Wages.Core;

namespace DocumentFlow.Entities.Wages;


[Description("Платёжная ведомость")]
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
}
