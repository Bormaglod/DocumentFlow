//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data;

public abstract class BaseDocument : DocumentInfo, IBaseDocument, IComparable, IComparable<BaseDocument>
{
    private int? documentNumber;
    private DateTime? documentDate;

    [Write(false)]
    public Guid OrganizationId { get; set; }

    [Computed]
    public string? OrganizationName { get; set; }

    [DenyCopying]
    public DateTime? DocumentDate 
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

    [DenyCopying]
    public int? DocumentNumber 
    { 
        get => documentNumber; 
        set
        {
            if (documentNumber != value) 
            { 
                documentNumber = value;
                NotifyPropertyChanged();
            }
        }
    }

    public int CompareTo(object? obj)
    {
        if (obj is BaseDocument other)
        {
            return CompareTo(other);
        }

        throw new Exception($"{obj} должен быть типа {GetType().Name}");
    }

    public int CompareTo(BaseDocument? other)
    {
        if (other == null)
        {
            return 1;
        }

        DateTime date = DocumentDate ?? DateTime.MinValue;
        DateTime other_date = other.DocumentDate ?? DateTime.MinValue;

        int res = date.CompareTo(other_date);
        if (res == 0)
        {
            int num = DocumentNumber ?? 0;
            int other_num = other.DocumentNumber ?? 0;
            res = num.CompareTo(other_num);
        }

        return res;
    }

    public override string ToString() => $"№{DocumentNumber} от {DocumentDate:d}";
}
