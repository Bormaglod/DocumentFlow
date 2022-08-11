//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2021
//-----------------------------------------------------------------------

using SqlKata;

namespace DocumentFlow.Data.Infrastructure;

public interface IFilter
{
    Control Control { get; }
    Query? CreateQuery<T>();
}
