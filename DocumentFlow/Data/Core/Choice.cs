//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.02.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Data.Core;

public class Choice<T> : Identifier<T>, IChoice<T>
    where T : struct, IComparable
{
    public Choice(T id, string name)
    {
        this.Id = id;
        this.name = name;
    }

    public Choice(T id) : this(id, id.ToString() ?? string.Empty) { }

    public string name { get; }
    public override string ToString() => name;
}
