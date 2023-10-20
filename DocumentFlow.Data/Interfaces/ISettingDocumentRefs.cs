//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.10.2023
//

namespace DocumentFlow.Data.Interfaces;

public interface ISettingDocumentRefs
{
    bool Enabled { get; }
    string? Key { get; }
}
