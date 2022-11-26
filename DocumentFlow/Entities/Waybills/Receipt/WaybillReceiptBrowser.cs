//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2022.11.26
//  - добавлен метод GetColumnsAfter
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Entities.Waybills;

public class WaybillReceiptBrowser : WaybillBrowser<WaybillReceipt>, IWaybillReceiptBrowser
{
    public WaybillReceiptBrowser(IWaybillReceiptRepository repository, IPageManager pageManager, IDocumentFilter filter)
        : base(repository, pageManager, filter: filter)
    {
        CreateStackedColumns("Заявка", new string[] { "purchase_request_date", "purchase_request_number" });
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
