//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Products;

public class Product : Directory
{
    public decimal price { get; set; }
    public int vat { get; set; }
    public Guid? measurement_id { get; set; }
    public string? measurement_name { get; protected set; }
    public decimal? weight { get; set; }
    public decimal? product_balance { get; protected set; }
    public bool thumbnails { get; protected set; }
    public IReadOnlyList<DocumentRefs>? Documents { get; protected set; }

    public void SetDocuments(IReadOnlyList<DocumentRefs> documents) => Documents = documents;
}
