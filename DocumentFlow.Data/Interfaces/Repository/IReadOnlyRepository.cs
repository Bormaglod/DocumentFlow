//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces.Filters;
using SqlKata;

using System.Data;

namespace DocumentFlow.Data.Interfaces.Repository;

public interface IReadOnlyRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    T Get(Key id, bool userDefindedQuery = true, bool ignoreAdjustedQuery = false);

    T Get(IDbConnection connection, Key id, bool userDefindedQuery = true, bool ignoreAdjustedQuery = false);

    T Get(Func<Query, Query>? callback = null);

    /// <summary>
    /// Возвращает записи указанной таблицы сформированной с помощью функции <see cref="GetBaseQuery"/>.
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<T> GetList(Func<Query, Query>? callback = null);

    /// <summary>
    /// Возвращает записи указанной таблицы сформированной с помощью функции <see cref="GetDefaultQuery"/>
    /// </summary>
    /// <param name="filters"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    IReadOnlyList<T> GetListUserDefined(IFilter? filter = null, Func<Query, Query>? callback = null);

    /// <summary>
    /// Возвращает записи указанной таблицы сформированной с помощью функции <see cref="GetBaseQuery"/>.
    /// Возвращаемые записи не имеют установленного флага deleted.
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    IReadOnlyList<T> GetListExisting(Func<Query, Query>? callback = null);

    bool HasPrivilege(params Privilege[] privilege);
}
