//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

public class Product : Directory
{
    private static readonly Dictionary<int, string> taxes = new()
    {
        [0] = "Без НДС",
        [10] = "10%",
        [20] = "20%"
    };

    private decimal price;
    private int vat;
    private Guid? measurementId;
    private decimal? weight;
    private string? docName;

    /// <summary>
    /// Возвращает или устанавливает цену материала / изделия (без учёта НДС).
    /// </summary>
    public decimal Price 
    { 
        get => price;
        set => SetProperty(ref price, value);
    }

    /// <summary>
    /// Возвращает или устанавливает процентную ставку НДС.
    /// </summary>
    public int Vat 
    { 
        get => vat;
        set => SetProperty(ref vat, value);
    }

    /// <summary>
    /// Возвращает или устанавливает идектификатор единицы измерения.
    /// </summary>
    public Guid? MeasurementId 
    { 
        get => measurementId;
        set => SetProperty(ref measurementId, value);
    }

    /// <summary>
    /// Возвращает или устанавливает вес материала / изделия в граммах.
    /// </summary>
    public decimal? Weight 
    { 
        get => weight;
        set => SetProperty(ref weight, value);
    }

    /// <summary>
    /// Возвращает или устанавливает наименование материала или изделия, которое будет испоьзоваться в документах.
    /// </summary>
    public string? DocName 
    { 
        get => docName;
        set => SetProperty(ref docName, value);
    }

    /// <summary>
    /// Возвращает наименование еденицы измерения.
    /// </summary>
    [Computed]
    public string? MeasurementName { get; protected set; }

    /// <summary>
    /// Возвращает остаток материала или изделия на текущий момент.
    /// </summary>
    [Computed]
    public decimal? ProductBalance { get; protected set; }

    /// <summary>
    /// Возвращает true, если материал или изделие имеют сохранённые эскизы изображений.
    /// </summary>
    [Computed]
    public bool Thumbnails { get; protected set; }

    /// <summary>
    /// Возвращает список документов/файлов прикреплённых к данному материалу или изделию.
    /// </summary>
    public IReadOnlyList<DocumentRefs>? Documents { get; protected set; }

    public static IReadOnlyDictionary<int, string> Taxes => taxes;

    /// <summary>
    /// Метод заменяет список прикреплённых к данному материалу или изделию документов/файлов.
    /// </summary>
    /// <param name="documents"></param>
    public void SetDocuments(IReadOnlyList<DocumentRefs> documents) => Documents = documents;
}
