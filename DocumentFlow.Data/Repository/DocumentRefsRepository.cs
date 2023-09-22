//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.12.2019
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Repository;

public class DocumentRefsRepository : OwnedRepository<long, DocumentRefs>, IDocumentRefsRepository
{
    public DocumentRefsRepository(IDatabase database) : base(database) { }

    public IReadOnlyList<DocumentRefs> GetDocumentsWithThumbnails(Guid owner)
    {
        return GetByOwner(owner, callback: q => q.WhereNotNull("thumbnail"));
    }
}
