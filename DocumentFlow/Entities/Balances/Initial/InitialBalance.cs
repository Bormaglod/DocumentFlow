//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalance : AccountingDocument
{
    public Guid reference_id { get; set; }
    public decimal operation_summa { get; set; }
    public decimal amount { get; set; }
}
