//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.07.2022
//
// Версия 2023.1.24
//  - IDatabase перенесён из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Data.Core.Repository;

public class ReportRepository : Repository<Guid, Report>, IReportRepository
{
    public ReportRepository(IDatabase database) : base(database) { }
}