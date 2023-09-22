//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Tools;
using DocumentFlow.Tools;

using Humanizer;

namespace DocumentFlow.Data.Models;

[EntityName("Материал")]
public class Material : Product
{
    private decimal? minOrder;
    private string? extArticle;
    private Guid? wireId;
    private string materialKind = "undefined";

    /// <summary>
    /// Возвращает или устанавливает минимальное количество материала возможное для заказа.
    /// </summary>
    public decimal? MinOrder 
    { 
        get => minOrder; 
        set
        {
            if (minOrder != value) 
            { 
                minOrder = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает альтернативный вариант артикула
    /// </summary>
    public string? ExtArticle 
    { 
        get => extArticle; 
        set
        {
            if (extArticle != value) 
            { 
                extArticle = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает идентификатор типа провода (для записей с установленным типом 
    /// материала <see cref="MaterialKind.Wire"/> 
    /// </summary>
    public Guid? WireId 
    { 
        get => wireId; 
        set
        {
            if (wireId != value) 
            { 
                wireId = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает тип материала в виде строки перечисления PostgresQL
    /// </summary>
    [EnumType("material_kind")]
    public string MaterialKind 
    { 
        get => materialKind; 
        set
        {
            if (materialKind != value)
            {
                materialKind = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает наименование материала являющегося оригиналом по отношению к текущему
    /// </summary>
    [Computed]
    public string? CrossName { get; protected set; }

    /// <summary>
    /// Возвращает true, если данный материал используется хотя бы в одной калькуляции (калькуляция
    /// должна быть в утверждённом состоянии).
    /// </summary>
    [Computed]
    public bool MaterialUsing { get; protected set; }

    /// <summary>
    /// Возвращает код статуса цены материала. Возможные варианты: 0 - действующая цена, 1 - цены установлена
    /// вручную, 2 - цена является устаревшей.
    /// </summary>
    [Computed]
    public int PriceStatus { get; protected set; }

    /// <summary>
    /// Возвращает наименование типа провода (для записей с установленным типом 
    /// материала <see cref="MaterialKind.Wire"/> 
    /// </summary>
    [Computed]
    public string? WireName { get; protected set; }

    /// <summary>
    /// Возвращает список совместимых комплектующих.
    /// </summary>
    [WritableCollection]
    public IList<CompatiblePart> CompatibleParts { get; protected set; } = null!;

    public MaterialKind Kind
    {
        get { return Enum.Parse<MaterialKind>(MaterialKind.Pascalize()); }
        protected set { MaterialKind = value.ToString().Underscore(); }
    }

    public string MaterialKindName => Kind.Description();
}
