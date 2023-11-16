//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Банк")]
public class Bank : Directory
{
    private decimal bik;
    private decimal account;
    private string? town;

    public decimal Bik
    {
        get => bik;
        set => SetProperty(ref bik, value);
    }

    public decimal Account
    {
        get => account;
        set => SetProperty(ref account, value);
    }

    public string? Town 
    {
        get => town;
        set => SetProperty(ref town, value);
    }
}
