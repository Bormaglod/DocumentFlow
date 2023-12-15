//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class InitialBalanceGoodsBrowser : BrowserPage<InitialBalanceGoods>, IInitialBalanceGoodsBrowser
{
    public InitialBalanceGoodsBrowser(IServiceProvider services, IInitialBalanceGoodsRepository repository, IConfiguration configuration)
        : base(services, repository, configuration)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var material_code = CreateText(x => x.GoodsCode, "Артикул", width: 150);
        var material_name = CreateText(x => x.GoodsName, "Продукция");
        var operation_summa = CreateCurrency(x => x.OperationSumma, "Сумма", width: 120);
        var amount = CreateNumeric(x => x.Amount, "Количество", width: 150, decimalDigits: 3);

        material_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        amount.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, date, number, material_code, material_name, operation_summa, amount });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }
}
