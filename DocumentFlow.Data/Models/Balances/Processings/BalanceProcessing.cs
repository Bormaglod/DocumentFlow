//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

public class BalanceProcessing : BalanceProduct 
{
    [Display(Name = "Материал", Order = 100)]
    public string MaterialName { get; protected set; } = string.Empty;

    [Display(AutoGenerateField = false)]
    public Guid ContractorId { get; protected set; }

    [Display(Name = "Контрагент", Order = 200)]
    public string ContractorName { get; protected set; } = string.Empty;
}
