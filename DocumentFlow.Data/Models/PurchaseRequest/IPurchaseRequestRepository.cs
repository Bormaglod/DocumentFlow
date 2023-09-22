//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Models;

public interface IPurchaseRequestRepository : IDocumentContractorRepository<PurchaseRequest>
{
    void Cancel(PurchaseRequest purchaseRequest);
    void Complete(PurchaseRequest purchaseRequest);
    IReadOnlyList<PurchaseRequest> GetByContractor(Guid? contractorId, PurchaseState? state);
    IReadOnlyList<PurchaseRequest> GetActiveByContractor(Guid contractorId, Guid? purchaseId);
    IReadOnlyList<PurchaseRequestPrice> GetProducts(PurchaseRequest purchaseRequest);
}
