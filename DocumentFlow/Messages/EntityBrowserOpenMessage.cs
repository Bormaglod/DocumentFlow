//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.12.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Messages;

public class EntityBrowserOpenMessage
{
    public EntityBrowserOpenMessage(Type browserType, string text)
    {
        BrowserType = browserType;
        Text = text;
    }

    public Type BrowserType { get; }
    public string Text {  get; }
}
