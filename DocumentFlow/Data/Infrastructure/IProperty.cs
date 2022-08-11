//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.07.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Infrastructure;

public interface IProperty : IIdentifier<Guid>
{
    string property_name { get; set; }
    string? title { get; set; }
}
