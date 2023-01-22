//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.05.2022
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Controls.Infrastructure в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Controls;

public interface IAccess
{
    bool ReadOnly { get; set; }
}
