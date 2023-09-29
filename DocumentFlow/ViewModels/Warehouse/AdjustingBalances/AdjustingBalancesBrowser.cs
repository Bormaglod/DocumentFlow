//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class AdjustingBalancesBrowser : BrowserPage<AdjustingBalances>, IAdjustingBalancesBrowser
{
    public AdjustingBalancesBrowser(IServiceProvider services, IPageManager pageManager, IAdjustingBalancesRepository repository, IConfiguration configuration)
        : base(services, pageManager, repository, configuration)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var material_name = CreateText(x => x.MaterialName, "Материал");
        var quantity = CreateNumeric(x => x.Quantity, "Кол-во", width: 200);

        material_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, date, number, material_name, quantity });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        CreateGrouping()
            .Add(material_name)
            .Register(date, "DateByDay", "По дням", GridGroupingHelper.DocumentByDay);

        MoveToEnd();
    }
}
