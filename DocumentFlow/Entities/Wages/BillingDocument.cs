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
    public BillingDocument() => billing_range = new(this);

#pragma warning disable IDE1006 // Стили именования
    public int billing_year { get; set; } = DateTime.Now.Year;
    public short billing_month { get; set; } = Convert.ToInt16(DateTime.Now.Month);
    public BillingRange billing_range { get; }
    public string[]? employee_names { get; protected set; }
    public string employee_names_text => employee_names != null ? string.Join(',', employee_names.Distinct()) : string.Empty;
#pragma warning restore IDE1006 // Стили именования
}
