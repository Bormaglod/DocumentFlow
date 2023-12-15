//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Models;
using DocumentFlow.Settings;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.Input.Enums;

namespace DocumentFlow.ViewModels;

public class GoodsBrowser : ProductBrowser<Goods>, IGoodsBrowser
{
    public GoodsBrowser(
        IServiceProvider services,
        IGoodsRepository repository,
        IDocumentRefsRepository documentRefs,
        IConfiguration configuration,
        IProductRowHeader productRowHeader,
        IBreadcrumb navigator,
        IOptions<LocalSettings> options) 
        : base(services, repository, documentRefs, configuration, productRowHeader, navigator, options) 
    {
        var is_service = CreateBoolean(x => x.IsService, "Услуга", width: 80);

        is_service.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;

        AddColumns(new GridColumn[] { is_service });

        var db = services.GetRequiredService<IDatabase>();
        if (db.HasPrivilege("materials", Privilege.Select))
        {
            var cost = CreateCurrency(x => x.CostPrice, "Себестоимость", width: 120);
            var profit_percent = CreateNumeric(x => x.ProfitPercent, "Прибыль, %", width: 110, mode: FormatMode.Percent, decimalDigits: 2);
            var profit_value = CreateCurrency(x => x.ProfitValue, "Прибыль", width: 100);
            var balance = CreateNumeric(x => x.ProductBalance, "Тек. остаток", 120, decimalDigits: 3);
            var approval = CreateDateTime(x => x.DateApproval, "Дата утв.", 100, format: "dd.MM.yyyy");

            AddColumns(new GridColumn[] { cost, profit_percent, profit_value, balance, approval });
        }

        ShowToolTip = (p) => p?.Note ?? string.Empty;
    }
}
