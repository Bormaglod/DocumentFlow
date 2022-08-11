//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceGoodsBrowser : Browser<InitialBalanceGoods>, IInitialBalanceGoodsBrowser
{
    public InitialBalanceGoodsBrowser(IInitialBalanceGoodsRepository repository, IPageManager pageManager)
        : base(repository, pageManager)
    {
        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var material_code = CreateText(x => x.goods_code, "Артикул", width: 150);
        var material_name = CreateText(x => x.goods_name, "Продукция");
        var operation_summa = CreateCurrency(x => x.operation_summa, "Сумма", width: 120);
        var amount = CreateNumeric(x => x.amount, "Количество", width: 150, decimalDigits: 3);

        material_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        amount.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, date, number, material_code, material_name, operation_summa, amount });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Нач. остатки";
}
