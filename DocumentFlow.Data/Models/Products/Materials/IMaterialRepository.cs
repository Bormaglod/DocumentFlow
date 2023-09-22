//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Models;

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

    /// <summary>
    /// Функция возвращает список материалов, которые могут выступать в качестве 
    /// оригинальных комплектующих (за исключением материала, указанного 
    /// в параметре exceptMaterial).
    /// </summary>
    /// <param name="exceptMaterial">Идентификатор материала, который будет исключён из 
    /// списка материалов возвращаемого функцией.</param>
    /// <returns>Список материалов, которые могут выступать в качестве 
    /// оригинальных комплектующих.</returns>
    IReadOnlyList<Material> GetCrossMaterials(Guid exceptMaterial);
}
