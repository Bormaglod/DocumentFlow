//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Balances.Initial;

[Description("Нач. остаток")]
internal class InitialBalanceGoods : InitialBalance
{
    public string? goods_code { get; protected set; }
    public string? goods_name { get; protected set; }
}