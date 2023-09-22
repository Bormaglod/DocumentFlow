//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Controls.Interfaces;

public interface ICreationBased
{
    string DocumentName { get; }
    Type DocumentEditorType { get; }
    bool CanCreateDocument(Type documentType);
    IDocumentInfo Create<T>(T document) where T : class, IDocumentInfo;
}
