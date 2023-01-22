//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Calculations;

public class CalculationDeductionRepository : CalculationItemRepository<CalculationDeduction>, ICalculationDeductionRepository
{
    public CalculationDeductionRepository(IDatabase database) : base(database) { }

    public void RecalculatePrices(Guid calculate_id)
    {
        throw new NotImplementedException();
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        return query
            .Select("calculation_deduction.*")
            .Select("d.item_name as deduction_name")
            .Select("c.item_name as calculation_name")
            .Select("d.base_calc as calculation_base")
            .Join("calculation as c", "c.id", "calculation_deduction.owner_id")
            .LeftJoin("deduction as d", "d.id", "calculation_deduction.item_id");
    }
}
