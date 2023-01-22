//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2022.12.11
//  - добавлен метод FillProductListFromPurvhaseRequest
// Версия 2022.12.17
//  - IDocumentRepository заменен на IDocumentContractorRepository
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Entities.PurchaseRequestLib;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Waybills;

public interface IWaybillReceiptRepository : IDocumentContractorRepository<WaybillReceipt>
{
    void FillProductListFromPurchaseRequest(WaybillReceipt waybillReceipt, PurchaseRequest? purchaseRequest);
}
