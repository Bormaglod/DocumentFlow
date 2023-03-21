//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.01.2022
//-----------------------------------------------------------------------

using System.Data;
using DocumentFlow.Data;

namespace DocumentFlow.Entities.Calculations;

public interface ICalculationOperationRepository : ICalculationItemRepository<CalculationOperation>
{
    IReadOnlyList<Property> GetListProperties();
    IReadOnlyList<CalculationOperationProperty> GetProperties(Guid operation_id);
    void AddProperty(CalculationOperationProperty prop, IDbTransaction transaction);
    void UpdateProperty(CalculationOperationProperty prop, IDbTransaction transaction);
    void DeleteProperty(CalculationOperationProperty prop, IDbTransaction transaction);
}
