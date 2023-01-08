//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

using System.Text.Json.Serialization;

namespace DocumentFlow.Controls.Core;

public class BalanceSheetFilterData : DocumentFilterData
{
    [JsonPropertyName("content")]
    public BalanceSheetContent Content { get; set; }

    [JsonPropertyName("amount_visible")]
    public bool AmountVisible { get; set; }

    [JsonPropertyName("summa_visible")]
    public bool SummaVisible { get; set; }
}