//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class InitialBalanceEmployeeBrowser : BrowserPage<InitialBalanceEmployee>, IInitialBalanceEmployeeBrowser
{
    public InitialBalanceEmployeeBrowser(IServiceProvider services, IPageManager pageManager, IInitialBalanceEmployeeRepository repository, IConfiguration configuration)
        : base(services, pageManager, repository, configuration)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var contractor_name = CreateText(x => x.EmployeeName, "Сотрудник");
        var our_debt = CreateCurrency(x => x.OurDebt, "Наш", width: 120);
        var employee_debt = CreateCurrency(x => x.EmployeeDebt, "Сотрудника", width: 120);

        contractor_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        CreateStackedColumns("Долг", new GridColumn[] { our_debt, employee_debt });

        AddColumns(new GridColumn[] { id, date, number, contractor_name, our_debt, employee_debt });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }
}
