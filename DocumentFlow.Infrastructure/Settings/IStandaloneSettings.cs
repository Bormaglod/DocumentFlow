//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2023
//
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Settings;

public interface IStandaloneSettings : IAppSettings
{
    void Configure(string key);
    T Get<T>() where T : new();
    void Write<T>();
}
