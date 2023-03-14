//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2023.3.14
//  - метод GetAllMaterials удалён
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Products;

public interface IMaterialRepository : IProductRepository<Material>
{
    /// <summary>
    /// Функция возвращает список материалов без проводов.
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<Material> GetMaterials();

    /// <summary>
    /// Функция возвращает список проводов.
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<Material> GetWires();
}
