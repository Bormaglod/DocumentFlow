//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.12.2019
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces.Repository;

public interface IDocumentRefsRepository : IOwnedRepository<long, DocumentRefs>
{
    IReadOnlyList<DocumentRefs> GetDocumentsWithThumbnails(Guid owner);
}
