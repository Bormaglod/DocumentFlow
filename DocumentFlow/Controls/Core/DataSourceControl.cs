//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.02.2022
//
// Версия 2022.11.26
//  - добавлено перечисление DataRefreshMethod
//  - добавлено свойство RefreshMethod
//  - в методе SetDataSource удален параметр autoRefresh - вместо него
//    используется свойство RefreshMethod с значением Immediately
//  - добавлен метод RefreshDataSourceOnLoad
// Версия 2022.12.24
//  - добавлены методы GetSingleValueRepositoryType, GetDocument(I) и 
//    GetDocument(Type, I)
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.4.2
//  - DataRefreshMethod перенесен в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Controls.Core;

public abstract class DataSourceControl<I, T> : BaseControl, IDataSourceControl, IDataSourceControl<I, T>
    where T : class, IIdentifier<I>
    where I : struct, IComparable
{
    private GettingDataSource<T>? dataSource;

    public DataSourceControl(string property) : base(property) { }

    public DataRefreshMethod RefreshMethod { get; set; } = DataRefreshMethod.OnLoad;

    public virtual T? GetDocument(I id)
    {
        string typeName = $"{typeof(T).Namespace}.I{typeof(T).Name}Repository";
        var type = Type.GetType(typeName);

        if (type != null)
        {
            return GetDocument(type, id);
        }

        return null;
    }

    #region IDataSourceControl interface

    public void SetDataSource(GettingDataSource<T> func)
    {
        dataSource = func;
        if (RefreshMethod == DataRefreshMethod.Immediately)
        {
            RefreshDataSource();
        }
    }

    public void RemoveDataSource()
    {
        dataSource = null;
        ClearItems();
    }

    public void Select(I? id)
    {
        if (this is IBindingControl binding)
        {
            binding.Value = id;
        }
    }

    public void RefreshDataSource(T? selectedValue)
    {
        RefreshDataSource(selectedValue?.Id);
    }

    public void RefreshDataSource(I? selectedValue)
    {
        RefreshDataSource();
        Select(selectedValue);
    }

    public void RefreshDataSource()
    {
        ClearItems();
        if (dataSource != null)
        {
            IEnumerable<T>? data = dataSource();
            if (data != null)
            {
                DoRefreshDataSource(data);
            }
        }
    }

    public void RefreshDataSource(Guid? selectValue)
    {
        RefreshDataSource();
        if (this is IBindingControl bindingControl) 
        { 
            bindingControl.Value = selectValue;
        }
    }

    public void RefreshDataSourceOnLoad()
    {
        if (RefreshMethod == DataRefreshMethod.OnLoad) 
        {
            RefreshDataSource();
        }
    }

    #endregion

    protected abstract void DoRefreshDataSource(IEnumerable<T> data);

    protected abstract void ClearItems();

    protected T? GetDocument(Type type, I id)
    {
        var repo = Services.Provider.GetService(type);
        if (repo != null && repo is IRepository<I, T> tr)
        {
            return tr.Get(id, fullInformation: false);
        }

        return null;
    }
}