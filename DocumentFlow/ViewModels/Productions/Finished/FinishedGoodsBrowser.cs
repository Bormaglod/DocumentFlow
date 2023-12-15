//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Enums;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class FinishedGoodsBrowser : BaseFinishedGoodsBrowser, IFinishedGoodsBrowser
{
    public FinishedGoodsBrowser(IServiceProvider services, IFinishedGoodsRepository repository, IConfiguration configuration, IDocumentFilter filter)
        : base(services, repository, configuration, filter: filter)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var lot_date = CreateDateTime(x => x.LotDate, "Дата", width: 150);
        var lot_number = CreateNumeric(x => x.LotNumber, "Номер", width: 100);
        var goods = CreateText(x => x.GoodsName, "Изделие", hidden: false);
        var quantity = CreateNumeric(x => x.Quantity, "Количество", width: 150, hidden: false, decimalDigits: 3);
        var measurement = CreateText(x => x.MeasurementName, "Ед. изм.", width: 100);
        var price = CreateCurrency(x => x.Price, "1 ед. изм.", width: 100);
        var cost = CreateCurrency(x => x.ProductCost, "Всего", width: 100);

        goods.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(quantity)
            .AsSummary(price, SummaryColumnFormat.Currency)
            .AsSummary(cost, SummaryColumnFormat.Currency);

        CreateStackedColumns("Партия", new GridColumn[] { lot_date, lot_number });
        CreateStackedColumns("Себестоимость", new GridColumn[] { price, cost });

        AddColumns(new GridColumn[] { id, date, number, lot_date, lot_number, goods, quantity, measurement, price, cost });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        CreateGrouping()
            .Register(date, "DateByDay", "По дням", GridGroupingHelper.DocumentByDay);

        MoveToEnd();
    }
}
