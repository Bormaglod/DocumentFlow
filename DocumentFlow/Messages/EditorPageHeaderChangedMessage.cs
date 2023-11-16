//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.11.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging.Messages;

namespace DocumentFlow.Messages;

public class EditorPageHeaderChangedMessage : ValueChangedMessage<string>
{
    public EditorPageHeaderChangedMessage(string value) : base(value) { }
}
