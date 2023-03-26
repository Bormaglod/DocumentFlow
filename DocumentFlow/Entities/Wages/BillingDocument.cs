//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//
// Версия 2023.1.28
//  - свойство employee_names_text больше не содержит повторяющиеся
//    значения
//  - добавлено подавление предупреждения IDE1006 о необходимости
//    писать слова с прописных символов
//  - добавлено наследование от IBilling
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Entities.Wages.Core;

namespace DocumentFlow.Entities.Wages;

public class BillingDocument : AccountingDocument, IBilling
{
    public BillingDocument() => BillingRange = new(this);

    public int BillingYear { get; set; } = DateTime.Now.Year;
    public short BillingMonth { get; set; } = Convert.ToInt16(DateTime.Now.Month);
    public BillingRange BillingRange { get; }
    public string[]? EmployeeNames { get; protected set; }
    public string EmployeeNamesText => EmployeeNames != null ? string.Join(',', EmployeeNames.Distinct()) : string.Empty;
}
