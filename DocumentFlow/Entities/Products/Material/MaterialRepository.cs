//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.3.14
//  - метод GetAllMaterials удалён
// Версия 2023.5.20
//  - изменены запросы в методах GetMaterials и GetWires
// Версия 2023.7.23
//  - изменены запросы в методах GetMaterials и GetWires (запрос
//    теперь форомляется с помощью GetQuery
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Entities.Products;

public class MaterialRepository : ProductRepository<Material>, IMaterialRepository
{
    public MaterialRepository(IDatabase database) : base(database) { }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter)
    {
        if (HasPrivilege("materials", Privilege.Select))
        {
            return query.From("materials");
        }
        else
        {
            return query.From("materials_simple");
        }
    }

    public IReadOnlyList<Material> GetMaterials()
    {
        using var conn = Database.OpenConnection();
        try
        {
            return GetQuery(conn)
                .WhereFalse("deleted")
                .WhereRaw("material_kind != 'wire'")
                .OrderBy("item_name")
                .Get<Material>()
                .ToList();
        }
        catch (Exception e)
        {
            ExceptionHelper.MesssageBox(e);
            return Array.Empty<Material>();
        }
    }

    public IReadOnlyList<Material> GetWires()
    {
        using var conn = Database.OpenConnection();
        try
        {
            return GetQuery(conn)
                .WhereFalse("deleted")
                .WhereRaw("material_kind = 'wire'")
                .OrderBy("item_name")
                .Get<Material>()
                .ToList();
        }
        catch (Exception e)
        {
            ExceptionHelper.MesssageBox(e);
            return Array.Empty<Material>();
        }
    }
}
