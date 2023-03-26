//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Productions.Returns;

[Description("Возврат материалов заказчику")]
public class ReturnMaterials : ShipmentDocument
{
    public DateTime OrderDate { get; protected set; }
    public int OrderNumber { get; protected set; }
}
