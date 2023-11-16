//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Операция")]
public class Operation : Directory
{
    private int produced;
    private int prodTime;
    private int productionRate;
    private Guid typeId;
    private DateTime? dateNorm;
    private decimal salary;

    /// <summary>
    /// Возвращает или устанавливает выработка за указанное время <see cref="ProdTime"/>.
    /// </summary>
    public int Produced 
    { 
        get => produced;
        set => SetProperty(ref produced, value);
    }

    /// <summary>
    /// Возвращает или устанавливает время за которое было произведено указанное количество операций <see cref="Produced"/> (указывается в минутах).
    /// </summary>
    public int ProdTime 
    { 
        get => prodTime;
        set => SetProperty(ref prodTime, value);
    }

    /// <summary>
    /// Возвращает или устанавливает норму выработки (шт./час).
    /// </summary>
    public int ProductionRate 
    { 
        get => productionRate;
        set => SetProperty(ref productionRate, value);
    }

    /// <summary>
    /// Возвращает или устанавливает идентификатор типа операции <seealso cref="OperationTypes.OperationType"/>.
    /// </summary>
    public Guid TypeId 
    { 
        get => typeId;
        set => SetProperty(ref typeId, value);
    }

    /// <summary>
    /// Возвращает или устанавливает дату нормирования.
    /// </summary>
    public DateTime? DateNorm 
    { 
        get => dateNorm;
        set => SetProperty(ref dateNorm, value);
    }

    /// <summary>
    /// Возвращает или устанавливает плату за выполнение ед. операции
    /// </summary>
    public decimal Salary 
    { 
        get => salary;
        set => SetProperty(ref salary, value);
    }

    [Computed]
    public bool OperationUsing { get; protected set; }

    [Computed]
    public string? TypeName { get; protected set; }

    /// <summary>
    /// Возвращает список изделий или услуг в которых (и только) будет использоваться эта операция. 
    /// </summary>
    [WritableCollection]
    public IList<OperationGoods> OperationGoods { get; protected set; } = null!;
}
