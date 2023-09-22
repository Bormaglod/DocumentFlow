//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class PayrollPaymentRepository : DocumentRepository<PayrollPayment>, IPayrollPaymentRepository
{
    public PayrollPaymentRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        return query
            .Select("payroll_payment.*")
            .Select("p.document_number as payroll_number")
            .Select("p.document_date as payroll_date")
            .Select("o.item_name as organization_name")
            .Select("gp.{billing_year, billing_month}")
            .Join("organization as o", "o.id", "payroll_payment.organization_id")
            .Join("payroll as p", "p.id", "payroll_payment.owner_id")
            .LeftJoin("gross_payroll as gp", "gp.id", "p.owner_id");
    }
}
