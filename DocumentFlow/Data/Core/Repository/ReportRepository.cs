//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.07.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core.Repository;

public class ReportRepository : Repository<Guid, Report>, IReportRepository
{
    public ReportRepository(IDatabase database) : base(database) { }
}