//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Balances;

public class BalanceProcessing : BalanceProduct 
{
    public string material_name { get; set; } = string.Empty;
}
