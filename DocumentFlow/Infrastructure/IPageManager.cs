//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data.Infrastructure;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Infrastructure;

public interface IPageManager
{
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
}
