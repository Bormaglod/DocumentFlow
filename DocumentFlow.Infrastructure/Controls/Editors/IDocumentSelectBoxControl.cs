//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Infrastructure.Controls;

public interface IDocumentSelectBoxControl<T> : IControl
    where T : class, IAccountingDocument
{
    T? SelectedItem { get; }
    IDocumentSelectBoxControl<T> CreateColumns(CreateColumns action);
    IDocumentSelectBoxControl<T> EnableEditor<E>(bool openById = false) where E : IEditorPage;
    IDocumentSelectBoxControl<T> EnableEditor(OpenDialog<T> action);
    IDocumentSelectBoxControl<T> ReadOnly(bool value = true);
    IDocumentSelectBoxControl<T> Required();
    IDocumentSelectBoxControl<T> DisableCurrentItem();
    IDocumentSelectBoxControl<T> SetDataSource(GettingDataSource<T> func, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad);
    IDocumentSelectBoxControl<T> DocumentChanged(ControlValueChanged<T?> action);
    IDocumentSelectBoxControl<T> DocumentSelected(ControlValueChanged<T?> action);
}
