//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.02.2022
//
// Версия 2022.12.24
//  - добавлено свойство DisableCurrentItem
//  - изменен вызов конструктора SelectDocumentForm
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.5.5
//  - из параметров конструктора удалены headerWidth и editorWidth
//
//-----------------------------------------------------------------------

using DocumentFlow.Dialogs;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Controls.Editors;

public class DfDocumentSelectBox<T> : SelectBox<T>, IDocumentSelectBoxControl<T>
    where T : class, IAccountingDocument
{
    private CreateColumns? createColumns;
    private bool disableCurrentItem = false;

    public DfDocumentSelectBox(string property, string header) 
        : base(property, header)
    {

    }

    protected override void OnSelect()
    {
        SelectDocumentForm<T> form = new(Items.ToList(), SelectedItem, disableCurrentItem);

        form.Columns.Clear();
        createColumns?.Invoke(form.Columns);
        if (form.ShowDialog() == DialogResult.OK)
        {
            SelectedItem = form.SelectedItem;

            SetTextValue(SelectedItem?.ToString() ?? string.Empty);

            OnValueChanged(SelectedItem);
            OnValueSelected(SelectedItem);
        }
    }

    #region IDocumentSelectBoxControl<T> interface

    IDocumentSelectBoxControl<T> IDocumentSelectBoxControl<T>.CreateColumns(CreateColumns action)
    {
        createColumns = action;
        return this;
    }

    IDocumentSelectBoxControl<T> IDocumentSelectBoxControl<T>.EnableEditor<E>(bool openById)
    {
        var pageManager = Services.Provider.GetService<IPageManager>()!;
        if (openById)
        {
            OpenAction = (t) => pageManager.ShowEditor<E>(t.Id);
        }
        else
        {
            OpenAction = pageManager.ShowEditor<E, T>;
        }

        return this;
    }

    IDocumentSelectBoxControl<T> IDocumentSelectBoxControl<T>.EnableEditor(OpenDialog<T> action)
    {
        OpenAction = action;
        return this;
    }

    IDocumentSelectBoxControl<T> IDocumentSelectBoxControl<T>.ReadOnly(bool value)
    {
        ReadOnly = value;
        return this;
    }

    IDocumentSelectBoxControl<T> IDocumentSelectBoxControl<T>.Required()
    {
        Required = true;
        return this;
    }

    IDocumentSelectBoxControl<T> IDocumentSelectBoxControl<T>.DisableCurrentItem()
    {
        disableCurrentItem = true;
        return this;
    }

    IDocumentSelectBoxControl<T> IDocumentSelectBoxControl<T>.SetDataSource(GettingDataSource<T> func, DataRefreshMethod refreshMethod)
    {
        RefreshMethod = refreshMethod;
        SetDataSource(func);
        return this;
    }

    IDocumentSelectBoxControl<T> IDocumentSelectBoxControl<T>.DocumentChanged(ControlValueChanged<T?> action)
    {
        SetValueChangedAction(action);
        return this;
    }

    IDocumentSelectBoxControl<T> IDocumentSelectBoxControl<T>.DocumentSelected(ControlValueChanged<T?> action)
    {
        SetValueSelectedAction(action);
        return this;
    }

    #endregion
}
