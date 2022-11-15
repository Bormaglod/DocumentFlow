﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.11.13
//  - добавлена реализация интерфейса IBalanceProductRepository
// Версия 2022.11.15
//  - метод UpdateMaterialRemaind перенесен в BalanceMaterialRepository
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using Humanizer;

using SqlKata;

namespace DocumentFlow.Entities.Balances;

public class BalanceProductRepository<T> : OwnedRepository<Guid, T>, IBalanceProductRepository<T>
    where T : BalanceProduct
{
    public BalanceProductRepository(IDatabase database) : base(database) { }

    public void ReacceptChangedDocument(T balance)
    {
        if (balance.owner_id == null)
        {
            return;
        }

        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            transaction.Connection.Execute($"call execute_system_operation(:id, 'accept'::system_operation, true, '{balance.document_type_code}')", new { id = balance.owner_id.Value }, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select($"{typeof(T).Name.Underscore()}.*")
            .SelectRaw("case when [amount] > 0 then [amount] else null end as [income]")
            .SelectRaw("case when [amount] < 0 then @[amount] else null end as [expense]")
            .SelectRaw("sum([amount]) over (order by [document_date], [document_number]) as [remainder]")
            .SelectRaw("sum([operation_summa] * sign([amount])) over (order by [document_date], [document_number]) as [monetary_balance]")
            .Select("dt.code as document_type_code")
            .Select("dt.document_name as document_type_name")
            .Join("document_type as dt", "dt.id", "document_type_id");
    }

    protected override Query GetQueryOwner(Query query, Guid owner_id) => query.Where($"{typeof(T).Name.Underscore()}.reference_id", owner_id);
}
