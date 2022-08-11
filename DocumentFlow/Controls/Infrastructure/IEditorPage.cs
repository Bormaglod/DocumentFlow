//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.09.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Infrastructure;

public interface IEditorPage : IPage
{
    Guid? Id { get; }
    void SetEntityParameters(Guid? id, Guid? owner_id, Guid? parent_id, bool readOnly);
}
