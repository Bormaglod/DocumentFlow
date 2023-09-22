//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

public class GrossPayrollEmployee : WageEmployee
{
    private Guid incomeItemId;

    [Display(AutoGenerateField = false)]
    public Guid IncomeItemId 
    { 
        get => incomeItemId; 
        set
        {
            if (incomeItemId != value)
            {
                incomeItemId = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Display(Name = "Статья дохода", Order = 100)]
    [ColumnMode(Width = 300)]
    [Write(false)]
    public string IncomeItemName { get; set; } = string.Empty;
}