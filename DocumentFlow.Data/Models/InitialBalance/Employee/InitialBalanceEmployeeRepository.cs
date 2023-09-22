//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class InitialBalanceEmployeeRepository : DocumentRepository<InitialBalanceEmployee>, IInitialBalanceEmployeeRepository
{
    public InitialBalanceEmployeeRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("initial_balance_employee.*")
            .Select("e.item_name as employee_name")
            .Join("our_employee as e", "e.id", "initial_balance_employee.reference_id");
    }
}
