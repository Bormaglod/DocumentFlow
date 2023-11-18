//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.11.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

namespace DocumentFlow.Messages;

public class EditorPageHeaderChangedMessage
{
    public EditorPageHeaderChangedMessage(IPage page, string header) => (Page, Header) = (page, header);

    public IPage Page { get; }
    public string Header { get; }
}
