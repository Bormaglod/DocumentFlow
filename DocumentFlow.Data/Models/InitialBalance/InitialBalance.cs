//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.06.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Models;

public class InitialBalance : AccountingDocument
{
    private Guid referenceId;
    private decimal operationSumma;
    private decimal amount;

    public Guid ReferenceId 
    { 
        get => referenceId;
        set => SetProperty(ref referenceId, value);
    }

    public decimal OperationSumma
    {
        get => operationSumma;
        set => SetProperty(ref operationSumma, value);
    }

    public decimal Amount 
    { 
        get => amount;
        set => SetProperty(ref amount, value);
    }
}
