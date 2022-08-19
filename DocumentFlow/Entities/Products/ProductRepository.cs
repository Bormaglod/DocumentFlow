﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.07.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Products;

public class ProductRepository<T> : DirectoryRepository<T>, IProductRepository<T>
    where T: Product
{
    public ProductRepository(IDatabase database) : base(database) { }

    public decimal GetAveragePrice(T product, DateTime? relevance_date = null)
    {
        using var conn = Database.OpenConnection();
        return conn.QuerySingle<decimal>(
            "select average_price(:product_id, :relevance_date)", 
            new { 
                product_id = product.id, 
                relevance_date = relevance_date ?? DateTime.Now 
            });
    }
}