//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Wages;

public class Wage1cEmployeeRepository : OwnedRepository<long, Wage1cEmployee>, IWage1cEmployeeRepository
{
    public Wage1cEmployeeRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter = null)
    {
        return query
            .Select("wage1c_employee.*")
            .Select("oe.item_name as employee_name")
            .Join("our_employee as oe", "oe.id", "wage1c_employee.employee_id")
            .OrderBy("oe.item_name");
    }
}
