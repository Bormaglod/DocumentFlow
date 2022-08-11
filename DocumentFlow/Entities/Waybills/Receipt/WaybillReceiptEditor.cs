//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.PaymentOrders.Documents;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Waybills;

public class WaybillReceiptEditor : WaybillEditor<WaybillReceipt, WaybillReceiptPrice>, IWaybillReceiptEditor
{
    public WaybillReceiptEditor(IWaybillReceiptRepository repository, IPageManager pageManager) : base(repository, pageManager) { }

    protected override IOwnedRepository<long, WaybillReceiptPrice> GetDetailsRepository()
    {
        return Services.Provider.GetService<IWaybillReceiptPriceRepository>()!;
    }

    protected override void DoAfterRefreshData()
    {
        base.DoAfterRefreshData();
        RegisterNestedBrowser<IDocumentPaymentBrowser, DocumentPayment>();
    }
}
