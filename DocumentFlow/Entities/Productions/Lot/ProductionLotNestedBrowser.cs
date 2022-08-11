//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Productions.Lot;

public class ProductionLotNestedBrowser : BaseProductionLotBrowser, IProductionLotNestedBrowser
{
    public ProductionLotNestedBrowser(IProductionLotRepository repository, IPageManager pageManager)
        : base(repository, pageManager)
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var goods = CreateText(x => x.goods_name, "Изделие", hidden: false);
        var calculation = CreateText(x => x.calculation_name, "Калькуляция", width: 150);
        var quantity = CreateNumeric(x => x.quantity, "Количество", width: 150, hidden: false, decimalDigits: 3);

        goods.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(quantity);

        AddColumns(new GridColumn[] { id, date, number, goods, calculation, quantity });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }
}