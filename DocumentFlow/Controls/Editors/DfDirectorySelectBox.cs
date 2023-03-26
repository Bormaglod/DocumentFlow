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
//
//-----------------------------------------------------------------------

using DocumentFlow.Dialogs;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Controls.Editors;

public class DfDirectorySelectBox<T> : SelectBox<T>
    where T : class, IDirectory
{
    public DfDirectorySelectBox(string property, string header, int headerWidth, int editorWidth = default)
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
}
