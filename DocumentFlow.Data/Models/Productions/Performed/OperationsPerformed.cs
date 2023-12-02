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
    private bool skipMaterial;

    /// <summary>
    /// Возвращает или устанавливает идентификатор сотрудника выполняющего операцию.
    /// </summary>
    public Guid EmployeeId 
    { 
        get => employeeId;
        set => SetProperty(ref employeeId, value);
    }

    /// <summary>
    /// Возвращает или устанавливает идентификатор выполняемой сотрудником операции.
    /// </summary>
    public Guid OperationId 
    { 
        get => operationId;
        set => SetProperty(ref operationId, value);
    }

    /// <summary>
    /// Возвращает или устанавливает идентификатор фактически использованного материал для операции.
    /// </summary>
    public Guid? ReplacingMaterialId 
    { 
        get => replacingMaterialId;
        set => SetProperty(ref replacingMaterialId, value);
    }

    /// <summary>
    /// Возвращает или устанавливает количество выполненных операций.
    /// </summary>
    public long Quantity
    {
        get => quantity;
        set => SetProperty(ref quantity, value);
    }

    /// <summary>
    /// Возвращает или устанавливает заработную плату, которая начислена за выполненную опрерацию.
    /// </summary>
    public decimal Salary 
    { 
        get => salary;
        set => SetProperty(ref salary, value);
    }

    /// <summary>
    /// Возвращает или устанавливает флаг определяющий файк оплаты по двойному тарифу.
    /// </summary>
    public bool? DoubleRate 
    { 
        get => doubleRate;
        set => SetProperty(ref doubleRate, value);
    }

    /// <summary>
    /// Возвращает или устанавливает флаг, который определяет возможность не учитывать используемый материал
    /// в операции.
    /// </summary>
    public bool SkipMaterial
    {
        get => skipMaterial;
        set => SetProperty(ref skipMaterial, value);
    }

    /// <summary>
    /// Возвращает или устанавливает артикул изделия для изготовления которого выполняется операция.
    /// </summary>
    [Computed]
    public string? GoodsCode { get; set; }

    /// <summary>
    /// Возвращает или устанавливает наименование изделия для изготовления которого выполняется операция.
    /// </summary>
    [Computed]
    public string? GoodsName { get; set; }

    /// <summary>
    /// Возвращает или устанавливает код калькуляции изделия.
    /// </summary>
    [Computed]
    public string CalculationName { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или устанавливает наименование материала который указан в калькуляции для данной операции.
    /// </summary>
    [Computed]
    public string? MaterialName { get; set; }

    /// <summary>
    /// Возвращает наименование партии.
    /// </summary>
    public string? LotName { get; protected set; }

    /// <summary>
    /// Возвращает наименование заказа.
    /// </summary>
    public string? OrderName { get; protected set; }

    /// <summary>
    /// Возвращает идентификатор калькуляции изделия.
    /// </summary>
    public Guid CalculationId { get; protected set; }

    /// <summary>
    /// Возвращает код операции.
    /// </summary>
    public string OperationCode { get; protected set; } = string.Empty;

    /// <summary>
    /// Возвращает наименование выполняемой сотрудником операции.
    /// </summary>
    public string? OperationName { get; protected set; }

    /// <summary>
    /// Возвращает наименование сотрудника выполняющего операцию.
    /// </summary>
    public string? EmployeeName { get; protected set; }
}