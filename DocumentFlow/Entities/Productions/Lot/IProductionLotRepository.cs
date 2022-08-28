//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.04.2022
//
// Версия 2022.8.28
//  - добавлен метод SetState
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.Productions.Lot;

public interface IProductionLotRepository : IDocumentRepository<ProductionLot>
{
    IReadOnlyList<ProductionLotOperation> GetOperations(Guid lot_id);
    void SetState(ProductionLot lot, LotState state);
}
