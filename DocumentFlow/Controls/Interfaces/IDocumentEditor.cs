//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Interfaces;

public interface IDocumentEditor
{
    Guid? OwnerId { get; set; }
}