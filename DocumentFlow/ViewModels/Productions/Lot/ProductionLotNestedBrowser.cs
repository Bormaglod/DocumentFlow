//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class ProductionLotNestedBrowser : BaseProductionLotBrowser, IProductionLotNestedBrowser
{
    public ProductionLotNestedBrowser(IServiceProvider services, IPageManager pageManager, IProductionLotRepository repository, IConfiguration configuration)
        : base(services, pageManager, repository, configuration)
    {
        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var goods = CreateText(x => x.GoodsName, "Изделие", hidden: false);
        var calculation = CreateText(x => x.CalculationName, "Калькуляция", width: 150);
        var quantity = CreateNumeric(x => x.Quantity, "Количество", width: 150, hidden: false, decimalDigits: 3);
        var state = CreateText(x => x.StateName, "Состояние", width: 150);
        var execute_percent = CreateProgress(x => x.ExecutePercent, "Выполнено", 100);

        goods.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        state.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(quantity);

        AddColumns(new GridColumn[] { id, date, number, goods, calculation, quantity, state, execute_percent });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }
}