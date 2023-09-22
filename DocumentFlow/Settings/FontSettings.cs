//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.07.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings;

public class FontSettings
{
    public FontSettings() { }

    public FontSettings(string familyName, float size, FontStyle style)
    {
        FamilyName = familyName;
        Size = size;
        Style = style;
    }

    public string FamilyName { get; set; } = "Times New Roman";
    public float Size { get; set; } = 10;
    public FontStyle Style { get; set; } = FontStyle.Regular;

    public Font CreateFont() => new(FamilyName, Size, Style);
}
