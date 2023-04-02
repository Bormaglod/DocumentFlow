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

using DocumentFlow.Dialogs;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Controls.Editors;

public class DfDirectorySelectBox<T> : SelectBox<T>, IDirectorySelectBoxControl<T>
    where T : class, IDirectory
{
    public DfDirectorySelectBox(string property, string header, int headerWidth = 100, int editorWidth = default)
        : base(property, header, headerWidth, editorWidth)
    {
    }

    public Guid? RootIdentifier { get; set; } = null;

    public bool ShowOnlyFolder { get; set; } = false;

    public bool RemoveEmptyFolders { get; set; } = false;

    public string? NameColumn { get; set; }

    public IReadOnlyDictionary<string, string>? Columns { get; set; }

    protected override void OnSelect()
    {
        SelectDirectoryForm<T> form = new(RootIdentifier, ShowOnlyFolder, RemoveEmptyFolders);
        if (Columns != null && Columns.Count > 0)
        {
            var nameColumn = NameColumn;
            nameColumn ??= Columns.First().Key;

            form.SetColumns(nameColumn, Columns);
        }

        form.AddItems(Items);
        form.SelectedItem = SelectedItem;
        if (form.ShowDialog() == DialogResult.OK)
        {
            T? oldValue = SelectedItem;
            SelectedItem = form.SelectedItem;

            SetTextValue(SelectedItem?.ToString() ?? string.Empty);

            OnValueChanged(oldValue, SelectedItem);
            OnManualValueChanged(oldValue, SelectedItem);
        }
    }

    #region IControl interface

    IControl IControl.SetHeaderWidth(int width)
    {
        HeaderWidth = width;
        return this;
    }

    IControl IControl.SetEditorWidth(int width)
    {
        EditorWidth = width;
        return this;
    }

    IControl IControl.Disable()
    {
        Enabled = false;
        return this;
    }

    IControl IControl.ReadOnly()
    {
        ReadOnly = true;
        return this;
    }

    IControl IControl.DefaultAsValue()
    {
        DefaultAsNull = false;
        return this;
    }

    #endregion

    #region IDirectorySelectBoxControl interface

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.SetRootIdentifier(Guid id)
    {
        RootIdentifier = id;
        return this;
    }

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.ShowOnlyFolder()
    {
        ShowOnlyFolder = true;
        return this;
    }

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.RemoveEmptyFolders()
    {
        RemoveEmptyFolders = true;
        return this;
    }

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.Required()
    {
        Required = true;
        return this;
    }

    IDirectorySelectBoxControl<T> IDirectorySelectBoxControl<T>.SetDataSource(Func<IEnumerable<T>?> func, DataRefreshMethod refreshMethod)
    {
        RefreshMethod = refreshMethod;
        SetDataSource(func);
        return this;
    }

    #endregion
}
