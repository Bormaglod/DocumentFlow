//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Entities.PaymentOrders.Documents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Waybills;

public class WaybillReceiptEditor : WaybillEditor<WaybillReceipt, WaybillReceiptPrice, IWaybillReceiptPriceRepository>, IWaybillReceiptEditor
{
    public WaybillReceiptEditor(IWaybillReceiptRepository repository, IPageManager pageManager) : base(repository, pageManager) { }

    protected override void DoAfterRefreshData()
    {
        base.DoAfterRefreshData();
        RegisterNestedBrowser<IDocumentPaymentBrowser, DocumentPayment>();
    }
}
