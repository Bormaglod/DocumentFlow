//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.10.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Infrastructure;

public interface IDirectory : IDocumentInfo, IItem
{
    Guid? parent_id { get; set; }
    bool is_folder { get; set; }
}
