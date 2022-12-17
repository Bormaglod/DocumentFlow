//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2022.12.11
//  - добавлен метод FillProductListFromPurvhaseRequest
// Версия 2022.12.17
//  - IDocumentRepository заменен на IDocumentContractorRepository
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.PurchaseRequestLib;

namespace DocumentFlow.Entities.Waybills;

public interface IWaybillReceiptRepository : IDocumentContractorRepository<WaybillReceipt>
{
    void FillProductListFromPurchaseRequest(WaybillReceipt waybillReceipt, PurchaseRequest? purchaseRequest);
}
