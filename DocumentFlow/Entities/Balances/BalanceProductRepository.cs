//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using Humanizer;

using SqlKata;

namespace DocumentFlow.Entities.Balances;

public class BalanceProductRepository<T> : OwnedRepository<Guid, T>
    where T : BalanceProduct
{
    public BalanceProductRepository(IDatabase database) : base(database) { }

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
