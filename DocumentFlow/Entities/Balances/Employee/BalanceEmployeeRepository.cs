﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Balances;

public class BalanceEmployeeRepository : OwnedRepository<Guid, BalanceEmployee>, IBalanceEmployeeRepository
{
    public BalanceEmployeeRepository(IDatabase database) : base(database) { }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("balance_employee.*")
            .SelectRaw("case when [amount] > 0 then [operation_summa] else null end as [employee_debt]")
            .SelectRaw("case when [amount] < 0 then [operation_summa] else null end as [organization_debt]")
            .SelectRaw("sum([operation_summa] * abs([amount])) over (order by [document_date], [document_number]) as [debt]")
            .Select("dt.code as document_type_code")
            .Select("dt.document_name as document_type_name")
            .Join("document_type as dt", "dt.id", "document_type_id");
    }

    protected override Query GetQueryOwner(Query query, Guid owner_id) => query.Where($"balance_employee.reference_id", owner_id);
}