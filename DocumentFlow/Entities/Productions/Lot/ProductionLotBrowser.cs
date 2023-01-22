//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//
// Версия 2022.8.28
//  - добавлено столбец state_name и метод BrowserCellStyle
//  - добавлено столбец execute_percent
// Версия 2022.8.29
//  - метод BrowserCellStyle перенесен в BaseProductionLotBrowser
// Версия 2023.1.5
//  - параметр IDocumentFilter заменен на IProductionLotFilter
//  - добавлена установка диапазона дат для фильтра IDocumentFilter
//  - добавлен вызов MoveToEnd для перемещения в конец таблицы
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Productions.Lot;

public class ProductionLotBrowser : BaseProductionLotBrowser, IProductionLotBrowser
{
    public ProductionLotBrowser(IProductionLotRepository repository, IPageManager pageManager, IProductionLotFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        AllowGrouping();

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var order_date = CreateDateTime(x => x.order_date, "Дата", width: 150);
        var order_number = CreateNumeric(x => x.order_number, "Номер", width: 100);
        var goods = CreateText(x => x.goods_name, "Изделие", hidden: false);
        var calculation = CreateText(x => x.calculation_name, "Калькуляция", width: 150);
        var quantity = CreateNumeric(x => x.quantity, "Количество", width: 150, hidden: false, decimalDigits: 3);
        var state = CreateText(x => x.state_name, "Состояние", width: 150);
        var execute_percent = CreateProgress(x => x.execute_percent, "Выполнено", 100);

        goods.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        state.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(quantity);

        CreateStackedColumns("Заказ", new GridColumn[] { order_date, order_number });

        AddColumns(new GridColumn[] { id, date, number, order_date, order_number, goods, calculation, quantity, state, execute_percent });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        filter?.SetDateRange(DateRange.CurrentYear);

        MoveToEnd();
    }
}
