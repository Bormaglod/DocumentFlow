//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Repository;

public class EmailLogRepository : Repository<long, EmailLog>, IEmailLogRepository
{
    public EmailLogRepository(IDatabase database) : base(database) { }
}