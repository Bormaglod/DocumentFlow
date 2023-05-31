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
// Версия 2023.5.31
//  - добавлен метод GetRemainder(T, DateTime?) и GetRemainder(Guid, DateTime?)
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Repositiry;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Products;

public class ProductRepository<T> : DirectoryRepository<T>, IProductRepository<T>
    where T: Product
{
    public ProductRepository(IDatabase database) : base(database) { }

    public decimal GetAveragePrice(T product, DateTime? relevanceDate = null) => GetAveragePrice(product.Id, relevanceDate);

    public decimal GetAveragePrice(Guid productId, DateTime? relevanceDate = null)
    {
        using var conn = Database.OpenConnection();
        return conn.QuerySingle<decimal>(
            "select average_price(:product_id, :relevance_date)",
            new
            {
                product_id = productId,
                relevance_date = relevanceDate ?? DateTime.Now
            });
    }

    public decimal GetRemainder(T product, DateTime? actualDate = null) => GetRemainder(product.Id, actualDate);

    public decimal GetRemainder(Guid productId, DateTime? actualDate = null)
    {
        using var conn = Database.OpenConnection();
        return conn.QuerySingle<decimal>(
            "select get_product_remainder(:product_id, :actual_date)",
            new
            {
                product_id = productId,
                actual_date = actualDate ?? DateTime.Now
            });
    }
}
