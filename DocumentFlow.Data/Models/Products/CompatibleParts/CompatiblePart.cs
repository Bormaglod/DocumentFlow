//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.05.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Models;

public class CompatiblePart : Entity<long>, ICopyable, IDependentEntity
{
    private string code = string.Empty;
    private string itemName = string.Empty;
    private Guid compatibleId;

    [Display(AutoGenerateField = false)]
    public Guid CompatibleId 
    { 
        get => compatibleId; 
        set
        {
            if (compatibleId != value) 
            { 
                compatibleId = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Write(false)]
    [Display(Name = "Артикул")]
    [ColumnMode(Width = 150)]
    public string Code 
    { 
        get => code; 
        set
        {
            if (code != value) 
            { 
                code = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Write(false)]
    [Display(Name = "Наименование")]
    [ColumnMode(AutoSizeColumnsMode = AutoSizeColumnsMode.Fill)]
    public string ItemName 
    { 
        get => itemName; 
        set
        {
            if (itemName != value) 
            { 
                itemName = value;
                NotifyPropertyChanged();
            }
        }
    }

    public object Copy()
    {
        var copy = (CompatiblePart)MemberwiseClone();
        copy.Id = 0;

        return copy;
    }

    public void SetOwner(Guid ownerId) => OwnerId = ownerId;
}
