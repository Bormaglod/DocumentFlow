//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.02.2022
//
// Версия 2022.12.24
//  - в конструктор добавлены параметры currentItem и disableCurrentItem
//  - добавлен метод GridContent_QueryCellStyle, в котором реализовано
//    выделение строки со значением currentItem, если disableCurrentItem
//    установлено в true
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using Syncfusion.WinForms.DataGrid.Events;

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Dialogs;

public partial class SelectDocumentForm<T> : Form
    where T : class, IAccountingDocument
{
    private readonly bool disableCurrentItem;
    private readonly T? currentItem;

    public SelectDocumentForm(IList<T> items, T? currentItem, bool disableCurrentItem)
    {
        InitializeComponent();

        AddItems(items);
        if (currentItem != null) 
        {
            gridContent.SelectedItem = items.FirstOrDefault(x => x.id == currentItem.id);
        }

        this.currentItem = currentItem;
        this.disableCurrentItem = disableCurrentItem;
    }

    public T? SelectedItem
    {
        get => gridContent.SelectedItem as T;
        set => gridContent.SelectedItem = value;
    }

    public Columns Columns => gridContent.Columns;

    private void AddItems(IList<T> items) => gridContent.DataSource = items;

    private void GridContent_CellDoubleClick(object sender, CellClickEventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void TextBoxExt1_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(textBoxExt1.Text))
        {
            gridContent.SearchController.ClearSearch();
        }
        else
        {
            gridContent.SearchController.Search(textBoxExt1.Text);
        }
    }

    private void ToolButton1_Click(object sender, EventArgs e) => textBoxExt1.Text = string.Empty;

    private void GridContent_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
    {
        if (e.DataRow.RowData is T row && currentItem != null && disableCurrentItem)
        {
            if (row.id == currentItem.id)
            {
                e.Style.TextColor = Color.Gray;
            }
        }
    }
}
