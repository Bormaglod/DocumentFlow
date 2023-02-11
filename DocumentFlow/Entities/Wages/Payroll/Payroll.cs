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

using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Wages.Core;

namespace DocumentFlow.Entities.Wages;


[Description("Платёжная ведомость")]
public class Payroll : AccountingDocument, IBilling//BasePayroll
{
    public Payroll() => billing_range = new(this);
    public int billing_year { get; } = DateTime.Now.Year;
    public short billing_month { get; } = Convert.ToInt16(DateTime.Now.Month);

#pragma warning disable IDE1006 // Стили именования
    public decimal wage { get; protected set; }
    public BillingRange billing_range { get; }
    public string[]? employee_names { get; protected set; }
    public string employee_names_text => employee_names != null ? string.Join(',', employee_names.Distinct()) : string.Empty;
#pragma warning restore IDE1006 // Стили именования
}
