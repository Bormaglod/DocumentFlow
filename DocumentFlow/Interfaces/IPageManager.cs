//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Interfaces;

public interface IPageManager
{
    void NotifyMainFormClosing();
    bool PageCanEdited(Type browserType);
    void ShowBrowser(Type browserType);
    void ShowEditor<E>(IDocumentInfo document)
        where E : IEditor;
    void ShowEditor(Type editorType, IDocumentInfo document);
    void ShowEditor(Type editorType, Guid id);
    void ShowAssociateEditor<B>(IDocumentInfo document) 
        where B : IBrowserPage;
    void ShowAssociateEditor<B>(Guid id)
        where B : IBrowserPage;
    void ShowAssociateEditor(Type browserType, Guid? objectId, IDocumentInfo? owner, Guid? parentId, bool readOnly);
    void ShowStartPage();
    void ShowAbout();
}
