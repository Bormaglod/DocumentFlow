//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.04.2019
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Dialogs;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DocumentFlow.Controls.Editors;

[ToolboxItem(true)]
public class DfDirectorySelectBox : DfSelectBox
{
    private IEnumerable<IDirectory>? dataSource;
    private readonly Dictionary<PropertyInfo, string> columns = new();
    private string? nameColumn;

    public event EventHandler<DataSourceLoadEventArgs>? DataSourceOnLoad;

    public DfDirectorySelectBox()
    {
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IEnumerable<IDirectory>? DataSource 
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

    public bool RemoveEmptyFolders { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Guid? RootIdentifier { get; set; }

    public bool CanSelectFolder { get; set; }

    public void SetColumns<T>(string nameColumn, IReadOnlyDictionary<string, string> columns)
    {
        this.columns.Clear();

        var type = typeof(T);

        this.nameColumn = nameColumn;
        foreach (var item in columns.Keys)
        {
            var prop = type.GetProperty(item);
            if (prop != null) 
            {
                this.columns.Add(prop, columns[item]);
            }
        }
    }

    protected override IDocumentInfo? GetDocument(Guid id)
    {
        ArgumentNullException.ThrowIfNull(DataSource);

        return DataSource.FirstOrDefault(x => x.Id == id);
    }

    protected override bool SelectItem([MaybeNullWhen(false)] out IDocumentInfo documentInfo)
    {
        var dialog = new DirectoryDialog()
        {
            Root = RootIdentifier,
            RemoveEmptyFolders = RemoveEmptyFolders,
            CanSelectFolder = CanSelectFolder
        };

        if (columns.Count > 0 && !string.IsNullOrEmpty(nameColumn)) 
        {
            dialog.SetColumns(nameColumn, columns);
        }

        LoadDataSource();
        if (dataSource != null)
        {
            dialog.SetDataSource(dataSource);
            dialog.SelectedItem = SelectedItem;
        }

        if (dialog.ShowDialog() == DialogResult.OK && dialog.SelectedDirectoryItem != null)
        {
            documentInfo = dialog.SelectedDirectoryItem;
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
            dataSource = args.Values?.OfType<IDirectory>().Order();
        }
    }
}
