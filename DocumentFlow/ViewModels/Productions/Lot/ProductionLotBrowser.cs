﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class ProductionLotBrowser : BaseProductionLotBrowser, IProductionLotBrowser
{
    public ProductionLotBrowser(IServiceProvider services, IProductionLotRepository repository, IConfiguration configuration, IProductionLotFilter filter)
        : base(services, repository, configuration, filter: filter)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var order_date = CreateDateTime(x => x.OrderDate, "Дата", width: 150);
        var order_number = CreateNumeric(x => x.OrderNumber, "Номер", width: 100);
        var goods = CreateText(x => x.GoodsName, "Изделие", hidden: false);
        var calculation = CreateText(x => x.CalculationName, "Калькуляция", width: 150);
        var quantity = CreateNumeric(x => x.Quantity, "Количество", width: 150, hidden: false, decimalDigits: 3);
        var state = CreateText(x => x.StateName, "Состояние", width: 150);
        var execute_percent = CreateProgress(x => x.ExecutePercent, "Выполнено", 100);
        var sold = CreateBoolean(x => x.Sold, "Реализовано", 100);

        goods.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        state.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(quantity);

        CreateStackedColumns("Заказ", new GridColumn[] { order_date, order_number });

        AddColumns(new GridColumn[] { id, date, number, order_date, order_number, goods, calculation, quantity, state, sold, execute_percent });
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
