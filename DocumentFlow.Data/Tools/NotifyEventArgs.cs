//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Data.Tools;

public class NotifyEventArgs : EventArgs
{
    public NotifyEventArgs(string entityName) => (Destination, EntityName) = (MessageDestination.List, entityName);
    public NotifyEventArgs(string entityName, Guid ownerId) => (Destination, EntityName, ObjectId) = (MessageDestination.List, entityName, ownerId);
    public NotifyEventArgs(string entityName, Guid objectId, MessageAction action) => (Destination, EntityName, ObjectId, Action) = (MessageDestination.Object, entityName, objectId, action);
    public NotifyEventArgs(string entityName, IDocumentInfo document, MessageAction action) => (Destination, EntityName, Document, ObjectId, Action) = (MessageDestination.Object, entityName, document, document.Id, action);

    public MessageDestination Destination { get; }
    public string EntityName { get; }
    public Guid ObjectId { get; } = Guid.Empty;
    public IDocumentInfo? Document { get; } = null;
    public MessageAction Action { get; } = MessageAction.Refresh;
}
