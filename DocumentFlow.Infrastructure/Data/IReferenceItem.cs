//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.05.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Data;

public interface IReferenceItem : IEntity<long>
{
    Guid ReferenceId { get; }
    string Code { get; }
    string Name { get; }

    void SetData(IDirectory directory);
    void Clear();
}
