//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Infrastructure;

public interface IEntity<T> : IIdentifier<T>
    where T : struct, IComparable
{
    Guid? owner_id { get; set; }
}
