//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceEmployeeEditor : DocumentEditor<InitialBalanceEmployee>, IInitialBalanceEmployeeEditor
{
    private Dictionary<decimal, string> debtChoice = new()
    {
        [-1] = "Наш долг",
        [1] = "Долг сотрудника"
    };

    public InitialBalanceEmployeeEditor(IInitialBalanceEmployeeRepository repository, IPageManager pageManager) 
        : base(repository, pageManager, true) 
    {
        AddControls(new Control[]
        {
            CreateDirectorySelectBox<OurEmployee, IOurEmployeeEditor>(x => x.ReferenceId, "Сотрудник", 100, 400, data: GetEmployees),
            CreateCurrencyTextBox(x => x.OperationSumma, "Сумма", 100, 100),
            CreateChoice(x => x.Amount, "Долг", 100, 200, choices: debtChoice)
        });
    }

    private IEnumerable<OurEmployee> GetEmployees() => Services.Provider.GetService<IOurEmployeeRepository>()!.GetAllValid();
}
