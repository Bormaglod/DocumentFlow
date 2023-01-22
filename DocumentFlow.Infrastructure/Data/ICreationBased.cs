//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.12.2022
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Data.Infrastructure в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Data;

public interface ICreationBased
{
    string DocumentName { get; }
    Type DocumentEditorType { get; }
    bool CanCreateDocument(Type documentType);
    Guid Create<T>(T document) where T : class, IIdentifier<Guid>;
}
