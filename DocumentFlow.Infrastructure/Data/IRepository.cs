//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Data.Infrastructure в DocumentFlow.Infrastructure.Data
// Версия 2023.5.18
//  - метод GetAll переименован в GetList
//  - метод GetAllDefault переименован в GetListUserDefined
//  - метод GetAllValid переименован в GetListExisting
//  - методы GetById переименованы в Get
//
//-----------------------------------------------------------------------

using SqlKata;

using System.Data;

namespace DocumentFlow.Infrastructure.Data;

public enum Privilege { Select, Insert, Update, Delete }

public interface IRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    T Get(Key id, bool fullInformation = true);

    T Get(Key id, IDbConnection connection, bool fullInformation = true);

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
    T Add();
    T Add(IDbTransaction transaction);
    T Add(T entity);
    T Add(T entity, IDbTransaction transaction);
    T CopyFrom(T original);
    T CopyFrom(T original, IDbTransaction transaction);
    void Update(T entity);
    void Update(T entity, IDbTransaction transaction);
    void Delete(Key id);
    void Delete(Key id, IDbTransaction transaction);
    void Delete(T entity);
    void Delete(T entity, IDbTransaction transaction);
    void Undelete(Key id);
    void Undelete(Key id, IDbTransaction transaction);
    void Undelete(T entity);
    void Undelete(T entity, IDbTransaction transaction);
    void Wipe(Key id);
    void Wipe(Key id, IDbTransaction transaction);
    void Wipe(T entity);
    void Wipe(T entity, IDbTransaction transaction);
    void WipeAll();
    void WipeAll(IDbTransaction transaction);
    void WipeAll(Guid? owner);
    void WipeAll(Guid? owner, IDbTransaction transaction);
    bool HasPrivilege(string table, params Privilege[] privilege);
    bool HasPrivilege(params Privilege[] privilege);
}
