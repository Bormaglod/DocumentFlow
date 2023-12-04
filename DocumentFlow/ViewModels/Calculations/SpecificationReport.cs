//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.ReportEngine;

namespace DocumentFlow.ViewModels;

public class SpecificationReport : Report<Calculation>
{
    public SpecificationReport(IServiceProvider services) : base(services)
    {
    }

    protected override Guid Id => new("19bc3ef4-5d0c-4d18-85e6-4e0a5f8f4522");

    protected override void SetParameterValues(Calculation document)
    {
        GetReport().SetParameterValue("id", document.Id.ToString());
    }
}

