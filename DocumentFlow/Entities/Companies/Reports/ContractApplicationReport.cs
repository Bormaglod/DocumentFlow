//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.ReportEngine;

namespace DocumentFlow.Entities.Companies;

public class ContractApplicationReport : Report<ContractApplication>
{
    protected override Guid Id => new("ee186ad9-6940-4702-8a23-86faab5e1c76");

    protected override void SetParameterValues(ContractApplication document)
    {
        GetReport().SetParameterValue("id", document.id);
    }
}
