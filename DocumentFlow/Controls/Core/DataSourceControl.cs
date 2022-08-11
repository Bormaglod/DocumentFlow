//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Controls.Core;

public abstract class DataSourceControl<I, T> : BaseControl, IDataSourceControl
    where T : class, IIdentifier<I>
    where I : struct, IComparable
{
    private Func<IEnumerable<T>?>? dataSource;

    public DataSourceControl(string property) : base(property) { }

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

    public void SetDataSource(Func<IEnumerable<T>?> func, bool autoRefresh = false)
    { 
        dataSource = func; 
        if (autoRefresh)
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

    #endregion

    protected abstract void DoRefreshDataSource(IEnumerable<T> data);

    protected abstract void ClearItems();
}