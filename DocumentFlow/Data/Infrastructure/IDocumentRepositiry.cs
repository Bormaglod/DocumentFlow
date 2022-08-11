//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2022
//-----------------------------------------------------------------------

using System.Data;

namespace DocumentFlow.Data.Infrastructure;

public interface IDocumentRepository<T> : IOwnedRepository<Guid, T>
    where T : IIdentifier<Guid>
{
    void Accept(Guid id);
    void Accept(Guid id, IDbTransaction transaction);
    void Accept(T entity);
    void Accept(T entity, IDbTransaction transaction);
    void CancelAcceptance(Guid id);
    void CancelAcceptance(Guid id, IDbTransaction transaction);
    void CancelAcceptance(T entity);
    void CancelAcceptance(T entity, IDbTransaction transaction);
}
