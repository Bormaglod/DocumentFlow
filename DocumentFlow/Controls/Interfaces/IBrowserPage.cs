//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

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
    /// <param name="text">Заголовок окна.</param>
    /// <param name="owner">Документ по отношению к которому записи этого окна являются зависимыми.</param>
    void UpdatePage(string text, IDocumentInfo? owner = null);
}
