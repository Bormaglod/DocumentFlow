//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.07.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces;

public interface IDependentEntity
{
    long Id { get; set; }
    void SetOwner(Guid ownerId);
}
