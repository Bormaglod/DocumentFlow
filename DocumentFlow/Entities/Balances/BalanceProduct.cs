//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.11.2021
//
// Версия 2023.1.29
//  - добавлен атрибут Display для свойств
//  - добавлено подавление предупреждения IDE1006 о необходимости
//    писать слова с прописных символов
//
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Balances;

public class BalanceProduct : Balance
{
    [Display(Name = "Приход", Order = 500)]
    public decimal? Income { get; protected set; }

    [Display(Name = "Расход", Order = 600)]
    public decimal? Expense { get; protected set; }

    [Display(Name = "Остаток", Order = 700)]
    public decimal Remainder { get; protected set; }

    [Display(Name = "Остаток, руб.", Order = 800)]
    public decimal MonetaryBalance { get; protected set; }
}
