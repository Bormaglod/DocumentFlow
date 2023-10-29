﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.02.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces.Repository;

public interface IEmailLogRepository : IRepository<long, EmailLog>
{
    IReadOnlyList<EmailLog> GetEmails(Guid? group_id, Guid? contractorId);
    Task Log(EmailLog log);
    Task Log(IEnumerable<EmailLog> logs);
}