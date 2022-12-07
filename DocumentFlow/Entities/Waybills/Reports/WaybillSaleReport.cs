﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.11.2022
//-----------------------------------------------------------------------

using DocumentFlow.ReportEngine;

namespace DocumentFlow.Entities.Waybills;

public class WaybillSaleReport : Report<WaybillSale>
{
    protected override Guid Id => new("7f7748ed-206d-40d8-b71e-6269b5069e98");

    protected override void SetParameterValues(WaybillSale document)
    {
        GetReport().SetParameterValue("id", document.id);
    }
}
