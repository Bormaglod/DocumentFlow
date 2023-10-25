//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.06.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings;

public class S3EndPointSettings
{
    public string Server { get; set; } = string.Empty;
    public int Port { get; set; }

    public override string ToString() => $"{Server}:{Port}";
}
