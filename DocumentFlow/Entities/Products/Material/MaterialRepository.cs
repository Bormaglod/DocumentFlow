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
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using SqlKata;

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
        string sql = "select * from material where not deleted and material_kind != 'wire' order by item_name";

        using var conn = Database.OpenConnection();
        try
        {
            return conn.Query<Material>(sql).ToList();
        }
        catch (Exception e)
        {
            ExceptionHelper.MesssageBox(e);
            return Array.Empty<Material>();
        }
    }

    public IReadOnlyList<Material> GetWires()
    {
        string sql = "select * from material where not deleted and material_kind = 'wire' order by item_name";

        using var conn = Database.OpenConnection();
        try
        {
            return conn.Query<Material>(sql).ToList();
        }
        catch (Exception e)
        {
            ExceptionHelper.MesssageBox(e);
            return Array.Empty<Material>();
        }
    }
}
