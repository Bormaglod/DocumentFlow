//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Products;

public class GoodsBrowser : ProductBrowser<Goods>, IGoodsBrowser
{
    public GoodsBrowser(IGoodsRepository repository, IPageManager pageManager, IProductRowHeader productRowHeader, IBreadcrumb navigator, IStandaloneSettings settings) 
        : base(repository, pageManager, productRowHeader, navigator: navigator, settings: settings) 
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.Code, "Код", width: 120);
        var name = CreateText(x => x.ItemName, "Наименование", hidden: false);
        var price = CreateCurrency(x => x.Price, "Цена", width: 80);
        var vat = CreateNumeric(x => x.Vat, "НДС", width: 80, mode: FormatMode.Percent);
        var weight = CreateNumeric(x => x.Weight, "Вес, г", width: 80, decimalDigits: 3);
        var is_service = CreateBoolean(x => x.IsService, "Услуга", width: 80);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        vat.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
        is_service.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

        AddColumns(new GridColumn[] { id, code, name, price, vat, weight, is_service });

        if (repository.HasPrivilege("materials", Privilege.Select))
        {
            var cost = CreateCurrency(x => x.CostPrice, "Себестоимость", width: 120);
            var profit_percent = CreateNumeric(x => x.ProfitPercent, "Прибыль, %", width: 110, mode: FormatMode.Percent, decimalDigits: 2);
            var profit_value = CreateCurrency(x => x.ProfitValue, "Прибыль", width: 100);
            var balance = CreateNumeric(x => x.ProductBalance, "Тек. остаток", 120, decimalDigits: 3);
            var approval = CreateDateTime(x => x.DateApproval, "Дата утв.", 100, format: "dd.MM.yyyy");

            AddColumns(new GridColumn[] { cost, profit_percent, profit_value, balance, approval });
        }
        
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });

        ShowToolTip = (p) => p?.Note ?? string.Empty;

        AllowDrawPreviewRow(height: 125);
    }

    protected override string HeaderText => "Продукция";
}
