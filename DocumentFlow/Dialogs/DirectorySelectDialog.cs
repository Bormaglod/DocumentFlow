//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace DocumentFlow.Dialogs;

public partial class DirectorySelectDialog<T, D, R> : Form, IDirectorySelectDialog<T, D, R>
    where T : class, IReferenceItem, new()
    where D : class, IDirectory
    where R : IRepository<Guid, D>
{
    private readonly IReadOnlyDictionary<string, string> columns = new Dictionary<string, string>
    {
        ["Code"] = "Артикул",
        ["ItemName"] = "Наименование"
    };

    private readonly IDirectorySelectBoxControl<D> select;

    public DirectorySelectDialog(IControls<T> controls)
    {
        InitializeComponent();

        var attr = typeof(D).GetCustomAttribute<DescriptionAttribute>();
        var title = attr == null ? typeof(D).Name : attr.Name;

        controls.Container = Controls;

        select = controls.CreateDirectorySelectBox<D>(x => x.ReferenceId, title, text => text
            .SetDataSource(GetDirectory, DataRefreshMethod.Immediately)
            .Required()
            .SetColumns(x => x.Code, columns)
            .EditorFitToSize()
            .SetHeaderWidth(120));
    }

    public bool Create(T item)
    {
        if (select is IDataSourceControl<Guid, D> source)
        {
            source.Select(null);
        }

        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(item);
            return true;
        }

        return false;
    }

    public bool Edit(T item)
    {
        if (select is IDataSourceControl<Guid, D> source) 
        {
            source.Select(item.ReferenceId);
        }
        
        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(item);
            return true;
        }

        return false;
    }

    private IEnumerable<D>? GetDirectory()
    {
        var repo = Services.Provider.GetService<R>();
        return repo?.GetListExisting(callback: query => query.OrderByDesc("is_folder").OrderBy("code"));
    }

    private void SaveControlData(T item)
    {
        if (select.SelectedItem != null)
        {
            item.SetData(select.SelectedItem);
        }
        else
        {
            item.Clear();
        }
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (select.SelectedItem == null)
        {
            MessageBox.Show("Необходимо выбрать изделие.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
