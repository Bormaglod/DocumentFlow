//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Settings;

public class ReportPage
{
    public PageSetup Settings { get; set; } = new();
    public Fonts Fonts { get; set; } = new();
}
