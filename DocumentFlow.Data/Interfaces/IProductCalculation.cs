//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.08.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;

namespace DocumentFlow.Data.Interfaces;

public interface IProductCalculation
{
    Guid CalculationId { get; }
    void SetCalculation(Calculation calculation);
}
