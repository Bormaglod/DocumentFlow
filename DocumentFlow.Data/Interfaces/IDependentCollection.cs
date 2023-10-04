//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces;

public interface IDependentCollection
{
    List<IDependentEntity> NewItems { get; }
    List<IDependentEntity> UpdateItems { get; }
    List<IDependentEntity> RemoveItems { get; }
    void CompleteChanged();
}
