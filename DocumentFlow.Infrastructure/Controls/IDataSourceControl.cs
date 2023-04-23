//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.10.2021
//
// Версия 2022.11.26
//  - добавлен метод RefreshDataSourceOnLoad
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Controls.Infrastructure в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Infrastructure.Controls;

public enum DataRefreshMethod { OnLoad, OnOpen, Immediately }

/// <summary>
/// При чтении данных из БД элемент управления получает значение которое копирует в 
/// соответствующее поле связанного с этим элементом объкта. Если объект содержит больше, 
/// чем 1 значение, то он должен реализовывать интерфейс IDataSourceControl. Кроме того,
/// если объект яаляется подчиненным к редактированию, т.е. у него имеется поле owner_id и
/// оно должно быть заполнено - необходимо реализовать для этого объекта и интерфейс <see cref="IGridDataSource"/>.
/// </summary>
public interface IDataSourceControl
{
    void RemoveDataSource();
    void RefreshDataSource();
    void RefreshDataSourceOnLoad();
}


public interface IDataSourceControl<I, T>
    where T : class, IIdentifier<I>
    where I : struct, IComparable
{
    void SetDataSource(GettingDataSource<T> func);
    void Select(I? id);
    void RefreshDataSource(T? selectedValue);
    void RefreshDataSource(I? selectedValue);
}