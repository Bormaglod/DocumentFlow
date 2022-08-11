//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.07.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings.Infrastructure;

public interface IMarginsSettings : ISettings
{
    int Left { get; set; }
    int Top { get; set; }
    int Right { get; set; }
    int Bottom { get; set; }
    bool MirrorOnEven { get; set; }
}
