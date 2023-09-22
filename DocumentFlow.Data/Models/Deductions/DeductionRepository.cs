//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class DeductionRepository : Repository<Guid, Deduction>, IDeductionRepository
{
    public DeductionRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("*")
            .SelectRaw("case base_calc when 'person'::base_deduction then value else null end as fix_value")
            .SelectRaw("case base_calc when 'person'::base_deduction then null else value end as percent_value");
    }
}
