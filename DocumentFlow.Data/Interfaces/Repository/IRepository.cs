//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using System.Data;

namespace DocumentFlow.Data.Interfaces.Repository;

public interface IRepository<Key, T> : IReadOnlyRepository<Key, T>
    where Key : struct, IComparable
    where T : IIdentifier<Key>
{
    T Add(T entity);
    T CopyFrom(T original);
    T CopyFrom(IDbConnection connection, T original, IDbTransaction? transaction = null);
    void Update(T entity);
    void Update(IDbConnection connection, T entity, IDbTransaction? transaction = null);
    void Delete(T entity);
    void Undelete(T entity);
    void Wipe(T entity);
    void WipeAll();
    void WipeAll(Guid owner);
}
