//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.12.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging.Messages;

using DocumentFlow.Controls.Interfaces;

namespace DocumentFlow.Messages;

public class PageVisibilityMessage : RequestMessage<bool>
{
    public PageVisibilityMessage(IPage page)
    {
        Page = page;
    }

    public IPage Page { get; }
}