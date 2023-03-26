//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//
// Версия 2023.1.29
//  - у свойства material_name метод set стал protected
//  - добавлено свойство contractor_name
//  - добавлен атрибут Display для свойств
//  - добавлено подавление предупреждения IDE1006 о необходимости
//    писать слова с прописных символов
//
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Balances;

public class BalanceProcessing : BalanceProduct 
{
    [Display(Name = "Материал", Order = 100)]
    public string MaterialName { get; protected set; } = string.Empty;

    [Display(Name = "Контрагент", Order = 200)]
    public string ContractorName { get; protected set; } = string.Empty;
}
