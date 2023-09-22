//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.12.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces.Repository;

public interface IDocumentContractorRepository<T> : IDocumentRepository<T>
    where T : IIdentifier<Guid>
{
    IReadOnlyList<T> GetByContractor(Guid? contractorId);
}
