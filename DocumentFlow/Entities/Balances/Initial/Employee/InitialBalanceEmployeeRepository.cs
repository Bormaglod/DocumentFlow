//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceEmployeeRepository : DocumentRepository<InitialBalanceEmployee>, IInitialBalanceEmployeeRepository
{
    public InitialBalanceEmployeeRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("initial_balance_employee.*")
            .Select("e.item_name as employee_name")
            .Join("our_employee as e", "e.id", "initial_balance_employee.reference_id");
    }
}
