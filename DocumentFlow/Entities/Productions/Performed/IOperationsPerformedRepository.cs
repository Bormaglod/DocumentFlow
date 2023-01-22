//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.05.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Productions.Performed;

public interface IOperationsPerformedRepository : IDocumentRepository<OperationsPerformed>
{
    IReadOnlyList<OurEmployee> GetWorkedEmployes(Guid? lot_id);
    IReadOnlyList<OperationsPerformed> GetSummary(Guid lot_id);
    OperationsPerformed? GetSummary(Guid lot_id, CalculationOperation operation, OurEmployee employee);
}
