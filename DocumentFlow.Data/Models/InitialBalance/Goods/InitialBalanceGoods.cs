//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Нач. остаток")]
public class InitialBalanceGoods : InitialBalance
{
    public string? GoodsCode { get; protected set; }
    public string? GoodsName { get; protected set; }
}