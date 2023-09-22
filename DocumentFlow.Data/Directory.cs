//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.03.2019
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data;

public abstract class Directory : DocumentInfo, IComparable, IComparable<Directory>, IDirectory
{
    private string code = string.Empty;
    private string? name;
    private Guid? parentId;

    [DenyCopying]
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

    public string? ItemName 
    {
        get => name;
        set
        {
            if (name != value) 
            { 
                name = value;
                NotifyPropertyChanged();
            }
        }
    }

    public Guid? ParentId
    {
        get => parentId;
        set
        {
            if (parentId != value) 
            { 
                parentId = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Write(false)]
    public bool IsFolder { get; set; }

    public override string ToString() => string.IsNullOrEmpty(ItemName) ? Code : ItemName;

    public int CompareTo(object? obj)
    {
        if (obj is Directory other)
        {
            return CompareTo(other);
        }

        throw new Exception($"{obj} должен быть типа {GetType().Name}");
    }

    public int CompareTo(Directory? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (IsFolder != other.IsFolder)
        {
            return IsFolder ? -1 : 1;
        }

        return ToString().CompareTo(other.ToString());
    }
}
