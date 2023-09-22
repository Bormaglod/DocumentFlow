//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Interfaces;

public interface IBrowserPage : IPage
{
    /// <summary>
    /// Свойство определяет возможность редактирования записей окна.
    /// </summary>
    bool ReadOnly { get; set; }

    /// <summary>
    /// Метод вызывается для обновления данных окна с установкой всех необходимых
    /// параметров (фильтра, репозитория и т.д.). Он вызывается при создании окна.
    /// </summary>
    /// <param name="owner">Идентификатор записи по отношению к которой записи этого окна являются зависимыми.</param>
    void UpdatePage(Guid? owner = null);
}
