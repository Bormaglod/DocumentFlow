﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Models;

public interface IBalanceMaterialRepository : IBalanceProductRepository<BalanceMaterial>
{
    void UpdateMaterialRemaind(BalanceMaterial balance);
}
