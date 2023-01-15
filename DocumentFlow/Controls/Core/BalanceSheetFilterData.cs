//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2023
//
// Версия 2023.1.15
//  - переименован в BalanceSheetFilterSettings
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

using System.Text.Json.Serialization;

namespace DocumentFlow.Controls.Core;

public class BalanceSheetFilterSettings : DocumentFilterSettings
{
    [JsonPropertyName("content")]
    public BalanceSheetContent Content { get; set; }

    [JsonPropertyName("amount_visible")]
    public bool AmountVisible { get; set; }

    [JsonPropertyName("summa_visible")]
    public bool SummaVisible { get; set; }
}