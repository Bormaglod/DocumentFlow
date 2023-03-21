//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.09.2021
//
// Версия 2023.1.28
//  - добавлено подавление предупреждения IDE1006 о необходимости
//    писать слова с прописных символов
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Accounts;

[Description("Расч. счёт")]
public class Account : Directory
{
#pragma warning disable IDE1006 // Стили именования
    public decimal account_value { get; set; }
    public Guid? bank_id { get; set; }
    public string? bank_name { get; protected set; }
    public string? company_name { get; protected set; }
#pragma warning restore IDE1006 // Стили именования
}
