//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Productions.Finished;

public class FinishedGoodsNestedBrowser : BaseFinishedGoodsBrowser, IFinishedGoodsNestedBrowser
{
    public FinishedGoodsNestedBrowser(IFinishedGoodsRepository repository, IPageManager pageManager)
        : base(repository, pageManager)
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
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

        AddColumns(new GridColumn[] { id, date, number, goods, quantity, price, cost });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }
}