//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class ReturnMaterialsBrowser : BrowserPage<ReturnMaterials>, IReturnMaterialsBrowser
{
    public ReturnMaterialsBrowser(IServiceProvider services, IReturnMaterialsRepository repository, IConfiguration configuration, IDocumentFilter filter)
        : base(services, repository, configuration, filter: filter)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var order_date = CreateDateTime(x => x.OrderDate, "Дата", width: 80, format: "dd.MM.yyyy");
        var order_number = CreateNumeric(x => x.OrderNumber, "Номер", width: 80);
        var contractor = CreateText(x => x.ContractorName, "Контрагент");
        var contract = CreateText(x => x.ContractName, "Договор", width: 200, visible: false);

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, date, number, order_date, order_number, contractor, contract });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        CreateStackedColumns("Заказ", new GridColumn[] { order_date, order_number });

        CreateGrouping();
        MoveToEnd();
    }
}
