//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.02.2022
//
// Версия 2022.12.11
//  - добавлены методы Cancel и Complete
// Версия 2022.12.17
//  - IDocumentRepository заменен на IDocumentContractorRepository
// Версия 2022.12.21
//  - добавлен метод GetByContractor(Guid?, PurchaseState?)
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public interface IPurchaseRequestRepository : IDocumentContractorRepository<PurchaseRequest>
{
    void Cancel(PurchaseRequest purchaseRequest);
    void Complete(PurchaseRequest purchaseRequest);
    IReadOnlyList<PurchaseRequest> GetByContractor(Guid? contractorId, PurchaseState? state);
}
