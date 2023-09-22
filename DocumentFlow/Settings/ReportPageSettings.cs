//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings;

public class ReportPageSettings
{
    public PageSetupSettings Settings { get; set; } = new();
    public FontCollectionSettings Fonts { get; set; } = new();
}
