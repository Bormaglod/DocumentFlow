//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Выполнение работы")]
public class OperationsPerformed : AccountingDocument
{
    private long quantity;
    private Guid employeeId;
    private Guid operationId;
    private Guid? replacingMaterialId;
    private decimal salary;
    private bool? doubleRate;

    public Guid EmployeeId 
    { 
        get => employeeId; 
        set
        {
            if (employeeId != value) 
            { 
                employeeId = value;
                NotifyPropertyChanged();
            }
        }
    }

    public Guid OperationId 
    { 
        get => operationId; 
        set
        {
            if (operationId != value)
            {
                operationId = value;
                NotifyPropertyChanged();
            }
        }
    }
    public Guid? ReplacingMaterialId 
    { 
        get => replacingMaterialId; 
        set
        {
            if (replacingMaterialId != value)
            {
                replacingMaterialId = value;
                NotifyPropertyChanged();
            }
        }
    }

    public long Quantity
    {
        get => quantity;
        set
        {
            if (quantity != value) 
            {
                quantity = value;
                NotifyPropertyChanged();
            }
        }
    }

    public decimal Salary 
    { 
        get => salary; 
        set
        {
            if (salary != value)
            {
                salary = value;
                NotifyPropertyChanged();
            }
        }
    }

    public bool? DoubleRate 
    { 
        get => doubleRate; 
        set
        {
            if (doubleRate != value)
            {
                doubleRate = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Computed]
    public string? GoodsName { get; set; }

    [Computed]
    public string CalculationName { get; set; } = string.Empty;

    [Computed]
    public string? MaterialName { get; set; }

    public string? LotName { get; protected set; }
    public string? OrderName { get; protected set; }
    public Guid CalculationId { get; protected set; }
    public string OperationCode { get; protected set; } = string.Empty;
    public string? OperationName { get; protected set; }
    public string? EmployeeName { get; protected set; }
}