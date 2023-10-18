//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.10.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data;

using System.Diagnostics.CodeAnalysis;

namespace DocumentFlow.Dialogs.Interfaces;

public interface IDocumentRefDialog : IDialog
{
    bool CreateThumbnailImage { get; }
    string FileNameWithPath { get; }
    bool Create(Guid owner, [MaybeNullWhen(false)] out DocumentRefs document);
    bool Edit(DocumentRefs refs);
}
