//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.10.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces;

public interface IDirectory : IDocumentInfo, IItem
{
    Guid? ParentId { get; set; }
    bool IsFolder { get; set; }
}
