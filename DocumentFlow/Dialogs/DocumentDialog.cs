//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;

namespace DocumentFlow.Dialogs;

public partial class DocumentDialog : Form
{
    public DocumentDialog()
    {
        InitializeComponent();
    }

    public IBaseDocument? SelectedDocumentItem => (IBaseDocument?)gridContent.SelectedItem;

    public Guid? SelectedItem => SelectedDocumentItem?.Id;

    public bool DisableCurrentItem { get; set; }

    public IBaseDocument? CurrentItem { get; set; }

    public Columns Columns => gridContent.Columns;

    public IEnumerable<IBaseDocument> Documents
    {
        get => (IEnumerable<IBaseDocument>)gridContent.DataSource;
        set => gridContent.DataSource = value;
    }

    public void SetDataSource(IEnumerable<IBaseDocument> documents)
    {
        gridContent.DataSource = documents;
        if (CurrentItem != null)
        {
            gridContent.SelectedItem = documents.FirstOrDefault(x => x.Id == CurrentItem.Id);
        }
    }

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
        if (e.DataRow.RowData is IBaseDocument row && CurrentItem != null && DisableCurrentItem)
        {
            if (row.Id == CurrentItem.Id)
            {
                e.Style.TextColor = Color.Gray;
            }
        }
    }
}
