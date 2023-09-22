//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Models;

public interface IGrossPayrollRepository : IDocumentRepository<GrossPayroll>
{
    /// <summary>
    /// Метод возвращает список сотрудников и их заработную плату.
    /// </summary>
    /// <param name="payroll">Ведомость начисленной заработной платы.</param>
    /// <returns>Список сотрудников и их заработная плата.</returns>
    IReadOnlyList<GrossPayrollEmployee> GetSummaryWage(GrossPayroll payroll);

    /// <summary>
    /// Метод возвращает список сотрудников и их заработную плату.
    /// </summary>
    /// <param name="payroll">Идентификатор ведомость начисленной заработной платы.</param>
    /// <returns>Список сотрудников и их заработная плата.</returns>
    IReadOnlyList<GrossPayrollEmployee> GetSummaryWage(Guid payrollId);
}
