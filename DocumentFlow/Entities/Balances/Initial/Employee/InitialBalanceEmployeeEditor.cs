//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceEmployeeEditor : DocumentEditor<InitialBalanceEmployee>, IInitialBalanceEmployeeEditor
{
    public InitialBalanceEmployeeEditor(IInitialBalanceEmployeeRepository repository, IPageManager pageManager) 
        : base(repository, pageManager, true) 
    {
        var emp = new DfDirectorySelectBox<OurEmployee>("reference_id", "Сотрудник", 100, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IOurEmployeeEditor, OurEmployee>(t)
        };

        var debt = new DfChoice<decimal>("amount", "Долг", 100, 200);
        var op_summa = new DfCurrencyTextBox("operation_summa", "Сумма", 100, 100);

        emp.SetDataSource(() => Services.Provider.GetService<IOurEmployeeRepository>()?.GetAllValid());

        debt.SetChoiceValues(new Dictionary<decimal, string>()
        {
            [-1] = "Наш долг",
            [1] = "Долг сотрудника"
        });

        AddControls(new Control[]
        {
            emp,
            op_summa,
            debt
        });
    }
}
