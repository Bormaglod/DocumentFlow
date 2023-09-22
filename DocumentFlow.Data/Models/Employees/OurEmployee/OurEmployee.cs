//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Сотрудник")]
public class OurEmployee : Employee
{
    private string[]? incomeItems;

    public string[]? IncomeItems 
    { 
        get => incomeItems; 
        set
        {
            if (incomeItems != value) 
            { 
                incomeItems = value;
                NotifyPropertyChanged();
            }
        }
    }
}
