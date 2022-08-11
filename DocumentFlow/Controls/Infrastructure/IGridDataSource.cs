//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.11.2021
//-----------------------------------------------------------------------

using System.Data;

namespace DocumentFlow.Controls.Infrastructure;

/// <summary>
/// Элемент управления, хранящий более чем 1 значение и имеющий подчиненное 
/// состояние по отношению к редактируемому объекту должен реализовать данный интерфейс
/// методы которого позволяют установить значение идентификатора owner_id и записать
/// данные в БД.
/// </summary>
public interface IGridDataSource
{
    void SetOwner(Guid owner_id);
    void UpdateData(IDbTransaction transaction);
}
