//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Dialogs;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace DocumentFlow.Controls.Editors;

[ToolboxItem(true)]
public class DfDocumentSelectBox : DfSelectBox
{
    private IEnumerable<IBaseDocument>? dataSource;

    public event EventHandler<DocumentDialogColumnsEventArgs>? DocumentDialogColumns;
    public event EventHandler<DataSourceLoadEventArgs>? DataSourceOnLoad;

    public DfDocumentSelectBox() 
    {
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IEnumerable<IBaseDocument>? DataSource
    {
        get
        {
            if (dataSource == null)
            {
                LoadDataSource();
            }

            return dataSource;
        }

        set => dataSource = value?.Order();
    }

    public bool DisableCurrentItem { get; set; } = false;

    protected override IDocumentInfo? GetDocument(Guid id)
    {
        ArgumentNullException.ThrowIfNull(DataSource);

        return DataSource.FirstOrDefault(x => x.Id == id);
    }

    protected override bool SelectItem([MaybeNullWhen(false)] out IDocumentInfo documentInfo)
    {
        var dialog = new DocumentDialog()
        {
            DisableCurrentItem = DisableCurrentItem,
            CurrentItem = SelectedItem == Guid.Empty ? null : (IBaseDocument)SelectedDocument
        };

        DocumentDialogColumns?.Invoke(this, new DocumentDialogColumnsEventArgs(dialog.Columns));

        LoadDataSource();
        if (dataSource != null)
        {
            dialog.SetDataSource(dataSource);
        }

        if (dialog.ShowDialog() == DialogResult.OK && dialog.SelectedDocumentItem != null)
        {
            documentInfo = dialog.SelectedDocumentItem;
            return true;
        }

        documentInfo = null;
        return false;
    }

    private void LoadDataSource()
    {
        if (DataSourceOnLoad != null)
        {
            var args = new DataSourceLoadEventArgs(SelectedItem);
            DataSourceOnLoad(this, args);
            dataSource = args.Values?.OfType<IBaseDocument>().Order();
        }
    }
}
