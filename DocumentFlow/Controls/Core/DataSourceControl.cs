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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data.Infrastructure;

using System.Reflection;

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
}