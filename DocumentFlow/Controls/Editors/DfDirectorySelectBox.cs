//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.04.2019
//
// Версия 2022.8.19
//  - добавлено свойство Columns
// Версия 2022.8.31
//  - свойство NameColumn по умолчанию устанавливается в первое значение
//    списка Columns
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.4.2
//  - добавлено наследование от IDirectorySelectBoxControl
//
//-----------------------------------------------------------------------

using DocumentFlow.Core.Reflection;
using DocumentFlow.Dialogs;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using System.Linq.Expressions;

namespace DocumentFlow.Controls.Editors;

public class DfDirectorySelectBox<T> : SelectBox<T>, IDirectorySelectBoxControl<T>
    where T : class, IDirectory
{
    private IReadOnlyDictionary<string, string>? columns;
    private string? nameColumn;
    private Guid? rootIdentifier = null;
    private bool showOnlyFolder = false;
    private bool removeEmptyFolders = false;

    public DfDirectorySelectBox(string property, string header, int headerWidth = default, int editorWidth = default)
        : base(property, header, headerWidth, editorWidth)
    {
    }

    protected override void OnSelect()
    {
        SelectDirectoryForm<T> form = new(rootIdentifier, showOnlyFolder, removeEmptyFolders);
        if (columns != null && columns.Count > 0)
        {
            var name = nameColumn;
            name ??= columns.First().Key;

            form.SetColumns(name, columns);
        }

        form.AddItems(Items);
        form.SelectedItem = SelectedItem;
        if (form.ShowDialog() == DialogResult.OK)
        {
            SelectedItem = form.SelectedItem;

            SetTextValue(SelectedItem?.ToString() ?? string.Empty);

            OnValueChanged(SelectedItem);
            OnValueSelected(SelectedItem);
        }
    }

    #region IDirectorySelectBoxControl interface

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.EnableEditor<E>(bool openById)
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

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.ReadOnly(bool value)
    {
        ReadOnly = value;
        return this;
    }

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.SetRootIdentifier(Guid? id)
    {
        rootIdentifier = id;
        return this;
    }

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.ShowOnlyFolder()
    {
        showOnlyFolder = true;
        return this;
    }

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.RemoveEmptyFolders()
    {
        removeEmptyFolders = true;
        return this;
    }

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.Required()
    {
        Required = true;
        return this;
    }

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.SetDataSource(GettingDataSource<T> func, DataRefreshMethod refreshMethod)
    {
        RefreshMethod = refreshMethod;
        SetDataSource(func);
        return this;
    }

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.DirectoryChanged(ControlValueChanged<T?> action)
    {
        SetValueChangedAction(action);
        return this;
    }

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.DirectorySelected(ControlValueChanged<T?> action)
    {
        SetValueSelectedAction(action);
        return this;
    }

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.SetColumns(Expression<Func<T, object?>> columnName, IReadOnlyDictionary<string, string> columns)
    {
        nameColumn = columnName.ToMember().Name;
        this.columns = columns;
        return this;
    }

    #endregion
}
