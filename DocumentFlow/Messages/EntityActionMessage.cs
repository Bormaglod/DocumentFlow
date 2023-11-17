//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.11.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Messages;

public class EntityActionMessage
{
    public EntityActionMessage(string entityName) => (Destination, EntityName) = (MessageDestination.List, entityName);
    public EntityActionMessage(string entityName, Guid ownerId) => (Destination, EntityName, ObjectId) = (MessageDestination.List, entityName, ownerId);
    public EntityActionMessage(string entityName, Guid objectId, MessageAction action) => (Destination, EntityName, ObjectId, Action) = (MessageDestination.Object, entityName, objectId, action);
    public EntityActionMessage(string entityName, IDocumentInfo document, MessageAction action) => (Destination, EntityName, Document, ObjectId, Action) = (MessageDestination.Object, entityName, document, document.Id, action);

    public MessageDestination Destination { get; }
    public string EntityName { get; }
    public Guid ObjectId { get; } = Guid.Empty;
    public IDocumentInfo? Document { get; } = null;
    public MessageAction Action { get; } = MessageAction.Refresh;
}