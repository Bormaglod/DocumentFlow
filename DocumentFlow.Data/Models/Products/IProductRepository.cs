//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Models;

public interface IProductRepository<T> : IDirectoryRepository<T>
    where T: Product
{
    decimal GetAveragePrice(T product, DateTime? relevanceDate = null);
    decimal GetAveragePrice(Guid productId, DateTime? relevanceDate = null);
    decimal GetRemainder(T product, DateTime? actualDate = null);
    decimal GetRemainder(Guid productId, DateTime? actualDate = null);
}
