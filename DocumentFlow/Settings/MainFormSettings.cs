//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.06.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings;

public class MainFormSettings
{
    public Point Location { get; set; } = new Point(0, 0);
    public Size Size { get; set; } = new Size(800, 600);
    public FormWindowState WindowState { get; set; } = FormWindowState.Normal;
    public int NavigatorWidth { get; set; } = 200;
}
