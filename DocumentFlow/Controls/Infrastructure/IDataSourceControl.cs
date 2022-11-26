﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.10.2021
//
// Версия 2022.11.26
//  - добавлен метод RefreshDataSourceOnLoad
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Infrastructure;

/// <summary>
/// При чтении данных из БД элемент управления получает значение которое копирует в 
/// соответствующее поле связанного с этим элементом объкта. Если объект содержит больше, 
/// чем 1 значение, то он должен реализовывать интерфейс IDataSourceControl. Кроме того,
/// если объект яаляется подчиненным к редактированию, т.е. у него имеется поле owner_id и
/// оно должно быть заполнено - необходимо реализовать для этого объекта и интерфейс <see cref="IGridDataSource"/>.
/// </summary>
public interface IDataSourceControl
{
    void RefreshDataSource();
    void RefreshDataSourceOnLoad();
}
