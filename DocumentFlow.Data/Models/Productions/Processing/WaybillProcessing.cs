//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Поступление в переработку")]
public class WaybillProcessing : Waybill
{
    public DateTime OrderDate { get; protected set; }
    public int OrderNumber { get; protected set; }

    [WritableCollection]
    public IList<WaybillProcessingPrice> Prices { get; protected set; } = null!;
}
