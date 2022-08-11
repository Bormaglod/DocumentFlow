//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using SqlKata;

using System.Data;

namespace DocumentFlow.Data.Infrastructure;

public enum Privilege { Select, Insert, Update, Delete }

public interface IRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    T GetById(Key id, bool fullInformation = true);

    T GetById(Key id, IDbConnection connection, bool fullInformation = true);

    T Get(Func<Query, Query>? callback = null);

    /// <summary>
    /// Возвращает записи указанной таблицы сформированной с помощью функции <see cref="GetBaseQuery"/>.
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<T> GetAll(Func<Query, Query>? callback = null);

    /// <summary>
    /// Возвращает записи указанной таблицы сформированной с помощью функции <see cref="GetDefaultQuery"/>
    /// </summary>
    /// <param name="filters"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    IReadOnlyList<T> GetAllDefault(IFilter? filter = null, Func<Query, Query>? callback = null);

    /// <summary>
    /// Возвращает записи указанной таблицы сформированной с помощью функции <see cref="GetBaseQuery"/>.
    /// Возвращаемые записи не имеют установленного флага deleted.
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    IReadOnlyList<T> GetAllValid(Func<Query, Query>? callback = null);
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
