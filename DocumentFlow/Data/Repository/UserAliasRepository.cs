//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.12.2019
//
// Версия 2023.1.24
//  - IDatabase перенесён из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data.Repositiry
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure.Data;

using SqlKata.Execution;

namespace DocumentFlow.Data.Repositiry;

public class UserAliasRepository : Repository<Guid, UserAlias>, IUserAliasRepository
{
    public UserAliasRepository(IDatabase database) : base(database) { }

    public IReadOnlyList<UserAlias> GetUsers()
    {
        using var conn = Database.OpenConnection();
        return GetUserDefinedQuery(conn)
            .WhereFalse("is_system")
#if DEBUG
            .OrWhere("pg_name", "postgres")
#endif
            .OrderBy("name")
            .Get<UserAlias>()
            .ToList();
    }
}
