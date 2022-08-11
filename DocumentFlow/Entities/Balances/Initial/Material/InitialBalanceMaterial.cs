//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Balances.Initial;

[Description("Нач. остаток")]
internal class InitialBalanceMaterial : InitialBalance
{
    public string? material_code { get; protected set; }
    public string? material_name { get; protected set; }
}