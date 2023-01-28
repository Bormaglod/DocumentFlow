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

using DocumentFlow.Controls.Editors;
using DocumentFlow.Entities.Employees.IncomeItems;
using DocumentFlow.Infrastructure;
using DocumentFlow.Entities.Balances;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Employees;

public class OurEmployeeEditor : BaseEmployeeEditor<OurEmployee>, IOurEmployeeEditor
{
    public OurEmployeeEditor(IOurEmployeeRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var items = new DfMultiSelectionComboBox("income_items", "Статьи дохода", 120, 500);
        items.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IIncomeItemRepository>();
            if (repo != null)
            {
                return repo.GetAllDefault();
            }

            return null;
        });

        AddControls(new Control[] { items });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IBalanceEmployeeBrowser, BalanceEmployee>();
    }
}