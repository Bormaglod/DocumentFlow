//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Приложение")]
public class ContractApplication : Directory
{
    private DateTime documentDate;
    private DateTime dateStart;
    private DateTime? dateEnd;
    private string? note;

    public DateTime DocumentDate 
    { 
        get => documentDate; 
        set
        {
            if (documentDate != value)
            {
                documentDate = value;
                NotifyPropertyChanged();
            }
        }
    }

    public DateTime DateStart
    { 
        get => dateStart; 
        set
        {
            if (dateStart != value) 
            { 
                dateStart = value;
                NotifyPropertyChanged();
            }
        }
    }

    public DateTime? DateEnd 
    { 
        get => dateEnd; 
        set
        {
            if (dateEnd != value) 
            { 
                dateEnd = value;
                NotifyPropertyChanged();
            }
        }
    }

    public string? Note 
    { 
        get => note; 
        set
        {
            if (note != value)
            {
                note = value;
                NotifyPropertyChanged();
            }
        }
    }

    [WritableCollection]
    public IList<PriceApproval> PriceApprovals { get; protected set; } = null!;

    public string? ContractName { get; protected set; }

    public override string ToString() => $"{ItemName} №{Code} от {DocumentDate:d}";
}
