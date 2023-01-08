//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2022.11.26
//  - добавлен метод GetColumnsAfter
// Версия 2022.12.18
//  - добавлена колонка "Оплата"
// Версия 2022.12.30
//  - IDocumentFilter заменен на IWaybillReceiptFilter
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Entities.Waybills;

public class WaybillReceiptBrowser : WaybillBrowser<WaybillReceipt>, IWaybillReceiptBrowser
{
    public WaybillReceiptBrowser(IWaybillReceiptRepository repository, IPageManager pageManager, IWaybillReceiptFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        var payment_exists = CreateBoolean(x => x.payment_exists, "Оплата", 100);

        CreateStackedColumns("Заявка", new string[] { "purchase_request_date", "purchase_request_number" });

        AddColumns(new GridColumn[] { payment_exists });
    }

    protected override GridColumn[] GetColumnsAfter(string mappingName)
    {
        if (mappingName == "document_number")
        {
            var date = CreateDateTime(x => x.purchase_request_date, "Дата", width: 150, format: "dd.MM.yyyy");
            var number = CreateNumeric(x => x.purchase_request_number, "Номер", width: 100);
            return new GridColumn[] { date, number };
        }

        return base.GetColumnsAfter(mappingName);
    }

    protected override string HeaderText => "Поступление";
}
