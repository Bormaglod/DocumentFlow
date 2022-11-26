//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.07.2022
//
// Версия 2022.11.26
//  - изменен метод GetColumnsAfter
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Waybills;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Entities.Productions.Processing;

public class WaybillProcessingBrowser : WaybillBrowser<WaybillProcessing>, IWaybillProcessingBrowser
{
    public WaybillProcessingBrowser(IWaybillProcessingRepository repository, IPageManager pageManager, IDocumentFilter filter)
        : base(repository, pageManager, filter: filter)
    {
        CreateStackedColumns("Заказ", new string[] { "order_date", "order_number" });
    }

    protected override GridColumn[] GetColumnsAfter(string MappingName)
    {
        if (MappingName == "invoice_number")
        {
            var order_date = CreateDateTime(x => x.order_date, "Дата", width: 150);
            var order_number = CreateNumeric(x => x.order_number, "Номер", width: 100);
            return new GridColumn[] { order_date, order_number };
        }
        
        return base.GetColumnsAfter(MappingName);
    }

    protected override string HeaderText => "Поступление в переработку";
}
