//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Models;

public interface IProductionLotRepository : IDocumentRepository<ProductionLot>
{
    decimal GetFinishedGoods(ProductionLot lot);
    IReadOnlyList<ProductionLotOperation> GetOperations(ProductionLot lot);
    IReadOnlyList<ProductionLot> GetActiveLots();
    IReadOnlyList<ProductionLot> GetActiveLots(Contractor contractor);
    IReadOnlyList<ProductionLot> GetActiveLots(Guid contractorId, Guid? goodsId = null);
    void SetState(ProductionLot lot, LotState state);
}
