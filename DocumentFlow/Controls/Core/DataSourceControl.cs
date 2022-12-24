//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Controls.Core;

public enum DataRefreshMethod { OnLoad, OnOpen, Immediately }

public abstract class DataSourceControl<I, T> : BaseControl, IDataSourceControl
    where T : class, IIdentifier<I>
    where I : struct, IComparable
{
    private Func<IEnumerable<T>?>? dataSource;

    public DataSourceControl(string property) : base(property) { }

    public DataRefreshMethod RefreshMethod { get; set; } = DataRefreshMethod.OnLoad;

    public Func<IEnumerable<T>?>? DataSourceFunc
    {
        get => dataSource;
        set
        {
            if (value == null)
            {
                DeleteDataSource();
            }
            else
            {
                SetDataSource(value);
            }
        }
    }

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

    public void SetDataSource(Func<IEnumerable<T>?> func)
    {
        dataSource = func;
        if (RefreshMethod == DataRefreshMethod.Immediately)
        {
            RefreshDataSource();
        }
    }

    public void DeleteDataSource()
    {
        dataSource = null;
        ClearItems();
    }

    #region IDataSourceControl interface

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
            return tr.GetById(id, fullInformation: false);
        }

        return null;
    }
}