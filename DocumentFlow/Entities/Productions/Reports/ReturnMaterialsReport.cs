//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.05.2023
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Productions.Returns;
using DocumentFlow.ReportEngine;

namespace DocumentFlow.Entities.Productions.Reports;

public class ReturnMaterialsReport : Report<ReturnMaterials>
{
    protected override Guid Id => new("2c6d9ec2-5e43-4c14-a41b-cf5bb8decd6f");

    protected override void SetParameterValues(ReturnMaterials document)
    {
        GetReport().SetParameterValue("id", document.OwnerId);
    }
}