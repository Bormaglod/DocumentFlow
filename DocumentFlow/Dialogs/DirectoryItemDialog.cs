//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Tools;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Tools;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace DocumentFlow.Dialogs;

[Dialog]
public partial class DirectoryItemDialog : Form
{
    private readonly IReadOnlyDictionary<string, string> columns = new Dictionary<string, string>
    {
        ["Code"] = "Артикул",
        ["ItemName"] = "Наименование"
    };

    private readonly IServiceProvider services;

    public DirectoryItemDialog(IServiceProvider services)
    {
        InitializeComponent();

        this.services = services;
    }

    public T? Get<T>(IEnumerable<T> items, Guid? selectedItem = null, bool withColumns = true)
        where T : class, IDirectory
    {
        return GetDirectoryItem(items, selectedItem, withColumns);
    }

    public T? Get<T, R>(Guid? selectedItem = null, bool withColumns = true)
        where T : class, IDirectory
        where R : IRepository<Guid, T>
    {
        var items = services
            .GetRequiredService<R>()
            .GetListExisting(callback: query => query.OrderByDesc("is_folder")
            .OrderBy("code"));

        return GetDirectoryItem(items, selectedItem, withColumns);
    }

    private T? GetDirectoryItem<T>(IEnumerable<T> items, Guid? selectedItem, bool withColumns)
        where T : class, IDirectory
    {
        var attr = typeof(T).GetCustomAttribute<EntityNameAttribute>();
        var title = attr == null ? typeof(T).Name : attr.Name;

        if (withColumns)
        {
            selectBox.SetColumns<T>("Code", columns);
        }

        selectBox.DataSource = items;
        selectBox.Header = title;

        if (selectedItem != null)
        {
            selectBox.SelectedItem = selectedItem.Value;
        }

        if (ShowDialog() == DialogResult.OK)
        {
            return items.First(x => x.Id == selectBox.SelectedItem);
        }

        return default;
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (selectBox.SelectedItem == Guid.Empty)
        {
            MessageBox.Show("Необходимо выбрать элемент справочника.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
