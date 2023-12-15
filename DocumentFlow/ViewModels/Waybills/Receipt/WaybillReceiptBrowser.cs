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

namespace DocumentFlow.ViewModels;

public class WaybillReceiptBrowser : WaybillBrowser<WaybillReceipt>, IWaybillReceiptBrowser
{
    public WaybillReceiptBrowser(IServiceProvider services, IWaybillReceiptRepository repository, IConfiguration configuration, IWaybillReceiptFilter filter)
        : base(services, repository, configuration, filter: filter)
    {
        var payment_exists = CreateBoolean(x => x.PaymentExists, "Оплата", 100);

        CreateStackedColumns("Заявка", new string[] { nameof(WaybillReceipt.PurchaseRequestDate), nameof(WaybillReceipt.PurchaseRequestNumber) });

        AddColumns(new GridColumn[] { payment_exists });
    }

    protected override GridColumn[] GetColumnsAfter(string mappingName)
    {
        if (mappingName == nameof(WaybillReceipt.DocumentNumber))
        {
            var date = CreateDateTime(x => x.PurchaseRequestDate, "Дата", width: 150, format: "dd.MM.yyyy");
            var number = CreateNumeric(x => x.PurchaseRequestNumber, "Номер", width: 100);
            return new GridColumn[] { date, number };
        }

        return base.GetColumnsAfter(mappingName);
    }
}
