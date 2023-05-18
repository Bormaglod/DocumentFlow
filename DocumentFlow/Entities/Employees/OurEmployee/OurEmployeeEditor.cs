//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.01.2022
//
// Версия 2023.1.28
//  - DocumentFlow.Entities.Wages.IncomeItems заменен на
//    DocumentFlow.Entities.Employees.IncomeItems
//
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Balances;
using DocumentFlow.Entities.Employees.IncomeItems;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Employees;

public class OurEmployeeEditor : BaseEmployeeEditor<OurEmployee>, IOurEmployeeEditor
{
    public OurEmployeeEditor(IOurEmployeeRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        EditorControls
            .AddMultiSelectionComboBox(x => x.IncomeItems, "Статьи дохода", combo =>
                combo
                    .SetDataSource(GetItems)
                    .SetHeaderWidth(120)
                    .SetEditorWidth(500));
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IBalanceEmployeeBrowser, BalanceEmployee>();
    }

    private IEnumerable<IItem> GetItems() => Services.Provider.GetService<IIncomeItemRepository>()!.GetListUserDefined();
}