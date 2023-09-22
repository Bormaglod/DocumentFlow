//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Controls.Events;

public class DocumentSelectedEventArgs : EventArgs
{
    public DocumentSelectedEventArgs(IDocumentInfo document) => Document = document;

    public IDocumentInfo Document { get; set; }
}
