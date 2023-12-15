//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.12.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Messages;

public class PageOpenMessage
{
    public PageOpenMessage(Type pageType) 
    { 
        PageType = pageType;
    }

    public Type PageType { get; }
}
