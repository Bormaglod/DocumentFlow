//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.07.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Settings;

public class ReportFont
{
    public ReportFont() { }

    public ReportFont(string familyName, float size, FontStyle style)
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
