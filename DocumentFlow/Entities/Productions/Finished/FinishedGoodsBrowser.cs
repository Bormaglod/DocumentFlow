//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//
// Версия 2023.1.5
//  - добавлен вызов MoveToEnd для перемещения в конец таблицы
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Productions.Finished;

public class FinishedGoodsBrowser : BaseFinishedGoodsBrowser, IFinishedGoodsBrowser
{
    public FinishedGoodsBrowser(IFinishedGoodsRepository repository, IPageManager pageManager, IDocumentFilter filter)
        : base(repository, pageManager, filter: filter)
    {
        AllowGrouping();

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var lot_date = CreateDateTime(x => x.lot_date, "Дата", width: 150);
        var lot_number = CreateNumeric(x => x.lot_number, "Номер", width: 100);
        var goods = CreateText(x => x.goods_name, "Изделие", hidden: false);
        var quantity = CreateNumeric(x => x.quantity, "Количество", width: 150, hidden: false);
        var price = CreateCurrency(x => x.price, "1 изд.", width: 100);
        var cost = CreateCurrency(x => x.product_cost, "Всего", width: 100);

        goods.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(quantity)
            .AsSummary(price, SummaryColumnFormat.Currency)
            .AsSummary(cost, SummaryColumnFormat.Currency);

        CreateStackedColumns("Партия", new GridColumn[] { lot_date, lot_number });
        CreateStackedColumns("Себестоимость", new GridColumn[] { price, cost });

        AddColumns(new GridColumn[] { id, date, number, lot_date, lot_number, goods, quantity, price, cost });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        MoveToEnd();
    }
}
