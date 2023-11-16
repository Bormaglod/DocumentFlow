//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.09.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Расч. счёт")]
public class Account : Directory
{
    private decimal acountValue;
    private Guid? bankId;

    public decimal AccountValue 
    {
        get => acountValue;
        set => SetProperty(ref acountValue, value);
    }

    public Guid? BankId 
    {
        get => bankId;
        set => SetProperty(ref bankId, value);
    }

    [Computed]
    public string? BankName { get; set; }

    [Computed]
    public string? CompanyName { get; set; }
}
