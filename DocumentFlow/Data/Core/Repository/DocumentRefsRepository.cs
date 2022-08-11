//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.12.2019
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core.Repository;

public class DocumentRefsRepository : OwnedRepository<long, DocumentRefs>, IDocumentRefsRepository
{
    public DocumentRefsRepository(IDatabase database) : base(database) { }
}
