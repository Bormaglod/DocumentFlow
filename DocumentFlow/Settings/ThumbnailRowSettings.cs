//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.07.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings;

public class ThumbnailRowSettings
{
    public int RowHeight { get; set; } = 125;

    public int LeftIndent { get; set; } = 50;

    public int VerticalBounds { get; set; } = 2;

    public int HorizontalBounds { get; set; } = 5;

    public int ImageSize { get; set; } = 120;
}