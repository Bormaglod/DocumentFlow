//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Calculations;

namespace DocumentFlow.Entities.Productions.Lot;

public class ProductionLotOperation : CalculationOperation
{
    public decimal QuantityByLot { get; protected set; }
}
