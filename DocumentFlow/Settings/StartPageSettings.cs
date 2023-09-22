//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.06.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings;

public class StartPageSettings
{
    public Size CardSize { get; set; } = new Size(210, 240);
    public int CardPadding { get; set; } = 10;
    public int CardRowValueWidth { get; set; } = 65;
}
