//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.10.2021
//
// Версия 2023.1.28
//  - добавлено подавление предупреждения IDE1006 о необходимости
//    писать слова с прописных символов
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Banks;

[Description("Банк")]
public class Bank : Directory
{
    public decimal Bik { get; set; }
    public decimal Account { get; set; }
    public string? Town { get; set; }
}
