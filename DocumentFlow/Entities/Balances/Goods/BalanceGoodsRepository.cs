//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2023.1.24
//  - IDatabase перенесён из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Balances;

public class BalanceGoodsRepository : BalanceProductRepository<BalanceGoods>, IBalanceGoodsRepository
{
    public BalanceGoodsRepository(IDatabase database) : base(database) { }
}
