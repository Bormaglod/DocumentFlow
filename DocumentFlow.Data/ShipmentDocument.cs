//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data;

public abstract class ShipmentDocument : AccountingDocument
{
    private Guid? contractorId;
    private Guid? contractId;

    public Guid? ContractorId 
    { 
        get => contractorId; 
        set
        {
            if (contractorId != value) 
            { 
                contractorId = value;
                NotifyPropertyChanged();
            }
        }
    }

    public Guid? ContractId 
    { 
        get => contractId; 
        set
        {
            if (contractId != value) 
            { 
                contractId = value;
                NotifyPropertyChanged();
            }
        }
    }
    public string? ContractorName { get; protected set; }
    public string? ContractName { get; protected set; }
}