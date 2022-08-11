//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Deductions;

public class DeductionRepository : Repository<Guid, Deduction>, IDeductionRepository
{
    public DeductionRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.parent_id);
        ExcludeField(x => x.owner_id);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("*")
            .SelectRaw("case [base_calc] when 'person'::base_deduction then [value] else null end as [fix_value]")
            .SelectRaw("case [base_calc] when 'person'::base_deduction then null else [value] end as [percent_value]");
    }
}
