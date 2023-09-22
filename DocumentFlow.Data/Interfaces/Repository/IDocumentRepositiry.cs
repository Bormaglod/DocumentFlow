//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2022
//-----------------------------------------------------------------------

using System.Data;

namespace DocumentFlow.Data.Interfaces.Repository;

public interface IDocumentRepository<T> : IOwnedRepository<Guid, T>
    where T : IIdentifier<Guid>
{
    T AddAndAccept(T entity);
    void Accept(T entity);
    void CancelAcceptance(T entity);
}
