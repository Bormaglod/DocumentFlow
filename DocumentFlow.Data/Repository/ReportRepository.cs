//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Data.Repository;

public class ReportRepository : Repository<Guid, Report>, IReportRepository
{
    public ReportRepository(IDatabase database) : base(database) { }
}