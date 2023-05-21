//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2021
//
// Версия 2023.1.21
//  - добавлено свойство Taxes
// Версия 2023.5.21
//  - добавлено свойство DocName
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;

namespace DocumentFlow.Entities.Products;

public class Product : Directory
{
    private static readonly Dictionary<int, string> taxes = new()
    {
        [0] = "Без НДС",
        [10] = "10%",
        [20] = "20%"
    };

    public string? DocName { get; set; }
    public decimal Price { get; set; }
    public int Vat { get; set; }
    public Guid? MeasurementId { get; set; }
    public string? MeasurementName { get; protected set; }
    public decimal? Weight { get; set; }
    public decimal? ProductBalance { get; protected set; }
    public bool Thumbnails { get; protected set; }
    public IReadOnlyList<DocumentRefs>? Documents { get; protected set; }
    public static IReadOnlyDictionary<int, string> Taxes => taxes;

    public void SetDocuments(IReadOnlyList<DocumentRefs> documents) => Documents = documents;
}
