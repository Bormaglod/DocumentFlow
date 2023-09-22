//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Interfaces;

namespace DocumentFlow.Controls.Interfaces;

public interface IEditorPage : IPage
{
    IEditor Editor { get; set; }

    /// <summary>
    /// Метод загружает данные документа, обновляет все элементы управления и
    /// создаёт подчинённые окна.
    /// </summary>
    /// <param name="doumentId">Идентификатор документа.</param>
    void RefreshPage(Guid doumentId);

    /// <summary>
    /// Метод создает новый документ, подчинённые окна должны быть добавлены после 
    /// сохранения документа.
    /// </summary>
    void CreatePage();

    /// <summary>
    /// Метод создает страницу для подчиненного окна предназначенного для
    /// просмотра таблиц.
    /// </summary>
    /// <typeparam name="B">Тип создаваемого окна.</typeparam>
    void RegisterNestedBrowser<B>() where B : IBrowserPage;

    void RegisterReport<R>() where R : IReport;
}
