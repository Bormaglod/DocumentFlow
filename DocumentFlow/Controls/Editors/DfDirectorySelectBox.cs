//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.04.2019
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Dialogs;

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

    protected override void OnSelect()
    {
        SelectDirectoryForm<T> form = new(RootIdentifier, ShowOnlyFolder, RemoveEmptyFolders);
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
