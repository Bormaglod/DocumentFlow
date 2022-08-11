//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

using System.Data;

namespace DocumentFlow.Data.Core;

public abstract class DocumentRepository<T> : OwnedRepository<Guid, T>, IDocumentRepository<T>
    where T : IIdentifier<Guid>
{
    protected DocumentRepository(IDatabase database) : base(database) { }

    public void Accept(Guid id)
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

    public void Accept(Guid id, IDbTransaction transaction) => ExecuteSystemOperation(id, SystemOperation.Accept, true, transaction);

    public void Accept(T entity) => Accept(entity.id);

    public void Accept(T entity, IDbTransaction transaction) => Accept(entity.id, transaction);

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

    public void CancelAcceptance(T entity) => CancelAcceptance(entity.id);

    public void CancelAcceptance(T entity, IDbTransaction transaction) => CancelAcceptance(entity.id, transaction);
}
