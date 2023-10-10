//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.10.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;

using SqlKata.Execution;

namespace DocumentFlow.Data.Repository;

public class CodeGeneratorRepository : Repository<int, CodeGenerator>, ICodeGeneratorRepository
{
    public CodeGeneratorRepository(IDatabase database) : base(database) { }

    public IReadOnlyList<CodeGenerator> GetTypes() 
    { 
        using var conn = GetConnection();
        return GetQuery(conn)
            .WhereRaw("code_info_value = 'type'::code_info")
            .Get<CodeGenerator>()
            .ToList();
    }

    public IReadOnlyList<CodeGenerator> GetBrands()
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .WhereRaw("code_info_value = 'brand'::code_info")
            .Get<CodeGenerator>()
            .ToList();
    }

    public IReadOnlyList<CodeGenerator> GetModels(CodeGenerator brand)
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .WhereRaw("code_info_value = 'model'::code_info")
            .Where("parent_id", brand.Id)
            .Get<CodeGenerator>()
            .ToList();
    }

    public IReadOnlyList<CodeGenerator> GetEngines()
    {
        using var conn = GetConnection();
        return GetQuery(conn)
            .WhereRaw("code_info_value = 'engine'::code_info")
            .Get<CodeGenerator>()
            .ToList();
    }
}
