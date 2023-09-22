//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.07.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings;

public class PreviewRowSettings
{
    public ThumbnailRowSettings ThumbnailRow { get; set; } = new();
}
