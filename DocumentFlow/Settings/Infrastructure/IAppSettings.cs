//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings.Infrastructure;

public interface IAppSettings
{
    T Get<T>(string key) where T : new();
    void Write<T>(string key, T section);
}
