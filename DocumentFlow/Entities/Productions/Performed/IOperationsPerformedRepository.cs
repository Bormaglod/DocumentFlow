//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Productions.Lot;

namespace DocumentFlow.Entities.Productions.Performed;

public interface IOperationsPerformedRepository : IDocumentRepository<OperationsPerformed>
{
    IReadOnlyList<Employee> GetWorkedEmployes(Guid? lot_id);
    IReadOnlyList<OperationsPerformed> GetSummary(Guid lot_id);
}
