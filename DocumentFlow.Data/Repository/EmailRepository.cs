//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;

using SqlKata.Execution;

namespace DocumentFlow.Data.Repository;

public class EmailRepository : Repository<long, Email>, IEmailRepository
{
    public EmailRepository(IDatabase database) : base(database) { }

    public Email? Get(string email)
    {
        using var conn = GetConnection();
        return GetUserDefinedQuery(conn)
            .Where("address", email)
            .FirstOrDefault<Email>();
    }
}
