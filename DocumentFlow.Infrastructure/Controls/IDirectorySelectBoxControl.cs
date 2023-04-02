//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Infrastructure.Controls;

public interface IDirectorySelectBoxControl<T> : IControl
    where T : class, IDirectory
{
    IDirectorySelectBoxControl<T> SetRootIdentifier(Guid id);
    IDirectorySelectBoxControl<T> ShowOnlyFolder();
    IDirectorySelectBoxControl<T> RemoveEmptyFolders();
    IDirectorySelectBoxControl<T> Required();
    IDirectorySelectBoxControl<T> SetDataSource(Func<IEnumerable<T>?> func, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad);
}
