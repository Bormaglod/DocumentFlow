//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.05.2023
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Productions.Performed;

namespace DocumentFlow.Dialogs.Infrastructure;

public interface IOperationsPerformedDialog
{
    CalculationOperation? GetOperation();
    OurEmployee? GetEmployee();
    bool Show(Guid lotId, Guid? operationId, Guid? empId);
    OperationsPerformed Get();
}
