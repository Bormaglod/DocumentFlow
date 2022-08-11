//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.02.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Infrastructure;

public interface IChoice<T> : IIdentifier<T>
    where T : struct, IComparable
{
    string name { get; }
}
