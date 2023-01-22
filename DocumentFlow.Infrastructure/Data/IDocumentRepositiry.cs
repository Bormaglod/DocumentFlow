//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2022
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Data.Infrastructure в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using System.Data;

namespace DocumentFlow.Infrastructure.Data;

public interface IDocumentRepository<T> : IOwnedRepository<Guid, T>
    where T : IIdentifier<Guid>
{
    T AddAndAccept(T entity);
    void Accept(T entity);
    void Accept(T entity, IDbTransaction transaction);
    void CancelAcceptance(Guid id);
    void CancelAcceptance(Guid id, IDbTransaction transaction);
    void CancelAcceptance(T entity);
    void CancelAcceptance(T entity, IDbTransaction transaction);
}
