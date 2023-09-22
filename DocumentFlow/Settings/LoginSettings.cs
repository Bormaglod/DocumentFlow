//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.06.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings;

public class LoginSettings
{
    public string LastConnection { get; set; } = string.Empty;
    public string PreviousUser { get; set; } = string.Empty;
}