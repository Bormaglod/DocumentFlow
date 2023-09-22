//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces;

public interface IDocumentInfo : IEntity<Guid>
{
    Guid UserCreatedId { get; }
    DateTime DateCreated { get; }
    Guid UserUpdatedId { get; }
    DateTime DateUpdated { get; }
    bool Deleted { get; }
    bool? HasDocuments { get; }

    void Loaded();
}