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
    private readonly Dictionary<decimal, string> debtChoice = new()
    {
        [-1] = "Наш долг",
        [1] = "Долг сотрудника"
    };

    public InitialBalanceEmployeeEditor(IInitialBalanceEmployeeRepository repository, IPageManager pageManager) 
        : base(repository, pageManager, true) 
    {
        EditorControls
            .CreateDirectorySelectBox<OurEmployee>(x => x.ReferenceId, "Сотрудник", (select) =>
                select
                    .SetDataSource(GetEmployees)
                    .Editor<IOurEmployeeEditor>()
                    .SetEditorWidth(400))
            .CreateCurrencyTextBox(x => x.OperationSumma, "Сумма")
            .CreateChoice<decimal>(x => x.Amount, "Долг", (choice) =>
                choice
                    .SetChoiceValues(debtChoice)
                    .SetEditorWidth(200));
    }

    private IEnumerable<OurEmployee> GetEmployees() => Services.Provider.GetService<IOurEmployeeRepository>()!.GetAllValid();
}
