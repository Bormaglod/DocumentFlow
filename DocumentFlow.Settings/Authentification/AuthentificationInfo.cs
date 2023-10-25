//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.09.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings.Authentification;

public class AuthentificationInfo
{
    public PostgresqlAuth Postgresql { get; set; } = new();
    public S3Auth S3 { get; set; } = new();
}
