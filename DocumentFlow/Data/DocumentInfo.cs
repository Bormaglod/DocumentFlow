//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.03.2019
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Data;

public class DocumentInfo : Entity<Guid>, IDocumentInfo
{
    public Guid UserCreatedId { get; protected set; }
    public DateTime DateCreated { get; protected set; }
    public string? UserCreated { get; protected set; }
    public Guid UserUpdatedId { get; protected set; }
    public DateTime DateUpdated { get; protected set; }
    public string? UserUpdated { get; protected set; }
    public bool Deleted { get; protected set; }
}
