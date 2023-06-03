//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2022.1.19
//  - добавлена колонка "Оплата"
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
// Версия 2023.6.3
//  - добавлена колонка PaymentDate
//  - добавлен метод BrowserCellStyle
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Styles;

namespace DocumentFlow.Entities.Waybills;

public class WaybillSaleBrowser : WaybillBrowser<WaybillSale>, IWaybillSaleBrowser
{
    public WaybillSaleBrowser(IWaybillSaleRepository repository, IPageManager pageManager, IDocumentFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        var payment_exists = CreateBoolean(x => x.PaymentExists, "Оплата", width: 100);
        var payment_date = CreateDateTime(x => x.PaymentDate, "Срок оплаты", width: 110, format: "dd.MM.yyyy");

        payment_date.AllowHeaderTextWrapping = true;
        
        AddColumns(new GridColumn[] { payment_exists, payment_date });
    }

    protected override string HeaderText => "Реализация";

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
