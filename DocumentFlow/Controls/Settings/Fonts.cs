//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.07.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Settings;

public class Fonts
{
    public ReportFont Title { get; set; } = new ReportFont("Times New Roman", 12, FontStyle.Bold);
    public ReportFont Header { get; set; } = new ReportFont("Times New Roman", 10, FontStyle.Bold);
    public ReportFont Base { get; set; } = new();
}
