//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.08.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Core;
using DocumentFlow.Data;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Infrastructure;

public interface IHostApp
{
    event EventHandler<NotifyEventArgs> OnAppNotify;
    void SendNotify(string entityName, IDocumentInfo document, MessageAction action);
    void SendNotify(MessageDestination destination, string entityName, Guid objectId, MessageAction action);
}
