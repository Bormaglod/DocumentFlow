//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.ReportEngine;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public class PurchaseRequestReport : Report<PurchaseRequest>
{
    protected override Guid Id => new("d0edcb83-c298-4d19-a216-309d33687e40");

    protected override void SetParameterValues(PurchaseRequest document)
    {
        GetReport().SetParameterValue("id", document.id);
    }
}

