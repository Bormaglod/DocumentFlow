//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.12.2019
//
// Версия 2023.1.24
//  - IDatabase перенесён из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

using SqlKata.Execution;

namespace DocumentFlow.Data.Core.Repository;

public class UserAliasRepository : Repository<Guid, UserAlias>, IUserAliasRepository
{
    public UserAliasRepository(IDatabase database) : base(database) { }

    public IReadOnlyList<UserAlias> GetUsers()
    {
        using var conn = Database.OpenConnection();
        return GetDefaultQuery(conn)
            .WhereFalse("is_system")
#if DEBUG
            .OrWhere("pg_name", "postgres")
#endif
            .OrderBy("name")
            .Get<UserAlias>()
            .ToList();
    }
}
