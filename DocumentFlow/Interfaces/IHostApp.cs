//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.08.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

namespace DocumentFlow.Interfaces;

public interface IHostApp
{
    event EventHandler<NotifyEventArgs> OnAppNotify;
    void SendNotify(string entityName, IDocumentInfo document, MessageAction action);
    void SendNotify(MessageDestination destination, string entityName, Guid objectId, MessageAction action);
}
