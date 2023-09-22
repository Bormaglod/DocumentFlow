//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.12.2019
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;

using SqlKata.Execution;

namespace DocumentFlow.Data.Repository;

public class UserAliasRepository : Repository<Guid, UserAlias>, IUserAliasRepository
{
    public UserAliasRepository(IDatabase database) : base(database) { }

    public IReadOnlyList<UserAlias> GetUsers()
    {
        using var conn = GetConnection();
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
