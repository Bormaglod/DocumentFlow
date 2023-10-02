//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.10.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Controls.Events;

public class DocumentChangedEventArgs : EventArgs
{
    public DocumentChangedEventArgs(IDocumentInfo? oldDocument, IDocumentInfo newDocument) => (OldDocument, NewDocument) = (oldDocument, newDocument);

    public IDocumentInfo? OldDocument { get; set; }
    public IDocumentInfo NewDocument { get; set; }
}
