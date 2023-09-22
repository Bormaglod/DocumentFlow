//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

public class Balance : BaseDocument
{
    private Guid referenceId;
    private decimal operationSumma;
    private decimal amount;
    private Guid documentTypeId;

    /// <summary>
    /// Возвращает или устанавливает идентификатор записи являющийся ссылкой на справочник по которому считаются остатки.
    /// </summary>
    public Guid ReferenceId 
    { 
        get => referenceId; 
        set
        {
            if (referenceId != value) 
            { 
                referenceId = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает сумму операции.
    /// </summary>
    public decimal OperationSumma 
    { 
        get => operationSumma; 
        set
        {
            if (operationSumma != value) 
            { 
                operationSumma = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает количественный оборот.
    /// </summary>
    public decimal Amount 
    { 
        get => amount; 
        set
        {
            if (amount != value) 
            { 
                amount = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает идентификатор типf документа который сформировал эту запись.
    /// </summary>
    public Guid DocumentTypeId 
    { 
        get => documentTypeId; 
        set
        {
            if (documentTypeId != value)
            {
                documentTypeId = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Computed]
    public string? DocumentTypeCode { get; protected set; }

    [Computed]
    public string? DocumentTypeName { get; protected set; }
}
