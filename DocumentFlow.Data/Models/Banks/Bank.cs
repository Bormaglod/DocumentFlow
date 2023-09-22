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
        set
        {
            if (bik != value) 
            { 
                bik = value;
                NotifyPropertyChanged();
            }
        }
    }

    public decimal Account
    {
        get => account;
        set 
        { 
            if (account != value)
            {
                account = value;
                NotifyPropertyChanged();
            }
        }
    }

    public string? Town 
    {
        get => town;
        set
        {
            if (town != value)
            {
                town = value;
                NotifyPropertyChanged();
            }
        }
    }
}
