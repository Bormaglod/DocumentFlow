//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.Wages;

public interface IGrossPayrollRepository : IDocumentRepository<GrossPayroll>
{
    void CalculateEmployeeWages(Guid gross_id, int year, short month);
    void CalculateEmployeeWages(GrossPayroll grossPayroll);
}
