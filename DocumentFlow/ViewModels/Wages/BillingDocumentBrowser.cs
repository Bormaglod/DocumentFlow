//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public abstract class BillingDocumentBrowser<T> : BrowserPage<T>
    where T : BillingDocument
{
    public BillingDocumentBrowser(IServiceProvider services, IPageManager pageManager, IDocumentRepository<T> repository, IConfiguration configuration, IDocumentFilter? filter = null)
        : base(services, pageManager, repository, configuration, filter: filter)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var billing_range = CreateText(x => x.BillingRange, "Расчётный период", width: 160);
        var emps = CreateText(x => x.EmployeeNamesText, "Сотрудники");

        emps.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, date, number, billing_range, emps });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [billing_range] = ListSortDirection.Ascending
        });

        CreateGrouping()
            .Register(date, "DateByDay", "По дням", GridGroupingHelper.DocumentByDay);
    }
}
