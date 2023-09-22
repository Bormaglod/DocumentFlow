﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Models;

public interface ICalculationMaterialRepository : ICalculationItemRepository<CalculationMaterial>
{
    void RecalculateCount(Guid calculate_id);
    IReadOnlyList<CalculationMaterial> GetOnlyGivingMaterials(Calculation calculation);
}