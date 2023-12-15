//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Styles;

namespace DocumentFlow.ViewModels;

public class WaybillSaleBrowser : WaybillBrowser<WaybillSale>, IWaybillSaleBrowser
{
    public WaybillSaleBrowser(IServiceProvider services, IWaybillSaleRepository repository, IConfiguration configuration, IDocumentFilter filter)
        : base(services, repository, configuration, filter: filter)
    {
        var payment_exists = CreateBoolean(x => x.PaymentExists, "Оплата", width: 100);
        var payment_date = CreateDateTime(x => x.PaymentDate, "Срок оплаты", width: 110, format: "dd.MM.yyyy");

        payment_date.AllowHeaderTextWrapping = true;
        
        AddColumns(new GridColumn[] { payment_exists, payment_date });
    }

    protected override void BrowserCellStyle(WaybillSale document, string column, CellStyleInfo style)
    {
        base.BrowserCellStyle(document, column, style);
        if (document.PaymentDate != null && column == nameof(document.PaymentDate)) 
        { 
            if (document.PaymentExists.HasValue && document.PaymentExists.Value)
            {
                style.TextColor = Color.Green;
            }
            else if (document.PaymentDate < DateOnly.FromDateTime(DateTime.Now))
            {
                style.TextColor = Color.Red;
            }
        }
    }
}
