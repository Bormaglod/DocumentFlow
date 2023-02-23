//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.08.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.2.23
//  - добавлена ссылка на DocumentFlow.Core.Exceptions
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

namespace DocumentFlow.Entities.Wages;

public class ReportCardEmployeeRepository : OwnedRepository<long, ReportCardEmployee>, IReportCardEmployeeRepository
{
    public ReportCardEmployeeRepository(IDatabase database) : base(database)
    {
    }

    public void PopulateReportCard(Guid reportCardId)
    {
        using var conn = Database.OpenConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            conn.Execute("call populate_report_card(:report_card_id)",
                     new { report_card_id = reportCardId },
                     transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e), e);
        }
    }

    public void PopulateReportCard(ReportCard reportCard) => PopulateReportCard(reportCard.id);

    protected override Query GetDefaultQuery(Query query, IFilter? filter = null)
    {
        return query
            .Select("report_card_employee.*")
            .Select("oe.item_name as employee_name")
            .SelectRaw("str_array_concat(labels, hours, ' ', date_part('days', date_trunc('month', now()) + '1 month'::interval - '1 day'::interval)::integer) as info")
            .Join("our_employee as oe", "oe.id", "report_card_employee.employee_id")
            .OrderBy("oe.item_name");
    }
}
