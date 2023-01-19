//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.02.2022
//
// Версия 2022.11.13
//  - добавлен отчет WaybillSaleReport
// Версия 2023.1.19
//  - добавлено зависимое окно "Платежи"
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.PaymentOrders.Documents;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Waybills;

public class WaybillSaleEditor : WaybillEditor<WaybillSale, WaybillSalePrice>, IWaybillSaleEditor
{
    public WaybillSaleEditor(IWaybillSaleRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        RegisterReport(new WaybillSaleReport());
    }

    protected override IOwnedRepository<long, WaybillSalePrice> GetDetailsRepository()
    {
        return Services.Provider.GetService<IWaybillSalePriceRepository>()!;
    }

    protected override void DoAfterRefreshData()
    {
        base.DoAfterRefreshData();
        RegisterNestedBrowser<IDocumentPaymentBrowser, DocumentPayment>();
    }
}
