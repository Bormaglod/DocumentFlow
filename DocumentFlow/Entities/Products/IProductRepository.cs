﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.07.2022
//
// Версия 2022.11.15
//  - добавлен метод GetAveragePrice(Guid, DateTime?)
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.5.31
//  - добавлен метод GetRemainder(T, DateTime?) и GetRemainder(Guid, DateTime?)
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Products;

public interface IProductRepository<T> : IDirectoryRepository<T>
    where T: Product
{
    decimal GetAveragePrice(T product, DateTime? relevanceDate = null);
    decimal GetAveragePrice(Guid productId, DateTime? relevanceDate = null);
    decimal GetRemainder(T product, DateTime? actualDate = null);
    decimal GetRemainder(Guid productId, DateTime? actualDate = null);
}
