//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Infrastructure.Controls;

public interface IComboBoxControl<T> : IControl
    where T : class, IDocumentInfo
{
    T? Selected { get; }
    void RemoveDataSource();

    IComboBoxControl<T> ReadOnly();
    IComboBoxControl<T> EnableEditor<E>()
        where E : IEditorPage;
    IComboBoxControl<T> SetDataSource(Func<IEnumerable<T>?> func, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad, Guid? selectValue = null);
    IComboBoxControl<T> ItemChanged(Action<T?> action);
    IComboBoxControl<T> ItemSelected(Action<T?> action);
}
