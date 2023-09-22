//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.09.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;

namespace DocumentFlow.Settings;

public class BalanceSheetFilterSettings : DocumentFilterSettings
{
    public bool AmountVisible { get; set; }
    public bool SummaVisible { get; set; }
    public BalanceSheetContent Content { get; set; }
    public bool ShowGivingMaterial { get; set; }
}