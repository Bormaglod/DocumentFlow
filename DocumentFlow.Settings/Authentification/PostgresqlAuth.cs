//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.09.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings.Authentification;

public class PostgresqlAuth
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
