//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.01.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Data;

public interface IDocumentRefs
{
    byte[]? FileContent { get; set; }
    long FileLength { get; set; }
    string? FileName { get; set; }
    string? Note { get; set; }
    string? Thumbnail { get; set; }
    bool ThumbnailExist { get; }
}