//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.06.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings;

public class S3Settings
{
    public S3EndPointSettings EndPoint { get; set; } = new();
}
