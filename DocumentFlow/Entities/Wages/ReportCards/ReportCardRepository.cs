//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using SqlKata;

namespace DocumentFlow.Entities.Wages;

public class ReportCardRepository : DocumentRepository<ReportCard>, IReportCardRepository
{
    public ReportCardRepository(IDatabase database) : base(database)
    {
        ExcludeField(x => x.owner_id);
    }

    protected override Query GetDefaultQuery(Query query, IFilter? filter)
    {
        var q = new Query("report_card_employee as rce")
            .Select("rce.owner_id")
            .SelectRaw("array_agg(oe.item_name) as employee_names")
            .Join("our_employee as oe", "oe.id", "rce.employee_id")
            .GroupBy("rce.owner_id");

        return query
            .Select("report_card.*")
            .Select("o.item_name as organization_name")
            .Select("q.employee_names")
            .Join("organization as o", "o.id", "report_card.organization_id")
            .LeftJoin(q.As("q"), j => j.On("q.owner_id", "report_card.id"));
    }
}
