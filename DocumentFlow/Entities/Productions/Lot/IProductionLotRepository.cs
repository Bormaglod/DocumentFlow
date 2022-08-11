//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.Productions.Lot;

public interface IProductionLotRepository : IDocumentRepository<ProductionLot>
{
    IReadOnlyList<ProductionLotOperation> GetOperations(Guid lot_id);
}
