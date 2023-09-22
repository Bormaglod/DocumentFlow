﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.ReportEngine;

namespace DocumentFlow.ViewModels;

public class ProcessMapReport : Report<Calculation>
{
    public ProcessMapReport(IServiceProvider services) : base(services)
    {
    }

    protected override Guid Id => new("b7945033-3dd6-4d80-b809-db8fe8939a1a");

    protected override void SetParameterValues(Calculation document)
    {
        GetReport().SetParameterValue("id", document.Id);
    }
}