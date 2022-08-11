//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Products;

public interface IMaterialRepository : IProductRepository<Material>
{
    /// <summary>
    /// Функция возвращает список всех материалов.
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<Material> GetAllMaterials();

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
