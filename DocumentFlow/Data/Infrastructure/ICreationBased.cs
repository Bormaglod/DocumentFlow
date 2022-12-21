//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.12.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Infrastructure;

public interface ICreationBased
{
    string DocumentName { get; }
    Type DocumentEditorType { get; }
    bool CanCreateDocument(Type documentType);
    Guid Create<T>(T document) where T : class, IIdentifier<Guid>;
}
