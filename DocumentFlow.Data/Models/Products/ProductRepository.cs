//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.07.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Repository;

namespace DocumentFlow.Data.Models;

public class ProductRepository<T> : DirectoryRepository<T>, IProductRepository<T>
    where T: Product
{
    public ProductRepository(IDatabase database) : base(database) { }

    public decimal GetAveragePrice(T product, DateTime? relevanceDate = null) => GetAveragePrice(product.Id, relevanceDate);

    public decimal GetAveragePrice(Guid productId, DateTime? relevanceDate = null)
    {
        using var conn = GetConnection();
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
        using var conn = GetConnection();
        return conn.QuerySingle<decimal>(
            "select get_product_remainder(:product_id, :actual_date)",
            new
            {
                product_id = productId,
                actual_date = actualDate ?? DateTime.Now
            });
    }
}
