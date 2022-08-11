//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.05.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Infrastructure;

public interface IAccess
{
    bool ReadOnly { get; set; }
}
