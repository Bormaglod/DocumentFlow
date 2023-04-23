//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

using System.Linq.Expressions;

namespace DocumentFlow.Infrastructure.Controls;

public interface IDirectorySelectBoxControl<T> : IControl
    where T : class, IDirectory
{
    IEnumerable<T> Items { get; }
    T? SelectedItem { get; }
    string ValueText { get; }
    IDirectorySelectBoxControl<T> EnableEditor<E>(bool openById = false) where E : IEditorPage;
    IDirectorySelectBoxControl<T> ReadOnly(bool value = true);
    IDirectorySelectBoxControl<T> SetRootIdentifier(Guid? id);
    IDirectorySelectBoxControl<T> ShowOnlyFolder();
    IDirectorySelectBoxControl<T> RemoveEmptyFolders();
    IDirectorySelectBoxControl<T> Required();
    IDirectorySelectBoxControl<T> SetDataSource(GettingDataSource<T> func, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad);
    IDirectorySelectBoxControl<T> DirectoryChanged(ControlValueChanged<T?> action);
    IDirectorySelectBoxControl<T> DirectorySelected(ControlValueChanged<T?> action);
    IDirectorySelectBoxControl<T> SetColumns(Expression<Func<T, object?>> columnName, IReadOnlyDictionary<string, string> columns);
}
