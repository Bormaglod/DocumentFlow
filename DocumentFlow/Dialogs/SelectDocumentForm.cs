//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Dialogs;

public partial class SelectDocumentForm<T> : Form
    where T : class, IAccountingDocument
{
    public SelectDocumentForm(IList<T> items)
    {
        InitializeComponent();

        AddItems(items);
    }

    public T? SelectedItem
    {
        get => gridContent.SelectedItem as T;
        set => gridContent.SelectedItem = value;
    }

    public Columns Columns => gridContent.Columns;

    private void AddItems(IList<T> items)
    {
        gridContent.DataSource = items;
    }

    private void GridContent_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
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

    private void ToolButton1_Click(object sender, EventArgs e)
    {
        textBoxExt1.Text = string.Empty;
    }
}
