//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.08.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Wages;

public class ReportCardLabelList : List<ReportCardLabel>, IReportCardLabelList
{
    public bool IsValidLabel(string label) => this.FirstOrDefault(x => x.Label == label) != null;

    public bool IsTimeLabel(string label) => this.FirstOrDefault(x => x.Label == label && !x.OnlyDay) != null;
}