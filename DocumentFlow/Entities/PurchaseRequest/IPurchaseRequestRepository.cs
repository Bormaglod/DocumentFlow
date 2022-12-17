//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.02.2022
//
// Версия 2022.12.11
//  - добавлены методы Cancel и Complete
// Версия 2022.12.17
//  - IDocumentRepository заменен на IDocumentContractorRepository
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public interface IPurchaseRequestRepository : IDocumentContractorRepository<PurchaseRequest>
{
    void Cancel(PurchaseRequest purchaseRequest);
    void Complete(PurchaseRequest purchaseRequest);
}
