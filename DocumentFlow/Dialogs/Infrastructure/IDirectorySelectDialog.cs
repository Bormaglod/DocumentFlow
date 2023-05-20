//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.05.2023
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Dialogs;

namespace DocumentFlow.Dialogs.Infrastructure;

public interface IDirectorySelectDialog<T, D, R> : IDataGridDialog<T>
    where T : IReferenceItem, new()
    where D : class, IDirectory
    where R : IRepository<Guid, D>
{
}
