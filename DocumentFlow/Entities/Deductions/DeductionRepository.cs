//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Deductions;

public class DeductionRepository : Repository<Guid, Deduction>, IDeductionRepository
{
    public DeductionRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.ParentId);
        ExcludeField(x => x.OwnerId);
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("*")
            .SelectRaw("case [base_calc] when 'person'::base_deduction then [value] else null end as [fix_value]")
            .SelectRaw("case [base_calc] when 'person'::base_deduction then null else [value] end as [percent_value]");
    }
}
