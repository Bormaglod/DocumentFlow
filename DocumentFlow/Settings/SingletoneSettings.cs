//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Settings;

namespace DocumentFlow.Settings;

public class SingletoneSettings : BaseSettings, ISingletoneSettings
{
    public SingletoneSettings()
    {
        PrepareJsonFile("appsettings");
    }

    public override T Get<T>(string key)
    {
        PrepareJson();
        return base.Get<T>(key);
    }
}
