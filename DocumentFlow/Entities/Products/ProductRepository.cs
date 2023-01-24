//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.07.2022
//
// Версия 2022.11.15
//  - добавлен метод GetAveragePrice(Guid, DateTime?)
// Версия 2023.1.24
//  - IDatabase перенесён из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Products;

public class ProductRepository<T> : DirectoryRepository<T>, IProductRepository<T>
    where T: Product
{
    public ProductRepository(IDatabase database) : base(database) { }

    public decimal GetAveragePrice(T product, DateTime? relevance_date = null) => GetAveragePrice(product.id, relevance_date);

    public decimal GetAveragePrice(Guid product_id, DateTime? relevance_date = null)
    {
        using var conn = Database.OpenConnection();
        return conn.QuerySingle<decimal>(
            "select average_price(:product_id, :relevance_date)",
            new
            {
                product_id,
                relevance_date = relevance_date ?? DateTime.Now
            });
    }
}
