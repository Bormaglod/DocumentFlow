//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Wages;

public class ReportCardBrowser : BillingDocumentBrowser<ReportCard>, IReportCardBrowser
{
    public ReportCardBrowser(IReportCardRepository repository, IPageManager pageManager, IDocumentFilter filter)
        : base(repository, pageManager, filter: filter)
    {
    }

    protected override string HeaderText => "Табель";
}
