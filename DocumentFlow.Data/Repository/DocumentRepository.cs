//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Repository;

public abstract class DocumentRepository<T> : OwnedRepository<Guid, T>, IDocumentRepository<T>
    where T : IIdentifier<Guid>
{
    protected DocumentRepository(IDatabase database) : base(database) { }

    public T AddAndAccept(T entity)
    {
        entity = Add(entity);
        ExecuteSystemOperation(entity.Id, SystemOperation.Accept, true);
        return entity;
    }

    public void Accept(T entity) => ExecuteSystemOperation(entity.Id, SystemOperation.Accept, true);

    public void CancelAcceptance(T entity) => ExecuteSystemOperation(entity.Id, SystemOperation.Accept, false);
}
