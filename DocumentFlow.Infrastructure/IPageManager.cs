//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.07.2022
//
// Версия 2022.8.28
//  - добавлен метод Get(TabPageAdv)
// Версия 2022.12.31
//  - добавлен метод ShowStartPage
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Infrastructure;

public interface IPageManager
{
    IPage? Get(TabPageAdv page);
    void AddToHistory(TabPageAdv page);
    void ClosePage(IPage page);
    void OnPageClosing();
    bool PageCanEdited(Type browserType);
    void ShowBrowser(Type browserType);
    void ShowEditor<E>(Guid id) 
        where E : IEditorPage;
    void ShowEditor<E, T>(T document) 
        where E : IEditorPage
        where T : IDocumentInfo;
    void ShowEditor(Type editorType, Guid objectId);
    void ShowEditor(IEditorPage editorPage, Guid objectId);
    void ShowEditor(Type browserType, Guid? objectId, Guid? owner_id, Guid? parentId, bool readOnly);
    void ShowStartPage();
}
