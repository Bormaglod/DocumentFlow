//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.10.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;

namespace DocumentFlow.Dialogs.Interfaces;

public interface IOperationsPerformedDialog : IDialog
{
    CalculationOperation Operation { get; }
    OurEmployee Employee { get; }
    bool Create(ProductionLot lot, CalculationOperation? operation, Employee? emp);
    OperationsPerformed Get();
}