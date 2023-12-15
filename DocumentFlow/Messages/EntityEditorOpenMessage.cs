//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.12.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Messages;

public class EntityEditorOpenMessage
{
    public EntityEditorOpenMessage(Type editorType, IDocumentInfo document)
    {
        EditorType = editorType;
        ObjectId = document.Id;

        if (document is IDirectory directory)
        {
            ParentId = directory.ParentId;
        }
    }

    public EntityEditorOpenMessage(Type editorType, Guid objectId)
    {
        EditorType = editorType;
        ObjectId = objectId;
    }

    public EntityEditorOpenMessage(Type editorType, Guid? objectId, IDocumentInfo? owner, Guid? parentId, bool readOnly)
    {
        EditorType = editorType;
        ObjectId = objectId;
        Owner = owner;
        ParentId = parentId;
        ReadOnly = readOnly;
    }

    public Type EditorType { get; }

    public Guid? ObjectId { get; }

    public IDocumentInfo? Owner { get; }

    public Guid? ParentId { get; }

    public bool ReadOnly { get; }
}