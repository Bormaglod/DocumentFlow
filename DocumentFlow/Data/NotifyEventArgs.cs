//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.11.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.1.23
//  - перемещено из DocumentFlow.Core в DocumentFlow.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Data
{
    public class NotifyEventArgs : EventArgs
    {
        public NotifyEventArgs(string entityName) => (Destination, EntityName) = (MessageDestination.List, entityName);
        public NotifyEventArgs(string entityName, Guid ownerId) => (Destination, EntityName, ObjectId) = (MessageDestination.List, entityName, ownerId);
        public NotifyEventArgs(string entityName, Guid objectId, MessageAction action) => (Destination, EntityName, ObjectId, Action) = (MessageDestination.Object, entityName, objectId, action);
        public NotifyEventArgs(string entityName, IDocumentInfo document, MessageAction action) => (Destination, EntityName, Document, ObjectId, Action) = (MessageDestination.Object, entityName, document, document.id, action);

        public MessageDestination Destination { get; }
        public string EntityName { get; }
        public Guid ObjectId { get; } = Guid.Empty;
        public IDocumentInfo? Document { get; } = null;
        public MessageAction Action { get; } = MessageAction.Refresh;
    }
}
