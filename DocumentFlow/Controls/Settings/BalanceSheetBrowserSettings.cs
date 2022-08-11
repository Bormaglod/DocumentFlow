//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

using System.Text.Json.Serialization;

namespace DocumentFlow.Controls.Settings;

public sealed class BalanceSheetBrowserSettings : BrowserSettings
{
    public BalanceSheetContent Content { get; set; } = BalanceSheetContent.Material;
    public bool ViewAmount { get; set; } = true;
    public bool ViewSumma { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? DateFrom { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? DateTo { get; set; }
}