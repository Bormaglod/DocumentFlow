//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class PayrollBrowser : BrowserPage<Payroll>, IPayrollBrowser
{
    public PayrollBrowser(IServiceProvider services, IPageManager pageManager, IPayrollRepository repository, IConfiguration configuration, IDocumentFilter filter)
        : base(services, pageManager, repository, configuration, filter: filter)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var billing_range = CreateText(x => x.BillingRange, "Расчётный период", width: 160);
        var emps = CreateText(x => x.EmployeeNamesText, "Сотрудники");
        var wage = CreateCurrency(x => x.Wage, "Сумма к выдаче", width: 150);

        emps.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, date, number, billing_range, emps, wage });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [billing_range] = ListSortDirection.Ascending
        });

        CreateGrouping()
            .Register(date, "DateByDay", "По дням", GridGroupingHelper.DocumentByDay);
    }
}
