//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Controls.Interfaces;

public interface IEditor
{
    IDocumentInfo DocumentInfo { get; }
    IEditorPage EditorPage { get; set; }
    bool AcceptSupported { get; }
    bool EnabledEditor { get; set; }

    /// <summary>
    /// Метод служит для контроля за созданием документа (объекта), а также осуществляет привязку к 
    /// элементам управления с помощью метода <seealso cref="PageContents.Editor.BindingControls"/>
    /// </summary>
    void Create();

    /// <summary>
    /// Метод загружает данные из БД, осуществляет привязку к элементам управления с помощью
    /// метода <seealso cref="PageContents.Editor.BindingControls"/> и возвращает 
    /// загруженный объект.
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns>Документ (объект), загруженный из БД.</returns>
    IDocumentInfo Load(Guid identifier);

    /// <summary>
    /// Метод сохраняет данные окна в БД и возвращает записаный объект. Если при записи СУБД меняет значения
    /// каких-либо полей (например, в тригере), то класс реализующий этот интерфейс может вновь прочитать 
    /// его и соответственно вернуть его именно с помощью этого метода.
    /// </summary>
    /// <returns>Документ (объект), записанные в БД.</returns>
    IDocumentInfo Save();

    /// <summary>
    /// Метод записывает документ, а затем переводит его в состояние "Проведён".
    /// </summary>
    /// <returns></returns>
    void Accept();

    /// <summary>
    /// Метод занового загружает данные из БД и осуществляет привязку к элементам управления с помощью
    /// метода <seealso cref="PageContents.Editor.BindingControls"/>
    /// </summary>
    void Reload();

    /// <summary>
    /// Этот метод вызывается из <seealso cref="EditorPage"/> для создания
    /// подчиненных окон. Этот метод должен содержать вызовы <seealso cref="IEditorPage.RegisterNestedBrowser{B}"/>
    /// для каждого подчинённого окна.
    /// </summary>
    void RegisterNestedBrowsers();
}
