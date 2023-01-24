//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.01.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Data;

public interface IDocumentRefs
{
    byte[]? file_content { get; set; }
    long file_length { get; set; }
    string? file_name { get; set; }
    string? note { get; set; }
    string? thumbnail { get; set; }
    bool thumbnail_exist { get; }
}