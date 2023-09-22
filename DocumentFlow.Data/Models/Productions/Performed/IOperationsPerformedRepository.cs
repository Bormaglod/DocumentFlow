//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Models;

public interface IOperationsPerformedRepository : IDocumentRepository<OperationsPerformed>
{
    IReadOnlyList<OurEmployee> GetWorkedEmployes(ProductionLot lot);
    IReadOnlyList<OperationsPerformed> GetSummary(ProductionLot lot);
    IReadOnlyList<WageEmployee> GetWages(BillingDocument billing);
    IReadOnlyList<WageEmployee> GetWages(int year, short month);
    OperationsPerformed? GetSummary(ProductionLot lot, CalculationOperation operation, OurEmployee employee);
}
