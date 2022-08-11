//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Data.Core;

public class Choice<T> : Identifier<T>, IChoice<T>
    where T : struct, IComparable
{
    public Choice(T id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public Choice(T id) : this(id, id.ToString() ?? string.Empty) { }

    public string name { get; }
    public override string ToString() => name;
}
