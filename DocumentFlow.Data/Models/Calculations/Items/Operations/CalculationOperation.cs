//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Произв. операция")]
public class CalculationOperation : CalculationItem
{
    private Guid? equipmentId;
    private Guid? toolsId;
    private Guid? materialId;
    private decimal materialAmount;
    private int repeats = 1;
    private string[]? previousOperation;
    private string? note;
    private decimal stimulCost;
    private byte[]? preview;

    /// <summary>
    /// Возвращает или устанавливает идентификатор оборудования на котором выполняется операция.
    /// </summary>
    public Guid? EquipmentId 
    { 
        get => equipmentId; 
        set
        {
            if (equipmentId != value) 
            { 
                equipmentId = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает идентификатор инструмента с помощью которого выполняется операция.
    /// </summary>
    public Guid? ToolsId 
    { 
        get => toolsId; 
        set
        {
            if (toolsId != value) 
            { 
                toolsId = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает идентификатор материала используемого при выполнении операции.
    /// </summary>
    public Guid? MaterialId 
    { 
        get => materialId; 
        set
        {
            if (materialId != value)
            {
                materialId = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает количество используемого материала на 1 опер. (в ед. изм. этой операции)
    /// </summary>
    public decimal MaterialAmount 
    { 
        get => materialAmount; 
        set
        {
            if (materialAmount != value) 
            { 
                materialAmount = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает количество повторов операции.
    /// </summary>
    public int Repeats 
    { 
        get => repeats; 
        set
        {
            if (repeats != value)
            {
                repeats = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает список кодов операций результат которых используется в текущей
    /// </summary>
    public string[]? PreviousOperation 
    { 
        get => previousOperation; 
        set
        {
            if (previousOperation != value)
            {
                previousOperation = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает дополнительную информацию об операции.
    /// </summary>
    public string? Note 
    { 
        get => note; 
        set
        {
            if (note != value)
            {
                note = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает стимулирующую выплату для данной операции.
    /// </summary>
    public decimal StimulCost 
    { 
        get => stimulCost; 
        set
        {
            if (stimulCost != value)
            {
                stimulCost = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает графическую информацию об операции.
    /// </summary>
    public byte[]? Preview 
    { 
        get => preview; 
        set
        {
            if (preview != value)
            {
                preview = value;
                NotifyPropertyChanged();
            }
        }
    }

    [WritableCollection]
    public IList<CalculationOperationProperty> Properties { get; protected set; } = null!;

    public string? OperationName { get; protected set; }
    public decimal ProducedTime { get; protected set; }
    public string? EquipmentName { get; protected set; }
    public string? ToolsName { get; protected set; }
    public string? MaterialName { get; protected set; }
    public decimal TotalMaterial { get; protected set; }
    public string PreviousOperationValue => PreviousOperation == null ? string.Empty : string.Join(',', PreviousOperation);
    public string[]? UsingOperations { get; protected set; }
    public string UsingOperationsValue => UsingOperations == null ? string.Empty : string.Join(',', UsingOperations);
    public bool IsCutting { get; protected set; }
}
