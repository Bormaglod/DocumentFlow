//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.07.2022
//
// Версия 2023.1.24
//  - IDatabase перенесён из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data.Repositiry
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Data.Repositiry;

public class ReportRepository : Repository<Guid, Report>, IReportRepository
{
    public ReportRepository(IDatabase database) : base(database) { }
}