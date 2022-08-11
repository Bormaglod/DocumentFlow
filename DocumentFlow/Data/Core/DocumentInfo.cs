//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.03.2019
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Data.Core;

public class DocumentInfo : Entity<Guid>, IDocumentInfo
{
    public Guid user_created_id { get; protected set; }
    public DateTime date_created { get; protected set; }
    public string? user_created { get; protected set; }
    public Guid user_updated_id { get; protected set; }
    public DateTime date_updated { get; protected set; }
    public string? user_updated { get; protected set; }
    public bool deleted { get; protected set; }
}
