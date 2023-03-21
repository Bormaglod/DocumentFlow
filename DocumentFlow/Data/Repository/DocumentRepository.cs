//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.2.23
//  - добавлена ссылка на DocumentFlow.Core.Exceptions
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data.Repositiry
//
//-----------------------------------------------------------------------

using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using System.Data;

namespace DocumentFlow.Data.Repositiry;

public abstract class DocumentRepository<T> : OwnedRepository<Guid, T>, IDocumentRepository<T>
    where T : IIdentifier<Guid>
{
    protected DocumentRepository(IDatabase database) : base(database) { }

    public T AddAndAccept(T entity)
    {
        var id = Create(entity);
        Accept(id);
        return GetById(id);
    }

    public void Accept(T entity) => Accept(entity.Id);

    public void Accept(T entity, IDbTransaction transaction) => Accept(entity.Id, transaction);

    public void CancelAcceptance(Guid id)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            CancelAcceptance(id, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public void CancelAcceptance(Guid id, IDbTransaction transaction) => ExecuteSystemOperation(id, SystemOperation.Accept, false, transaction);

    public void CancelAcceptance(T entity) => CancelAcceptance(entity.Id);

    public void CancelAcceptance(T entity, IDbTransaction transaction) => CancelAcceptance(entity.Id, transaction);

    private void Accept(Guid id)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            Accept(id, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    private void Accept(Guid id, IDbTransaction transaction) => ExecuteSystemOperation(id, SystemOperation.Accept, true, transaction);
}
