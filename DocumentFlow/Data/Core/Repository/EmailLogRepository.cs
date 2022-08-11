//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.02.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core.Repository;

public class EmailLogRepository : Repository<long, EmailLog>, IEmailLogRepository
{
    public EmailLogRepository(IDatabase database) : base(database) { }
}