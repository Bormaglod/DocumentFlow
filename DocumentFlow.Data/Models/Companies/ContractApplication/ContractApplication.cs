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
        set => SetProperty(ref documentDate, value);
    }

    public DateTime DateStart
    { 
        get => dateStart;
        set => SetProperty(ref dateStart, value);
    }

    public DateTime? DateEnd 
    { 
        get => dateEnd;
        set => SetProperty(ref dateEnd, value);
    }

    public string? Note 
    { 
        get => note;
        set => SetProperty(ref note, value);
    }

    [WritableCollection]
    public IList<PriceApproval> PriceApprovals { get; protected set; } = null!;

    [Computed]
    public string? ContractName { get; set; }

    public override string ToString() => $"{ItemName} №{Code} от {DocumentDate:d}";
}
