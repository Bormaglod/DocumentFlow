//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.07.2022
//
// Версия 2022.11.15
//  - добавлен метод GetAveragePrice(Guid, DateTime?)
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.Products;

public interface IProductRepository<T> : IDirectoryRepository<T>
    where T: Product
{
    decimal GetAveragePrice(T product, DateTime? relevance_date = null);
    decimal GetAveragePrice(Guid product_id, DateTime? relevance_date = null);
}
