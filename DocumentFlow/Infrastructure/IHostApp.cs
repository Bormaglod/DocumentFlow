﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.08.2021
//-----------------------------------------------------------------------

using DocumentFlow.Core;
using DocumentFlow.Data;
using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Infrastructure;

public interface IHostApp
{
    event EventHandler<NotifyEventArgs> OnAppNotify;
    void SendNotify(string entityName, IDocumentInfo document, MessageAction action);
    void SendNotify(MessageDestination destination, string entityName, Guid objectId, MessageAction action);
}