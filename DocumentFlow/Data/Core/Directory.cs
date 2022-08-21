//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.03.2019
//
// Версия 2022.8.21
//  - свойство code теперь не допускает значения null
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Data.Core;

public abstract class Directory : DocumentInfo, IComparable, IComparable<Directory>, IDirectory
{
    [DataOperation(DataOperation.Add | DataOperation.Update)]
    public string code { get; set; } = string.Empty;
    public string? item_name { get; set; }
    public Guid? parent_id { get; set; }

    [Exclude]
    public bool is_folder { get; set; }
    public override string ToString() => string.IsNullOrEmpty(item_name) ? code : item_name;

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

        if (is_folder != other.is_folder)
        {
            return is_folder ? -1 : 1;
        }

        return ToString().CompareTo(other.ToString());
    }
}
