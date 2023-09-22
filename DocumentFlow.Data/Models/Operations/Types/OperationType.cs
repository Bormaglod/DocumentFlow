//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Вид операции")]
public class OperationType : Directory
{
    private decimal salary;

    /// <summary>
    /// Возвращает или устанавливает базовую часовую ставку для расчёта заработной платы.
    /// </summary>
    public decimal Salary 
    { 
        get => salary; 
        set
        {
            if (salary != value) 
            { 
                salary = value;
                NotifyPropertyChanged();
            }
        }
    }
}
