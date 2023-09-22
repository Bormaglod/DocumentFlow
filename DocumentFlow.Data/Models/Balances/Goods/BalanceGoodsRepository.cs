﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Data.Models;

public class BalanceGoodsRepository : BalanceProductRepository<BalanceGoods>, IBalanceGoodsRepository
{
    public BalanceGoodsRepository(IDatabase database) : base(database) { }
}
