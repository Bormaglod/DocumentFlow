//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Repository;

using SqlKata;

namespace DocumentFlow.Data.Models;

public class CuttingRepository : DirectoryRepository<Cutting>, ICuttingRepository
{
    public CuttingRepository(IDatabase database) : base(database)
    {
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter) => query.From("cuttings");

    public IReadOnlyList<int> GetAvailableProgram(int? includeProgram)
    {
        using var conn = GetConnection();
        if (includeProgram == null)
        {
            var sql = $"with all_programs as ( select generate_series(1, 99) as id ) select a.id from all_programs a left join cutting on (program_number = a.id and not deleted) where program_number is null order by a.id";
            return conn.Query<int>(sql).ToList();
        }
        else
        {
            var sql = $"with all_programs as ( select generate_series(1, 99) as id ) select a.id from all_programs a left join cutting on (program_number = a.id and not deleted) where program_number is null or program_number = :program_number order by a.id";
            return conn.Query<int>(sql, new { program_number = includeProgram }).ToList();
        }
    }
}
