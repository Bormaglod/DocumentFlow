//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.Products;

public interface IProductRepository<T> : IDirectoryRepository<T>
    where T: Product
{
    decimal GetAveragePrice(T product, DateTime? relevance_date = null);
}
