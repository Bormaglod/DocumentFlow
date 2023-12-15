//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.12.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

namespace DocumentFlow.Messages;

public class PageCloseMessage
{
    public PageCloseMessage(IPage page)
    {
        Page = page;
    }

    public IPage Page { get; }
}