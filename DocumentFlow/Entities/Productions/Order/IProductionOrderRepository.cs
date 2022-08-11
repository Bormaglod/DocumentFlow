﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.Productions.Order;

public interface IProductionOrderRepository : IDocumentRepository<ProductionOrder>
{
    IReadOnlyList<ProductionOrderPrice> GetList(ProductionOrder order);
}
