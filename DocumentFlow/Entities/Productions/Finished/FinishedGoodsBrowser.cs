//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//
// Версия 2023.1.5
//  - добавлен вызов MoveToEnd для перемещения в конец таблицы
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.2.23
//  - добавлена колонка "Ед. изм."
//  - для колонки "Количество" добавлен параметр decimalDigits равный 3
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Productions.Finished;

public class FinishedGoodsBrowser : BaseFinishedGoodsBrowser, IFinishedGoodsBrowser
{
    public FinishedGoodsBrowser(IFinishedGoodsRepository repository, IPageManager pageManager, IDocumentFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        AllowGrouping();

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

        MoveToEnd();
    }
}
