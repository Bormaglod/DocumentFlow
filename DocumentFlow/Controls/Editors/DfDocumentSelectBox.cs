//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.02.2022
//
// Версия 2022.12.24
//  - добавлено свойство DisableCurrentItem
//  - изменен вызов конструктора SelectDocumentForm
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Dialogs;

namespace DocumentFlow.Controls.Editors;

public class DfDocumentSelectBox<T> : SelectBox<T>
    where T : class, IAccountingDocument
{
    public DfDocumentSelectBox(string property, string header, int headerWidth, int editorWidth = default) 
        : base(property, header, headerWidth, editorWidth)
    {

    }

    public event EventHandler<ColumnDataEventArgs>? Columns;

    public bool DisableCurrentItem { get; set; } = false;

    protected override void OnSelect()
    {
        SelectDocumentForm<T> form = new(Items.ToList(), SelectedItem, DisableCurrentItem);

        form.Columns.Clear();
        Columns?.Invoke(this, new ColumnDataEventArgs(form.Columns));
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
