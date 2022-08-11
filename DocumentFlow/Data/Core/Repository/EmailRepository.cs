//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.02.2022
//-----------------------------------------------------------------------

using SqlKata.Execution;

namespace DocumentFlow.Data.Core.Repository;

public class EmailRepository : Repository<long, Email>, IEmailRepository
{
    public EmailRepository(IDatabase database) : base(database) { }

    public Email? Get(string email)
    {
        using var conn = Database.OpenConnection();
        return GetDefaultQuery(conn)
            .Where("address", email)
            .FirstOrDefault<Email>();
    }
}
