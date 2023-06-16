//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.03.2019
//
// Версия 2022.8.21
//  - свойство code теперь не допускает значения null
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data
// Версия 2023.6.16
//  - в свойства Code и ItemName добавлен вызов NotifyPropertyChanged при
//    их изменении
//  - Атрибут Exclude заменен на AllowOperation(DataOperation.None)
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Data;

public abstract class Directory : DocumentInfo, IComparable, IComparable<Directory>, IDirectory
{
    private string code = string.Empty;
    private string? name;

    [AllowOperation(DataOperation.Add | DataOperation.Update)]
    public string Code
    {
        get => code;
        set
        {
            if (code != value)
            {
                code = value;
                NotifyPropertyChanged(nameof(Code));
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
                NotifyPropertyChanged(nameof(ItemName));
            }
        }
    }

    public Guid? ParentId { get; set; }

    [AllowOperation(DataOperation.None)]
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
