//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//
// Версия 2023.2.11
//  - добавлен метод GetSummaryWage
//
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Wages.Core;

namespace DocumentFlow.Entities.Wages;

public interface IGrossPayrollEmployeeRepository : IPayrollEmployeeRepository<GrossPayrollEmployee>
{
    /// <summary>
    /// Метод возвращает список сотрудников и их заработную плату.
    /// </summary>
    /// <param name="payroll">Ведомость начисленной заработной платы.</param>
    /// <returns>Список сотрудников и их заработная плата.</returns>
    IReadOnlyList<GrossPayrollEmployee> GetSummaryWage(GrossPayroll payroll);
}
